using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    public class AFN : Automata{
        
        public AFN(Automata datos){
            this.Simbolos = datos.ObtenerSimbolos();
            this.EdoFinales = datos.ObtenerFinales();
            this.NumEstados = EdoFinales.Count;
            this.Estados = datos.ObtenerEstados();
            this.EdoInicial = datos.ObtenerEdoIncial();
        }

        public bool Acepta(String cadena){
            int i = 0;            
            int EdoActual = EdoInicial;
            String ResultadoMover="";
            Console.WriteLine("Estado inicial {0}", EdoActual);
            while (i < cadena.Length){
                if (Simbolos.Contains(cadena[i]) == false)
                    break;
                foreach (Estado estado in Estados){                    
                    if (estado.Etiqueta == EdoActual){
                        if (estado.ObtenerTransiciones().Count == 0 )
                            return false;//Salir del ciclo si el estado no tiene mas movimientos posibles y la cadena no se ha terminado se leer                        
                        foreach (char[] transicion in estado.ObtenerTransiciones()){                            
                            if (transicion[1] == cadena[i]){                                
                                i++;
                                EdoActual = Convert.ToInt32(transicion[0]);
                                Console.WriteLine("Estado actual {0}", EdoActual);
                                break;
                            }
                            else if (transicion[1] == 'E'){
                                Console.WriteLine("Me movere con epsilon");
                                ResultadoMover = MoverE(Convert.ToInt32(transicion[0]),cadena[i]);
                                if (ResultadoMover != ""){                                    
                                    EdoActual = Convert.ToInt32(ResultadoMover);
                                    Console.WriteLine("Estado actual: {0}",EdoActual);
                                    i++;
                                    break;
                                }                                                                
                            }
                        }                       
                    }
                    if (i == cadena.Length)
                        break;
                }                
            }
            if (i == cadena.Length && EdoFinales.Contains(EdoActual) == true)
            {
                Console.WriteLine("Cadena aceptada");
                return true;
            }
            else if (i == cadena.Length && EdoFinales.Contains(EdoActual) == false)
            {
                Console.WriteLine("Cadena rechazada, no se llego a un estado final");
                return false;
            }
            else
            {
                Console.WriteLine("Cadena rechazada, simbolo desconocido");
                return false;
            }
        }

        String MoverE(int Estado_Actual,char simbolo){
            String resultado = "";
            Estado estado = ObtenerEstado(Estado_Actual);
            //Console.WriteLine("Moviendome con epsilon en {0}",Estado_Actual);
            foreach (char[] trancicion in estado.ObtenerTransiciones()) {
                if (trancicion[1] == simbolo)
                    return "" + Convert.ToInt32(trancicion[0]);
                else if (trancicion[1] == 'E') {
                    resultado = MoverE(Convert.ToInt32(trancicion[0]), simbolo);
                    if (resultado != "")
                        return resultado;
                }
            }
            return "";
        }

    }
}
