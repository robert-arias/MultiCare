﻿using Enfermeria.Controller.Login;
using Enfermeria.Model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Enfermeria {
    public partial class FRM_Login : Form {

        private LoginController controlador;

        public FRM_Login() {
            InitializeComponent();
            controlador = new LoginController(this);
            pbCargando.BackColor = Color.GhostWhite;
        }

        public bool VerificarCampos() {
            return string.IsNullOrEmpty(txtUsuario.Text) && string.IsNullOrEmpty(txtContrasenia.Text);
        }

        public void MostrarContrasenia() {
            if (cbMostrarContrasenia.Checked)
                txtContrasenia.PasswordChar = '\0';
            else
                txtContrasenia.PasswordChar = '●';
        }

        public string GetNombreUsuario() {
            return txtUsuario.Text;
        }

        public string GetContrasenia() {
            return txtContrasenia.Text;
        }

        public bool VerificarContraseña(DataTable usuario) {
            byte[] contrasenaIngresada = Seguridad.EncryptPassword(usuario.Rows[0][4].ToString(), txtContrasenia.Text);
            return Seguridad.CheckPassword(contrasenaIngresada, (byte[])usuario.Rows[0][5]);
        }

        public void EstadoInicial() {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            cbMostrarContrasenia.Checked = false;
            txtContrasenia.PasswordChar = '●';
        }

        public void MostrarMensaje(string mensaje) {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ConfirmarEnvioCodigo() {
            string message = "¿Seguro que desea recibir un código para restaurar su contraseña?";
            DialogResult boton = MessageBox.Show(message, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return boton == DialogResult.Yes;
        }

        public void CambiarFocus(object sender, bool accion) {
            if (accion) {
                if (sender.GetHashCode() == 16454088) //txt1
                    txt2.Focus();
                else if (sender.GetHashCode() == 61494432) //txt2
                    txt3.Focus();
                else if (sender.GetHashCode() == 46729429) //txt3
                    txt4.Focus();
            }
            else {
                if (sender.GetHashCode() == 53046711) //txt4
                    txt3.Focus();
                else if (sender.GetHashCode() == 46729429) //txt3
                    txt2.Focus();
                else if (sender.GetHashCode() == 61494432) //txt2
                    txt1.Focus();
            }
        }
    }
}
