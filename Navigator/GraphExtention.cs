using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigator;

namespace GraphExtention
{
    static class GraphExtention
    {
        /// <summary>
        /// Выводит информацию о графе в виде матрицы смежности.
        /// </summary>
        public static void Print(this Graph graph)
        {
            Console.WriteLine("Матрица смежности:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t");
            for (int i = 0; i < graph.CitysNames.Length; i++)
            {
                Console.Write(graph.CitysNames[i] + "\t");
            }
            Console.WriteLine();

            for (int i = 0; i < graph.AdjacencyMatrix.GetLength(0); i++)
            {
                Console.Write(graph.CitysNames[i] + "\t");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < graph.AdjacencyMatrix.GetLength(1); j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == -1)
                        Console.Write("-\t");
                    else
                        Console.Write(graph.AdjacencyMatrix[i, j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
