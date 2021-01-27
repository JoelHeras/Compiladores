using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica5
{
    class TablaM
    {
        public String[,] Entradas;//
        public String simbolo_t;//Para guardar simbolos terminales
        public String simbolo_n;//Para guardar simbolos no terminales

        public void Agregar_Entrada(int fila,int columna,int num_prod){
            Entradas[fila, columna] += "(" +num_prod + ")";
        }

        TablaM Obtener_Tabla(){
            return this;
        }
    }
}
