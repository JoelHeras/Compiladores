using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    public class GestionadorCadenas{
        String Alfabeto;

        public GestionadorCadenas(){
            Alfabeto = "";
        }

        public void setAlfabeto(String alfabeto){
            this.Alfabeto = alfabeto;
        }

        public String getAlfabeto(){
            return Alfabeto;
        }

        public String generarcadena(){            
            var rnd = new Random();
            int tam = 8;
            String cadena = "";
            while (tam > 0){
                cadena += Alfabeto[rnd.Next(Alfabeto.Length)];
                tam--;
            }
            return cadena;
        }

    }
}
