using ConsoleApp;
using System;


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
        //сделать функцию инит, подумать как связать её с конструктором, функции из меню 
        //

        public Person(ref Info info)
        {
            Console.Write("Введите игровое имя: ");
            string UserName = Console.ReadLine() ?? ""; //ЧТО ЭТО ЗА СИНТАКСИС? - Это означает что мы допускаем значение "" (т.е. если в cw ничего не введено(т.е. null), то переменная = "" ( а не null)
            //проверка, что корректно ввёл
            while (string.IsNullOrEmpty(UserName)) //здесь try catch точно не нужен
            {
                Console.Write("Имя не может быть пустым. Введите новое игровое имя: ");
                UserName = Console.ReadLine() ?? "";
            }

            // Присваим имя пользователя в структуре
            info.userName=UserName;
            // Game.User проиницализирует все поля структуры (т.е. возьмет данные в бд и закинет в структуру).
            // Ничего не возвращает т.к. мы передадим ссылку (ref )
            Game.User(ref info);
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
        // Метод новой/старой игры. 
        // !!!!Поменяла название NewGame на BullsАndCowsGame т.к. логичнее для всех игр , т.е. не только новая игра , но и загрузка старой
        public void BullsАndCowsGame(ref Info info)
        {
            int trial = 1;
            while (trial != 0)
            {
                // проверяем, что число четырехзначное и с уникальными цифрами
                while (true)
                {
                    trial = TryInt();
                    // если число подходит
                    if (trial > 999 && trial < 10000 && Game.CheckUnique(trial.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Число должно быть четырехзначным с уникальными(неповторяющимися) цифрами ");
                    }
                }
                // новая попытка
                info.countAttempt += 1;
                // Проверка совпадает ли введеное пользователем число с секретным.
                int bull, cow;
                // Считаем количество быков и коров. 
                Game.BullsАndCowsGame(trial.ToString(), info.number.ToString(), out bull, out cow);
                info.textGame += $"Быков: {bull} Коров: {cow} Попытка: {trial}     +\t";// заменить на пробелы
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
            info.rating += 1 / info.countAttempt;
            // обновляем данные в бд/ сохраняем игру
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
        public void Menu(ref Info info)
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
                        Console.WriteLine("Выход");
                        Console.WriteLine("Goodbye!");
                        flag = false;
                        break;
                    case 1:
                        // По идеи так, но надо будет подробнее посмотреть на логику и сохранение полей ( не сломалось ли что-то)
                        Console.WriteLine("Загрузка прошлой игры: ");
                        // Проверка есть ли сохраненная игра 
                        if (String.IsNullOrEmpty(info.textGame))
                        {
                            Console.WriteLine("Сохраненной игры не найдено. Создайте новую игру");
                        }
                        else
                        {
                            Console.WriteLine(info.textGame);
                        }
                        BullsАndCowsGame(ref info);
                        break;
                    case 2:
                        Console.WriteLine("Создание новой игры");
                        // ЗАГАДЫВАЕМ ЧИСЛО
                        info.number = Game.GeneratingNumber();
                        Console.WriteLine("Число от 1000 до 9999 загадано. Цифры в числе уникальны");
                        Console.WriteLine("Введите 0 для завершения игры"); //что это и зачем, тут же нигде пользователь ничего не вводит
                        Console.WriteLine("Игра началась");
                        info.textGame = ""; // обнуляем текст и кол-во попыток (для нового слова новый текст и новое кол-во попыток)
                        info.countAttempt = 0;
                        BullsАndCowsGame(ref info);  
                        break;
                    case 3:
                        Console.WriteLine("Таблица лучших игроков");
                        // метод в game вывода таблицы лучших игроков 
                        //Game.BestPlayers();
                        Console.WriteLine("Имя    Рейтинг");
                        string[,] result = new string[Data.Counter(), 2]; //надо сделать через гейм, а не дату
                        result = Game.BestPlayers();
                        for (int i = 0; i< Data.Counter() -1;i++)
                        {
                            Console.WriteLine(result[i,0] + "    " + result[i, 1]);
                        }
                        break;
                }
            }
        }
    }
}
