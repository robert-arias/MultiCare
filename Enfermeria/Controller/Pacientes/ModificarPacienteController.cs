﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enfermeria.View.Pacientes;
using Enfermeria.Model;
using System.Windows.Forms;

namespace Enfermeria.Controller.Pacientes
{
    class ModificarPacienteController
    {
        FRM_ModificarPaciente frm_ModificarPaciente;
        Conexion conexion;
        public ModificarPacienteController(FRM_ModificarPaciente frm_ModificarPaciente)
        {
            this.frm_ModificarPaciente = frm_ModificarPaciente;
            conexion = new Conexion();
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
            frm_ModificarPaciente.SoloNumeros(e);
        }


        public void ValidarNombre(object sender, KeyPressEventArgs e)
        {
            frm_ModificarPaciente.SoloLetras(e);
        }

        public void ValidarApellidos(object sender, KeyPressEventArgs e)
        {
            frm_ModificarPaciente.SoloLetras(e);
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

       
    }
}