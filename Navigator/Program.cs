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
            string[] Citys = GetCitysNames();
            
        }

        static string[] GetCitysNames() 
        {
            byte CountOfCity;

            while (true) 
            {
                Console.WriteLine("Введите количество городов(от 3 до 10): ");
                byte.TryParse(Console.ReadLine(), out CountOfCity);
                
                if (CountOfCity<3) 
                {
                    Console.WriteLine("Вы ввели слишком маленькое число, так не интересно:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");
                    
                    while (Console.ReadKey().Key!=ConsoleKey.Enter);
                    Console.Clear();
                    continue;
                }

                if (CountOfCity >10)
                {
                    Console.WriteLine("Вы ввели слишком большое число, вам надоест вводить длинну дорог:3");
                    Console.WriteLine("Нажмите enter и попробуйте заново");

                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                    Console.Clear();
                    continue;
                }

                Console.Clear();
                break;
            }

            string[] Citys = new string[CountOfCity];

            //TODO добавить проверку на уникальность названий. 
            for (byte i = 0; i < CountOfCity; i++)
            {
                Console.WriteLine(string.Format("Введите название {0}-ого города:", i+1));
                Citys[i] = Console.ReadLine();
                Console.Clear();
            }

            return Citys;
        }
    }
}
