using System;
using System.Collections.Generic;
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

    }
}
