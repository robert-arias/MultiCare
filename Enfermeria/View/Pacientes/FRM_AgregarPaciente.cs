using Enfermeria.Controller;
using Enfermeria.Model;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Enfermeria.View.Pacientes {
    public partial class FRM_AgregarPaciente : Form
    {
        PacienteController pacienteController;
        public FRM_AgregarPaciente()
        {
            InitializeComponent();
            pacienteController = new PacienteController(this);
            cbSexo.Text = "Seleccionar";
        }

        public void EstadoInicial()
        {
            btnAgregar.Enabled = false;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnVerificar.Enabled = true;

            txtCedula.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            txtFecha.Value =  DateTime.Now;
            txtNombre.Text = "";
            cbSexo.Text = "Seleccionar";

            txtCedula.Enabled = true;
            txtApellidos.Enabled = false;
            txtEdad.Enabled = false;
            DesactivarFecha(false);
            txtNombre.Enabled = false;
            cbSexo.Enabled = false;
            
        }

        private void DesactivarFecha(bool desactivar) {
            if (!desactivar) {
                txtFecha.BackColor = Color.Silver;
                txtFecha.Enabled = false;
            }
            else {
                txtFecha.BackColor = Color.White;
                txtFecha.Enabled = true;
            }
        }

        public void ActivarCampos()
        {
            btnAgregar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnVerificar.Enabled = true;

           
            txtCedula.Enabled = false;
            txtApellidos.Enabled = true;
            txtEdad.Enabled = true;
            DesactivarFecha(true);
            txtNombre.Enabled = true;
            cbSexo.Enabled = true;

        }

        public string GetCedula()
        {
            return txtCedula.Text;
        }

        public bool VerificarCampos()
        {
            bool vacio = false;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lbNombre.Visible = true;
                vacio = true;
            }
            else
                lbNombre.Visible = false;


            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                //lbApellidos.Visible = true;
                vacio = true;
            }
            else
            {
                //lbApellidos.Visible = false;

            }

            if ((DateTime.Now.Year - txtFecha.Value.Year) < 18)
            {
                lbFecha.Visible = true;
                vacio = true;
            }
            else
                lbFecha.Visible = false;

            if (cbSexo.SelectedIndex == 0 )
            {
                lbSexo.Visible = true;
                vacio = true;
            }
            else
                lbSexo.Visible = false;


            return vacio;
        }

        public void MensajeInformativo(string message)
        {
            MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ShowConfirmation()
        {
            string message = $"¿Desea agregar al paciente { txtNombre.Text }, cédula: { txtCedula.Text} ?";
            DialogResult boton = MessageBox.Show(message, "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            return boton == DialogResult.OK;
        }

        public Paciente GetPaciente()
        {          
            return new Paciente(txtCedula.Text, txtNombre.Text, txtApellidos.Text, txtFecha.Value.ToString(),  int.Parse(txtEdad.Text),
                cbSexo.GetItemText(cbSexo.SelectedItem), "Habilitado");
        }

        public void SoloLetras(KeyPressEventArgs v)
        {
            if (Char.IsLetter(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Solo se admiten letras.");
            }

            if (!(char.IsLetter(v.KeyChar) || char.IsSeparator(v.KeyChar) || char.IsControl(v.KeyChar)))
                v.Handled = true;
        }
    }
}
