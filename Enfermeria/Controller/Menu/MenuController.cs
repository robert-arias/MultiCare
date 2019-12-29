using Enfermeria.View.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Controller.Menu {
    public class MenuController {

        private FRM_Menu frm_Menu;

        public MenuController(FRM_Menu frm_Menu) {
            this.frm_Menu = frm_Menu;
            AgregarEventos();
        }

        private void AgregarEventos() {
            frm_Menu.btnPacientes.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnMedicamentos.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnPrescripciones.Click += new EventHandler(CambiarSubMenu);
            frm_Menu.btnMenu.Click += new EventHandler(CambiarSubMenu);
        }

        private void CambiarSubMenu(object sender, EventArgs e) {
            frm_Menu.CambiarTab(sender);
        }
    }
}
