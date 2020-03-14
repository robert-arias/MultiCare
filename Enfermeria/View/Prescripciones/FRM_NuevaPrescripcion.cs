using Enfermeria.Controller.Prescripciones;
using Enfermeria.View.Prescripciones.ConsultarPacientes;
using System;
using System.Windows.Forms;

namespace Enfermeria.View.Prescripciones {
    public partial class FRM_NuevaPrescripcion : Form {

        private NuevaPrescripcionController controller;
        private FRM_ConsultarPacientesPrescripcion frm_ConsultarPacientes;

        public FRM_NuevaPrescripcion() {
            InitializeComponent();
            controller = new NuevaPrescripcionController(this);
            frm_ConsultarPacientes = new FRM_ConsultarPacientesPrescripcion();
        }

        public void AbrirConsultarPacientes() {
            frm_ConsultarPacientes.ShowDialog();
        }
    }
}
