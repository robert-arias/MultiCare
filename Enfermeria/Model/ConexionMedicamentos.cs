using System;
using System.Data;
using System.Data.SQLite;

namespace Enfermeria.Model {
    public class ConexionMedicamentos : Conexion{
        
        private SQLiteConnection sqlConnection;

        public ConexionMedicamentos() {
            Conectar();
            sqlConnection = GetConexion();
        }

        public bool ExisteCodigoMedicamento(string codigo) {
            try {
                string query = "select * from Medicamentos where codigo_medicamento = @codigo";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@codigo", codigo);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                sqlConnection.Close();

                return !(dataSet.Tables[0].Rows.Count == 0);
            }
            catch (SQLiteException e) {
                sqlConnection.Close();
                return false;
            }
        }

        public bool UpdateMedicamento(Medicamento medicamento) {
            try {
                string update = "update Medicamentos set nombre_medicamento = @nombre, categoria = @categoria, " +
                    "unidad_medida = @unidad where codigo_medicamento = @codigo";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(update, sqlConnection);
                command.Parameters.AddWithValue("@codigo", medicamento.codigoMedicamento);
                command.Parameters.AddWithValue("@nombre", medicamento.nombreMedicamento);
                command.Parameters.AddWithValue("@categoria", medicamento.categoria);
                command.Parameters.AddWithValue("@unidad", medicamento.unidadMedida);
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (SQLiteException E) {
                sqlConnection.Close();
                return false;
            }
        }

        public DataSet GetMedicamento(string codigo) {
            try {
                string query = "select * from Medicamentos where codigo_medicamento = @codigo";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@codigo", codigo);
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

        public DataSet GetAllMedicamento()
        {
            try
            {
                string query = "select * from Medicamentos";
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);              
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataSet data = new DataSet();
                sqlDataAdapter.Fill(data);
                sqlConnection.Close();

                return data;
            }
            catch (SQLiteException e)
            {
                sqlConnection.Close();
                return null;
            }
        }


        public DataSet GetBusquedaMedicamentos(string query)
        {
            Console.WriteLine("El query"+query);
            try
            {
              
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand(query, sqlConnection);
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command);
                DataSet data = new DataSet();
                sqlDataAdapter.Fill(data);
                sqlConnection.Close();

                return data;
            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                return null;
                
            }
        }
        public bool AgregarMedicamento(Medicamento medicamento) {
            try {
                string insert = "insert into Medicamentos values (@codigo, @nombre, @unidad, @categoria)";
                SQLiteCommand command = new SQLiteCommand(insert, sqlConnection);
                command.Parameters.AddWithValue("@codigo", medicamento.codigoMedicamento);
                command.Parameters.AddWithValue("@nombre", medicamento.nombreMedicamento);
                command.Parameters.AddWithValue("@categoria", medicamento.categoria);
                command.Parameters.AddWithValue("@unidad", medicamento.unidadMedida);
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

    }
}
