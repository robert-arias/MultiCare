using System.Data;
using System.Data.SQLite;

namespace Enfermeria.Model {
    public class ConexionPacientes : Conexion{
        private SQLiteConnection sqlConnection;

        public ConexionPacientes() {
            Conectar();
            sqlConnection = GetConexion();
        }

        public bool ExisteCedulaPaciente(string cedula) {
            try {
                string query = "select * from Pacientes where cedula = @cedula";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@cedula", cedula);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                DataSet dataset = new DataSet();
                dataAdapter.Fill(dataset);
                sqlConnection.Close();

                return !(dataset.Tables[0].Rows.Count == 0);
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                return false;
            }
        }

        public bool AgregarPaciente(Paciente paciente) {
            try {
                string insert = "insert into Pacientes values(@cedula, @nombre, @apellidos, @fechaNacimiento, @edad, @sexo, @estado)";
                SQLiteCommand command = new SQLiteCommand(insert, sqlConnection);
                command.Parameters.AddWithValue("@cedula", paciente.cedula);
                command.Parameters.AddWithValue("@nombre", paciente.nombre);
                command.Parameters.AddWithValue("@apellidos", paciente.apellidos);
                command.Parameters.AddWithValue("@fechaNacimiento", paciente.fechaNacimiento);
                command.Parameters.AddWithValue("@edad", paciente.edad);
                command.Parameters.AddWithValue("@sexo", paciente.sexo);
                command.Parameters.AddWithValue("@estado", paciente.estado);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                return false;
            }
        }

        public DataSet GetPaciente(string cedula) {
            try {
                string query = "select * from Pacientes where cedula = @cedula";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@cedula", cedula);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataSet data = new DataSet();
                sqlDataAdapter.Fill(data);
                sqlConnection.Close();

                return data;
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                return null;
            }
        }

        public bool UpdatePaciente(Paciente paciente) {
            string query = "update Pacientes set nombre = @nombre, apellidos = @apellidos, fecha_nacimiento = @fechaNacimiento, " +
                "edad = @edad, sexo = @sexo, estado = @estado where cedula = @cedula";
            try {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@cedula", paciente.cedula);
                command.Parameters.AddWithValue("@nombre", paciente.nombre);
                command.Parameters.AddWithValue("@apellidos", paciente.apellidos);
                command.Parameters.AddWithValue("@fechaNacimiento", paciente.fechaNacimiento);
                command.Parameters.AddWithValue("@edad", paciente.edad);
                command.Parameters.AddWithValue("@sexo", paciente.sexo);
                command.Parameters.AddWithValue("@estado", paciente.estado);
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (SQLiteException E) {
                sqlConnection.Close();
                return false;
            }
        }

    }
}
