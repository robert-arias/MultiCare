using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model {
    public class Usuario {

        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string nombreUsuario { get; set; }
        public string salt { get; set; }
        public byte[] contrasenia { get; set; }

        public Usuario(string nombre, string apellidos, string nombreUsuario, string salt, byte[] contrasenia) {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.nombreUsuario = nombreUsuario;
            this.contrasenia = contrasenia;
            this.salt = salt;
        }

    }
}
