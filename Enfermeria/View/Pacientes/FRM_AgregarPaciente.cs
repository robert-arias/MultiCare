using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Model;
using Enfermeria.Controller.Pacientes;
using Enfermeria.Controller;

namespace Enfermeria.View.Pacientes
{
    public partial class FRM_AgregarPaciente : Form
    {
        PacienteController pacienteController;
        public FRM_AgregarPaciente()
        {
            InitializeComponent();
            cbSexo.SelectedItem("Seleccionar") ;
            pacienteController = new PacienteController(this);
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
            cbSexo.SelectedItem("Seleccionar");

            txtCedula.Enabled = true;
            txtApellidos.Enabled = false;
            txtEdad.Enabled = false;
            txtFecha.Enabled=false;
            txtNombre.Enabled = false;
            cbSexo.Enabled = false;
            
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
            txtFecha.Enabled = true;
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

            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                lbCedula.Visible = true;
                vacio = true;
            }
            else
            {
                lbCedula.Visible = false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lbNombre.Visible = true;
                vacio = true;
            }
            else
            {
                lbNombre.Visible = false;
            }


            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                lbApellidos.Visible = true;
                vacio = true;
            }
            else
            {
                lbApellidos.Visible = false;

            }

            if ((DateTime.Now.Year - txtFecha.Value.Year) < 18)
            {
                lbFecha.Visible = true;
                vacio = true;
            }
            else
            {
                lbFecha.Visible = false;
            }

            if (cbSexo.selectedIndex == 0 )
            {
                lbSexo.Visible = true;
                vacio = true;
            }
            else
            {
                lbSexo.Visible = false;
            }


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
            string message = "¿Desea agregar al paciente cédula: " + txtCedula.Text + "  " + " nombre:  " + txtNombre.Text + " ?";
            DialogResult boton = MessageBox.Show(message, "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (boton == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        public Paciente GetPaciente()
        {          
            return new Paciente(txtCedula.Text,txtNombre.Text,txtApellidos.Text,txtFecha.Value.ToString(),int.Parse(txtEdad.Text),Convert.ToString(cbSexo.selectedValue),"Habilitado");
        }


        public void SoloNumeros(KeyPressEventArgs v)
        {
            if (Char.IsDigit(v.KeyChar))
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
                MessageBox.Show("Solo se admiten números.");
            }
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
        }


    }
}
