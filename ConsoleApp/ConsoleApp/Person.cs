namespace ConsoleApp
{
    enum Day
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    class Person
    {
        public Person(ref Info info)
        {
            Console.Write("Введите имя для игры: ");
            string UserName = Console.ReadLine();
            // Проверка, что имя не пустое и не содержит более 30 символов.
            while ((string.IsNullOrEmpty(UserName)) || (UserName.Length > 30))
            {
                Console.Write("Имя пустое или содежит более 30 символов. Введите новое имя для игры: ");
                UserName = Console.ReadLine();
            }
            // Присваим имя пользователя в структуре.
            info.UserName=UserName;
            // Game.User проиницализирует все поля структуры (т.е. возьмет данные в бд и закинет в структуру).
            // Функция ничего не возвращает т.к. мы передаём ссылку на структуру (ref).
            Game.User(ref info);
        }

        /// <summary>
        /// Проверка, что пользователь ввёл число. 
        /// </summary>
        /// <returns>Возвращает число, которое ввёл пользователь.</returns>
        public static int TryInt()
        {
            int num;
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите число:");
                    num = int.Parse(Console.ReadLine());
                    return num;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Некорректный формат введенных данных.");
                }
            }
        }

        /// <summary>
        /// Логика игры (взаимодействие с пользователем). 
        /// </summary>
        /// <param name="info">Информация о пользователе.</param>
        public void BullsАndCowsGame(ref Info info)
        {
            // Это число, которое вводит пользователь.
            int trial;
            var flag = true;
            while (flag)
            {
                // Пока пользователь не ввёдет 0 или попытку для игры.
                while (true)
                {
                    // Это число, которое вводит пользователь.
                    trial = TryInt();
                    // Проверяем, что число четырехзначное и с уникальными цифрами. Если это так, выходим из цикла.
                    if ((trial > 999) && (trial < 10000) && (Game.CheckUnique(trial.ToString())))
                    {
                        break;
                    }
                    // Если пользователь ввёл 0, то выходим в меню.
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
                // Если пользователь ввёл 0: выходим в меню, дальше код не читается.
                if (flag == false) { break; }
                // Если пользователь ввёл попытку, то увеличиваем количество попыток на 1.
                info.CountAttempt += 1;
                // Проверка совпадает ли введеное пользователем число с секретным.
                // Считаем количество быков и коров. 
                Game.BullsАndCowsGame(trial.ToString(), info.Number.ToString(), out int bull, out int cow);
                // Для сохранения (при необходимости) запоминаем текст игры.
                info.TextGame += $"Попытка: {trial} Быков: {bull} Коров: {cow};";
                // Если пользователь отгадал число.
                if (bull == 4)
                {
                    Console.WriteLine("ПОБЕДА! Число отгадано.");
                    // Обнуляем текст игры.
                    info.TextGame = "";
                    // Пересчитываем рейтинг и выходим.
                    info.Rating += (double)1 / info.CountAttempt; 
                    break;
                }
                // Если пользователь не отгадал число.
                else
                {
                    // Выводим количество быков и коров в числе пользователя.
                    Console.WriteLine($"Количество быков: {bull} +  Количество коров: {cow}");
                }
            }
            // Обновляем данные о пользователе в бд.
            Game.Update(info);
        }

        /// <summary>
        /// Получение текущего времени и приветствие пользователя.
        /// </summary>
        public static void Time()
        {
            DateTime dateTime = DateTime.Now;
            // Получение текущего часа
            int hour = dateTime.Hour;
            Day dayTime;
            if (hour > 5 && hour < 12)
            {
                dayTime = Day.Morning;
            }
            else if (hour >= 12 && hour < 17)
            {
                dayTime = Day.Afternoon;
            }
            else if (hour >= 17 && hour < 23)
            {
                dayTime = Day.Evening;
            }
            else
            {
                dayTime = Day.Night;
            }
            switch (dayTime)
            {
                case Day.Morning:
                    Console.WriteLine("Доброе утро");
                    break;
                case Day.Afternoon:
                    Console.WriteLine("Добрый день");
                    break;
                case Day.Evening:
                    Console.WriteLine("Добрый вечер");
                    break;
                case Day.Night:
                    Console.WriteLine("Доброй ночи");
                    break;
            }
        }

        public void Menu(ref Info info)
        {
            var flag = true;
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
                        Console.WriteLine("До свидания!");
                        flag = false;
                        break;
                    case 1:
                        Console.WriteLine("Вы выбрали продолжить незаконченную игру.");
                        // Проверка, есть ли сохраненная игра.
                        if (string.IsNullOrEmpty(info.TextGame))
                        {
                            Console.WriteLine("Незаконченной игры не найдено. Создайте новую игру.");
                        }
                        // Если сохранённая игра есть.
                        else
                        {
                            Console.WriteLine("Введите 0 для завершения игры.");
                            // Выводим текст сохранённой игры.
                            string text = info.TextGame;
                            for (var i = 0; i < text.Length; i++)
                            {
                                if (text[i] == ';') Console.WriteLine("");
                                else Console.Write(text[i]);
                            }
                            // Запускаем продолжение игры.
                            BullsАndCowsGame(ref info);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Вы выбрали создание новой игры.");
                        // Программа загадывает тайное число.
                        info.Number = Game.GeneratingNumber();
                        Console.WriteLine("Число от 1000 до 9999 загадано. Цифры в числе уникальны.");
                        Console.WriteLine("Введите 0 для завершения игры."); 
                        Console.WriteLine("Игра началась!");
                        // Не обнуляем строку, а перезаписываем на случай, если пользователь не ввёдет в игре ничего, но захочет сохранить её.
                        info.TextGame = "Сохранённая игра: ";
                        // Обнуляем количество попыток.
                        info.CountAttempt = 0;
                        // Запускаем новую игру.
                        BullsАndCowsGame(ref info);  
                        break;
                    case 3:
                        Console.WriteLine("Таблица лучших игроков:"); 
                        //Ограничение 30 символов на столбец, чтобы таблица красиво выводилась.
                        Console.Write("{0, -30}", "Имя");
                        Console.WriteLine("{0, -30}", "Рейтинг");
                        // Объявляем и инициализируем двумерный массив.
                        string[,] result = Game.BestPlayers(); 
                        // Выводим таблицу.
                        for (var i = 0; i< Game.Counter();i++)
                        {
                            Console.Write("{0, -30}", result[i, 0]);
                            Console.WriteLine("{0, -30}", Math.Round(Convert.ToDouble(result[i, 1]), 3));
                        }
                        break;
                    // На случай, если пользователь введёт несуществующий пункт меню.
                    default:
                        Console.WriteLine("Такого пункта не существует, попробуйте ещё раз.");
                        break;
                }
            }
        }
    }
}
