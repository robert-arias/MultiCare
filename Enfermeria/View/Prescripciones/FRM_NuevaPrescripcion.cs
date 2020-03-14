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
            frm_ConsultarPacientes = new FRM_ConsultarPacientesPrescripcion(this);
        }

        public void AbrirConsultarPacientes() {
            frm_ConsultarPacientes.ShowDialog();
        }

        public void SetPacienteSeleccionado(string cedula, string nombreCompleto, string edad) {
            txtCedula.Text = cedula;
            txtNombreCompleto.Text = nombreCompleto;
            txtEdad.Text = edad;
        }
    }
}
