using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model {
    public class ConexionUsuarios {

        private Conexion conexion;
        private SQLiteConnection sqlConnection;

        public ConexionUsuarios() {
            conexion = new Conexion();
            sqlConnection = conexion.GetConexion();
        }

        public DataTable GetUsuario(string usuario) {
            string busqueda = "select * from usuarios where usuario = @usuario";
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
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

    }
}
