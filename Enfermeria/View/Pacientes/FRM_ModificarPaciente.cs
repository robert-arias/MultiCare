using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Controller.Pacientes;
using Enfermeria.Model;


namespace Enfermeria.View.Pacientes
{
    public partial class FRM_ModificarPaciente : Form
    {
        private ModificarPacienteController modificarPacienteController;
        public FRM_ModificarPaciente()
        {
            InitializeComponent();
            modificarPacienteController = new ModificarPacienteController(this);
            cbSexo.SelectedItem("Seleccionar");
        }

        public void EstadoInicial()
        {
            btnModificar.Enabled = false;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnVerificar.Enabled = true;

            txtCedula.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            txtFecha.Value = DateTime.Today;
            txtNombre.Text = "";
            cbSexo.SelectedItem("Seleccionar");

            txtCedula.Enabled = true;
            txtApellidos.Enabled = false;
            txtEdad.Enabled = false;
            txtFecha.Enabled = false;
            txtNombre.Enabled = false;
            cbSexo.Enabled = false;

            rbDeshabilitado.Enabled = false;
            rbHabilitado.Enabled = false;

            rbDeshabilitado.Checked = false;
            rbHabilitado.Checked = false;



        }

        public void ActivarCampos()
        {
            btnModificar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnVerificar.Enabled = true;


            txtCedula.Enabled = false;
            txtApellidos.Enabled = true;
            txtEdad.Enabled = true;
            txtFecha.Enabled = true;
            txtNombre.Enabled = true;
            cbSexo.Enabled = true;
            rbDeshabilitado.Enabled = true;
            rbHabilitado.Enabled = true;

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

            if (cbSexo.selectedIndex == 0)
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
            string message = "¿Desea modificar al paciente cédula: " + txtCedula.Text + "  " + " nombre:  " + txtNombre.Text + " ?";
            DialogResult boton = MessageBox.Show(message, "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (boton == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        public string Estado()
        {
            string estado = "";

            if (rbHabilitado.Checked == true)            
                estado= "Habilitado";           
            else if (rbDeshabilitado.Checked==true)
                estado = "Deshabilitado";


            return estado;
        }

        public Paciente GetPaciente()
        {
            Console.WriteLine(txtFecha.Value.ToString());
            return new Paciente(txtCedula.Text, txtNombre.Text, txtApellidos.Text, txtFecha.Value.ToString(), int.Parse(txtEdad.Text), Convert.ToString(cbSexo.selectedValue), Estado());
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


        public void LlenarCampos(DataSet data)
        {
            txtNombre.Text = data.Tables[0].Rows[0][1].ToString();
            txtApellidos.Text = data.Tables[0].Rows[0][2].ToString();
            txtFecha.Value = Convert.ToDateTime(data.Tables[0].Rows[0][3].ToString());
            txtEdad.Text = data.Tables[0].Rows[0][4].ToString();
            cbSexo.SelectedItem(data.Tables[0].Rows[0][5].ToString());

            if (data.Tables[0].Rows[0][6].ToString() == "Habilitado")
            {
                rbHabilitado.Checked = true;

            }
            else
            {
                rbDeshabilitado.Checked = true;
            }
        }
    }
}
