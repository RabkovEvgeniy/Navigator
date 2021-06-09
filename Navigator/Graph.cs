using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigator
{
    
    class Graph
    {
        private struct Mark
        {
            public int PreviousCityIndex { get; set; }
            public int RouteLenth { get; set; }
        }

        private string[] CitysNames;
        private int[,] AdjacencyMatrix;
        public Graph(string[] CitysNames, int[,] AdjacencyMatrix) 
        {
            this.CitysNames = (string[])CitysNames.Clone();
            this.AdjacencyMatrix = (int[,])AdjacencyMatrix.Clone();
        }

        public int AlgorithmDijkstra(out List<string> route, string firstCity,string secondCity) 
        {
            route = new List<string>();
            
            //Находим индексы начальной и конечной вершины и возвращаем -1 если их нет.
            int firstCityIndex = Array.IndexOf(CitysNames, firstCity);
            int lastCityIndex = Array.IndexOf(CitysNames, secondCity);
            if (firstCityIndex == -1 || lastCityIndex == -1)
                return -1;

            //Создаем и инициализируем массив флагов посещения и марок.
            bool[] isVisited = new bool[CitysNames.Length];
            Mark[] marks = new Mark[CitysNames.Length];
            for (int i = 0; i < CitysNames.Length; i++)
            {
                isVisited[i] = false;
                marks[i].PreviousCityIndex = -1;
                marks[i].RouteLenth = (i == firstCityIndex) ? 0 : int.MaxValue;
            }

            int actualVertexIndex;
            for (int i = 0; i < CitysNames.Length; i++)
            {
                //Находим непройденную вершину с минимальной длиной маршрута.
                actualVertexIndex = -1;
                for (int j = 0; j < CitysNames.Length; j++)
                {
                    if (isVisited[j])
                        continue;
                    if (actualVertexIndex == -1)
                    {
                        actualVertexIndex = j;
                        continue;
                    }
                    if (marks[actualVertexIndex].RouteLenth > marks[j].RouteLenth)
                        actualVertexIndex = j;
                }

                //Если минимальная вершина недостежима ни из одной пройденной вершины,
                //значит граф не связный, проверяем найден ли путь до конечной вершины,
                //если да выводим, иначе конечная вершина недостижима.
                if (marks[actualVertexIndex].RouteLenth == int.MaxValue)
                {
                    if (marks[lastCityIndex].PreviousCityIndex == -1)
                        return -1;
                    else break;
                }

                //Отмечаем вершину как пройденную
                isVisited[actualVertexIndex] = true;

                //Для каждой вершины из множества смежности,если она достижима и не пройдена,
                //пытаемся сократить путь, если получается, обновляем марку.
                int distance;
                for (int j = 0; j < CitysNames.Length; j++)
                {
                    if (AdjacencyMatrix[actualVertexIndex, j] == -1)
                        continue;
                    if (isVisited[j])
                        continue;
                    distance = AdjacencyMatrix[actualVertexIndex, j] + marks[actualVertexIndex].RouteLenth;
                    if (marks[j].RouteLenth>distance)
                    {
                        marks[j].RouteLenth = distance;
                        marks[j].PreviousCityIndex = actualVertexIndex;
                    }
                }
            }

            int tempIndex = lastCityIndex;
            while (tempIndex != -1) 
            {
                route.Add(CitysNames[tempIndex]);
                tempIndex = marks[tempIndex].PreviousCityIndex;
            }
            route.Reverse();

            return marks[lastCityIndex].RouteLenth;
        }

        /// <summary>
        /// Выводит информацию о графе в виде матрицы смежности.
        /// </summary>
        public void Print() 
        {
            Console.WriteLine("Матрица смежности:");
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
                    if (AdjacencyMatrix[i, j] == int.MaxValue) 
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
