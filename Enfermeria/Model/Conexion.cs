using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model {
    public class Conexion {

        private SQLiteConnection connection;

        public Conexion() {
            Conectar();
        }

        private void Conectar() {
            if (!Directory.Exists("./Data")) {
                Directory.CreateDirectory("./Data");
                CrearBaseDatos();
            }
            else 
                CrearBaseDatos();
        }

        private void CrearBaseDatos() {
            if (!File.Exists("./Data/database.sqlite3")) {
                SQLiteConnection.CreateFile("./Data/database.sqlite3");
                connection = new SQLiteConnection("Data Source=./Data/database.sqlite3");
                string script = File.ReadAllText(@"./scripts/db.dat");
                SQLiteCommand c = new SQLiteCommand(script, connection);
                connection.Open();
                c.ExecuteNonQuery();
                connection.Close();
                CrearAdmin();

            }
            else
                connection = new SQLiteConnection("Data Source=./Data/database.sqlite3");
        }

        private void CrearAdmin() {
            string salt = Seguridad.GetSalt();
            Usuario usuario = new Usuario("Admin", "", "admin", salt, Seguridad.EncryptPassword(salt, "admin"));
            AgregarUsuario(usuario);
        }

        public SQLiteConnection GetConexion() {
            return connection;
        }

        private bool AgregarUsuario(Usuario usuario) {
            string insert = "insert into Usuarios (nombre, apellidos, usuario, sal, contrasenia) values " +
                "(@nombre, @apellidos, @usuario, @sal, @contrasenia)";
            try {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(insert, connection);
                command.Parameters.AddWithValue("@nombre", usuario.nombre);
                command.Parameters.AddWithValue("@apellidos", usuario.apellidos);
                command.Parameters.AddWithValue("@usuario", usuario.nombreUsuario);
                command.Parameters.AddWithValue("@sal", usuario.salt);
                command.Parameters.AddWithValue("@contrasenia", usuario.contrasenia);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SQLiteException) {
                connection.Close();
                throw;
            }
        }

        public bool ExisteCedulaPaciente(string cedula)
        {
            try
            {
                string query = "select * from Pacientes where cedula='" + cedula + "'";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                DataSet dataset = new DataSet();
                dataAdapter.Fill(dataset);
                connection.Close();

                if (dataset.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;

            }
            catch (SQLiteException e)
            {
                connection.Close();              
                return false;
            }
        }
        public bool AgregarPaciente(Paciente paciente)
        {
            try
            {
                string insert = $"insert into Pacientes values('{paciente.cedula}','{paciente.nombre}','{paciente.apellidos}','{paciente.fechaNacimiento}',{paciente.edad},'{paciente.sexo}','{paciente.estado}')";
                SQLiteCommand command = new SQLiteCommand(insert, connection);
                Console.WriteLine(insert);
                connection.Open();             
                command.ExecuteNonQuery();
                connection.Close();              
                return true;
            }
            catch (SQLiteException e)
            {              
                connection.Close();              
                return false;
            }

        }

        public DataSet GetPaciente(string cedula)
        {          
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("select * from Pacientes where cedula='" + cedula + "'", connection);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataSet data = new DataSet();
                sqlDataAdapter.Fill(data);
                connection.Close();
                Console.WriteLine(data.Tables[0].Rows[0][3].ToString());
                return data;
            }
            catch (SQLiteException e)
            {               
                connection.Close();
                return null;
            }
        }

        public bool UpdatePaciente(Paciente paciente)
        {
            string query = "update Pacientes set nombre ='" + paciente.nombre + "' , apellidos='" + paciente.apellidos + "',fecha_nacimiento='" + paciente.fechaNacimiento + "',edad=" + paciente.edad + ",sexo='" + paciente.sexo + "',estado='"+paciente.estado+"'  where cedula= " + paciente.cedula;          
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SQLiteException E)
            {
                connection.Close();              
                return false;
            }
        }


        public bool ExisteCodigoMedicamento(string codigo)
        {
            try
            {
                string query = "select * from Medicamentos where codigo_Medicamento='" + codigo + "'";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                connection.Close();

                if (dataSet.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;

            }
            catch (SQLiteException e)
            {
                connection.Close();               
                return false;
            }
        }

        public bool UpdateMedicamento(Medicamento medicamento)
        { 
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("update Medicamentos set nombre_Medicamento='" + medicamento.nombreMedicamento + "', categoria ='" + medicamento.categoria + "',unidad_medida='" + medicamento.unidadMedida + "',cantidad_disponible=" + medicamento.catidadDisponible + " where codigo_medicamento = '" + medicamento.codigoMedicamento + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (SQLiteException E)
            {              
                connection.Close();
                return false;
            }
        }

        public DataSet GetMedicamento(string codigo)
        {
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("select * from Medicamentos where codigo_medicamento = '" + codigo + "'", connection);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataSet data = new DataSet();
                sqlDataAdapter.Fill(data);
                connection.Close();               
                return data;
            }
            catch (SQLiteException e)
            {                
                connection.Close();
                return null;
            }
        }

        public bool AgregarMedicamento(Medicamento medicamento)
        {
            try
            {
                string insert = $"insert into Medicamentos values ('{medicamento.codigoMedicamento}','{medicamento.nombreMedicamento}','{medicamento.unidadMedida}','{medicamento.categoria}',{medicamento.catidadDisponible})";
                SQLiteCommand command = new SQLiteCommand(insert, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                connection.Close();             
                return false;
            }

        }

    }


}
