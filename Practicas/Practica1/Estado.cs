using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    public class Estado
    {
        bool Final;
        public int Etiqueta;
        List<char[]> Transiciones=new List<char[]>();

        public Estado(){
        }

        public void InicializarEstado(int etiqueta, Boolean fh){
            Etiqueta = etiqueta;
            Final = fh;
        }

        public void EstablecerTransiciones(List<char[]> trancisiones){
            Transiciones = trancisiones;
        }

        public void AgregarTransicion(char simbolo, int destino){
            char[] a = new char[2];
            a[0] = Convert.ToChar(destino);
            a[1] = simbolo;
            Transiciones.Add(a);
        }

        public List<char[]> ObtenerTransiciones(){
            return Transiciones;
        }

        public char[] ObtenerTransicion(int i){
            return Transiciones[i];
        }

        public int ObtenerEtiqueta(){
            return Etiqueta;
        }

    }
}
