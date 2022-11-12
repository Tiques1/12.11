using System;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.IO;
namespace Abcd
{
    class Programma
    {
        public static string[][] fornextsorting = new string[1000][]; // "Дальнейшая сортировка" - сюда сохраняем какую-либо выборку для возможности дальнейшей сортировки
        public static int j;
        public static void Main()
        {
            //0 страна; 1 столица; 2 площадь; 3 население; 4 континент

            string[][] line = new string[1000][]; // ХЗ КАК СОЗДАТЬ МАССИВ МАССИВОВ НЕИЗВЕСТНОЙ ДЛИНЫ

            string[] lines = File.ReadAllLines("C:\\Users\\Сергей\\source\\repos\\gitPractic03\\countrey.txt");

            int i = 0;
            foreach (string s in lines)
            {
                line[i] = s.Split(';');
                i++;
            }
            
            Sorting(line);
            Console.WriteLine("РАЗДЕЛИТЕЛЬ РАЗДЕЛИТЕЛЬ РАЗДЕЛИТЕЛЬ");
            NextSorting(fornextsorting);
           
 

            //for (int a = 0; a<j-1;a++)
                //File.WriteAllTextAsync(@"C:\Users\Сергей\source\repos\gitPractic03\hui.txt", string.Join(" ", fornextsorting[a]));    НЕ ЗАПИСЫВАЕТСЯ!!! ГОВОРИТ, ЧТО ФАЙЛ ЗАНЯТ ДРУГИМ ПРОЦЕССОМ
  
            
        }
        
        public static void Sorting(string[][] line) // сортировка и запись в "дальнейшую сортировку"
        {          
            string param = Lands();
           
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == null)
                    break;
                else
                    if (line[i][4] == param)
                    {
                    Console.WriteLine(string.Join(" ", line[i]));
                    fornextsorting[j] = line[i];
                    j++;
                    }
            }


        }
        
        public static void NextSorting(string[][] fornextsorting)
        {
            int c = 0;
            while (c<10000)
            {
                for (int i = 0; i < j - 1; i++)
                {
                    string[][] temporary = new string[1][];
                    int x = Int32.Parse(fornextsorting[i][3]);
                    int y = Int32.Parse(fornextsorting[i+1][3]);
                    if (x > y)
                    {
                        temporary[0] = fornextsorting[i];
                        fornextsorting[i] = fornextsorting[i + 1];
                        fornextsorting[i + 1] = temporary[0];
                    }                                      
                }
                c++;
            }
            
            for(int i = 0; i < j - 1; i++)
                Console.WriteLine(string.Join(" ", fornextsorting[i]));
        }
     
       
        public static string Lands() // МЕТОД, КОТОРЫЙ ОПРЕДЕЛЯЕТ ПО КАКОМУ ЗНАЧЕНИЮ ПАРАМЕТРА "КОНТИНЕНТ" БУДЕТ ПРОИСХОДИТЬ СОРТИРОВКА
        {
            Console.WriteLine("Веберите континент(напечатайте только цифру)");
            Console.WriteLine("1. Австралия и океания");
            Console.WriteLine("2. Европа");
            Console.WriteLine("3. Северная Америка");
            Console.WriteLine("4. Азия");
            Console.WriteLine("5. Африка");

            string name = Console.ReadLine();
            switch (name)
            {
                case "1":
                    return "Австралия и Океания";
                case "2":
                    return "Европа";
                case "3":
                    return "Северная Америка";
                case "4":
                    return "Азия";
                case "5":
                    return "Африка";
                default:
                    return "Неверный формат!";

            }
        }
       
    }
}
