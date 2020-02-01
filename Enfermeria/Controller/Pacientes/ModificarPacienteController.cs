using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enfermeria.View.Pacientes;
using Enfermeria.Model;
using System.Windows.Forms;
using BunifuAnimatorNS;

namespace Enfermeria.Controller.Pacientes
{
    class ModificarPacienteController
    {
        FRM_ModificarPaciente frm_ModificarPaciente;
        ConexionPacientes conexion;

        public ModificarPacienteController(FRM_ModificarPaciente frm_ModificarPaciente)
        {
            this.frm_ModificarPaciente = frm_ModificarPaciente;
            conexion = new ConexionPacientes();
            AgregarEventosPaciente();
        }


        private void AgregarEventosPaciente()
        {
            frm_ModificarPaciente.btnModificar.Click += new EventHandler(ModificarPaciente);
            frm_ModificarPaciente.btnLimpiar.Click += new EventHandler(Limpiar);
            frm_ModificarPaciente.btnVerificar.Click += new EventHandler(Verificar);
            frm_ModificarPaciente.txtCedula.KeyDown += new KeyEventHandler(VerificarEnter);
            frm_ModificarPaciente.txtCedula.KeyPress += new KeyPressEventHandler(ValidarCedula);
            frm_ModificarPaciente.txtNombre.KeyPress += new KeyPressEventHandler(ValidarNombre);
            frm_ModificarPaciente.txtApellidos.KeyPress += new KeyPressEventHandler(ValidarApellidos);


        }

        public void ValidarCedula(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsSeparator(e.KeyChar) || Char.IsControl(e.KeyChar))) {
                e.Handled = true;
                frm_ModificarPaciente.alerta.CambiarMensaje("En el campo cédula solo se permiten dígitos numéricos");
                MostrarConAnimacion(frm_ModificarPaciente.alerta);
            }
            else if (frm_ModificarPaciente.alerta.Visible)
                frm_ModificarPaciente.alerta.Visible = false;
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
                frm_ModificarPaciente.alerta.CambiarMensaje("En el campo cédula solo se permiten dígitos alfabéticos");
                MostrarConAnimacion(frm_ModificarPaciente.alerta);
            }
            else if (frm_ModificarPaciente.alerta.Visible)
                frm_ModificarPaciente.alerta.Visible = false;
        }

        public void LlenarCampos()
        {
            if(conexion.GetPaciente(frm_ModificarPaciente.GetCedula()) != null)
            {
                if (conexion.GetPaciente(frm_ModificarPaciente.GetCedula()).Tables[0].Rows.Count != 0)
                {
                    frm_ModificarPaciente.LlenarCampos(conexion.GetPaciente(frm_ModificarPaciente.GetCedula()));
                    frm_ModificarPaciente.ActivarCampos();
                }

            }
           
        }



        private void VerificarEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(frm_ModificarPaciente.GetCedula()))
                {
                    if (frm_ModificarPaciente.GetCedula().Length >= 9)
                    {
                        if (conexion.ExisteCedulaPaciente(frm_ModificarPaciente.GetCedula()))
                        {
                            LlenarCampos();
                        }
                        else
                        {
                            frm_ModificarPaciente.MensajeError("La cédula de identidad ingresada no se encuentra en los registros.");
                        }

                    }
                    else
                    {
                        frm_ModificarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío o se ingresaron" +
                       " menos de 9 dígitos.");
                    }

                }
                else

                    frm_ModificarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío.");


                e.SuppressKeyPress = true; //remove ding windows sound.

            }
        }



        private void Verificar(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(frm_ModificarPaciente.GetCedula()))
            {
                if (frm_ModificarPaciente.GetCedula().Length >= 9)
                {
                    if (conexion.ExisteCedulaPaciente(frm_ModificarPaciente.GetCedula()))
                    {
                        LlenarCampos();
                    }
                    else
                    {
                        frm_ModificarPaciente.MensajeError("La cédula de identidad ingresada no se encuentra en los registros.");
                    }

                }
                else
                {
                    frm_ModificarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío o se ingresaron" +
                   " menos de 9 dígitos.");
                }

            }
            else
            {
                frm_ModificarPaciente.MensajeError("El campo \"número de cédula\" se encuentra vacío.");

            }
        }

        private void Limpiar(object sender, EventArgs e)
        {
            frm_ModificarPaciente.EstadoInicial();
        }

        private void ModificarPaciente(object sender, EventArgs e)
        {
            if (!frm_ModificarPaciente.VerificarCampos())
            {
                if (frm_ModificarPaciente.ShowConfirmation())
                {
                    if (conexion.UpdatePaciente(frm_ModificarPaciente.GetPaciente()))
                    {
                        frm_ModificarPaciente.MensajeInformativo("Paciente modificado con  éxito.");
                        frm_ModificarPaciente.EstadoInicial();

                    }
                    else
                    {
                        frm_ModificarPaciente.MensajeError("No se ha modificado al paciente.");
                    }

                }
                else
                {
                    frm_ModificarPaciente.MensajeError("No se ha modificado al paciente.");
                }

            }
            else
            {
                frm_ModificarPaciente.MensajeError("Algunos campos se encuentran vacíos." +
                    "\nLos campos con el asterisco (*) rojo son aquellos que deben ser modificados.");
            }

        }

        private void MostrarConAnimacion(Control control) {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(control, false, Animation.VertSlide);
        }

    }
}
