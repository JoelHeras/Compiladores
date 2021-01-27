using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
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
            String ResultadoMover = "";
            Console.WriteLine("Estado inicial {0}", EdoActual);
            while (i < cadena.Length){
                if (Simbolos.Contains(cadena[i]) == false)
                    break;
                foreach (Estado estado in Estados){
                    if (estado.Etiqueta == EdoActual){
                        if (estado.ObtenerTransiciones().Count == 0)
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
                                ResultadoMover = MoverE(Convert.ToInt32(transicion[0]), cadena[i]);
                                if (ResultadoMover != ""){
                                    EdoActual = Convert.ToInt32(ResultadoMover);
                                    Console.WriteLine("Estado actual: {0}", EdoActual);
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
            if (i == cadena.Length && EdoFinales.Contains(EdoActual) == true){
                Console.WriteLine("Cadena aceptada");
                return true;
            }
            else if (i == cadena.Length && EdoFinales.Contains(EdoActual) == false){
                Console.WriteLine("Cadena rechazada, no se llego a un estado final");
                return false;
            }
            else{
                Console.WriteLine("Cadena rechazada, simbolo desconocido");
                return false;
            }
        }

        String MoverE(int Estado_Actual, char simbolo){
            String resultado = "";
            Estado estado = ObtenerEstado(Estado_Actual);            
            foreach (char[] trancicion in estado.ObtenerTransiciones()){
                if (trancicion[1] == simbolo)
                    return "" + Convert.ToInt32(trancicion[0]);
                else if (trancicion[1] == 'E'){
                    resultado = MoverE(Convert.ToInt32(trancicion[0]), simbolo);
                    if (resultado != "")
                        return resultado;
                }
            }
            return "";
        }

        public AFD Subconjuntos(){//No olvidar quitar automata de parametro, solo es auxiliar hasta completarlo            
            AFD Resultado;
            Automata res = new Automata();
            String[] cerradura;
            String destado="";
            int i,d,s;//Contadores auxiliares
            bool final=false;
            List<String[]> resTransciones = new List<String[]>();//Transiciones del resultado
            String[] transicionaux = new String[3];//Arreglo auxiliar para agregar datos en la lista de transiciones del resultado
            List<String[]> T = new List<String[]>();//Lista de estados auxiliar para el codigo  
            List<String> Destados = new List<String> ();//Lista de estados en T sin separaciones
            String[] pila=new string[1] {""+EdoInicial };//Crear una pila para la primer e-cerradura con el estado inicial                 
            cerradura = Ecerradura(pila);//Realizar la e-cerradura al primer elemento y obtener el primer estado en T
            T.Add(cerradura);
            res.EstablecerEdoInical(T.Count);
            i = 0;//Para recorrer el T                                              
            while (i < T.Count){//Mientras el ultimo elemento de T no se haya marcado(Marcado: que se haya analizado todos los simbolos del automata)
                for (s=0;s<Simbolos.Length;s++){
                    /*Realizar el cerradura de mover*/
                    pila = Mover(T[i], Simbolos[s]);
                    cerradura = Ecerradura(pila);
                    for (d = 0; d < cerradura.Length; d++){// Introducir los datos de la cerradura en un String
                        destado = destado + cerradura[d];
                        if (EdoFinales.Contains(int.Parse(cerradura[d])))//Si la cerradura contiene al final, marcar el estado como final
                            final = true;
                    }
                    if (Destados.Contains(destado)==false){//Si el estado es nuevo, agregarlo
                        T.Add(cerradura);
                        Destados.Add(destado);
                    }                    
                    //Crear la transcicion
                    transicionaux[0] = ""+(i+1);//Origen
                    transicionaux[1] = ""+((Destados.IndexOf(destado)+2));//Destino
                    transicionaux[2] = ""+Simbolos[s];//Simbolo
                    resTransciones.Add(new String[] { transicionaux[0],transicionaux[1],transicionaux[2] });
                    if (final == true)
                        res.EstablecerFinal(i+2);
                    destado = "";//Limpiar el string para destado
                    final = false;//Resetear el marcador de final
                }
                i++;
            }            
            foreach (String[] transci in resTransciones) 
               res.AgregarTransicion(transci);
            
            for (i = 0; i < Simbolos.Length; i++)
                res.EstablecerSimbolo(""+Simbolos[i]);
            Resultado = new AFD(res);//Inicializar el nuevo AFD
            return Resultado;
        }

        String[] Mover(String[] pila,char simbolo) {
            String[] elementos;//Arreglo donde se guardan los estados que genera la pila de e-cerradura            
            String datos="";//Variable auxiliar para concatenar los elementos y despues separarlos
            Estado estado_Act;            
            int i=0;
            int elemento_super;            
            while (i < pila.Length){//Recorrer la pila de estados
                elemento_super = int.Parse(pila[i]);//Obtener el elemento al tope de la pila
                foreach (Estado estado in Estados){
                    if (estado.ObtenerEtiqueta() == elemento_super && estado.ObtenerTransiciones().Count > 0){
                        estado_Act = estado;//Obtener la informacion del estado al tope de la pila                                            
                        if (estado_Act.ObtenerTransiciones().Count > 0){
                            foreach (char[] transcion in estado_Act.ObtenerTransiciones()){
                                if (transcion[1] == simbolo){//Si el estado tiene una transcion con el simbolo, agregarla a los estados de pila                                 
                                    if (datos.Contains(Convert.ToInt32(transcion[0]) + ",") == false){//Si elemento no se ha agregado, agregarlo a pila y datos
                                        datos += Convert.ToInt32(transcion[0]) + ",";
                                    }
                                }
                            }
                        }
                        i++;
                    }
                    else if (estado.ObtenerEtiqueta() == elemento_super && estado.ObtenerTransiciones().Count == 0)
                        i++;
                }                
            }
            datos = datos.TrimEnd(','); ;//Eliminar la coma sobrante al final
            elementos = datos.Split(',');//Dividir los datos e introducirlos en el array            
            return elementos;
        }

        String[] Ecerradura(String[] pilaString){
            String[] elementos;//Arreglo donde se guardan los estados que genera la pila de e-cerradura            
            String datos ="";//Variable auxiliar para concatenar los elementos y despues separarlos
            Estado estado_Act;
            List<String> pila=new List<string>();            
            int i;
            int elemento_super;            
            for (i = 0; i < pilaString.Length; i++){//Agregar los elementos del String[] a la pila
                pila.Add(pilaString[i]);
                datos += pila[i] + ",";                
            }
            i = 0;
            while (i < pila.Count) {//Recorrer la pila de estados
                elemento_super = int.Parse(pila[i]);//Obtener el elemento al tope de la pila
                foreach (Estado estado in Estados){
                    if (estado.ObtenerEtiqueta() == elemento_super ){
                        estado_Act = estado;//Obtener la informacion del estado al tope de la pila                                            
                        if (estado_Act.ObtenerTransiciones().Count > 0) {
                            foreach (char[] transcion in estado_Act.ObtenerTransiciones()){                                
                                if (transcion[1] == 'E'){//Si el estado tiene una transcion E, agregarla a la pila de e-cerradura                                 
                                    if (datos.Contains(Convert.ToInt32(transcion[0]) + ",") == false){//Si elemento no se ha agregado, agregarlo a pila y datos
                                        datos += Convert.ToInt32(transcion[0]) + ",";
                                        pila.Add(Convert.ToInt32(transcion[0]) + "");
                                    }
                                }
                            }
                        }
                        i++;
                    }
                }
            }
            datos = datos.TrimEnd(','); ;//Eliminar la coma sobrante al final
            elementos = datos.Split(',');//Dividir los datos e introducirlos en el array            
            return elementos;
        }

    }
}
