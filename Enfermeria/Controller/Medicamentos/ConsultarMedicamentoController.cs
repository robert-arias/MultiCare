using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Model;
using Enfermeria.View.Medicamentos;


namespace Enfermeria.Controller.Medicamentos
{
   

    class ConsultarMedicamentoController
    {
        ConexionMedicamentos conexion;
        FRM_ConsultarMedicamento frm_ConsultarMedicamento;

        public ConsultarMedicamentoController(FRM_ConsultarMedicamento frm_ConsultarMedicamento)
        {
            this.conexion = new ConexionMedicamentos();
            this.frm_ConsultarMedicamento = frm_ConsultarMedicamento;
            AgregarEventos();
        }

        public void AgregarEventos()
        {
            frm_ConsultarMedicamento.btnLimpiar.Click += new EventHandler(EstadoInicial);
            frm_ConsultarMedicamento.btnBuscar.Click += new EventHandler(GetBusquedaMedicamentos);
            frm_ConsultarMedicamento.btnTodos.Click += new EventHandler(GetAll);
            frm_ConsultarMedicamento.btnReporte.Click += new EventHandler(Reporte);
            frm_ConsultarMedicamento.txtBuscar.KeyDown += new KeyEventHandler(GetBusquedaMedicamentosEnter);
        }

        public void EstadoInicial(object sender, EventArgs e)
        {
            frm_ConsultarMedicamento.EstadoInicial();
        }



        public void Reporte(object sender, EventArgs e)
        {
            frm_ConsultarMedicamento.Reporte();
        }


        public void GetAll(object sender, EventArgs e)
        {
            if(conexion.GetAllMedicamento().Tables[0].Rows.Count >= 1)
            {
                if (conexion.GetAllMedicamento()!= null)
                {
                    frm_ConsultarMedicamento.FillBusqueda(conexion.GetAllMedicamento());
                }
                else
                {
                    frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados.");
                }
            }
            else
            {
                frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados.");
            }
        }

        public void GetBusquedaMedicamentosEnter(object sender, KeyEventArgs e)
        {
            DataSet resultado = conexion.GetBusquedaMedicamentos(frm_ConsultarMedicamento.GetBusquedaMedicamentos());

            if (e.KeyCode == Keys.Enter)
            {
                if (frm_ConsultarMedicamento.Verificar())
                {
                    if (resultado != null)
                    {
                        if (resultado.Tables[0].Rows.Count >= 1)
                        {
                            frm_ConsultarMedicamento.FillBusqueda(resultado);
                        }
                        else
                        {
                            frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                        }

                    }
                    else
                    {
                        frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                    }
                }
                else
                {
                    frm_ConsultarMedicamento.MensajeError("Verifique que todos los datos selecccionados e ingresados " +
                                           "sean correctos.");
                }
            }
            e.SuppressKeyPress = true; //remove ding windows sound.
        }

        public void GetBusquedaMedicamentos(object sender, EventArgs e)
        {
            DataSet resultado = conexion.GetBusquedaMedicamentos(frm_ConsultarMedicamento.GetBusquedaMedicamentos());

            if (frm_ConsultarMedicamento.Verificar())
            {
                if (resultado != null)
                {
                    if (resultado.Tables[0].Rows.Count >= 1)
                    {
                        frm_ConsultarMedicamento.FillBusqueda(resultado);
                    }
                    else
                    {
                        frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                    }

                }
                else
                {
                    frm_ConsultarMedicamento.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                }
            }
            else
            {
                frm_ConsultarMedicamento.MensajeError("Verifique que todos los datos selecccionados e ingresados " +
                                       "sean correctos.");
            }

        }


     

    }
}
