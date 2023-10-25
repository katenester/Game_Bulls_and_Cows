using ConsoleApp;
using System;
using static System.Net.Mime.MediaTypeNames;


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
        public Person(ref Info info)
        {
            Console.Write("Введите имя для игры: ");
            string UserName = Console.ReadLine();
            //Проверка, что имя не пустое.
            while (string.IsNullOrEmpty(UserName) || UserName.Length > 30)
            {
                Console.Write("Имя пустое или содежит больше 30 символов. Введите новое имя для игры: ");
                UserName = Console.ReadLine();
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
            bool flag = true;
            while (flag)
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
                    else if (trial == 0)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Число должно быть четырехзначным с уникальными (неповторяющимися) цифрами");
                    }
                }
                if (flag == false) { break; }
                // новая попытка
                info.countAttempt += 1;
                // Проверка совпадает ли введеное пользователем число с секретным.
                int bull, cow;
                // Считаем количество быков и коров. 
                Game.BullsАndCowsGame(trial.ToString(), info.number.ToString(), out bull, out cow);
                info.textGame += $"Попытка: {trial} Быков: {bull} Коров: {cow};";// заменить на пробелы
                // Если пользователь отгадал число 
                if (bull == 4)
                {
                    // если число отгадано , то пересчитываем рейтинг для пользователя
                    Console.WriteLine("ПОБЕДА! Число отгадано");
                    info.textGame = "";
                    info.rating += (double)1 / info.countAttempt; //перенесла эту строчку сюда, так кажется логичнее, зачем здесь дабл
                    break;
                }
                else
                {
                    Console.WriteLine("Количество быков: " + bull + " Количество коров: " + cow);
                }
            }
            // Пересчитываем рейтинг только после полных отградываний слов. Т.е. выход произошел из-за каких ситуаций
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
                Console.WriteLine("Выберите номер из списка, что вы хотите сделать:");
                Console.WriteLine("1. Продолжить незаконченную игру");
                Console.WriteLine("2. Создать новую игру");
                Console.WriteLine("3. Отобразить таблицу лучших игроков");
                Console.WriteLine("0. Выйти");
                int n = TryInt();
                switch (n)
                {
                    case 0:
                        Console.WriteLine("Вы выбрали выйти из игры. Игра сохранена!");
                        Console.WriteLine("Goodbye!");
                        flag = false;
                        break;
                    case 1:
                        // По идеи так, но надо будет подробнее посмотреть на логику и сохранение полей ( не сломалось ли что-то)
                        Console.WriteLine("Вы выбрали продолжить незаконченную игру.");
                        // Проверка есть ли сохраненная игра 
                        if (String.IsNullOrEmpty(info.textGame))
                        {
                            Console.WriteLine("Незаконченной игры не найдено. Создайте новую игру.");
                        }
                        else
                        {
                            Console.WriteLine("Введите 0 для завершения игры.");
                            string text = info.textGame;
                            for (int i = 0; i < text.Length; i++)
                            {
                                if (text[i] == ';') Console.WriteLine("");
                                else Console.Write(text[i]);
                            }
                            BullsАndCowsGame(ref info);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Создание новой игры");
                        // ЗАГАДЫВАЕМ ЧИСЛО
                        info.number = Game.GeneratingNumber();
                        Console.WriteLine("Число от 1000 до 9999 загадано. Цифры в числе уникальны.");
                        Console.WriteLine("Введите 0 для завершения игры."); 
                        Console.WriteLine("Игра началась!");
                        info.textGame = "Сохранённая игра: "; // обнуляем текст и кол-во попыток (для нового слова новый текст и новое кол-во попыток)
                        info.countAttempt = 0;
                        BullsАndCowsGame(ref info);  
                        break;
                    case 3:
                        Console.WriteLine("Таблица лучших игроков"); 
                        // метод в game вывода таблицы лучших игроков 
                        //Game.BestPlayers();
                        Console.Write("{0, -30}", "Имя"); //ограничение 30 символов, добавила проверку 
                        Console.WriteLine("{0, -30}", "Рейтинг");
                        string[,] result = new string[Game.Counter(), 2]; 
                        result = Game.BestPlayers();
                        for (int i = 0; i< Game.Counter();i++)
                        {
                            Console.Write("{0, -30}", result[i, 0]);
                            Console.WriteLine("{0, -30}", Math.Round(Convert.ToDouble(result[i, 1]), 3));
                            //Console.WriteLine(result[i,0] + '\t' + '\t' + Math.Round(Convert.ToDouble(result[i, 1]), 3));
                        }
                        break;
                    default:
                        Console.WriteLine("Такого пункта не существует, попробуйте ещё раз.");
                        break;
                }
            }
        }
    }
}
