using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica5
{
    class CargadorDeGLC
    {
        GLC Gramatica;

        public GLC Leer_archivo(String ubicacion){
            Gramatica = new GLC();
            StreamReader datos;            
            if (File.Exists(ubicacion) == false) {
                Console.WriteLine("El archivo no existe");
                Gramatica.Simbolo_i = 'ñ';
                return Gramatica;
            }
            else if (ubicacion.Contains(".glc")==false){
                Console.WriteLine("El archivo no es del formato correcto");
                Gramatica.Simbolo_i ='ñ' ;
                return Gramatica;
            }
            else {               
                datos = new StreamReader(ubicacion);                
                Cargar_gramatica(datos);
                datos.Close();
                return Gramatica;
            }
        }

        GLC Cargar_gramatica(StreamReader archivo){
            String linea;
            Char cabecera;
            String cuerpo="";
            int prod_act=0;            
            Produccion prod;
            linea = archivo.ReadLine();
            while(linea != null){
                prod = new Produccion();
                cabecera =linea[0];                
                if (prod_act==0){//Agregar el inicial si es la primer producción
                    Gramatica.Simbolo_i = cabecera;
                }
                for(int i = 1; i < linea.Length; i++){
                    cuerpo += linea[i];                    
                    if (linea[i] >= 'A' && linea[i]<= 'Z' && linea[i]!='E'){
                        if(Gramatica.simbolo_n.Contains(linea[i]) == false)
                            Gramatica.simbolo_n += linea[i];
                    }
                    else if(linea[i]>='a' && linea[i]<='z' && linea[i] != 'E'){
                        if (Gramatica.simbolo_t.Contains(linea[i]) == false)
                            Gramatica.simbolo_t += linea[i];
                    }
                } 
                if(Gramatica.simbolo_n.Contains(cabecera)==false)
                    Gramatica.simbolo_n += cabecera;
                prod.Num_prod = prod_act;
                prod.Simbolo = cabecera;
                prod.Cuerpo = cuerpo;
                Gramatica.Producciones.Add(prod);
                cuerpo = "";
                linea = archivo.ReadLine();
                prod_act++;
            }
            return Gramatica;
        }
    }
}
