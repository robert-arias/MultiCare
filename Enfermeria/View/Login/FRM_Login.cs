using Enfermeria.Controller.Login;
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
            alertaLogin.CambiarImagenWarning();
            alertaCambiarContrasenia.CambiarImagenWarning();
        }

        public bool VerificarCamposLogin() {
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

        public void EstadoInicialLogin() {
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

        public bool VerificarCamposCambiarContrasenia() {
            return string.IsNullOrEmpty(txtNuevaContrasenia.Text) && string.IsNullOrEmpty(txtRepetirContrasenia.Text);
        }

        public void MostrarContrasenias() {
            if (cbMostrarContrasenias.Checked) {
                txtNuevaContrasenia.PasswordChar = '\0';
                txtRepetirContrasenia.PasswordChar = '\0';
            }
            else {
                txtNuevaContrasenia.PasswordChar = '●';
                txtRepetirContrasenia.PasswordChar = '●';
            }
        }

        public void EstadoInicial() {
            pEnviarCodigo.Visible = true;
            pIngresarCodigo.Visible = true;
            pEnviarCodigo.SendToBack();
            pIngresarCodigo.SendToBack();
            pCambiarContrasenia.SendToBack();

            alertaCambiarContrasenia.Visible = false;
            alertaCodigoIncorrecto.Visible = false;
            alertaLogin.Visible = false;
            alertaNoInternet.Visible = false;

            txtUsuarioRecuperar.Text = "";
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txtNuevaContrasenia.Text = "";
            txtRepetirContrasenia.Text = "";

            btnEnviarCodigo.Enabled = true;
            btnCancelarEnviarCodigo.Enabled = true;
            pbCargando.Visible = false;
            pbCargando.animated = false;
        }

    }
}