using Enfermeria.Controller.Prescripciones;
using System;
using System.Data;
using System.Windows.Forms;

namespace Enfermeria.View.Prescripciones.ConsultarPacientes {
    public partial class FRM_ConsultarPacientesPrescripcion : Form {

        private ConsultarPacientesPrescripcionController controller;

        public FRM_ConsultarPacientesPrescripcion() {
            InitializeComponent();
            controller = new ConsultarPacientesPrescripcionController(this);
        }

        public void MostrarDatos(DataTable resultadosBusqueda) {
            LimpiarBusquedas();
            if (resultadosBusqueda != null) {
                if (resultadosBusqueda.Rows.Count > 0) {
                    foreach (DataRow dataRow in resultadosBusqueda.Rows) {
                        int i = dgvBusqueda.Rows.Add();
                        DataGridViewRow row = dgvBusqueda.Rows[i];
                        row.Cells["cedula"].Value = dataRow[0].ToString();
                        row.Cells["nombre"].Value = dataRow[1].ToString();
                        row.Cells["apellidos"].Value = dataRow[2].ToString();
                        row.Cells["edad"].Value = dataRow[3].ToString();
                    }
                }
            }
        }

        public void LimpiarBusquedas() {
            do {
                foreach (DataGridViewRow row in dgvBusqueda.Rows) {
                    dgvBusqueda.Rows.Remove(row);
                }
            } while (dgvBusqueda.Rows.Count >= 1);
        }

    }
}
