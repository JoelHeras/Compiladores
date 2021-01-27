using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
namespace Practica1
{
    public class GestionadorCadenas
    {
        String Alfabeto;

        public GestionadorCadenas(){
            Alfabeto = "";
        }

        public void EstablecerAlfabeto(String alfabeto){
            this.Alfabeto = alfabeto;
        }

        public String ObtenerAlfabeto(){
            return Alfabeto;
        }

        public String Generarcadena(){
            //randum en java
            var rnd = new Random();
            int tam = 8;
            String cadena = "";
            while (tam > 0)
            {
                cadena += Alfabeto[rnd.Next(Alfabeto.Length)];
                tam--;
            }
            return cadena;
        }

    }
}
