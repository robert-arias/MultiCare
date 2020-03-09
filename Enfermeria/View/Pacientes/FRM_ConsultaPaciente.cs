using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enfermeria.Controller.Pacientes;

namespace Enfermeria.View.Pacientes
{
    public partial class FRM_ConsultaPaciente : Form
    {
        ConsultarPacienteController consultarPacienteController;
        public FRM_ConsultaPaciente()
        {
            InitializeComponent();
            consultarPacienteController = new ConsultarPacienteController(this);
        }

        public void ActivarDesactivarcbSexo()
        {
            if (rbSexo.Checked)
            {
                cbSexo.Enabled = true;
            }
            else
            {
                cbSexo.Enabled = false;
            }
           
        }

        public void FillBusqueda(DataSet pacientes)
        {
            dgvBusqueda.DataSource = pacientes.Tables[0];
        }

        public string GetBusquedaPaciente()
        {
            string query = "";

            if (rbCedula.Checked)
            {
                query = $"select * from Pacientes where cedula ='{txtBuscar.Text}'";
            }
            else if (rbNombre.Checked)
            {
                query = $"select * from Pacientes where nombre like '%{txtBuscar.Text}%'";
            }
            else if (rbApellidos.Checked)
            {
                query = $"select * from Pacientes where apellidos like '%{txtBuscar.Text}%'";
            }
            else if (rbEdad.Checked)
            {
                query = $"select * from Pacientes where edad ='{txtBuscar.Text}'";
            }
            else if (rbEstado.Checked)
            {
                query = $"select * from Pacientes where estado ='{txtBuscar.Text}'";
            }          
            return query;
        }

        public void MensajeInformativo(string message)
        {
            MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, " Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public bool Verificar()
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                return true;
            }
            else
                return false;
        }

        public string Genero()
        {
           return $"select * from Pacientes where sexo ='{ cbSexo.GetItemText(cbSexo.SelectedItem)}'";
        }

        private void rbSexo_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
