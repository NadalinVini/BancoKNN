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

            Console.WriteLine("Digite o número de vizinhos mais proximos: ");
            string ler = Console.ReadLine();
            int k = Convert.ToInt32(ler);
            double[] empate1 = new double[k];
            double[] empate2 = new double[k];
            double[] empate3 = new double[k];

            //Utilizando metodo para buscar o inicio e o fim
            Metodos metodo = new Metodos();

            List<KNN> listaKNN = new List<KNN>();

            //Metodos que recuperam o inicio e o fim 
            var inicio = metodo.Inicio(lines);
            var fim = metodo.Fim(lines);

            //Variaveis do tipo array
            string[,] confusaoString = new string[3, 3];
            Double[,] valores = new Double[fim - inicio, 5];
            int[,] confusao = new int[3, 3];
            Double[] arrayDistancia = new double[fim - inicio];

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

            #region Variáveis
            //Variaveis Uteis
            //var menor = 0.0;
            validador = 0;
            var distancia = 0.0;
            var tipo = 0.0;
            int acertos = 0;
            int erros = 0;
            var um = 0;
            var dois = 0;
            var tres = 0;
            var p = 0;
            #endregion

            //Primeiro for representa o P da operação, muda apenas quando o Q completa todos os elementos
            for (int i = inicio; i < fim; i++)
            {
                //menor = 100.0;
                mais = 0;
                listaKNN.Clear();
                um = 0;
                dois = 0;
                tres = 0;
                p = 0;
                tipo = 0.0;   

                //Elemento Q que percorre todas as linhas
                for (int j = inicio; j < fim; j++)
                {
                    
                    if (i != j || validador == (fim-inicio-1))
                    {
                        //Calculando a distancia
                        distancia = Math.Sqrt(
                        Math.Pow((valores[validador, 0] - valores[mais, 0]), 2)
                        + Math.Pow((valores[validador, 1] - valores[mais, 1]), 2)
                        + Math.Pow((valores[validador, 2] - valores[mais, 2]), 2)
                        + Math.Pow((valores[validador, 3] - valores[mais, 3]), 2));

                        KNN knn = new KNN();
                        knn.Distancia = distancia;
                        knn.Categoria = valores[mais, 4];

                        listaKNN.Add(knn);                       

                        var order = (from l in listaKNN
                                     orderby l.Distancia ascending
                                     select l).ToList();                        

                       
                        if (mais == (fim - inicio) - 1)
                        {
                            foreach (var x in order)
                            {
                                
                                if(k == p)
                                {
                                    if(um > dois && um > tres)
                                    {
                                        tipo = 1.0;

                                    }
                                    else if(dois> um && dois> tres )
                                    {
                                        tipo = 2.0;
                                    }
                                    else if(tres > um && tres > dois)
                                    {
                                        tipo = 3.0;
                                    }

                                    #region Empate
                                    else if (dois > tres && dois == um)
                                    {
                                        if(empate2[0] > empate1[0])
                                        {
                                            tipo = 2.0;
                                        }
                                        else
                                        {
                                            tipo = 1.0;
                                        }
                                    }
                                    else if (dois > um && dois == tres)
                                    {
                                        if (empate2[0] > empate3[0])
                                        {
                                            tipo = 2.0;
                                        }
                                        else
                                        {
                                            tipo = 3.0;
                                        }
                                    }
                                    else if (tres > dois && tres == um)
                                    {
                                        if(empate3[0] > empate1[0])
                                        {
                                            tipo = 3.0;
                                        }
                                        else
                                        {
                                            tipo = 1.0;
                                        }
                                    }
                                    else if(tres > um && tres == dois )
                                    {
                                        if(empate3[0] > empate2[0])
                                        {
                                            tipo = 3.0;
                                        }
                                        else
                                        {
                                            tipo = 2.0;
                                        }
                                    }
                                    else if (um > tres && um == dois)
                                    {
                                        if (empate1[0] > empate2[0])
                                        {
                                            tipo = 1.0;
                                        }
                                        else
                                        {
                                            tipo = 2.0;
                                        }
                                    }
                                    else if (um > dois && um == tres)
                                    {
                                        if (empate1[0] > empate3[0])
                                        {
                                            tipo = 1.0;
                                        }
                                        else
                                        {
                                            tipo = 3.0;
                                        }
                                    }
                                    #endregion

                                    break;
                                }
                                if (x.Categoria.Equals(1))
                                {
                                    um = um + 1;
                                    empate1[p] = x.Distancia;
                                }
                                if (x.Categoria.Equals(2))
                                {
                                    dois = dois + 1;
                                    empate2[p] = x.Distancia;
                                }
                                if (x.Categoria.Equals(3))
                                {
                                    tres = tres + 1;
                                    empate3[p] = x.Distancia;
                                }
                                p++;
                            }                            
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
            taxaAcerto = (taxaAcerto / 149) * 100;
            taxaErro = (taxaErro / 149) * 100;

            //Trazendo eles para inteiro novamente;
            acertos = Convert.ToInt32(taxaAcerto);
            erros = 100 - acertos;

            //Imprimindo Matriz de confusão
            Console.WriteLine("");
            Console.WriteLine("1      2       3");
            Console.WriteLine("");
            Console.WriteLine(confusao[0, 0] + "     " + confusao[0, 1] + "       " + confusao[0, 2] );
            Console.WriteLine("");
            Console.WriteLine(confusao[1, 0] + "      " + confusao[1, 1] + "      " + confusao[1, 2] );
            Console.WriteLine("");
            Console.WriteLine(confusao[2, 0] + "      " + confusao[2, 1] + "      " + confusao[2, 2] );
            Console.WriteLine("");
            Console.WriteLine("Sendo 1 --> Iris-setosa");
            Console.WriteLine("      2 --> Iris-Versicolor");
            Console.WriteLine("      3 --> Iris-Virginica");
            Console.WriteLine("");
            Console.WriteLine("Taxa de acerto = " + acertos + "%");
            Console.WriteLine("Taxa de erro = " + erros + "%");
            Console.WriteLine("");

            //Adicionando uma nova iris
            Console.WriteLine("Informe a nova iris no formato 0.0,0.0,0.0,0.0       Obs: K = 1;");
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
