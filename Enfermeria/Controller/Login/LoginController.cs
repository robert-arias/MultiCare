using BunifuAnimatorNS;
using Enfermeria.Model;
using Enfermeria.View.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enfermeria.Controller.Login {
    public class LoginController {

        private FRM_Login frm_Login;
        private ConexionUsuarios db;
        private FRM_Menu frm_Menu;

        public LoginController(FRM_Login frm_Login) {
            this.frm_Login = frm_Login;
            db = new ConexionUsuarios();
            frm_Menu = new FRM_Menu();
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
            frm_Login.btnCancelar.Click += new EventHandler(CancelarEnvioCodigo);
            frm_Menu.FormClosed += Frm_Menu_FormClosed;
        }

        private void CancelarEnvioCodigo(object sender, EventArgs e) {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(frm_Login.pLogin, false, Animation.HorizSlide);
            frm_Login.btnCancelar.Enabled = true;
            frm_Login.pbCargando.Visible = false;
            frm_Login.pbCargando.animated = false;
            frm_Login.txtUsuarioRecuperar.Text = "";
        }

        private void EnviarCodigo(object sender, EventArgs e) {
            if (frm_Login.ConfirmarEnvioCodigo()) {
                frm_Login.btnCancelar.Enabled = false;
                frm_Login.pbCargando.Visible = true;
                frm_Login.pbCargando.animated = true;
                frm_Login.pbCargando.BackColor = Color.GhostWhite;
            }
        }

        private void RecuperarContrasenia(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(frm_Login.txtUsuario.Text)) {
                frm_Login.txtUsuarioRecuperar.Text = frm_Login.txtUsuario.Text;
            }
            BunifuTransition transition = new BunifuTransition();
            transition.HideSync(frm_Login.pLogin, false, Animation.HorizSlide);
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
                            if (frm_Login.VerificarContraseña(usuario)) {
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
    }
}
