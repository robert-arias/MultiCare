using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enfermeria.View.Medicamentos;
using Enfermeria.Model;
using System.Windows.Forms;

namespace Enfermeria.Controller.Medicamentos
{
    class AgregarMedicamentoController
    {
        FRM_AgregarMedicamento frm_AgregarMedicamento;
        ConexionMedicamentos conexion;
        public AgregarMedicamentoController(FRM_AgregarMedicamento frm_AgregarMedicamento)
        {
            conexion = new ConexionMedicamentos();
            this.frm_AgregarMedicamento = frm_AgregarMedicamento;
            AgregarEventosMedicamentos();
        }

        private void AgregarEventosMedicamentos()
        {
            frm_AgregarMedicamento.btnAgregar.Click += new EventHandler(AgregarMedicamento);
            frm_AgregarMedicamento.btnVerificar.Click += new EventHandler(Verificar);
            frm_AgregarMedicamento.txtCodigo.KeyDown += new KeyEventHandler(VerificarEnter);
            frm_AgregarMedicamento.btnLimpiar.Click += new EventHandler(Limpiar);
            frm_AgregarMedicamento.txtCantidad.KeyPress += new KeyPressEventHandler(ValidarCantidadDisponible);
            frm_AgregarMedicamento.txtUnidadMedida.KeyPress += new KeyPressEventHandler(ValidarMedida);
        }

        public void ValidarCantidadDisponible(object sender, KeyPressEventArgs e)
        {
            frm_AgregarMedicamento.SoloNumeros(e);
        }

        public void ValidarMedida(object sender, KeyPressEventArgs e)
        {
            frm_AgregarMedicamento.SoloNumeros(e);
        }

        private void Verificar(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(frm_AgregarMedicamento.GetCodigo()))
            {

                if (!conexion.ExisteCodigoMedicamento(frm_AgregarMedicamento.GetCodigo()))
                {
                    frm_AgregarMedicamento.ActivarCampos();
                }
                else
                {
                    frm_AgregarMedicamento.MensajeError("El código de medicamento ingresado no se encuentra en los registros.");
                }

            }
            else
            {
                frm_AgregarMedicamento.MensajeError("El campo \"código de medicamento\" se encuentra vacío.");

            }

        }


        public void VerificarEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(frm_AgregarMedicamento.GetCodigo()))
                {

                    if (!conexion.ExisteCodigoMedicamento(frm_AgregarMedicamento.GetCodigo()))
                    {
                        frm_AgregarMedicamento.ActivarCampos();
                    }
                    else
                    {
                        frm_AgregarMedicamento.MensajeError("El código de medicamento ingresado no se encuentra en los registros.");
                    }

                }
                else
                {
                    frm_AgregarMedicamento.MensajeError("El campo \"código de medicamento\" se encuentra vacío.");

                }
                e.SuppressKeyPress = true; //remove ding windows sound.

            }
        }

        private void Limpiar(object sender, EventArgs e)
        {
            frm_AgregarMedicamento.EstadoInicial();
        }

        private void AgregarMedicamento(object sender, EventArgs e)
        {
            if (!frm_AgregarMedicamento.VerificarCampos())
            {
                if (frm_AgregarMedicamento.ShowConfirmation())
                {
                    if (conexion.AgregarMedicamento(frm_AgregarMedicamento.GetMedicamento()))
                    {
                        frm_AgregarMedicamento.MensajeInformativo("Se ha agregado el nuevo medicamento con éxito.");
                        frm_AgregarMedicamento.EstadoInicial();
                    }
                    else
                    {
                        frm_AgregarMedicamento.MensajeError("Se ha producido un error.\nVerifique los datos.");

                    }
                }
                else
                {
                    frm_AgregarMedicamento.MensajeError("No se ha agregado el medicamento.");
                }

            }
            else
            {
                frm_AgregarMedicamento.MensajeError("Algunos campos se encuentran vacíos." +
                    "\nLos campos con el asterisco (*) rojo son aquellos que deben ser modificados.");
            }
        }
    }
}
