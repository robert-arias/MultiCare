﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Model;
using Enfermeria.Controller.Medicamentos;

namespace Enfermeria.View.Medicamentos
{
    public partial class FRM_AgregarMedicamento : Form
    {
        AgregarMedicamentoController agregarMedicamentoController;
        public FRM_AgregarMedicamento()
        {
            InitializeComponent();
            agregarMedicamentoController = new AgregarMedicamentoController(this);
        }

        public Medicamento GetMedicamento()
        {
            Console.WriteLine(txtCodigo.Text, txtNombre.Text, txtUnidadMedida.Text + "  " + cbUnidadMedida.selectedValue, cbCategoria.selectedValue, Convert.ToInt32(txtCantidad.Text));
            return new Medicamento(txtCodigo.Text, txtNombre.Text, txtUnidadMedida.Text + "  " + cbUnidadMedida.selectedValue, cbCategoria.selectedValue,Convert.ToInt32(txtCantidad.Text));

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
            txtCantidad.Enabled = true;
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
            txtCantidad.Enabled = false;
            cbUnidadMedida.Enabled = false;
                             
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtUnidadMedida.Text = "";
            cbCategoria.SelectedItem("Seleccionar");
            cbUnidadMedida.SelectedItem("Seleccionar");
            txtCantidad.Text = "";


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


            if (cbCategoria.selectedIndex==0)
            {
                lbCategoria.Visible = true;

                vacio = true;
            }
            else
            {
                lbCategoria.Visible = false;

            }

            if (string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.selectedIndex == 0 || !string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.selectedIndex == 0 ||
                string.IsNullOrEmpty(txtUnidadMedida.Text) && cbUnidadMedida.selectedIndex != 0)
            {
                lbUnidadMedida.Visible = true;

                vacio = true;
            }
            else
            {
                lbUnidadMedida.Visible = false;

            }

            if (string.IsNullOrEmpty(txtCantidad.Text))
            {

                lbCantidad.Visible = true;

                vacio = true;

            }
            else
            {
                lbCantidad.Visible = false;

            }


            return vacio;
        }

        public bool ShowConfirmation()
        {
            string message = "¿Desea agregar el medicamento código: " + txtCodigo.Text + "  " + " nombre:  " + txtNombre.Text + " ?";
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
