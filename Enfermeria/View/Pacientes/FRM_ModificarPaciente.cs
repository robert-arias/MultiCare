using Enfermeria.Controller.Pacientes;
using Enfermeria.Model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Enfermeria.View.Pacientes {
    public partial class FRM_ModificarPaciente : Form
    {
        private ModificarPacienteController modificarPacienteController;
        public FRM_ModificarPaciente()
        {
            InitializeComponent();
            modificarPacienteController = new ModificarPacienteController(this);
            cbSexo.Text = "Seleccionar";
            alerta.CambiarImagenWarning();
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
            cbSexo.Text = "Seleccionar";

            txtCedula.Enabled = true;
            txtApellidos.Enabled = false;
            txtEdad.Enabled = false;
            DesactivarFecha(false);
            txtNombre.Enabled = false;
            cbSexo.Enabled = false;

            rbDeshabilitado.Enabled = false;
            rbHabilitado.Enabled = false;

            rbDeshabilitado.Checked = false;
            rbHabilitado.Checked = false;
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
            btnModificar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnVerificar.Enabled = true;


            txtCedula.Enabled = false;
            txtApellidos.Enabled = true;
            txtEdad.Enabled = true;
            DesactivarFecha(true);
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

            if (cbSexo.SelectedIndex == 0)
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

            return boton == DialogResult.OK;
        }

        public string Estado()
        {
            return rbHabilitado.Checked ? "Habilitado" : "Deshabilitado";
        }

        public Paciente GetPaciente()
        {
            Console.WriteLine(txtFecha.Value.ToString());
            return new Paciente(txtCedula.Text, txtNombre.Text, txtApellidos.Text, txtFecha.Value.ToString(),
                int.Parse(txtEdad.Text), cbSexo.GetItemText(cbSexo.SelectedItem), Estado());
        }


        public void LlenarCampos(DataSet data)
        {
            txtNombre.Text = data.Tables[0].Rows[0][1].ToString();
            txtApellidos.Text = data.Tables[0].Rows[0][2].ToString();
            txtFecha.Value = Convert.ToDateTime(data.Tables[0].Rows[0][3].ToString());
            txtEdad.Text = data.Tables[0].Rows[0][4].ToString();
            cbSexo.Text = data.Tables[0].Rows[0][5].ToString();

            if (data.Tables[0].Rows[0][6].ToString().Equals("Habilitado"))
                rbHabilitado.Checked = true;
            else
                rbDeshabilitado.Checked = true;
        }
    }
}
