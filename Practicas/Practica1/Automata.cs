using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica1
{
    public class Automata
    {
        protected String Simbolos;
        protected List<int> EdoFinales=new List<int>();
        protected int EdoInicial;
        protected int NumEstados;//aux
        protected List<Estado> Estados=new List<Estado>();
        
        public Automata(){
            Simbolos = "";
            NumEstados = 0;
        }

        public void EstablecerEdoInical(int edoinicial){
            EdoInicial = edoinicial;
        }

        public List<int> ObtenerFinales(){
            return EdoFinales;
        }
        public void ObtenerFinal(int final){
            if (EdoFinales.Contains(final) == false)//Evitar agregar elementos repetidos
                EdoFinales.Add(final);
        }
        public List<Estado> ObtenerEstados(){
            return Estados;
        }
        public void EstablecerSimbolo(String a) {
            if (Simbolos.Contains(a) == false && a != "E")
                Simbolos += a;
        }
        public int ObtenerEdoIncial(){
            return EdoInicial;
        }
        public String ObtenerSimbolos(){
            return Simbolos;
        }
        public void AgregarEstado(String[] info) {
            NumEstados++;
            //0 etiqueta,1 destino, 2 simbolo
            Estado a = new Estado();            
            a.InicializarEstado(int.Parse(info[0]),false);
            //info[1]=info[1].Remove(0,1);
            //Console.WriteLine("{0} {1} {2}", info[0],info[1], info[2]);
            a.AgregarTransicion(Convert.ToChar(info[2]),int.Parse(info[1]) );
            Estados.Add(a);
            //System.out.println(Estados.size());
        }

        public bool ValidarEstado(int origen){            
            int i = 0;
            Estado a = new Estado();
            while (i < Estados.Count()){
                a = Estados[i];
                if (a.ObtenerEtiqueta() == origen){
                    return true;
                }
                i++;
            }
            return false;
        }

        public Estado ObtenerEstado(int etiq){            
            int i = 0;
            Estado a = new Estado();
            while (i < Estados.Count()){
                a = Estados[i];
                if (a.ObtenerEtiqueta() == etiq){
                    break;
                }
                i++;
            }
            return a;
        }

        public void AgregarTransicion(String[] info){
            this.EstablecerSimbolo(""+info[2]);
            info[0].Trim('-');
            Estado a = new Estado();            
            if ((ValidarEstado(int.Parse(info[0])))==true){                
                a = ObtenerEstado(int.Parse(info[0]));
                // 2 el simbolo, 1 destino
                a.AgregarTransicion(Convert.ToChar(info[2]), int.Parse(info[1]));
            }
            else{
                AgregarEstado(info);
            }
        }

        public bool EsAFN(){
            int i;
            i = Simbolos.Length;            
            int[] conteo = new int[i];
            i = 0;
            if (EdoInicial == 0 || EdoFinales.Count == 0)
                return false;
            foreach (Estado estado in Estados){
                if (estado.ObtenerTransiciones().Count != Simbolos.Length)//Si el estado no tiene misma cantidad de trancisiones y simbolos, es AFN
                    return true;
                foreach (char[] transicion in estado.ObtenerTransiciones()){
                    //Transicion 0 destino(int, debe convertirse), 1 simbolo(char)
                    if (transicion[1] == 'E')//Si se encuentra un Epsilon es AFN
                        return true;
                    i = Simbolos.IndexOf(transicion[1]);
                    conteo[i]++;
                    if (conteo[i] > 1)//Si el estado tiene mas de una transcicion con el mismo simbolo, es AFN
                        return true;
                }
                for (i = 0; i < Simbolos.Length; i++){//Reiniciar los contadores de transiciones para cada simbolo
                    if(conteo[i]==0)//Si algun simbolo no tiene trancisiones en el estado actual, es AFN
                        return true;
                    conteo[i] = 0;
                    }
            }
            return false;
        }

        public bool EsAFD(){
            int i;            
            i = Simbolos.Length;
            int[] conteo = new int[i];
            if (EdoInicial == 0 || Estados .Count == 0)//Si el automata esta mal definido o esta vacio, no es AFD
                return false;            
            foreach (Estado estado in Estados){
                if (estado.ObtenerTransiciones().Count != Simbolos.Length)//Si el estado no tiene misma cantidad de trancisiones y simbolos, no es AFD
                    return false;
                foreach (char[] transicion in estado.ObtenerTransiciones()){
                    //Transicion 0 destino(int, debe convertirse), 1 simbolo(char)
                    if (transicion[1] == 'E')
                        return false;//Si hay una trancision Epsilon, no es AFD
                    i = Simbolos.IndexOf(transicion[1]);
                    conteo[i]++;
                    if (conteo[i] > 1)//Si el estado tiene mas de una transcicion con el mismo simbolo, no es AFD
                        return false;
                }
                for (i = 0; i < Simbolos.Length; i++){//Reiniciar los contadores de transiciones para cada simbolo
                    if(conteo[i]==0)//Si algun simbolo no tiene transciciones en el estado actual, no es AFD
                        return false;
                    conteo[i] = 0;
                    }
            }
            return true;
        }

    }
}
