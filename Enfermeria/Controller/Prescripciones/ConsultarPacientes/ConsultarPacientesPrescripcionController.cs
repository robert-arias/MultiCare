using System;
using System.Windows.Forms;
using Enfermeria.Model;
using Enfermeria.View.Prescripciones.ConsultarPacientes;

namespace Enfermeria.Controller.Prescripciones {
    public class ConsultarPacientesPrescripcionController {

        private FRM_ConsultarPacientesPrescripcion frm_ConsultarPacientes;
        private ConexionPacientesPrescripcion conexion;

        public ConsultarPacientesPrescripcionController(FRM_ConsultarPacientesPrescripcion frm_ConsultarPacientes) {
            this.frm_ConsultarPacientes = frm_ConsultarPacientes;
            conexion = new ConexionPacientesPrescripcion();

            AgregarEventos();
        }

        private void AgregarEventos() {
            frm_ConsultarPacientes.btnBuscar.Click += new EventHandler(RealizarConsulta);
            frm_ConsultarPacientes.FormClosed += CerrarVentana;

            frm_ConsultarPacientes.dgvBusqueda.CellDoubleClick += new DataGridViewCellEventHandler(
                SeleccionarPaciente);
        }

        private void SeleccionarPaciente(object sender, DataGridViewCellEventArgs e) {
            frm_ConsultarPacientes.PacienteSeleccionado(e.RowIndex);
        }

        private void CerrarVentana(object sender, FormClosedEventArgs e) {
            frm_ConsultarPacientes.Hide();
        }

        private void RealizarConsulta(object sender, EventArgs e) {
            frm_ConsultarPacientes.MostrarDatos(conexion.GetResultadosBusqueda(
                frm_ConsultarPacientes.txtBuscar.Text));
        }
    }
}
