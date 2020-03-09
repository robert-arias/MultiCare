using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Enfermeria.Controller.Medicamentos;

namespace Enfermeria.View.Medicamentos
{
    public partial class FRM_ConsultarMedicamento : Form
    {
        ConsultarMedicamentoController consultarMedicamentoController;
        FRM_ReporteMedicamentos frm_ReporteMedicamentos;

        public FRM_ConsultarMedicamento()
        {
            InitializeComponent();
            this.consultarMedicamentoController = new ConsultarMedicamentoController(this);
            frm_ReporteMedicamentos = new FRM_ReporteMedicamentos();
        }

        public void MensajeInformativo(string message)
        {
            MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, " Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string GetBusquedaMedicamentos()
        {
           
            if (rbCodigo.Checked)
            {
                return $"select * from Medicamentos where codigo_medicamento='{txtBuscar.Text}'";
            }else if (rbNombre.Checked)
            {
                return $"select * from Medicamentos where nombre_medicamento like '%{txtBuscar.Text}%'";
            }
            else
                return $"select * from Medicamentos where categoria='{txtBuscar.Text}'";
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

        public void FillBusqueda(DataSet medicamentos)
        {          
            dgvBusqueda.DataSource = medicamentos.Tables[0];
        }

        public void EstadoInicial()
        {
            LimpiarDGVBusqueda();
            txtBuscar.Text = "";
        }
        public void LimpiarDGVBusqueda()
        {
            do
            {
                foreach (DataGridViewRow row in dgvBusqueda.Rows)
                {
                    dgvBusqueda.Rows.Remove(row);
                }
            } while (dgvBusqueda.Rows.Count >= 1);
        }


        public void Reporte()
        {
            DataSetMedicamento dataSetMedicamentos = new DataSetMedicamento();
            int fila = dgvBusqueda.Rows.Count - 1;
            for (int i = 0; i <= fila; i++)
            {
                dataSetMedicamentos.Tables[0].Rows.Add

                    (new object[] {

                          dgvBusqueda[0, i].Value.ToString(),
                          dgvBusqueda[1, i].Value.ToString(),
                          dgvBusqueda[2, i].Value.ToString(),
                          dgvBusqueda[3, i].Value.ToString(),
                        
                    }
                    );
            }
           
            ReportDocument report = new ReportDocument();
            string fileName = "View\\Medicamentos\\CrystalReportMedicamentos.rpt";
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, fileName);         
            report.Load(path);
            report.SetDataSource(dataSetMedicamentos);
            frm_ReporteMedicamentos.crystalReportViewer1.ReportSource = report;
            frm_ReporteMedicamentos.ShowDialog();
        }
    }
}
