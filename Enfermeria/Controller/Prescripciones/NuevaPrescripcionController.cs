using Enfermeria.View.Prescripciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Controller.Prescripciones {
    public class NuevaPrescripcionController {

        private FRM_NuevaPrescripcion frm_NuevaPrescripcion;

        public NuevaPrescripcionController(FRM_NuevaPrescripcion frm_NuevaPrescripcion) {
            this.frm_NuevaPrescripcion = frm_NuevaPrescripcion;

            AgregarEventos();
        }

        private void AgregarEventos() {
            frm_NuevaPrescripcion.btnConsultarPacientes.Click += new EventHandler(ConsultarPacientes);
        }

        private void ConsultarPacientes(object sender, EventArgs e) {
            frm_NuevaPrescripcion.AbrirConsultarPacientes();
        }

    }
}
