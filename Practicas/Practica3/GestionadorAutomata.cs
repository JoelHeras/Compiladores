using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica_3
{
    class GestionadorAutomata
    {
        Automata automata = new Automata();

        public GestionadorAutomata() { }

        public Automata getAutomata(){
            return automata;
        }

        public void LeerArchivo(String direccion){
            StreamReader datos;
            String linea;
            String[] estado_aux = new string[3];
            List<String> faltantes = new List<String>();
            if (File.Exists(direccion) == false)            
                Console.WriteLine("El archivo no existe");            
            else if (direccion.Contains(".af") == false)            
                Console.WriteLine("El archivo no es del formato correcto");            
            else{                
                datos = new StreamReader(direccion);
                //Leer automata
                linea = datos.ReadLine();                
                EstablecerInicial(linea);
                linea = datos.ReadLine();               
                EstablecerFinales(linea);
                linea = datos.ReadLine();
                while (linea != null){
                    linea = linea.Trim('>');                    
                    EstablecerTransicion(linea);
                    linea = datos.ReadLine();
                }
                datos.Close();
                foreach(Estado estado in automata.ObtenerEstados()) {//Realizar una busqueda de estados faltantes
                    /*Si un automata tiene un estado sin ninguna transicion, se crea el estado sin trancisiones*/
                    foreach (char[] transicion in estado.ObtenerTransiciones()) {
                        if (automata.ValidarEstado(Convert.ToInt32(transicion[0])) == false) {
                            faltantes.Add(""+Convert.ToInt32(transicion[0]));
                        }
                    }
                }
                foreach(String estado in faltantes) {
                    estado_aux[0] = estado;
                    estado_aux[1] = "";
                    estado_aux[2] = "";
                    automata.AgregarEstado(estado_aux);
                }
            }
        }

        public void EstablecerInicial(String line){
            if (line.Contains("inicial:")){
                int a = int.Parse(line.Trim('I', 'i', 'n', 'c', 'a', 'l', ':'));                
                automata.EstablecerEdoInical(a);
            }
        }

        public void EstablecerFinales(String line){
            if (line.Contains("finales:")){
                String[] a;
                String b = line.Trim('f', 'i', 'n', 'a', 'l', 'e', 's', ':');
                a = b.Split(',');                
                for (int i = 0; i < a.Length; i++){                    
                    automata.EstablecerFinal(int.Parse(a[i]));
                }
            }
        }

        public void EstablecerTransicion(String line){
            String[] transicion = new String[3];            
            if (line.Contains("->")){                                
                transicion = line.Split('-', ',');
                transicion[1] = transicion[1].Remove(0, 1);                
                automata.AgregarTransicion(transicion);
            }
        }

        public void Guardar_Automata(AFN Automata){
            String finales = "";
            String Direccion;
            String Nombre_archivo;
            int etiqueta;
            Console.WriteLine("Direccion donde se guardara el automata:");
            Direccion = Console.ReadLine();
            Console.WriteLine("Nombre del archivo (Dejar en vacio si es generico):");
            Nombre_archivo = Console.ReadLine();
            if (Nombre_archivo == "")
                Nombre_archivo = "AFN.af";
            if (!Nombre_archivo.Contains(".af"))
                Nombre_archivo += ".af";
            foreach (int final in automata.ObtenerFinales())
                finales += final + ",";
            finales = finales.TrimEnd(',');
            StreamWriter archivo = File.CreateText(Direccion + Nombre_archivo);
            archivo.WriteLine("Inicial:" + automata.ObtenerEdoIncial());
            archivo.WriteLine("Finales:" + finales);
            foreach (Estado estado in Automata.ObtenerEstados()){
                etiqueta = estado.ObtenerEtiqueta();
                foreach (char[] trancision in estado.ObtenerTransiciones())
                    archivo.WriteLine("{0}->{1},{2}", etiqueta, Convert.ToInt32(trancision[0]), trancision[1]);
            }
            archivo.Close();
            Console.WriteLine("***************Guardado***************");
        }

        public void Guardar_Automata(AFD Automata){
            String finales = "";
            String Direccion;
            String Nombre_archivo;
            int etiqueta;
            Console.WriteLine("Direccion donde se guardara el automata:");
            Direccion = Console.ReadLine();
            Console.WriteLine("Nombre del archivo (Dejar en vacio si es generico):");
            Nombre_archivo = Console.ReadLine();
            if (Nombre_archivo == "")
                Nombre_archivo = "AFD.af";
            if (!Nombre_archivo.Contains(".af"))
                Nombre_archivo += ".af";
            foreach (int final in Automata.ObtenerFinales())
                finales += final + ",";
            finales = finales.TrimEnd(',');
            StreamWriter archivo = File.CreateText(Direccion + Nombre_archivo);
            archivo.WriteLine("Inicial:" + Automata.ObtenerEdoIncial());
            archivo.WriteLine("Finales:" + finales);
            foreach (Estado estado in Automata.ObtenerEstados()){
                etiqueta = estado.ObtenerEtiqueta();
                foreach (char[] trancision in estado.ObtenerTransiciones())
                    archivo.WriteLine("{0}->{1},{2}", etiqueta, Convert.ToInt32(trancision[0]), trancision[1]);
            }
            archivo.Close();
            Console.WriteLine("***************Guardado***************");
        }
    }
}
