using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIPO
{
    public class Voluntario
    {
        public string nombre { set; get; }
        public string apellido { set; get; }
        public int edad { set; get; }
        public string Correo { set; get; }
        public string DNI { set; get; }
        public string telefono { set; get; }
        public Uri foto { set; get; }
        public string horario { set; get; }
        public string zona { set; get; }

        public Voluntario(string nombre, string apellido, int edad, string correo, string dNI, string telefono, Uri foto, string horario, string zona)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
            Correo = correo;
            DNI = dNI;
            this.telefono = telefono;
            this.foto = foto;
            this.horario = horario;
            this.zona = zona;
        }
    }
}
