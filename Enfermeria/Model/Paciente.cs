using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model
{
   public class Paciente
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string fechaNacimiento { get; set; }
        public int edad { get; set; }
        public string sexo { get; set; }
        public string estado { get; set; }

        public Paciente(string cedula, string nombre, string apellidos, string fechaNacimiento, int edad, string sexo,string estado)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.fechaNacimiento = fechaNacimiento;
            this.edad = edad;
            this.sexo = sexo;
            this.estado = estado;
        }

        public Paciente(string cedula)
        {
            this.cedula = cedula;

        }
    }
}
