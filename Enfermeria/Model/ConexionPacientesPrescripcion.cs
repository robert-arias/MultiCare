using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model {
    public class ConexionPacientesPrescripcion : Conexion {

        private SQLiteConnection sqlConnection;

        public ConexionPacientesPrescripcion() {
            Conectar();
            sqlConnection = GetConexion();
        }

        public DataTable GetResultadosBusqueda(string nombreCompleto) {
            nombreCompleto = "%" + nombreCompleto + "%";
            string busqueda = "select cedula, nombre, apellidos, edad from " +
                "(select cedula, nombre, apellidos, edad, nombre || ' ' || apellidos as fullname from usuarios) " +
                "where fullname like @nombreCompleto";
            try {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(busqueda, sqlConnection);
                command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
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

    }
}
