using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    public class AFD : Automata{     

        public AFD(Automata datos) {
            this.Simbolos = datos.ObtenerSimbolos();
            this.EdoFinales = datos.ObtenerFinales();
            this.NumEstados = EdoFinales.Count;
            this.Estados = datos.ObtenerEstados();
            this.EdoInicial = datos.ObtenerEdoIncial();
        }

        public bool acepta(String cadena) {
            int i = 0;            
            int EdoActual = EdoInicial;            
            while (i < cadena.Length) {
                if (Simbolos.Contains(cadena[i]) == false)
                    break;
                foreach (Estado estado in Estados) {                    
                    if (estado.Etiqueta == EdoActual) {
                        foreach (char[] transicion in estado.ObtenerTransiciones()) {
                            if (transicion[1] == cadena[i]) {                            
                                i++;
                                EdoActual = Convert.ToInt32(transicion[0]);                                
                                break;
                            }                            
                        }                        
                    }
                    if (i == cadena.Length)
                        break;                    
                }                
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
