using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Controller.Medicamentos;
using Enfermeria.Model;

namespace Enfermeria.View.Medicamentos
{
    public partial class FRM_ModificarMedicamento : Form
    {
        ModificarMedicamentoController modificarMedicamentoController;
        public FRM_ModificarMedicamento()
        {
            InitializeComponent();
            modificarMedicamentoController = new ModificarMedicamentoController(this);
            cbCategoria.Text = "Seleccionar";
            cbUnidadMedida.Text = "Seleccionar";
            alerta.CambiarImagenWarning();
        }

        public void LlenarCampos(DataSet data)
        {           
            string texto = data.Tables[0].Rows[0][2].ToString();
            char[] delimiterChars = { ' ', ' ' };
            string[] partes = texto.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            txtUnidadMedida.Text = partes[0].ToString();         
            cbUnidadMedida.Text = partes[1].ToString();
            txtNombre.Text = data.Tables[0].Rows[0][1].ToString();
            cbCategoria.Text = data.Tables[0].Rows[0][3].ToString();
        }

        public Medicamento GetMedicamento()
        {
            string unidad = txtUnidadMedida.Text + "  " + cbUnidadMedida.GetItemText(cbUnidadMedida.SelectedItem);
            return new Medicamento(txtCodigo.Text, txtNombre.Text,
                unidad, cbCategoria.GetItemText(cbCategoria.SelectedItem));
        }

        public string GetCodigo()
        {
            return txtCodigo.Text;
        }

        public void ActivarCampos()
        {
            txtCodigo.Enabled = false;
            txtNombre.Enabled = true;
            txtUnidadMedida.Enabled = true;
            cbCategoria.Enabled = true;
            cbUnidadMedida.Enabled = true;

            btnModificar.Enabled = true;
            btnVerificar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;

        }

        public void EstadoInicial()
        {
            txtCodigo.Enabled = true;
            txtNombre.Enabled = false;
            txtUnidadMedida.Enabled = false;
            cbCategoria.Enabled = false;
            cbUnidadMedida.Enabled = false;

            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtUnidadMedida.Text = "";
            cbCategoria.Text = "Seleccionar";
            cbUnidadMedida.Text = "Seleccionar";


            btnModificar.Enabled = false;
            btnVerificar.Enabled = true;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = true;
        }

        public bool VerificarCampos()
        {
            bool vacio = false;

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                lbCodigo.Visible = true;
                vacio = true;
            }
            else
            {
                lbCodigo.Visible = false;

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


            if (cbCategoria.SelectedIndex == 0)
            {
                lbCategoria.Visible = true;

                vacio = true;
            }
            else
            {
                lbCategoria.Visible = false;

            }

            if (string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.SelectedIndex == 0 || !string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.SelectedIndex == 0 ||
                string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.SelectedIndex != 0)
            {
                lbUnidadMedida.Visible = true;

                vacio = true;
            }
            else
            {
                lbUnidadMedida.Visible = false;

            }


            return vacio;
        }

        public bool ShowConfirmation()
        {
            string message = "¿Desea modificar el medicamento código: " + txtCodigo.Text + "  " + " nombre:  " + txtNombre.Text + " ?";
            DialogResult boton = MessageBox.Show(message, "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (boton == DialogResult.OK)
            {
                return true;
            }

            return false;
        }
        public void MensajeInformativo(string message)
        {
            MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, " Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}
