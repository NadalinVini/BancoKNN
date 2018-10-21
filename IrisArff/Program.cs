using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisArff
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lendo o documento Iris.txt
            //Caso de Erro vá em propriedades e mude para Copiar sempre (arquivo Iris.txt)
            string[] lines = System.IO.File.ReadAllLines("Iris.txt");

            //Utilizando metodo para buscar o inicio e o fim
            Metodos metodo = new Metodos();

            //Metodos que recuperam o inicio e o fim 
            var inicio = metodo.Inicio(lines);
            var fim = metodo.Fim(lines);

            //Variaveis do tipo array
            string[,] confusaoString = new string[3, 3];
            Double[,] valores = new Double[fim-inicio, 5];
            int[,] confusao = new int[3, 3];
                       
            //Indices que vão ser usados para descobrir a posição dos valores
            var validador = 0;
            var mais = 0;
    
            //Convertendo os valores das minhas linhas em tipo Double
            for (int i = inicio; i < fim; i++)
            {
                String[] separado = lines[i].ToString().Split(',');
                valores[validador, 0] = Double.Parse(separado[0]) / 10;
                valores[validador, 1] = Double.Parse(separado[1]) / 10;
                valores[validador, 2] = Double.Parse(separado[2]) / 10;
                valores[validador, 3] = Double.Parse(separado[3]) / 10;

                //Trocando o nome das petalas por valores
                if (separado[4].Equals("Iris-setosa"))
                    valores[validador, 4] = 1.0;
                if (separado[4].Equals("Iris-virginica"))
                    valores[validador, 4] = 3.0;
                if (separado[4].Equals("Iris-versicolor"))
                    valores[validador, 4] = 2.0;

                validador++;
            }

            //Variaveis Uteis
            var menor = 0.0;
            validador = 0;
            var distancia = 0.0;
            var tipo = 0.0;            
            int acertos = 0;
            int erros = 0;            

            //Primeiro for representa o P da operação, muda apenas quando o Q completa todos os elementos
            for (int i = inicio; i < fim; i++)
            {
                menor = 100.0;
                mais = 0;

                //Elemento Q que percorre todas as linhas
                for (int j = inicio; j < fim; j++)
                {                 

                    if (i != j)
                    {
                        //Calculando a distancia
                        distancia = Math.Sqrt(
                        Math.Pow((valores[validador, 0] - valores[mais, 0]), 2)
                        + Math.Pow((valores[validador, 1] - valores[mais, 1]), 2)
                        + Math.Pow((valores[validador, 2] - valores[mais, 2]), 2)
                        + Math.Pow((valores[validador, 3] - valores[mais, 3]), 2));

                        //Pega o menor valor de cada P por Q
                        if (distancia <= menor)
                        {
                            tipo = valores[mais, 4];
                            menor = distancia;
                        }

                    }
                    mais++;
                }

                //Caso P seja igual o menor valor de Q, +1 acerto
                if (tipo.Equals(valores[validador, 4]))
                {
                    //Somando acertos
                    acertos = acertos + 1;

                    //Separando os valores para criar a matriz de confusão
                    if (tipo.Equals(1.0))
                    {
                        confusao[0, 0] = confusao[0, 0] + 1;
                    }

                    if (tipo.Equals(2.0))
                    {
                        confusao[1, 1] = confusao[1, 1] + 1;
                    }

                    if (tipo.Equals(3.0))
                    {
                        confusao[2, 2] = confusao[2, 2] + 1;
                    }
                }

                //Caso o tipo do P não seja igual o menor valor de Q
                else
                {
                    
                    erros = erros + 1;

                    //Separando os valores para criar a matriz de confusão
                    //e descobrindo a qual valor ele encontrou ao inves do
                    //certo.

                    if (tipo.Equals(1.0) && valores[validador, 4].Equals(2.0))
                    {
                        confusao[0, 1] = confusao[0, 1] + 1;
                    }
                    if (tipo.Equals(1.0) && valores[validador, 4].Equals(3.0))
                    {
                        confusao[0, 2] = confusao[0, 2] + 1;
                    }
                    if (tipo.Equals(2.0) && valores[validador, 4].Equals(1.0))
                    {
                        confusao[1, 0] = confusao[1, 0] + 1;
                    }
                    if (tipo.Equals(2.0) && valores[validador, 4].Equals(3.0))
                    {
                        confusao[1, 2] = confusao[1, 2] + 1;
                    }
                    if (tipo.Equals(3.0) && valores[validador, 4].Equals(1.0))
                    {
                        confusao[2, 0] = confusao[2, 0] + 1;
                    }
                    if (tipo.Equals(3.0) && valores[validador, 4].Equals(2.0))
                    {
                        confusao[2, 1] = confusao[2, 1] + 1;
                    }
                }

                validador++;
            }

            //Convertendo os acertos em double para calcular a porcentagem
            Double taxaAcerto = Convert.ToDouble(acertos);
            Double taxaErro = Convert.ToDouble(erros);

            //Calculando a porcentagem
            taxaAcerto = (taxaAcerto / (fim -inicio)) * 100;
            taxaErro = (taxaErro / (fim - inicio)) * 100;

            //Trazendo eles para inteiro novamente;
            acertos = Convert.ToInt32(taxaAcerto);
            erros = Convert.ToInt32(taxaErro);

            //Imprimindo Matriz de confusão
            Console.WriteLine("         Matriz de Confusao        ");
            Console.WriteLine("");
            Console.WriteLine("a      b       c    <-- Classificação");
            Console.WriteLine("");
            Console.WriteLine(confusao[0, 0] + "     " + confusao[0, 1] + "       " + confusao[0, 2] + "    a = Iris-setosa");
            Console.WriteLine("");
            Console.WriteLine(confusao[1, 0] + "      " + confusao[1, 1] + "      " + confusao[1, 2] + "    b = Iris-versicolor");
            Console.WriteLine("");
            Console.WriteLine(confusao[2, 0] + "      " + confusao[2, 1] + "      " + confusao[2, 2] + "    c = Iris-virginica");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Taxa de acerto = " + acertos + "%");
            Console.WriteLine("Taxa de erro = " + erros + "%");
            Console.WriteLine("");

            //Adicionando uma nova iris
            Console.WriteLine("Informe a nova iris no formato 0.0,0.0,0.0,0.0");
            //Lendo a iris
            string linhanova = Console.ReadLine();
            //Pulando linha
            Console.WriteLine("");

            var iris = metodo.NovaIris(valores, linhanova);

            Console.WriteLine("Sua iris é: " + iris);

            Console.ReadKey();
        }
    }

}
