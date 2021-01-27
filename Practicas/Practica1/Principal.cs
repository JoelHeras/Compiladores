using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    class Principal
    {
        //Rutas de prueba
        //C:\Users\joel_\Documents\Compiladores 3CV5\Practicas\Practica 1\automata.af
        //C:\Users\joel_\Documents\Compiladores 3CV5\Practicas\Entrada.af
        static void Main(string[] args){
            Automata automata = new Automata();
            GestionadorAutomata gestAuto = new GestionadorAutomata();
            Console.WriteLine("Escribe la direccion del archivo ");
            String archivo = Console.ReadLine();
            String cadena;
            gestAuto.LeerArchivo(archivo);
            GestionadorCadenas Cadenas = new GestionadorCadenas();
            automata = gestAuto.ObtenerAutomata();
            if (automata.ObtenerSimbolos() == "")
                return ;
            Console.WriteLine("Automata cargado ");
            Cadenas.EstablecerAlfabeto(automata.ObtenerSimbolos());
            cadena = Cadenas.Generarcadena();
            Console.WriteLine("Cadena generada automaticamente: {0}",cadena);
            foreach(Estado estado in automata.ObtenerEstados()){
                Console.WriteLine("Estado: {0}",estado.ObtenerEtiqueta());
                foreach(char[] transicion in estado.ObtenerTransiciones()){
                    //Transicion 0 destino, 1 simbolo
                    Console.WriteLine("Simbolo:{0}, Destino:{1}",transicion[1],Convert.ToInt32(transicion[0]));
                }
            }
            Console.WriteLine("Definiendo el tipo del automata....");
            if (automata.EsAFN() == false && automata.EsAFD() == false)
            {
                Console.WriteLine("No es un automata");
                Console.ReadKey();
                return;
            }
            bool AFD = automata.EsAFD();
            bool AFN = automata.EsAFN();
            Console.WriteLine("AFN {0}",AFN);
            Console.WriteLine("AFD {0}",AFD);
            Console.WriteLine("Escribe una cadena");
            String cadena1 = Console.ReadLine();
            if (AFD == true) {
                AFD automataDeter = new AFD(automata);
                automataDeter.Acepta(cadena);
                automataDeter.Acepta(cadena1);
                gestAuto.Guardar_Automata(automataDeter);
            }
            else if (AFN  == true){
                AFN automataNDeter = new AFN(automata);
                //automataNDeter.Acepta(cadena);
                automataNDeter.Acepta(cadena1);
                gestAuto.Guardar_Automata(automataNDeter);
            }                      

            Console.ReadKey();            
            
        }

        
    }
}
