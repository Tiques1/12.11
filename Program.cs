using System;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.IO;
using System.Data;

namespace Abcd
{
    class Programma
    {
        public static string[][] fornextsorting = new string[1000][]; // "Дальнейшая сортировка" - сюда сохраняем какую-либо выборку для возможности дальнейшей сортировки
        public static int j = 0;
        public static void Main()
        {
            Console.WriteLine(@"Вставьте путь к файлу countrey.txt Весь путь кроме названия самого файла: \countrey.txt !!!");
            string path = Console.ReadLine() + @"\countrey.txt";
            string[] lines = File.ReadAllLines(path);
            string[][] line = new string[lines.Length][]; // [][циферки] 0 страна; 1 столица; 2 площадь; 3 население; 4 континент

            int i = 0;
            foreach (string s in lines)
            {
                line[i] = s.Split(';');
                i++;
            }

            do
            {
                Sorting(line);
                Compare();
                SearchByLetter();              
            }
            while (Reset());




            }

        public static void Sorting(string[][] line) // сортировка по материкам и запись в "дальнейшую сортировку"
        {
            string[] param = new string[5];
            for (int i = 0; i < 5; i++)
            {
                param[i] = Lands();
                if (param[i] == "0")
                    break;
            }
            for (int i = 0; i < line.Length; i++)
            {
                if ((line[i][4] == param[0]) | (line[i][4] == param[1]) | (line[i][4] == param[2]) | (line[i][4] == param[3]) | (line[i][4] == param[4]))
                {
                    Console.WriteLine(string.Join(" ", line[i]));
                    fornextsorting[j] = line[i];
                    j++;
                }
            }
        }

        public static void SortingToUp(string[][] fornextsorting, int squareorpeople)
        {
            
            int c = 0;
            while (c < 10000)
            {
                for (int i = 0; i < j - 1; i++)
                {
                    string[][] temporary = new string[1][];
                    int x = Int32.Parse(fornextsorting[i][squareorpeople]);
                    int y = Int32.Parse(fornextsorting[i + 1][squareorpeople]);
                    if (x > y)
                    {
                        temporary[0] = fornextsorting[i];
                        fornextsorting[i] = fornextsorting[i + 1];
                        fornextsorting[i + 1] = temporary[0];
                    }
                }
                c++;
            }

            for (int i = 0; i < j - 1; i++)
                Console.WriteLine(string.Join(" ", fornextsorting[i]));
        }
        public static void SortingToDown(string[][] fornextsorting,int  squareorpeople)
        {

            bool flag = true;
            while (flag == true)
            {
                flag = false;
                for (int i = 0; i < j - 1; i++)
                {
                    string[][] temporary = new string[1][];
                    int x = Int32.Parse(fornextsorting[i][squareorpeople]);
                    int y = Int32.Parse(fornextsorting[i + 1][squareorpeople]);
                    if (x < y)
                    {
                        temporary[0] = fornextsorting[i];
                        fornextsorting[i] = fornextsorting[i + 1];
                        fornextsorting[i + 1] = temporary[0];
                        flag = true;
                    }
                }
                
            }

            for (int i = 0; i < j - 1; i++)
                Console.WriteLine(string.Join(" ", fornextsorting[i]));
        }

        public static void Compare()
        {
            Console.WriteLine("Сортируем дальше");
            Console.WriteLine("1. По возрастанию численности населения");
            Console.WriteLine("2. По убыванию численности населения");
            Console.WriteLine("3. По возрастанию площади");
            Console.WriteLine("4. По убыванию площади");

            string num = Console.ReadLine();

            if (num == "1" | num == "2" | num == "3" || num == "4")
            {
                if (num == "1")
                    SortingToUp(fornextsorting, 3);
                if (num == "2")
                    SortingToDown(fornextsorting, 3);
                if (num == "3")
                    SortingToUp(fornextsorting, 2);
                if (num == "4")
                    SortingToDown(fornextsorting, 2);
            }
            else
            {
                Console.WriteLine("Нет такого варианта!");
                Compare();
            }

        }
        public static string Lands() 
        {
            Console.WriteLine("Выберите континенты(печатайте только по одной цифре за раз)");
            Console.WriteLine("Напечатайте 0, если хотите прекратить выбор");
            Console.WriteLine("1. Австралия и океания");
            Console.WriteLine("2. Европа");
            Console.WriteLine("3. Северная Америка");
            Console.WriteLine("4. Азия");
            Console.WriteLine("5. Африка");

            string name = Console.ReadLine();
            if (name == "0" | name == "1" | name == "2" | name == "3" | name == "4" | name == "5")
            {
                switch (name)
                {
                    case "0":
                        return "0";
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
            else
            {
                Console.WriteLine("Нет такого варианта!");
                return Lands();
            }
        }
        
        public static void SearchByLetter()
        {
            string countryorcapital = Select();
            int couorcap = 0;
            if (countryorcapital == "страны")
                couorcap = 0;
            if (countryorcapital == "столицы")
                couorcap = 1;

            Console.WriteLine("Введите букву, с которой начинается название " + countryorcapital);
            char[] letter = Console.ReadLine().ToUpper().ToCharArray();
           

            int z = 0;
            for(int i = 0; i < j ; i++)
            {
                char[] name = fornextsorting[i][couorcap].ToCharArray();
                if (letter[0] == name[0])
                {
                    fornextsorting[z] = fornextsorting[i];
                    z++;
                }
            }
            
            for (int i = 0; i < z; i++)
                Console.WriteLine(string.Join(" ", fornextsorting[i]));

        }        
        public static string Select()
        {
            Console.WriteLine("0. Сортировка по названию страны");
            Console.WriteLine("1. Сортировка по названию столицы");
            int couorcap = Int32.Parse(Console.ReadLine());
            
            if (couorcap == 0)
            {
                return "страны";
            }
            if (couorcap == 1)
            {
                return "столицы";
            }
            else
            {
                Console.WriteLine("Нет такого варианта");
                return Select();
            }
            
        }

        public static bool Reset()
        {
            Console.WriteLine("Для выхода напечатайте 0. Если хотите сбросить сортировку и начать заново, напечатайте 1");
            int res = Int32.Parse(Console.ReadLine());
            if (res == 1)
            {
                for (int i = 0; i < 1000; i++)
                    fornextsorting[i] = null;
                j = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }    
}
