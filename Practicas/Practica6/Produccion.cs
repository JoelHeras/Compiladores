using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica5
{
    class Produccion{
        public char Simbolo;
        public String Cuerpo;
        public int Num_prod;

        void modificar_produccion(char simbolo,String cuerpo,int num_prod){
            Num_prod = num_prod;
            Simbolo = simbolo;
            Cuerpo = cuerpo;
        }
    }
}
