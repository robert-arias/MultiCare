using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enfermeria.View.Pacientes;
using Enfermeria.Model;
using System.Windows.Forms;
using System.Data;

namespace Enfermeria.Controller.Pacientes
{
    class ConsultarPacienteController
    {
        FRM_ConsultaPaciente frm_ConsultaPaciente;
        ConexionPacientes conexion;

        public ConsultarPacienteController(FRM_ConsultaPaciente frm_ConsultaPaciente)
        {
            this.frm_ConsultaPaciente = frm_ConsultaPaciente;
            conexion = new ConexionPacientes();
            AgregarEventos();
        }

        public void AgregarEventos()
        {
          
            frm_ConsultaPaciente.btnBuscar.Click += new EventHandler(GetBusquedaPacientes);
            frm_ConsultaPaciente.cbSexo.SelectedIndexChanged += new EventHandler(Genero);
            frm_ConsultaPaciente.rbSexo.CheckedChanged += new EventHandler(ActivarDesactivarcbSexo);
            frm_ConsultaPaciente.btnTodos.Click += new EventHandler(GetAll);
           
            
        }

        public void GetAll(object sender, EventArgs e)
        {
            if (conexion.GetAll().Tables[0].Rows.Count >= 1)
            {
                if (conexion.GetAll() != null)
                {
                    frm_ConsultaPaciente.FillBusqueda(conexion.GetAll());
                }
                else
                {
                    frm_ConsultaPaciente.MensajeInformativo("No se han encontrado resultados.");
                }
            }
            else
            {
                frm_ConsultaPaciente.MensajeInformativo("No se han encontrado resultados.");
            }
        }


        public void ActivarDesactivarcbSexo(object sender, EventArgs e)
        {
            frm_ConsultaPaciente.ActivarDesactivarcbSexo();
        }
        public void Genero(object sender, EventArgs e)
        {
            frm_ConsultaPaciente.FillBusqueda(conexion.GetBusquedaPacientes(frm_ConsultaPaciente.Genero()));
        }
        public void GetBusquedaPacientes(object sender, EventArgs e)
        {
            DataSet resultado = conexion.GetBusquedaPacientes(frm_ConsultaPaciente.GetBusquedaPaciente());

            if (frm_ConsultaPaciente.Verificar())
            {
                if (resultado != null)
                {
                    if (resultado.Tables[0].Rows.Count >= 1)
                    {
                        frm_ConsultaPaciente.FillBusqueda(resultado);
                    }
                    else
                    {
                        frm_ConsultaPaciente.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                    }

                }
                else
                {
                    frm_ConsultaPaciente.MensajeInformativo("No se han encontrado resultados para la búsqueda especificada.");
                }
            }
            else
            {
                frm_ConsultaPaciente.MensajeError("Verifique que todos los datos selecccionados e ingresados " +
                                       "sean correctos.");
            }

        }


    }
}
