using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;
using System.Diagnostics.CodeAnalysis;

namespace Laba5
{
    internal class Program
    {
        static int[] MonthlyTemp(int[,] temperatures, int monthIndex)
        {
            int[] monthlyTemperatures = new int[30];

            for (int day = 0; day < 30; day++)
            {
                monthlyTemperatures[day] = temperatures[monthIndex, day];
            }

            return monthlyTemperatures;
        }
        static double[] AverageTemp(int[,] temperatures)
        {
            double[] averages = new double[12];

            for (int month = 0; month < 12; month++)
            {
                double sum = 0;

                for (int day = 0; day < 30; day++)
                {
                    sum += temperatures[month, day];
                }

                averages[month] = sum / 30; // Средняя температура
            }

            return averages;
        }
        static int[] Shuffl(int[] array)
        {
            Random r = new Random();
            foreach (int i in array)
            {
                array[i] = r.Next(-30, 30);
            }
            return array;
        }
        static void PrintMatrixList(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write(item + "\t"); // Выводим элемент с табуляцией
                }
                Console.WriteLine(); // Переход на новую строку после каждой строки матрицы
            }
        }
        static LinkedList<LinkedList<int>> MatrizCountList(LinkedList<LinkedList<int>> array1, LinkedList<LinkedList<int>> array2)
        {
            LinkedList<LinkedList<int>> matriz = new LinkedList<LinkedList<int>>();

            var row1 = array1.First;
            var row2 = array2.First;

            // Умножение соответствующих элементов
            while (row1 != null && row2 != null)
            {
                LinkedList<int> resultRow = new LinkedList<int>();
                var col1 = row1.Value.First;//значение в первом элементе в этом случае это лист 
                var col2 = row2.Value.First;

                while (col1 != null && col2 != null)
                {
                    int product = col1.Value * col2.Value;// значение элемента в листе 
                    resultRow.AddLast(product);
                    col1 = col1.Next;//переход на следующий элемент в списке
                    col2 = col2.Next;//
                }

                matriz.AddLast(resultRow);
                row1 = row1.Next;//переход на следующий элемент в списке
                row2 = row2.Next;
            }
            return matriz;
        }

        static LinkedList<LinkedList<int>> MatrizList(int a, int b, int c, int d)
        {
            LinkedList<LinkedList<int>> matriz = new LinkedList<LinkedList<int>>();
            LinkedList<int> row = new LinkedList<int>(); // Создание новой строки
            row.AddLast(a);row.AddLast(b);
            matriz.AddLast(row);
            LinkedList<int> col = new LinkedList<int>(); col.AddLast(c);col.AddLast(d);
            matriz.AddLast(col);
            return matriz;
        }
        static int CountLettersList(List<char> mainarray, List<char> b)
        {
            int k = 0;
            foreach (char i in mainarray)
            {
                foreach (char j in b)
                {
                    if (i == j)
                    {
                        k++;
                    }
                }
            }
            return k;
        }
        //выводит вещественный массив
        static void PrintArray(float[] array)
        {
            foreach (var number in array)
            {
                Console.Write($"{number:F3} "); // Форматирование до трех знакаов после запятой
            }
            Console.WriteLine(); // Переход на новую строку
        }
        //выводит среднюю температуру
        static float[] SrArray(int[,] arr)
        {
            float[] ints = new float[12];
            for (int i = 0;i< arr.GetLength(0);i++)
            {
                int summa = 0;
                for (int j = 0;j< arr.GetLength(1);j++)
                {
                    summa += arr[i,j];
                }
                ints.Append(summa/12);
            }
            return ints;
        }
        static int[,] MatrizCount(int[,] array1, int[,] array2)
        {
            int[,] matriz = new int[2, 2];
            for (int i = 0;i < 2;i++)
            {
                matriz[i,0] = array1[i,0]*array2[i,0];
                matriz[i, 1] = array1[i, 1] * array2[i, 1];
            }
            return matriz;
        }
        static int[,] Matriz(int a, int b,int c, int d)
        {
            int[,] matriz = new int[2, 2];
            matriz[0, 0] = a;
            matriz[0, 1] = b;
            matriz[1, 0] = c;
            matriz[1, 1] = d;
            return matriz;
        }
        static uint CountLetters(char[] mainarray, char[] b)
        {
            uint k = 0;
            foreach (char i in mainarray)
            {
                foreach(char j in b)
                {
                    if (i == j)
                    {
                        k++;
                    }
                }
            }
            return k;
        }
        static void Main(string[] args)
        {
            Task1(args);
            Task2();
            Task3();
            Task4(args);
            Task5();
            Task6();
        }
        static void Task1(string[] args)
        {
            /*Упражнение 6.1 Написать программу, которая вычисляет число гласных и согласных букв в
файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла
заносится в массив символов. Количество гласных и согласных букв определяется проходом
по массиву. Предусмотреть метод, входным параметром которого является массив символов.
Метод вычисляет количество гласных и согласных букв.*/
            Console.WriteLine("задание 6.1");
            string name = args[0];
            //массив гласных , согласных
            char[] glas = "аеёиоуыэюя".ToCharArray(); //консертация в чар
            char[] sogl = "бвгджзйклмнпрстфхцчшщ".ToCharArray();

            FileStream text = new FileStream(name, FileMode.Open);
            StreamReader reader = new StreamReader(text);
            string stroki = reader.ReadToEnd();
            text.Close();
            char[] chars = stroki.ToLower().ToCharArray();
            uint glasc = CountLetters(chars, glas);
            uint soglc = CountLetters(chars, sogl);
            Console.WriteLine($"гласных {glasc}");
            Console.WriteLine($"согласных {soglc}");
        }
        static void Task2()
        {
            /*Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в
виде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,
метод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).
             */
            Console.WriteLine("задание 6.2");
            Console.WriteLine("напишите первую матрицу");
            int index11 = int.Parse(Console.ReadLine());
            int index12 = int.Parse(Console.ReadLine());
            int index21 = int.Parse(Console.ReadLine());
            int index22 = int.Parse(Console.ReadLine());
            int[,] arry1 = Matriz(index11, index12, index21, index22);
            Console.WriteLine("напишите вторую матрицу");
            int stroka11 = int.Parse(Console.ReadLine());
            int stroka12 = int.Parse(Console.ReadLine());
            int stroka21 = int.Parse(Console.ReadLine());
            int stroka22 = int.Parse(Console.ReadLine());
            int[,] arry2 = Matriz(index11, index12, index21, index22);
            int[,] mainarray1 = MatrizCount(arry1, arry2);
            int k = 0;
            foreach (var el in mainarray1)
            {
                Console.Write(el + "\t"); // Вывод элемента
                if (k == 1)
                {
                    Console.WriteLine("\n");
                    k++;
                }
                else
                {
                    k++;
                }
            }
        }
        static void Task3()
        /*Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать
двумерный рандомный массив temperature[12,30], в котором будет храниться температура
для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
значения температур случайным образом. Для каждого месяца распечатать среднюю
температуру. Для этого написать метод, который по массиву temperature [12,30] для каждого
месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
средних температур. Полученный массив средних температур отсортировать по
возрастанию.
         */
        {
            Console.WriteLine("задание 6.3");
            Console.WriteLine("задание 6.3");
            int[,] arrTemp = new int[12, 30];
            Random random = new Random();
            for (int i = 0;i< arrTemp.GetLength(0);i++)
            {
                for (int j = 0;j< arrTemp.GetLength(1);j++)
                {
                    arrTemp[i, j] = random.Next(-30, 30);
                }
            }
            float[] srarray = SrArray(arrTemp);
            Console.WriteLine("Исходный массив:");
            PrintArray(srarray);
            // Сортировка массива
            Array.Sort(srarray);            
            // Вывод отсортированного массива
            Console.WriteLine("Отсортированный массив:");
            PrintArray(srarray);
        }
        static void Task4(string[] args)
        /*Написать программу, которая вычисляет число гласных и согласных букв в
файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла
заносится в массив символов. Количество гласных и согласных букв определяется проходом
по массиву. Предусмотреть метод, входным параметром которого является массив символов.
Метод вычисляет количество гласных и согласных букв.выполнить с помощью коллекции List<T>.
         */
        {
            Console.WriteLine("задание 6.1");

            string name = args[0];
            List<char> glas = "аеёиоуыэюя".ToCharArray().ToList(); //консертация в чар
            List<char> sogl = "бвгджзйклмнпрстфхцчшщ".ToCharArray().ToList();
            FileStream text = new FileStream(name, FileMode.Open);
            StreamReader reader = new StreamReader(text);
            string stroki = reader.ReadToEnd();
            text.Close();
            List<char> chars = stroki.ToLower().ToCharArray().ToList();
            int glasc = CountLettersList(chars, glas);
            int soglc = CountLettersList(chars, sogl);
            Console.WriteLine($"гласных {glasc}");
            Console.WriteLine($"согласных {soglc}");
        }
        static void Task5()
        /*Написать программу, реализующую умножению двух матриц, заданных в
виде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,
метод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).выполнить с помощью коллекций
LinkedList<LinkedList<T>>.
         */
        {
            Console.WriteLine("задание 6.2");
            Console.WriteLine("напишите первую матрицу");
            int index11 = int.Parse(Console.ReadLine());
            int index12 = int.Parse(Console.ReadLine());
            int index21 = int.Parse(Console.ReadLine());
            int index22 = int.Parse(Console.ReadLine());
            LinkedList<LinkedList<int>> arry1 = MatrizList(index11, index12, index21, index22);
            Console.WriteLine("напишите вторую матрицу");
            int stroka11 = int.Parse(Console.ReadLine());
            int stroka12 = int.Parse(Console.ReadLine());
            int stroka21 = int.Parse(Console.ReadLine());
            int stroka22 = int.Parse(Console.ReadLine());
            LinkedList<LinkedList<int>> arry2 = MatrizList(index11, index12, index21, index22);
            LinkedList<LinkedList<int>> res = MatrizCountList(arry1, arry2);
            PrintMatrixList(res);
        }
        static void Task6()
        /*Написать программу, вычисляющую среднюю температуру за год. Создать
двумерный рандомный массив temperature[12,30], в котором будет храниться температура
для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
значения температур случайным образом. Для каждого месяца распечатать среднюю
температуру. Для этого написать метод, который по массиву temperature [12,30] для каждого
месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
средних температур. Полученный массив средних температур отсортировать по
возрастанию.Dictionary<TKey, TValue>. В качестве ключей выбрать строки – названия месяцев, а в
качестве значений – массив значений температур по дням.
         */
        {
            Console.WriteLine("задание 6.3");
            // Создаем двумерный массив для хранения температур
            int[,] temperature = new int[12, 30];
            Random random = new Random();

            // Генерируем случайные температуры от -10 до 35
            for (int month = 0; month < 12; month++)
            {
                for (int day = 0; day < 30; day++)
                {
                    temperature[month, day] = random.Next(-10, 36);
                }
            }

            // Вычисляем средние температуры
            double[] averageTemperatures = AverageTemp(temperature);

            // Создаем словарь для хранения температур по месяцам
            Dictionary<string, int[]> monthTemperatures = new Dictionary<string, int[]>
            {
                { "Январь", MonthlyTemp(temperature, 0) },
                { "Февраль", MonthlyTemp(temperature, 1) },
                { "Март", MonthlyTemp(temperature, 2) },
                { "Апрель", MonthlyTemp(temperature, 3) },
                { "Май", MonthlyTemp(temperature, 4) },
                { "Июнь", MonthlyTemp(temperature, 5) },
                { "Июль", MonthlyTemp(temperature, 6) },
                { "Август", MonthlyTemp(temperature, 7) },
                { "Сентябрь", MonthlyTemp(temperature, 8) },
                { "Октябрь", MonthlyTemp(temperature, 9) },
                { "Ноябрь", MonthlyTemp(temperature, 10) },
                { "Декабрь", MonthlyTemp(temperature, 11) }
            };

            // Сортируем средние температуры по возрастанию
            var sortedAverageTemperatures = averageTemperatures
                .Select((temp, index) => new { Month = monthTemperatures.Keys.ElementAt(index), Temperature = temp })
                .OrderBy(x => x.Temperature)
                .ToList();
            foreach (var entry in sortedAverageTemperatures)
            {
                Console.WriteLine($"{entry.Month}: {entry.Temperature:F2}°C");
            }
        }
    }
}