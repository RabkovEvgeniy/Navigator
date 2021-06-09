using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Navigator";
            string[] citysNames = GetCitysNames();
            int[,] adjacencyMatrix = GetAdjacencyMatrix(citysNames);
            
            Graph graph = new Graph(citysNames, adjacencyMatrix);
            graph.Print();
            Console.WriteLine();
            
            string firstCityName, lastCityName;
            List<string> route;

            Console.WriteLine("Введите название стартового города:");
            Console.ForegroundColor = ConsoleColor.Gray;
            firstCityName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Введите название конечного города:");
            Console.ForegroundColor = ConsoleColor.Gray;
            lastCityName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Длинна кратчайшего пути: " + graph.AlgorithmDijkstra(out route, firstCityName, lastCityName));

            foreach (var item in route)
            {
                Console.Write(" -> "+item);
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите ESC чтобы закрыть программу");
            Console.ForegroundColor = ConsoleColor.Green;
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        /// <summary>
        /// Считывает из консоли сначала количество городов, а затем их названия
        /// </summary>
        /// <returns>Массив строк с названиями городов</returns>
        static string[] GetCitysNames() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            byte countOfCitys;

            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Введите количество городов(от 3 до 10): ");
                try
                {
                    Console.ResetColor();
                    countOfCitys = byte.Parse(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Что-то пошло не так, нажмите enter и попробуйте заново");
                    Console.ForegroundColor = ConsoleColor.Green;
                    while (Console.ReadKey().Key!=ConsoleKey.Enter);
                    continue;
                }

                if (countOfCitys<3) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели слишком маленькое число, так не интересно:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    while (Console.ReadKey().Key!=ConsoleKey.Enter);
                    continue;
                }

                if (countOfCitys >10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели слишком большое число, вам надоест вводить длины дорог:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");
                    Console.ForegroundColor = ConsoleColor.Green;
                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                    continue;
                }
                break;
            }

            string[] citys = new string[countOfCitys];

            //TODO добавить проверку на уникальность названий. 
            for (byte i = 0; i < countOfCitys; i++)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Введите краткое название {0}-ого города (Не больше 4 символов):", i+1));
                Console.ResetColor();
                citys[i] = Console.ReadLine();
                if (citys[i].Length < 0 || citys[i].Length > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Название слишком длинное, при выводе матрица съедет(");
                    Console.WriteLine("Нажмите enter и попробуйте снова");
                    Console.ForegroundColor = ConsoleColor.Green;
                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                    i--;
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Green;
            }

            return citys;
        }

        /// <summary>
        /// Считывает из консоли длинну дорог от каждого города до каждого
        /// </summary>
        /// <param name="citysNames">Имена городов</param>
        /// <returns>Возвращает матрицу смежности,где -1 - отсутвие дороги</returns>
        static int[,] GetAdjacencyMatrix(string[] citysNames)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int[,] adjacencyMatrix = new int[citysNames.Length, citysNames.Length];
            string input;
            int temp;
            for (int i = 0; i < citysNames.Length; i++)
            {
                for (int j = i; j < citysNames.Length; j++)
                {
                    Console.Clear();
                    if (i == j)
                    {
                        adjacencyMatrix[j, i] = -1;
                        continue;
                    }

                    Console.WriteLine("Можно вводить только целые положительные числа, если дороги нет введите \"-\"");
                    Console.WriteLine(string.Format("Введите длинну дороги из города {0} в город {1}:",citysNames[i],citysNames[j]));

                    Console.ResetColor();
                    input = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    if (input == "-")
                    {
                        adjacencyMatrix[i, j] = -1;
                        adjacencyMatrix[j, i] = -1;
                    }
                    else 
                    {
                        try
                        {
                            temp = int.Parse(input);
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Что-то пошло не так, нажмите enter и попробуйте снова");
                            Console.ForegroundColor = ConsoleColor.Green;
                            while (Console.ReadKey().Key != ConsoleKey.Enter);
                            j--;
                            continue;
                        }

                        if(temp == 0) 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Длинна дороги не может быть равна нулю");
                            Console.WriteLine("Нажмите enter и попробуйте заново");
                            Console.ForegroundColor = ConsoleColor.Green;
                            while (Console.ReadKey().Key != ConsoleKey.Enter);
                            j--;
                            continue;
                        }

                        if (temp < 0) 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Длинна дороги не может быть отрицательной");
                            Console.WriteLine("Нажмите enter и попробуйте заново");
                            Console.ForegroundColor = ConsoleColor.Green;
                            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                            j--;
                            continue;
                        }

                        adjacencyMatrix[i, j] = temp;
                        adjacencyMatrix[j, i] = temp;
                    }

                }
            }
            return adjacencyMatrix;
        }
    }
}
