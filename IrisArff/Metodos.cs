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
        public string NovaIris(double[,] valores, string linhanova)
        {            
            var menor = 0.0;
            var distancia = 0.0;
            var tipo = 0.0;

            //Novo array
            Double[] novaIris = new Double[4];
            //Splitando valores
            String[] separador = linhanova.Split(',');

            //Repetindo o processo
            for (int a = 0; a < separador.Length; a++)
            {
                novaIris[a] = Double.Parse(separador[a]) / 10;
            }

            menor = 100.0;

            for (int b = 0; b < valores.Length; b++)
            {
                if (b == 150)
                {
                    break;
                }
                distancia = Math.Sqrt(
                    Math.Pow((valores[b, 0] - novaIris[0]), 2)
                    + Math.Pow((valores[b, 1] - novaIris[1]), 2)
                    + Math.Pow((valores[b, 2] - novaIris[2]), 2)
                    + Math.Pow((valores[b, 3] - novaIris[3]), 2)
                );

                if (distancia <= menor)
                {

                    menor = distancia;
                    tipo = valores[b, 4];
                }
            }

            String iris = "";

            if (tipo.Equals(1.0))
            {
                iris = "Iris-setosa";
            }
            if (tipo.Equals(2.0))
            {
                iris = "Iris-versicolor";
            }
            if (tipo.Equals(3.0))
            {
                iris = "Iris-virginica";
            }

            return iris;
        }
    }
}
