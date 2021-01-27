using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica5
{
    class Principal
    {
        static void Main(string[] args)
        {
            GLC Gramatica= new GLC();
            CargadorDeGLC cargar=new CargadorDeGLC();
            AlgoritmoLL1 algoritmo = new AlgoritmoLL1();
            TablaM Tabla = new TablaM();
            String Direccion;
            Console.WriteLine("Introduce la dirección del archivo.glc");
            //C:\Users\joel_\Documents\Compiladores 3CV5\Practicas\Practica 5\gramatica.glc
            Direccion = Console.ReadLine();
            Gramatica = cargar.Leer_archivo(Direccion);
            if (Gramatica.Simbolo_i =='ñ'){
                return ;
            }
            Console.WriteLine("Gramatica cargada\n Producciones:");
            foreach (Produccion produccion in Gramatica.Producciones){
                Console.WriteLine("Produccion: {0} {1}->{2}",produccion.Num_prod,produccion.Simbolo,produccion.Cuerpo);
            }
            Console.WriteLine("Simbolos no terminales: "+Gramatica.simbolo_n);
            Console.WriteLine("Simbolos terminales: " + Gramatica.simbolo_t);
            Tabla=algoritmo.Crear_Tabla(Gramatica);
            for (int i = 0; i < Gramatica.simbolo_t.Length; i++)
                Console.Write("\t{0}",Gramatica.simbolo_t[i]);
            Console.WriteLine();
            for (int i = 0; i < Gramatica.simbolo_n.Length; i++){
                Console.Write("{0}",i);
                for(int n = 0; n < Gramatica.simbolo_t.Length; n++)
                {
                    Console.Write("\t{0}",Tabla.Entradas[i,n]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        
        }
    }
}
