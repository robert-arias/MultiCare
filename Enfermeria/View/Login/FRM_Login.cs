using Enfermeria.Controller.Login;
using Enfermeria.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace Enfermeria {
    public partial class FRM_Login : Form {

        private LoginController controlador;

        public FRM_Login() {
            InitializeComponent();
            controlador = new LoginController(this);
        }

        public bool VerificarCampos() {
            return string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text);
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

        public bool VerificarContraseña(DataTable usuario) {
            byte[] contrasenaIngresada = Seguridad.EncryptPassword(usuario.Rows[0][3].ToString(), txtContrasenia.Text);
            return Seguridad.CheckPassword(contrasenaIngresada, (byte[])usuario.Rows[0][4]);
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

    }
}
