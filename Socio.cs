using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIPO
{
    public class Socio
    {
        public string nombre { set; get; }
        public string apellido { set; get; }
        public int edad { set; get; }
        public string Correo { set; get; }
        public string DNI { set; get; }
        public string telefono { set; get; }
        public Uri foto { set; get; }
        public string cuenta { set; get; }
        public double cuantia { set; get; }

        public Socio(string nombre, string apellido, int edad, string correo, string dNI, string telefono, Uri foto, string cuenta, double cuantia)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
            Correo = correo;
            DNI = dNI;
            this.telefono = telefono;
            this.foto = foto;
            this.cuenta = cuenta;
            this.cuantia = cuantia;
        }
    }
}
