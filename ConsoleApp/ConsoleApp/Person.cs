using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    enum Day
    {
        morning,
        afternoon,
        evening,
    }
    class Person
    {
        string userName = "";
        string textGame = "";
        int countAttempt = 0;
        int number = 0;
        double rating = 0;

        //сделать функцию инит, подумать как связать её с конструктором, функции из меню 
        //
        public Person()
        {
            Console.Write("Введите игровое имя: ");
            string UserName = Console.ReadLine() ?? ""; //ЧТО ЭТО ЗА СИНТАКСИС?
            //проверка, что корректно ввёл
            while (string.IsNullOrEmpty(UserName)) //здесь try catch точно не нужен
            {
                Console.Write("Имя не может быть пустым. Введите новое игровое имя: ");
                UserName = Console.ReadLine() ?? "";
            }
            string[] info = Game.User(UserName);
            userName = info[0];
            textGame = info[1];
            countAttempt = int.Parse(info[2]);
            number = int.Parse(info[3]);
            rating = double.Parse(info[4]);
        }
        // Предлагаю проверку делать тут, чтобы сделать код менее грамоздким и избежать повторения кода
        public static int TryInt()
        {
            // мб следует сделать через try catch
            while (true)
            {
                Console.WriteLine("Введите число:");
                if (int.TryParse(Console.ReadLine(), out var value)) { return value; }
                else { Console.WriteLine("Некорректный формат введенных данных."); }
            }
        }
        // Метод новой игры
        public static void NewGame(string userName)
        {

        }
        // Получение текущего времени
        public static void Time()
        {
            DateTime dateTime = DateTime.Now;
            // Получение текущего часа
            int hour = dateTime.Hour;
            if (hour > 5 && hour < 12)
            {
                Console.WriteLine($"Good {Day.morning}!");
            }
            else if (hour >= 12 && hour < 19)
            {
                Console.WriteLine($"Good {Day.afternoon}!");
            }
            else
            {
                Console.WriteLine($"Good {Day.evening}!");
            }
        }
        public void Menu()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Выберите номер из списка :");
                Console.WriteLine("1. Загрузка прошлой игры");
                Console.WriteLine("2. Создание новой игры");
                Console.WriteLine("3. Отображение таблицы лучших игроков");
                Console.WriteLine("0. Выход");
                int n = TryInt();
                switch (n)
                {
                    case 0:
                        Console.WriteLine("Goodbye!");
                        flag = false;
                        break;
                    case 1:
                        break;
                    case 2:
                        //NewGame(UserName);
                        break;
                    case 3:
                        // метод в game вывода таблицы лучших игроков 
                        Game.BestPlayers();
                        break;
                }
            }
        }
    }
}
