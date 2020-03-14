using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace Enfermeria.Model {
    public class ConexionUsuarios : Conexion {
        
        private SQLiteConnection sqlConnection;

        public ConexionUsuarios() {
            Conectar();
            sqlConnection = GetConexion();
        }

        public DataTable GetUsuario(string usuario) {
            string busqueda = "select nombre, apellidos, correo from usuarios where usuario = @usuario";
            try {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(busqueda, sqlConnection);
                command.Parameters.AddWithValue("@usuario", usuario);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataTable data = new DataTable();
                sqlDataAdapter.Fill(data);
                sqlConnection.Close();
                return data;
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        private DataTable GetContrasenia(string usuario) {
            string busqueda = "select sal, contrasenia from usuarios where usuario = @usuario";
            try {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(busqueda, sqlConnection);
                command.Parameters.AddWithValue("@usuario", usuario);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataTable data = new DataTable();
                sqlDataAdapter.Fill(data);
                sqlConnection.Close();
                return data;
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        public bool VerificarContrasenia(string nombreUsuario, string txtContrasenia) {
            DataTable usuario = GetContrasenia(nombreUsuario);
            byte[] contrasenaIngresada = Seguridad.EncryptPassword(usuario.Rows[0][0].ToString(), txtContrasenia);
            return Seguridad.CheckPassword(contrasenaIngresada, (byte[])usuario.Rows[0][1]);
        }

        public void CambiarContrasenia(string password, string usuario) {
            string salt = Seguridad.GetSalt();
            byte[] newPassword = Seguridad.EncryptPassword(salt, password);

            string update = "update usuarios set sal = @salt, contrasenia = @password where usuario = @usuario";

            try {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(update, sqlConnection);
                command.Parameters.AddWithValue("@salt", salt);
                command.Parameters.AddWithValue("@password", newPassword);
                command.Parameters.AddWithValue("@usuario", usuario);
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                Debug.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
