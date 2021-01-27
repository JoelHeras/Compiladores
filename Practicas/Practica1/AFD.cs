using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    public class AFD : Automata{

        public AFD(Automata datos) {
            this.Simbolos = datos.ObtenerSimbolos();
            this.EdoFinales = datos.ObtenerFinales();
            this.NumEstados = EdoFinales.Count;
            this.Estados = datos.ObtenerEstados();
            this.EdoInicial = datos.ObtenerEdoIncial();
        }

        public bool Acepta(String cadena) {
            int i = 0;
            int n;
            int EdoActual = EdoInicial;
            Console.WriteLine("Estado inicial {0}", EdoActual);
            while (i < cadena.Length) {
                if (Simbolos.Contains(cadena[i]) == false)
                    break;
                foreach (Estado estado in Estados) {
                    n = i;
                    if (estado.Etiqueta == EdoActual) {
                        foreach (char[] transicion in estado.ObtenerTransiciones()) {
                            if (transicion[1] == cadena[i]) {
                                n = i;
                                i++;
                                EdoActual = Convert.ToInt32(transicion[0]);
                                Console.WriteLine("Estado actual {0}", EdoActual);
                                break;
                            }                            
                        }
                        //Console.WriteLine("Sali del foreach de transiciones " + i);
                    }
                    if (i == cadena.Length)
                        break;

                    //Console.WriteLine("No encontre al estado V:");
                }
                //Console.WriteLine("Sali del foreach de estados " + i);
            }
            if (i == cadena.Length && EdoFinales.Contains(EdoActual) == true) {
                Console.WriteLine("Cadena aceptada");
                return true;
            }
            else if (i==cadena.Length && EdoFinales.Contains(EdoActual) == false) {
                Console.WriteLine("Cadena rechazada, no se llego a un estado final");
                return false;
            }
            else {
                Console.WriteLine("Cadena rechazada, simbolo desconocido");
                return false;
            }
        }
        
    }
}
