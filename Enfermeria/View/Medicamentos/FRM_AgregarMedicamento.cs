﻿using Enfermeria.Controller.Medicamentos;
using Enfermeria.Model;
using System;
using System.Windows.Forms;

namespace Enfermeria.View.Medicamentos {
    public partial class FRM_AgregarMedicamento : Form
    {
        AgregarMedicamentoController agregarMedicamentoController;
        public FRM_AgregarMedicamento()
        {
            InitializeComponent();
            agregarMedicamentoController = new AgregarMedicamentoController(this);
            cbCategoria.Text = "Seleccionar";
            cbUnidadMedida.Text = "Seleccionar";
            alerta.CambiarImagenWarning();
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

            btnAgregar.Enabled = true;
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


            btnAgregar.Enabled = false;
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


            if (cbCategoria.SelectedIndex==0)
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
            string message = "¿Desea agregar el medicamento código: " + txtCodigo.Text + ", " + " nombre: " + txtNombre.Text + "?";
            DialogResult boton = MessageBox.Show(message, "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            return boton == DialogResult.OK;
        }
        public void MensajeInformativo(string message)
        {
            MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, " Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
