using Enfermeria.View.Menu;
using Enfermeria.View.Pacientes;
using Enfermeria.View.Medicamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Controller.Menu {
    public class MenuController {

        private FRM_Menu frm_Menu;
        private FRM_AgregarPaciente frm_AgregarPaciente;
        private FRM_ModificarPaciente frm_ModificarPaciente;
       // private FRM_ConsultarPaciente frm_ConsultarPaciente;
        private FRM_AgregarMedicamento frm_AgregarMedicamento;
        private FRM_ModificarMedicamento frm_ModificarMedicamento;
      //  private FRM_ConsultarMedicamento frm_ConsultarMedicamento;
        

        public MenuController(FRM_Menu frm_Menu) {
            this.frm_Menu = frm_Menu;
            frm_AgregarPaciente = new FRM_AgregarPaciente();
            frm_ModificarPaciente = new FRM_ModificarPaciente();
           // frm_ConsultarPaciente = new FRM_ConsultarPaciente();
            frm_AgregarMedicamento = new FRM_AgregarMedicamento();
            frm_ModificarMedicamento = new FRM_ModificarMedicamento();
          //  frm_ConsultarMedicamento = new FRM_ConsultarMedicamento();
         
            AgregarEventos();
        }

        private void AgregarEventos() {

            frm_Menu.btnPacientes.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnAgregarPacientes.Click += new EventHandler(OpenAgregarPaciente);
            frm_AgregarPaciente.btnCancelar.Click += new EventHandler(CancelarAgregarPaciente);
            frm_AgregarPaciente.FormClosed += CerrarAgregarPaciente;

            frm_Menu.btnModificarPacientes.Click += new EventHandler(OpenModificarPaciente);
            frm_ModificarPaciente.btnCancelar.Click += new EventHandler(CancelarModificarPaciente);
            frm_ModificarPaciente.FormClosed += CerrarAgregarPaciente;

            frm_Menu.btnBuscarPacientes.Click += new EventHandler(OpenConsultarPaciente);
          //  frm_ConsultarPaciente.FormClosed += CerrarConsultarPaciente;


            frm_Menu.btnBuscarMedicamentos.Click += new EventHandler(OpenConsultarMedicamento);
          //  frm_ConsultarMedicamento.FormClosed += CerrarConsultarMedicamento;

            frm_Menu.btnMedicamentos.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnAgregarMedicamentos.Click += new EventHandler(OpenAgregarMedicamento);
            frm_AgregarMedicamento.FormClosed += CerrarAgregarMedicamento;
            frm_AgregarMedicamento.btnCancelar.Click += new EventHandler(CancelarAgregarMedicamento);

            frm_Menu.btnModificarMedicamentos.Click += new EventHandler(OpenModificarMedicamento);
            frm_ModificarMedicamento.btnCancelar.Click += new EventHandler(CancelarModificarMedicamento);
            frm_ModificarMedicamento.FormClosed += CerrarModificarMedicamento;

            frm_Menu.btnPrescripciones.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnMenu.Click += new EventHandler(CambiarSubMenu);
        }

        private void CambiarSubMenu(object sender, EventArgs e) {
            frm_Menu.CambiarTab(sender);
        }

        //AGREGAR PACIENTE
        private void OpenAgregarPaciente(object sender, EventArgs e)
        {
            frm_Menu.Hide();
            frm_AgregarPaciente.Show();
            frm_AgregarPaciente.EstadoInicial();
        }

        private void CerrarAgregarPaciente(object sender, EventArgs e)
        {
            frm_AgregarPaciente.Hide();
            frm_AgregarPaciente.EstadoInicial();
            frm_Menu.Show();


        }

        private void CancelarAgregarPaciente(object sender, EventArgs e)
        {
            frm_AgregarPaciente.Hide();
            frm_AgregarPaciente.EstadoInicial();
            frm_Menu.Show();

        }

        //MODIFICAR PACIENTE
        private void OpenModificarPaciente(object sender, EventArgs e)
        {
            frm_Menu.Hide();
            frm_ModificarPaciente.Show();
            frm_ModificarPaciente.EstadoInicial();
        }

        private void CerrarModificarPaciente(object sender, EventArgs e)
        {
            frm_ModificarPaciente.Hide();
            frm_ModificarPaciente.EstadoInicial();
            frm_Menu.Show();

        }

        private void CancelarModificarPaciente(object sender, EventArgs e)
        {
            frm_ModificarPaciente.Hide();
            frm_ModificarPaciente.EstadoInicial();
            frm_Menu.Show();

        }

        //CONSULTAR PACIENTE
        private void OpenConsultarPaciente(object sender, EventArgs e)
        {
            frm_Menu.Hide();
           // frm_ConsultarPaciente.Show();
            //frm_ConsultarPaciente.EstadoInicial();
        }

        private void CerrarConsultarPaciente(object sender, EventArgs e)
        {
           // frm_ConsultarPaciente.Hide();
           // frm_ConsultarPaciente.EstadoInicial();
            frm_Menu.Show();


        }

        //MODIFICAR MEDICAMENTO
        private void OpenModificarMedicamento(object sender, EventArgs e)
        {
            frm_Menu.Hide();
            frm_ModificarMedicamento.Show();
            frm_ModificarMedicamento.EstadoInicial();
        }

        private void CerrarModificarMedicamento(object sender, EventArgs e)
        {
            frm_ModificarMedicamento.Hide();
           frm_ModificarMedicamento.EstadoInicial();
            frm_Menu.Show();

        }

        private void CancelarModificarMedicamento(object sender, EventArgs e)
        {
            frm_ModificarMedicamento.Hide();
           frm_ModificarMedicamento.EstadoInicial();
            frm_Menu.Show();

        }

        //AGREGAR MEDICAMENTO

        private void OpenAgregarMedicamento(object sender, EventArgs e)
        {
            frm_Menu.Hide();
            frm_AgregarMedicamento.Show();
            frm_AgregarMedicamento.EstadoInicial();
        }

        private void CerrarAgregarMedicamento(object sender, EventArgs e)
        {
            frm_AgregarMedicamento.Hide();
            frm_AgregarMedicamento.EstadoInicial();
            frm_Menu.Show();

        }

        private void CancelarAgregarMedicamento(object sender, EventArgs e)
        {
            frm_AgregarMedicamento.Hide();
            frm_AgregarMedicamento.EstadoInicial();
            frm_Menu.Show();

        }
        //CONSULTAR MEDICAMENTO
        private void OpenConsultarMedicamento(object sender, EventArgs e)
        {
            frm_Menu.Hide();
          //  frm_ConsultarMedicamento.Show();
           // frm_ConsultarMedicamento.EstadoInicial();
        }

        private void CerrarConsultarMedicamento(object sender, EventArgs e)
        {
           // frm_ConsultarMedicamento.Hide();
          //  frm_ConsultarMedicamento.EstadoInicial();
            frm_Menu.Show();

        }



    }
}
