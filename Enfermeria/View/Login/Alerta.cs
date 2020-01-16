using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Enfermeria.View.Login {
    public partial class Alerta : UserControl {
        public Alerta() {
            InitializeComponent();
        }

        public void BackToDefault() {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Alerta));
            pictureBox1.Image = ((Image)(resources.GetObject("pictureBox1.Image")));
            label1.Text = "Alerta";
            label2.Text = "no hay conexión a internet";
        }

        public void CambiarImagenWarning() {
            string fileName = "assets\\error.png";
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, fileName);
            pictureBox1.Image = Image.FromFile(path);
        }

        public void CambiarMensaje(string mensaje) {
            label2.Text = mensaje;
        }

        public void CambiarTitulo(string mensaje) {
            label1.Text = mensaje;
        }

        private void lkCerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Visible = false;
        }
    }
}
