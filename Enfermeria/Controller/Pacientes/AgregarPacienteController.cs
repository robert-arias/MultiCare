using BunifuAnimatorNS;
using Enfermeria.Model;
using Enfermeria.View.Pacientes;
using System;
using System.Windows.Forms;

namespace Enfermeria.Controller {
    public class PacienteController
    {
        FRM_AgregarPaciente frm_AgregarPaciente;
        ConexionPacientes conexion;

        public PacienteController(FRM_AgregarPaciente frm_AgregarPaciente)
        {
            this.frm_AgregarPaciente = frm_AgregarPaciente;
            conexion = new ConexionPacientes();
            AgregarEventosPaciente();

        }

        private void AgregarEventosPaciente()
        {
            frm_AgregarPaciente.btnAgregar.Click += new EventHandler(AgregarPaciente);
            frm_AgregarPaciente.btnLimpiar.Click += new EventHandler(Limpiar);
            frm_AgregarPaciente.btnVerificar.Click += new EventHandler(Verificar);
            frm_AgregarPaciente.txtCedula.KeyDown += new KeyEventHandler(VerificarEnter);
            frm_AgregarPaciente.txtCedula.KeyPress += new KeyPressEventHandler(ValidarCedula);
            frm_AgregarPaciente.txtNombre.KeyPress += new KeyPressEventHandler(ValidarNombre);
            frm_AgregarPaciente.txtApellidos.KeyPress += new KeyPressEventHandler(ValidarApellidos);
        }

        public void ValidarCedula(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsSeparator(e.KeyChar) || Char.IsControl(e.KeyChar))) {
                e.Handled = true;
                frm_AgregarPaciente.alerta.CambiarMensaje("En el campo cédula solo se permiten dígitos numéricos");
                MostrarConAnimacion(frm_AgregarPaciente.alerta);
            }
            else if (frm_AgregarPaciente.alerta.Visible)
                frm_AgregarPaciente.alerta.Visible = false;
        }
        
        public void ValidarNombre(object sender, KeyPressEventArgs e)
        {
            SoloLetras(e);
        }

        public void ValidarApellidos(object sender, KeyPressEventArgs e)
        {
            SoloLetras(e);
        }

        private void SoloLetras(KeyPressEventArgs e) {
            if (!(char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsControl(e.KeyChar))) {
                e.Handled = true;
                frm_AgregarPaciente.alerta.CambiarMensaje("En el campo cédula solo se permiten dígitos alfabéticos");
                MostrarConAnimacion(frm_AgregarPaciente.alerta);
            }
            else if (frm_AgregarPaciente.alerta.Visible)
                frm_AgregarPaciente.alerta.Visible = false;
        }

        private void VerificarEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(frm_AgregarPaciente.GetCedula()))
                {
                    if (frm_AgregarPaciente.GetCedula().Length >= 9)
                    {
                        if (!conexion.ExisteCedulaPaciente(frm_AgregarPaciente.GetCedula()))
                        {
                            frm_AgregarPaciente.ActivarCampos();
                        }
                        else
                        {
                            frm_AgregarPaciente.MensajeError("La cédula de identidad ingresada se encuentra en los registros.");
                        }
                    }
                    else
                    {
                        frm_AgregarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío o se ingresaron" +
                       " menos de 9 dígitos.");
                    }

                }
                else

                    frm_AgregarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío.");

                e.SuppressKeyPress = true; //remove ding windows sound.
            }
        }

        private void Verificar(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(frm_AgregarPaciente.GetCedula()))
            {
                if (frm_AgregarPaciente.GetCedula().Length >= 9)
                {
                    if (!conexion.ExisteCedulaPaciente(frm_AgregarPaciente.GetCedula()))
                    {
                        frm_AgregarPaciente.ActivarCampos();
                    }
                    else
                    {
                        frm_AgregarPaciente.MensajeError("La cédula de identidad ingresada se encuentra en los registros.");
                    }

                }
                else
                {
                    frm_AgregarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío o se ingresaron" +
                   " menos de 9 dígitos.");
                }

            }
            else
            {
                frm_AgregarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío.");

            }
        }

        private void Limpiar(object sender, EventArgs e)
        {
            frm_AgregarPaciente.EstadoInicial();
        }

        private void AgregarPaciente(object sender, EventArgs e)
        {
            if (!frm_AgregarPaciente.VerificarCampos())
            {
                if (frm_AgregarPaciente.ShowConfirmation())
                {
                    if (conexion.AgregarPaciente(frm_AgregarPaciente.GetPaciente()))
                    {
                        frm_AgregarPaciente.MensajeInformativo("Se ha agregado al nuevo paciente con éxito.");
                        frm_AgregarPaciente.EstadoInicial();
                    }
                    else
                    {
                        frm_AgregarPaciente.MensajeError("Se ha producido un error.\nVerifique los datos.");

                    }
                }
                else
                {
                    frm_AgregarPaciente.MensajeInformativo("No se ha agregado al paciente.");
                }
            }
            else
            {
                frm_AgregarPaciente.MensajeError("Algunos campos se encuentran vacíos." +
                    "\nLos campos con el asterisco (*) rojo son aquellos que deben ser modificados.");
            }
        }

        private void MostrarConAnimacion(Control control) {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(control, false, Animation.VertSlide);
        }

    }
}
