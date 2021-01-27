using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica5
{
    class AlgoritmoLL1
    {
        GLC Gramatica=new GLC();

        public TablaM Crear_Tabla(GLC gramatica){
            Gramatica = gramatica;
            Gramatica.simbolo_t += "$";
            String primero="",siguiente="";
            int i;
            int columna, fila;//Marcadores para insertar elementos en la casilla correspondiente
            TablaM Tabla;
            Tabla = new TablaM();
            Tabla.Entradas=new String[Gramatica.simbolo_n.Length, Gramatica.simbolo_t.Length];
            Tabla.simbolo_n = Gramatica.simbolo_n;
            Tabla.simbolo_t = Gramatica.simbolo_t;            
            foreach (Produccion produccion in Gramatica.Producciones){
                if (produccion.Cuerpo != "E"){
                    primero = Calcular_Primero(produccion.Cuerpo, "" + produccion.Simbolo);
                    fila = Gramatica.simbolo_n.IndexOf(produccion.Simbolo);
                    for (i = 0; i < primero.Length; i++){//Agregar elemento a la tabla en cruce de elemento,simbolo
                        columna = Gramatica.simbolo_t.IndexOf(primero[i]);
                        Tabla.Agregar_Entrada(fila, columna, produccion.Num_prod);                        
                    }
                }
                else{                    
                    siguiente = Calcular_Siguiente(produccion.Simbolo,""+produccion.Simbolo);
                    fila = Gramatica.simbolo_n.IndexOf(produccion.Simbolo);
                    for (i = 0; i < siguiente.Length; i++){//Agregar elemento a la tabla en cruce de elemento,simbolo
                        columna = Gramatica.simbolo_t.IndexOf(siguiente[i]);
                        Tabla.Agregar_Entrada(fila,columna,produccion.Num_prod);                      
                    }
                }
                primero = "";
                siguiente = "";                
            }
            return Tabla;
        }

        String Calcular_Primero(string cuerpo,string llamadas){
            String alpha;
            String primero = "";
            String revision = "";
            alpha = cuerpo;
            int conteo_E=0;
            for (int i=0;i<alpha.Length;i++){
                if (alpha[i] >= 'a' && alpha[i] <= 'z') {
                    primero += alpha[i];
                    break;
                }
                else if (alpha[i] == 'E'){
                    conteo_E++;
                }
                else if (alpha[i] >= 'A' && alpha[i] <= 'Z' && alpha[i]!='E') {
                    if (llamadas.Contains(alpha[i]) == false){//Evitar el ciclamiento de la busqueda de primeros
                        llamadas += alpha[i];
                        foreach(Produccion produccion in Gramatica.Producciones) {//Leer todos las producciones para encontrar los primeros de alpha[i]=N_T
                            if (produccion.Simbolo == alpha[i]){
                                primero += Calcular_Primero(produccion.Cuerpo, llamadas);
                            }
                        }                        
                    }
                    if (primero.Contains('E')==false)
                        break;
                }
            }
            ///Rutina para eliminar elementos repetidos
            for (int r = 0; r < primero.Length; r++)
                if (revision.Contains(primero[r]) == false)
                    revision += primero[r];
            primero = revision;
            return primero;
        }

        String Calcular_Siguiente(char simbolo,String llamadas){
            String siguiente="";
            int subindice;
            String cuerpo;
            String revision="";
            if(simbolo==Gramatica.Simbolo_i)//Si es el inicial, agregar $
                siguiente += "$";
            foreach (Produccion produccion in Gramatica.Producciones){
                if (produccion.Cuerpo.Contains(simbolo) == true){//Buscar producciones con el simbolo a buscar                    
                    subindice = produccion.Cuerpo.IndexOf(simbolo);
                    if (subindice == (produccion.Cuerpo.Length - 1) && llamadas.Contains(produccion.Simbolo)==false){
                        llamadas += produccion.Simbolo;
                        siguiente += Calcular_Siguiente(produccion.Simbolo,llamadas);

                    }
                    else if(subindice<produccion.Cuerpo.Length){
                        cuerpo = produccion.Cuerpo.Substring(subindice+1);
                        siguiente += Calcular_Primero(cuerpo,""+produccion.Simbolo);
                    }

                }                
            }
            //Rutina para eliminar elementos repetidos
            for (int r = 0; r < siguiente.Length; r++)
                if (revision.Contains(siguiente[r]) == false)
                    revision += siguiente[r];
            siguiente = revision;
            return siguiente;
        }
    }
}
