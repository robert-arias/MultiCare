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

        private string codigo_recuperacion = null;
        private string username = null;

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
            frm_Login.btnEnviarCodigo.Click += new EventHandler(EnviarCodigoBoton);
            frm_Login.txtUsuarioRecuperar.KeyDown += new KeyEventHandler(EnviarCodigoEnter);
            frm_Login.btnCancelarEnviarCodigo.Click += new EventHandler(CancelarMostrarLogin);
            frm_Login.btnCancelarIngresarCodigo.Click += new EventHandler(CancelarMostrarLogin);
            frm_Login.txt1.KeyPress += new KeyPressEventHandler(IngresandoCodigo1);
            frm_Login.txt2.KeyPress += new KeyPressEventHandler(IngresandoCodigo2);
            frm_Login.txt3.KeyPress += new KeyPressEventHandler(IngresandoCodigo3);
            frm_Login.txt4.KeyPress += new KeyPressEventHandler(IngresandoCodigo4);
            frm_Login.btnConfirmarCodigo.Click += new EventHandler(VerificarCodigo);
            frm_Login.btnCambiarContrasenia.Click += new EventHandler(CambiarContraseniaBoton);
            frm_Login.txtNuevaContrasenia.KeyDown += new KeyEventHandler(CambiarContraseniaEnter);
            frm_Login.txtRepetirContrasenia.KeyDown += new KeyEventHandler(CambiarContraseniaEnter);
            frm_Login.cbMostrarContrasenias.CheckedChanged += new EventHandler(MostrarContrasenias);
            frm_Menu.FormClosed += Frm_Menu_FormClosed;
            recuperacionEmail.MailClient.SendCompleted += new SendCompletedEventHandler(CorreoEnviado);
        }

        private void MostrarContrasenias(object sender, EventArgs e) {
            frm_Login.MostrarContrasenias();
        }

        private void CambiarContraseniaEnter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                CambiarContrasenia();
                e.SuppressKeyPress = true;
            }
        }

        private void CambiarContraseniaBoton(object sender, EventArgs e) {
            CambiarContrasenia();
        }

        private void EnviarCodigoEnter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                EnviarCodigo();
                e.SuppressKeyPress = true;
            }
        }

        private void VerificarCodigo(object sender, EventArgs e) {
            VerificarCodigoVerificacion();
        }

        private void VerificarCodigoVerificacion() {
            string codigoIngresado = frm_Login.txt1.Text + frm_Login.txt2.Text + frm_Login.txt3.Text +
                frm_Login.txt4.Text;
            if (codigoIngresado.Equals(codigo_recuperacion)) {
                EsconderConAnimacion(frm_Login.pIngresarCodigo, Animation.HorizSlide);
            }
            else {
                frm_Login.alertaCodigoIncorrecto.CambiarImagenWarning();
                frm_Login.alertaCodigoIncorrecto.CambiarMensaje("El código ingresado es incorrecto");
                MostrarConAnimacion(frm_Login.alertaCodigoIncorrecto, Animation.VertSlide);
            }
        }

        private void CancelarMostrarLogin(object sender, EventArgs e) {
            EstadoInicial();
        }

        private void EnviarCodigoBoton(object sender, EventArgs e) {
            EnviarCodigo();
        }

        private void RecuperarContrasenia(object sender, EventArgs e) {
            frm_Login.txtUsuarioRecuperar.Focus();
            if (!string.IsNullOrEmpty(frm_Login.txtUsuario.Text))
                frm_Login.txtUsuarioRecuperar.Text = frm_Login.txtUsuario.Text;
            EsconderConAnimacion(frm_Login.pLogin, Animation.HorizSlide);
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
            if (!frm_Login.VerificarCamposLogin()) {
                if (!string.IsNullOrEmpty(frm_Login.txtUsuario.Text)) {
                    if (!string.IsNullOrEmpty(frm_Login.txtContrasenia.Text)) {
                        DataTable usuario = db.GetUsuario(frm_Login.GetNombreUsuario());
                        if (usuario.Rows.Count > 0) {
                            if (db.VerificarContrasenia(frm_Login.GetNombreUsuario(), frm_Login.GetContrasenia())) {
                                frm_Login.MostrarMensaje("Hola, " + usuario.Rows[0][0].ToString() + ".\n" +
                                    "Se ha ingresado al sistema correctamente");
                                frm_Login.EstadoInicialLogin();
                                frm_Login.alertaLogin.Visible = false;
                                frm_Login.Hide();
                                frm_Menu.Show();
                            }
                            else {
                                frm_Login.alertaLogin.CambiarMensaje("Contraseña incorrecta");
                                MostrarConAnimacion(frm_Login.alertaLogin, Animation.VertSlide);
                            }
                        }
                        else {
                            frm_Login.alertaLogin.CambiarMensaje("Usuario no registrado");
                            MostrarConAnimacion(frm_Login.alertaLogin, Animation.VertSlide);
                        }
                    }
                    else {
                        frm_Login.alertaLogin.CambiarMensaje("Campo contraseña vacío");
                        MostrarConAnimacion(frm_Login.alertaLogin, Animation.VertSlide);
                    }
                }
                else {
                    frm_Login.alertaLogin.CambiarMensaje("Campo nombre de usuario vacío");
                    MostrarConAnimacion(frm_Login.alertaLogin, Animation.VertSlide);
                }
            }
            else {
                frm_Login.alertaLogin.CambiarMensaje("Nombre de usuario y contraseña vacíos");
                MostrarConAnimacion(frm_Login.alertaLogin, Animation.VertSlide);
            }
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
                EsconderConAnimacion(frm_Login.pEnviarCodigo, Animation.HorizSlide);
                frm_Login.txt1.Focus();
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
            VerificarCodigoVerificacion();
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) {
                frm_Login.txt3.Focus();
            }
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void MostrarConAnimacion(Control control, Animation tipo) {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(control, false, tipo);
        }

        private void EsconderConAnimacion(Control control, Animation tipo) {
            BunifuTransition transition = new BunifuTransition();
            transition.HideSync(control, false, tipo);
        }

        private void EnviarCodigo() {
            if (!string.IsNullOrEmpty(frm_Login.txtUsuarioRecuperar.Text)) {
                DataTable usuario = db.GetUsuario(frm_Login.txtUsuarioRecuperar.Text);
                if (usuario.Rows.Count > 0) {
                    if (recuperacionEmail.VerificarConexionInternet()) {
                        username = frm_Login.txtUsuarioRecuperar.Text;
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
                    else
                        MostrarConAnimacion(frm_Login.alertaNoInternet, Animation.VertSlide);
                }
                else
                    frm_Login.MostrarMensaje("El nombre de usuario ingresado no se encuentra registrado.");
            }
            else
                frm_Login.MostrarMensaje("El campo nombre de usuario se encuentra vacío.");
        }

        private void CambiarContrasenia() {
            if (!frm_Login.VerificarCamposCambiarContrasenia()) {
                if (!string.IsNullOrEmpty(frm_Login.txtNuevaContrasenia.Text)) {
                    if (!string.IsNullOrEmpty(frm_Login.txtRepetirContrasenia.Text)) {
                        if (frm_Login.txtNuevaContrasenia.Text.Length >= 8) {
                            frm_Login.alertaCambiarContrasenia.Visible = false;
                            if (frm_Login.txtNuevaContrasenia.Text.Equals(frm_Login.txtRepetirContrasenia.Text)) {
                                db.CambiarContrasenia(frm_Login.txtNuevaContrasenia.Text, username);
                                frm_Login.MostrarMensaje("La contraseña ha sido actualizada con éxito");
                                EstadoInicial();
                            }
                            else {
                                frm_Login.alertaCambiarContrasenia.CambiarMensaje("Las contraseñas no coinciden");
                                MostrarConAnimacion(frm_Login.alertaCambiarContrasenia, Animation.VertSlide);
                            }
                        }
                        else {
                            frm_Login.alertaCambiarContrasenia.CambiarMensaje("La contraseña debe ser mayor o igual a 8 caracteres");
                            MostrarConAnimacion(frm_Login.alertaCambiarContrasenia, Animation.VertSlide);
                        }
                    }
                    else {
                        frm_Login.alertaCambiarContrasenia.CambiarMensaje("El campo repetir contraseña se encuentra vacío");
                        MostrarConAnimacion(frm_Login.alertaCambiarContrasenia, Animation.VertSlide);
                    }
                }
                else {
                    frm_Login.alertaCambiarContrasenia.CambiarMensaje("El campo nueva contraseña se encuentra vacío");
                    MostrarConAnimacion(frm_Login.alertaCambiarContrasenia, Animation.VertSlide);
                }
            }
            else {
                frm_Login.alertaCambiarContrasenia.CambiarMensaje("Los campos se encuentran vacíos");
                MostrarConAnimacion(frm_Login.alertaCambiarContrasenia, Animation.VertSlide);
            }
        }

        private void EstadoInicial() {
            MostrarConAnimacion(frm_Login.pLogin, Animation.HorizSlide);
            frm_Login.EstadoInicial();
            username = null;
            codigo_recuperacion = null;
        }

    }
}