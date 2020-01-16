using Enfermeria.Controller.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enfermeria.View.Menu {
    public partial class FRM_Menu : Form {

        private MenuController controlador;

        public FRM_Menu() {
            InitializeComponent();
            controlador = new MenuController(this);
        }

        public void CambiarTab(object sender) {
            if (sender == btnPacientes)
                subMenu.SelectedTab = tbPacientes;
            if (sender == btnMedicamentos)
                subMenu.SelectedTab = tbMedicamentos;
            if (sender == btnPrescripciones)
                subMenu.SelectedTab = tbPrescripciones;
            if (sender == btnMenu)
                subMenu.SelectedTab = tbMenu;
        }

      
    }
}
