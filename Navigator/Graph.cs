using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigator
{
    class Graph
    {
        private string[] CitysNames;
        private uint?[,] AdjacencyMatrix;
        public Graph(string[] CitysNames, uint?[,] AdjacencyMatrix) 
        {
            this.CitysNames = (string[])CitysNames.Clone();
            this.AdjacencyMatrix = (uint?[,])AdjacencyMatrix.Clone();
        }

        public void Print() 
        {
            Console.WriteLine("Матрица смежности:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t");
            for (int i = 0; i < CitysNames.Length; i++)
            {
                Console.Write(CitysNames[i]+"\t");
            }
            Console.WriteLine();

            for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                Console.Write(CitysNames[i]+"\t");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < AdjacencyMatrix.GetLength(1); j++)
                {
                    if (AdjacencyMatrix[i, j] == null) 
                        Console.Write("-");
                    
                    Console.Write(AdjacencyMatrix[i,j]+"\t");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }

    }
}
