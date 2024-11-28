using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dz5
{
    // струтура студентов со всеми их данными
    struct Student
    {
        public byte Id;
        public string SurName { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public string Exam { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{Id}, Год рождения: {YearOfBirth}, Экзамен: {Exam}, Баллы: {Score}";
        }
    }
    internal class Program
    {
        static void AddStudent(Dictionary<byte, Student> students)
        {
            var student = new Student();
            Console.Write("Введите Id ");
            student.Id = byte.Parse(Console.ReadLine());
            Console.Write("Введите фамилию: ");
            student.SurName = Console.ReadLine();
            Console.Write("Введите имя: ");
            student.Name = Console.ReadLine();
            Console.Write("Введите год рождения: ");
            student.YearOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Введите экзамен: ");
            student.Exam = Console.ReadLine();
            Console.Write("Введите баллы: ");
            student.Score = int.Parse(Console.ReadLine());

            students.Add(student.Id, student);
            Console.WriteLine("Студент добавлен.");
        }
        //соритрует по баллам
        static void SortStudents(Dictionary<byte, Student> students)
        {
            var sortedStudents = students.Values.OrderBy(s => s.Score).ToList();//Извлекает все значения (студентов) из словаря students.Сортирует этих студентов по их баллам(свойству Score) в порядке возрастания.
            foreach (var student in sortedStudents)
            {
                Console.WriteLine(student);
            }
        }
        // удаление 
        static void RemoveStudent(Dictionary<byte, Student> students)
        {
            Console.Write("Введите id");
            byte id = byte.Parse(Console.ReadLine());
            if (students.Remove(id))
            {
                Console.WriteLine("Студент удален.");
            }
        }
            //для упорядочивания инфы про студентов и изьятия данных из файла
        static void ReadStudentsFromFile(Dictionary<byte, Student> students, string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);  
                foreach (var line in lines)//перебирает каждую строчку в файле
                {
                    var parts = line.Split(',');
                    if (parts.Length == 6)
                    {
                        var student = new Student
                        {
                            Id = byte.Parse(parts[0].Trim()),
                            SurName = parts[1].Trim(),//Trim() удаляет лишние пробелы 
                            Name = parts[2].Trim(),
                            YearOfBirth = int.Parse(parts[3].Trim()),
                            Exam = parts[4].Trim(),
                            Score = int.Parse(parts[5].Trim())
                        };
                        students.Add(student.Id, student);
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        //ждя вывода списка
        static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        static List<string> Shuffle(List<string> list)
        {
            Random rand = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                // Генерируем случайный индекс
                int j = rand.Next(0, i + 1);
                // Меняем местами элементы
                string temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            return list;
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
        }
        static void Task1()
        /*Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List
должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с
картинками. Вывести в консоль перемешанные номера (изначальный List и полученный
List). Перемешать любым способом.
         */
        {
            Console.WriteLine("task 1");
            string folderPath = @"картинки";

            // Получаем список файлов и папок в указанной папке
            List<string> fileList = new List<string>();
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                fileList.Add(file);
            }
            for (int i = 0; i < 32; i++)
            {
                fileList.Add(fileList[i]);
            }
            Console.WriteLine("список до ");
            PrintList(fileList);
            Shuffle(fileList);
            Console.WriteLine("список после  ");
            PrintList(fileList);
        }

        static void Task2()
        /*2. Создать студента из вашей группы (фамилия, имя, год рождения, с каким экзаменом
поступил, баллы). Создать словарь для студентов из вашей группы (10 человек).
Необходимо прочитать информацию о студентах с файла. В консоли необходимо создать
меню:
a. Если пользователь вводит: Новый студент, то необходимо ввести
информацию о новом студенте и добавить его в List
b. Если пользователь вводит: Удалить, то по фамилии и имени удаляется
студент
c. Если пользователь вводит: Сортировать, то происходит сортировка по баллам
(по возрастанию)
        */
        {
            Console.WriteLine("задание 2");
            Dictionary<byte, Student> students = new Dictionary<byte, Student>();
            ReadStudentsFromFile(students, "студенты.txt");
            bool Flag = true;
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            while (Flag)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Новый студент");
                Console.WriteLine("2. Удалить");
                Console.WriteLine("3. Сортировать");
                Console.WriteLine("иначе напишите выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();
                if (choice == "выход")
                {
                    Flag = false;
                }
                switch (choice)
                {
                    case "1":
                        AddStudent(students);
                        break;
                    case "2":
                        RemoveStudent(students);
                        break;
                    case "3":
                        SortStudents(students);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void Task3()
        /*3. Создать бабулю. У бабули есть Имя, возраст, болезнь и лекарство от этой болезни,
которое она принимает (болезней может быть у бабули несколько). Реализуйте список
бабуль. Также есть больница (у больницы есть название, список болезней, которые они
лечат и вместимость). Больниц также, как и бабуль несколько. Бабули по очереди
поступают (реализовать ввод с клавиатуры) и дальше идут в больницу, в зависимости от
заполненности больницы и списка болезней, которые лечатся в данной больнице,
реализовать функционал, который будет распределять бабулю в нужную больницу. Если
бабуля не имеет болезней, то она хочет только спросить - она может попасть в первую
свободную клинику. Если бабулю ни одна из больниц не лечит, то бабуля остаётся на
улице плакать. На экран выводить список всех бабуль, список всех больниц, болезни,
которые там лечат и сколько бабуль в данный момент находится в больнице, также
вывести процент заполненности больницы. P.S. Бабуля попадает в больницу, если там
лечат более 50% ее болезней. Больницы реализовать в виде стека, бабуль в виде
очереди.
         */
        {

        }

        static void Task4()
        /*4. Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший
путь.
         */
        {

        }
    }
}
