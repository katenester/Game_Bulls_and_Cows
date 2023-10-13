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
        public void NewGame()
        {
            int trial = 1;
            while (trial != 0)
            {
                trial = TryInt();
                countAttempt += 1;
                // Проверка совпадает ли введеное пользователем число с секретным.
                // Мб реализовать проверку сразу тут, но это вроде как логика игры , поэтому ВОПРОСИК
                int bull, cow;
                // Считаем количество быков и коров
                Game.MainGame(trial, number, out bull, out cow);
                textGame += $"Быков: {bull} Коров: {cow} Попытка: {trial}     +\t";// заменить на пробелы
                // Если пользователь отгадал число 
                if (bull == 4)
                {
                    // если число отгадано , то пересчитываем рейтинг для пользователя
                    Console.WriteLine("ПОБЕДА! Число отгадано");
                    break;
                }
                else
                {
                    Console.WriteLine("Количество быков: " + bull + "Количество коров : " + cow);
                }
            }
            rating += 1 / countAttempt;
            // обновляем данные в бд/ сохраняем игру
            // ПОДУМАТЬ КАК СДЕЛАТЬ ЛУЧШЕ ! ПЯТЬ ПАРАМЕТРОВ ВЫГЛЯДАТ НЕ ОЧЕНЬ
            string[] info = new string[5] { userName, textGame.ToString(), countAttempt.ToString(), number.ToString(), rating.ToString() };
            //Обновляем данные 
            Game.Update(info);
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
                        // По идеи так, но надо будет подробнее посмотреть на логику и сохранение полей ( не сломалось ли что-то)
                        NewGame();
                        break;
                    case 2:
                        // ЗАГАДЫВАЕМ ЧИСЛО
                        number = Game.GeneratingNumber();
                        Console.WriteLine("Число от 1000 до 9999 загадано");
                        Console.WriteLine("Введите 0 при сохранении игры");
                        Console.WriteLine("Игра началась");
                        textGame = ""; // обнуляем текст и кол-во попыток (для нового слова новый текст и новое кол-во попыток)
                        countAttempt = 0;
                        NewGame();
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
