using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enfermeria.View.Login {
    public partial class Alerta : UserControl {
        public Alerta() {
            InitializeComponent();
        }

        private void lkCerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Visible = false;
        }
    }
}
