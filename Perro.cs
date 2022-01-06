using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIPO
{
    public class Perro
    {
        public string nombre { set; get; }
        public Uri foto { set; get; }
        public string genero{ set; get; }
        public string raza { set; get; }
        public int tamano { set; get; }
        public double peso{ set; get; }
        public int edad { set; get; }
        public string datosInteresantes { set; get; }
        public bool esterilizado { set; get; }
        public bool vacunado { set; get; }
        public bool PPP { set; get; }
        public bool Padrino { set; get; }
        public string nombrePadrino { set; get; }
        public double aportacion { set; get; }
        public string telefono { set; get; }

        public Perro(string nombre, Uri fotoPerro, string genero, string raza, int tamano, double peso, int edad, string datosInteresantes, bool esterilizado, bool vacunado, bool pPP, bool padrino, string nombrePadrino, double aportacion, string telefono)
        {
            this.nombre = nombre;
            foto = fotoPerro;
            this.genero = genero;
            this.raza = raza;
            this.tamano = tamano;
            this.peso = peso;
            this.edad = edad;
            this.datosInteresantes = datosInteresantes;
            this.esterilizado = esterilizado;
            this.vacunado = vacunado;
            PPP = pPP;
            Padrino = padrino;
            if (padrino == true){
                this.nombrePadrino = nombrePadrino;
                this.aportacion = aportacion;
                this.telefono = telefono;
            }
            else
            {
                this.nombrePadrino = "";
                this.aportacion = 0.0;
                this.telefono = "";
            }
        }

    }
}
