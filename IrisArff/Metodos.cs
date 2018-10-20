using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisArff
{
    public class Metodos
    {
        public int Inicio(string[] array)
        {
            var posicao = 0;
            var inicio = 0;                     

            foreach(var linha in array)
            {
                if (linha.Equals("@DATA"))
                {
                    inicio = posicao + 1;                    
                }               
                posicao++;
            }
            return inicio;
        }
        public int Fim(string[] array)
        {
            var posicao = 0;
            var fim = 0;

            foreach (var linha in array)
            {           
                if (linha.Contains("%a"))
                {
                    fim = posicao - 2;
                }
                posicao++;
            }
            return fim;
        }
    }
}
