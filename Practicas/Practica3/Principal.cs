using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    class Principal
    {
        static void Main(string[] args)
        {
            //Rutas de prueba
            //C:\Users\joel_\Documents\Compiladores 3CV5\Practicas\AFN.af
            //C:\Users\joel_\Documents\Compiladores 3CV5\Practicas\Entrada.af
            Automata automata = new Automata();
            GestionadorAutomata gestAuto = new GestionadorAutomata();
            Console.WriteLine("Escribe la direccion del archivo ");
            String archivo = Console.ReadLine();
            gestAuto.LeerArchivo(archivo);
            GestionadorCadenas Cadenas = new GestionadorCadenas();
            automata = gestAuto.getAutomata();
            if (automata.ObtenerSimbolos() == "")
                return;
            Console.WriteLine("Automata cargado ");
            if (automata.esAFN() == false && automata.esAFD() == false){
                Console.WriteLine("No es un automata");
                Console.ReadKey();
                return;
            }
            bool AFD = automata.esAFD();
            bool AFN = automata.esAFN();                    
            if (AFN == true){//llamar a convertir el AFN
                AFN automataNDeter = new AFN(automata);
                Console.WriteLine("Convirtiendo a AFD....");
                AFD nuevoAFD = automataNDeter.Subconjuntos();

                Console.WriteLine("Automata convertido");                
                String cadena;
                Console.WriteLine("Escribe una cadena para probarla en el nuevoAFD");
                cadena=Console.ReadLine();
                nuevoAFD.acepta(cadena);
            }
            Console.ReadKey();
        }       
    }
}
