using Enfermeria.Model;
using Enfermeria.View.Menu;
using System;
using System.Collections.Generic;
using System.Data;
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
            frm_Menu.FormClosed += Frm_Menu_FormClosed;
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
                        frm_Login.MostrarMensaje("El usuario o la contraseña ingresada son incorrectas.");
                }
                else
                    frm_Login.MostrarMensaje("El usuario o la contraseña ingresada son incorrectas.");
            }
            else
                frm_Login.MostrarMensaje("Verifique que se han ingresado todos los datos");
        }

        private void MostrarContrasenia(object sender, EventArgs e) {
            frm_Login.MostrarContrasenia();
        }

        private void CloseLogin(object sender, EventArgs e) {
            frm_Login.Close();
        }
    }
}
