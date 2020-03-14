using System;
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
        }

        private void RealizarConsulta(object sender, EventArgs e) {
            frm_ConsultarPacientes.MostrarDatos(conexion.GetResultadosBusqueda(
                frm_ConsultarPacientes.txtBuscar.Text));
        }
    }
}
