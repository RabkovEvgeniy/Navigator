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
            Console.ForegroundColor = ConsoleColor.Green;
            string[] CitysNames = GetCitysNames();
            uint?[,] AdjacencyMatrix = GetAdjacencyMatrix(CitysNames);
        }

        /// <summary>
        /// Считывает из консоли сначала количество городов, а затем их названия
        /// </summary>
        /// <returns>Массив строк с названиями городов</returns>
        static string[] GetCitysNames() 
        {
            byte CountOfCitys;

            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Введите количество городов(от 3 до 10): ");
                try
                {
                    Console.ResetColor();
                    CountOfCitys = byte.Parse(Console.ReadLine());
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

                if (CountOfCitys<3) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели слишком маленькое число, так не интересно:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    while (Console.ReadKey().Key!=ConsoleKey.Enter);
                    continue;
                }

                if (CountOfCitys >10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели слишком большое число, вам надоест вводить длинну дорог:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");
                    Console.ForegroundColor = ConsoleColor.Green;
                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                    continue;
                }
                break;
            }

            string[] Citys = new string[CountOfCitys];

            //TODO добавить проверку на уникальность названий. 
            for (byte i = 0; i < CountOfCitys; i++)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Введите название {0}-ого города:", i+1));
                Console.ResetColor();
                Citys[i] = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
            }

            return Citys;
        }

        /// <summary>
        /// Считывает из консоли длинну дорог от каждого города до каждого
        /// </summary>
        /// <param name="CitysNames">Имена городов</param>
        /// <returns>Возвращает матрицу смежности,где null - отсутвие дороги</returns>
        static uint?[,] GetAdjacencyMatrix(string[] CitysNames)
        {
            
            uint?[,] AdjacencyMatrix = new uint?[CitysNames.Length, CitysNames.Length];
            string input;
            int temp;
            for (int i = 0; i < CitysNames.Length; i++)
            {
                for (int j = i; j < CitysNames.Length; j++)
                {
                    Console.Clear();
                    if (i == j)
                    {
                        AdjacencyMatrix[j, i] = null;
                        continue;
                    }

                    Console.WriteLine("Можно вводить только целые положительные числа, если дороги нет введи \"-\"");
                    Console.WriteLine(string.Format("Введите длинну дороги из города {0} в город {1}:",CitysNames[i],CitysNames[j]));

                    Console.ResetColor();
                    input = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    if (input == "-")
                    {
                        AdjacencyMatrix[i, j] = null;
                        AdjacencyMatrix[j, i] = null;
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

                        AdjacencyMatrix[i, j] = (uint)temp;
                        AdjacencyMatrix[j, i] = (uint)temp;
                    }

                }
            }
            return AdjacencyMatrix;
        }
    }
}
