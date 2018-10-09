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
            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\opet\Desktop\iris.arff");
            int contador = 71;
            int result = 0;
            string[,] array = new string[150, 5];

            foreach (var line in lines)
            {
                contador = (contador + 1);
                string linhas = lines[contador];

                var words = linhas.Split(',');                

                foreach (var item in words)
                {
                    var um = words[0];
                    var dois = words[1];
                    var tres = words[2];
                    var quatro = words[3];
                    var cinco = words[4];

                    #region Converter
                    //var unico = Convert.ToDouble(um);
                    //   unico = unico / 10;
                    //var duplo = Convert.ToDouble(dois);
                    //   duplo = duplo / 10;
                    //var triplo = Convert.ToDouble(tres);
                    //   triplo = triplo / 10;
                    //var quarto = Convert.ToDouble(quatro);
                    //   quarto = quarto / 10;
                    #endregion

                    if(result <= 149)
                    {
                        array[result, 0] = um;
                        array[result, 1] = dois;
                        array[result, 2] = tres;
                        array[result, 3] = quatro;
                        array[result, 4] = cinco;                        

                        Console.WriteLine(array[result, 1]);

                        result = (result + 1);
                    }
                                        
                    //Console.WriteLine(item);
                }
                //Console.WriteLine(words);

                if (contador == 221)
                {
                    break;
                }
            }                                 
            System.Console.ReadKey();
        }
    }
}
