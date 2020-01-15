using Bunifu.UI.WinForms.BunifuTextbox;
using BunifuAnimatorNS;
using Enfermeria.Model;
using Enfermeria.View.Menu;
using System;
using System.ComponentModel;
using System.Data;
using System.Net.Mail;
using System.Windows.Forms;

namespace Enfermeria.Controller.Login {
    public class LoginController {

        private FRM_Login frm_Login;
        private ConexionUsuarios db;
        private FRM_Menu frm_Menu;
        private RecuperacionEmail recuperacionEmail;

        public string codigo_recuperacion = null;

        public LoginController(FRM_Login frm_Login) {
            this.frm_Login = frm_Login;
            db = new ConexionUsuarios();
            frm_Menu = new FRM_Menu();
            recuperacionEmail = new RecuperacionEmail();
            AgregarEventos();
        }

        private void AgregarEventos() {
            frm_Login.lkCerrar.Click += new EventHandler(CloseLogin);
            frm_Login.cbMostrarContrasenia.CheckedChanged += new EventHandler(MostrarContrasenia);
            frm_Login.btnIngresar.Click += new EventHandler(IngresarBoton);
            frm_Login.txtUsuario.KeyDown += new KeyEventHandler(IngresarEnter);
            frm_Login.txtContrasenia.KeyDown += new KeyEventHandler(IngresarEnter);
            frm_Login.lkRecuperar.Click += new EventHandler(RecuperarContrasenia);
            frm_Login.btnEnviarCodigo.Click += new EventHandler(EnviarCodigo);
            frm_Login.btnCancelarEnviarCodigo.Click += new EventHandler(CancelarMostrarLogin);
            frm_Login.btnCancelarIngresarCodigo.Click += new EventHandler(CancelarMostrarLogin);
            frm_Login.txt1.KeyPress += new KeyPressEventHandler(IngresandoCodigo1);
            frm_Login.txt2.KeyPress += new KeyPressEventHandler(IngresandoCodigo2);
            frm_Login.txt3.KeyPress += new KeyPressEventHandler(IngresandoCodigo3);
            frm_Login.txt4.KeyPress += new KeyPressEventHandler(IngresandoCodigo4);
            frm_Login.btnConfirmarCodigo.Click += new EventHandler(VerificarCodigoActivacion);
            frm_Menu.FormClosed += Frm_Menu_FormClosed;
            recuperacionEmail.MailClient.SendCompleted += new SendCompletedEventHandler(CorreoEnviado);
        }

        private void VerificarCodigoActivacion(object sender, EventArgs e) {
            string codigoIngresado = frm_Login.txt1.Text + frm_Login.txt2.Text + frm_Login.txt3.Text +
                frm_Login.txt4.Text;
            if (codigoIngresado.Equals(codigo_recuperacion)) {
                frm_Login.MostrarMensaje("Código correcto");
            }
            else {
                frm_Login.alertaCodigoIncorrecto.CambiarImagenWarning();
                frm_Login.alertaCodigoIncorrecto.CambiarMensaje("El código ingresado es incorrecto");
                BunifuTransition transition = new BunifuTransition();
                transition.ShowSync(frm_Login.alertaCodigoIncorrecto, false, Animation.VertSlide);
            }
        }

        private void IngresandoCodigo1(object sender, KeyPressEventArgs e) {
            if (char.IsLetterOrDigit(e.KeyChar)) {
                frm_Login.txt2.Focus();
                if (!string.IsNullOrEmpty(frm_Login.txt1.Text))
                    frm_Login.txt2.Text = e.KeyChar.ToString();
            }
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void IngresandoCodigo2(object sender, KeyPressEventArgs e) {
            if (char.IsLetterOrDigit(e.KeyChar)) {
                frm_Login.txt3.Focus();
                if (!string.IsNullOrEmpty(frm_Login.txt2.Text))
                    frm_Login.txt3.Text = e.KeyChar.ToString();
            }

            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) {
                frm_Login.txt1.Focus();
            }
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void IngresandoCodigo3(object sender, KeyPressEventArgs e) {
            if (char.IsLetterOrDigit(e.KeyChar)) {
                frm_Login.txt4.Focus();
                if (!string.IsNullOrEmpty(frm_Login.txt3.Text))
                    frm_Login.txt4.Text = e.KeyChar.ToString();
            }

            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) {
                frm_Login.txt2.Focus();
            }
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void IngresandoCodigo4(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) {
                frm_Login.txt3.Focus();
            }
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void CancelarMostrarLogin(object sender, EventArgs e) {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(frm_Login.pLogin, false, Animation.HorizSlide);
            frm_Login.pEnviarCodigo.Visible = true;
            frm_Login.btnCancelarEnviarCodigo.Enabled = true;
            frm_Login.pbCargando.Visible = false;
            frm_Login.pbCargando.animated = false;
            frm_Login.txtUsuarioRecuperar.Text = "";
            frm_Login.txt1.Text = "";
            frm_Login.txt2.Text = "";
            frm_Login.txt3.Text = "";
            frm_Login.txt4.Text = "";
            frm_Login.pCodigo.SendToBack();
        }

        private void EnviarCodigo(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(frm_Login.txtUsuarioRecuperar.Text)) {
                DataTable usuario = db.GetUsuario(frm_Login.txtUsuarioRecuperar.Text);
                if (usuario.Rows.Count > 0) {
                    if (recuperacionEmail.VerificarConexionInternet()) {
                        frm_Login.alertaNoInternet.Visible = false;
                        if (frm_Login.ConfirmarEnvioCodigo()) {
                            frm_Login.btnCancelarEnviarCodigo.Enabled = false;
                            frm_Login.btnEnviarCodigo.Enabled = false;
                            frm_Login.pbCargando.Visible = true;
                            frm_Login.pbCargando.animated = true;

                            codigo_recuperacion = Seguridad.GetSalt();
                            recuperacionEmail.EnviarCodigo(frm_Login.GetNombreUsuario(),
                                (usuario.Rows[0][0].ToString() + " " + usuario.Rows[0][1].ToString()),
                                usuario.Rows[0][2].ToString(), codigo_recuperacion);
                        }
                        else
                            frm_Login.MostrarMensaje("No se ha enviado el código de recuperación.");
                    }
                    else {
                        BunifuTransition transition = new BunifuTransition();
                        transition.ShowSync(frm_Login.alertaNoInternet, false, Animation.VertSlide);
                    }
                }
                else
                    frm_Login.MostrarMensaje("El nombre de usuario ingresado no se encuentra registrado.");
            }
            else
                frm_Login.MostrarMensaje("El campo nombre de usuario se encuentra vacío.");
        }

        private void RecuperarContrasenia(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(frm_Login.txtUsuario.Text)) {
                frm_Login.txtUsuarioRecuperar.Text = frm_Login.txtUsuario.Text;
            }
            BunifuTransition transition = new BunifuTransition();
            transition.HideSync(frm_Login.pLogin, false, Animation.HorizSlide);
            frm_Login.txtUsuarioRecuperar.Focus();
        }

        private void Frm_Menu_FormClosed(object sender, FormClosedEventArgs e) {
            frm_Login.Close();
        }

        private void IngresarEnter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                IniciarSesion();
                e.SuppressKeyPress = true;
            }
        }

        private void IngresarBoton(object sender, EventArgs e) {
            IniciarSesion();
        }

        private void IniciarSesion() {
            if (!frm_Login.VerificarCampos()) {
                if (!string.IsNullOrEmpty(frm_Login.txtUsuario.Text)) {
                    if (!string.IsNullOrEmpty(frm_Login.txtContrasenia.Text)) {
                        DataTable usuario = db.GetUsuario(frm_Login.GetNombreUsuario());
                        if (usuario.Rows.Count > 0) {
                            if (db.VerificarContrasenia(frm_Login.GetNombreUsuario(), frm_Login.GetContrasenia())) {
                                frm_Login.MostrarMensaje("Hola, " + usuario.Rows[0][0].ToString() + ".\n" +
                                    "Se ha ingresado al sistema correctamente");
                                frm_Login.EstadoInicial();
                                frm_Login.Hide();
                                frm_Menu.Show();
                            }
                            else
                                frm_Login.MostrarMensaje("La contraseña ingresada es incorrecta.");
                        }
                        else
                            frm_Login.MostrarMensaje("El nombre de usuario ingresado no existe en nuestros registros.");
                    }
                    else
                        frm_Login.MostrarMensaje("El campo contraseña se encuentra vacío.");
                }
                else
                    frm_Login.MostrarMensaje("El campo nombre de usuario se encuentra vacío.");
            }
            else
                frm_Login.MostrarMensaje("El campo de nombre de usuario y contraseña se encuentran vacíos.");
        }

        private void MostrarContrasenia(object sender, EventArgs e) {
            frm_Login.MostrarContrasenia();
        }

        private void CloseLogin(object sender, EventArgs e) {
            frm_Login.Close();
        }

        private void CorreoEnviado(object sender, AsyncCompletedEventArgs e) {
            frm_Login.pbCargando.Visible = false;
            frm_Login.pbCargando.animated = false;
            if (e.Error != null) {
                frm_Login.MostrarMensaje($"Ha ocurrido un error al enviar el enviar el código de recuperación. \n" +
                    $"El error ocurrido fue: {e.Error.ToString()}");
            }
            else {
                frm_Login.MostrarMensaje("El código de recuperación ha sido enviado con éxito.");
                BunifuTransition transition = new BunifuTransition();
                transition.HideSync(frm_Login.pEnviarCodigo, false, Animation.HorizSlide);
                frm_Login.txt1.Focus();
            }
            
        }
    }
}