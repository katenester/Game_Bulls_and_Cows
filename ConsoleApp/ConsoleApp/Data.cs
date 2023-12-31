﻿namespace ConsoleApp
{
    class Data
    {
        public const string path = @"c:\temp\1.txt";

        /// <summary>
        /// Метод инициализации структуры.
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void InitializeStruct(ref Info info)
        {
            // Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt.
            string? line;
            using StreamReader sr = new(path);
            // Пока не дойдём до конца файла или не найдём имя читаем файл построчно.
            for (var i = 0; (line = sr.ReadLine()) != null; i++)
            {
                if ((i % 5 == 0) && (line == info.UserName))
                {
                    // Строка является каждой пятой строкой, и она равна "UserName".
                    break;
                }
            } 
            // Если пользователя нет в бд, то добавляем его и инициализируем поля.
            if (line == null)
            {
                sr.Close();
                AddNewUser(info);
            }
            // Если пользователь есть в бд.
            else
            {
                // Поле info.userName уже проинициализоривано. В цикле поиска указатель в sr.ReadLine() поставлен на текст игры.
                info.TextGame = sr.ReadLine() ?? "";
                info.CountAttempt = int.Parse(sr.ReadLine() ?? "");
                info.Number = int.Parse(sr.ReadLine() ?? "");
                info.Rating = double.Parse(sr.ReadLine() ?? "");
                sr.Close();
            }

        }

        /// <summary>
        /// Метод добавления нового пользователя в бд. Добавляем имя + 4 пустые строки (для value type записывается 0).
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void AddNewUser(Info info)
        {
            // Добавляем true, чтобы информация записывалась в конец файла, а не заменяла содержимое.
            using StreamWriter sw = new(path, true); 
            sw.WriteLine(info.UserName);
            sw.WriteLine(info.TextGame);
            sw.WriteLine(info.CountAttempt);
            sw.WriteLine(info.Number);
            sw.WriteLine(info.Rating);
            sw.Close();
        }

        /// <summary>
        /// Метод для перезаписи строки.
        /// </summary>
        /// <param name="path">Путь к файлу (бд).</param>
        /// <param name="lineIndex">Индекс строки, которую нужно заменить.</param>
        /// <param name="newValue">Новое значение строки.</param>
        private static void RewriteLine(string path, int lineIndex, string newValue) 
        {
            int i = 0;
            string tempPath = path + ".txt";
            // Чтение файла.
            using (StreamReader sr = new(path)) 
            using (StreamWriter sw = new(tempPath)) 
            {
                while (!sr.EndOfStream)
                {
                    // Чтение строки из файла.
                    string line = sr.ReadLine() ?? "";
                    if (lineIndex == i)
                        sw.WriteLine(newValue);
                    else
                        sw.WriteLine(line);
                    i++;
                }
            }
            // Удаление старого файла.
            File.Delete(path);
            // Переименовывание временного файла.
            File.Move(tempPath, path); 
        }

        /// <summary>
        /// Метод обновления информации о пользователе в бд.
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void UpdateInfo(Info info)
        {
            // Строка для чтения.
            string line; 
            using StreamReader sr = new(path);
            // Номер строки, на которой находится программа.
            var numberLine = 0; 
            for (var k = 0; (line = sr.ReadLine() ?? "") != null; k++) 
            {
                //Поиск игрового имени пользователя.
                if (line == info.UserName) 
                {
                    // Выход из цикла.
                    break; 
                }
                // Счёт номера строки.
                numberLine++;
            }
            sr.Close();
            // Обновление текста игры.
            RewriteLine(path, numberLine + 1, info.TextGame);
            // Обновление количества попыток.
            RewriteLine(path, numberLine + 2, (info.CountAttempt).ToString());
            // Обновление загаданного числа.
            RewriteLine(path, numberLine + 3, info.Number.ToString());
            // Обновление рейтинга.
            RewriteLine(path, numberLine + 4, info.Rating.ToString());
        }

        /// <summary>
        /// Метод подсчёта количества игроков в базе данных.
        /// </summary>
        /// <returns>Количество игроков в базе данных.</returns>
        public static int CountUsers() 
        {
            using StreamReader sr = new(path);
            int counter = 0;
            for (var i = 0; (sr.ReadLine()) != null; i++)
            {
                if (i % 5 == 0)
                {
                    // Строка является каждой пятой строкой и игровым именем пользователя.
                    counter++;
                }
            }
            sr.Close();
            return counter;
        }

        /// <summary>
        /// Метод для составления таблицы лучших игроков.
        /// </summary>
        /// <param name="result">Двумерный массив для хранения игровых имен и рейтингов игроков.</param>
        /// <returns>Двухмерный массив, состоящий из имен и соответствующего рейтинга игровов,
        /// в порядке убывания рейтинга.</returns>
        public static string[,] GetRating(string[,] result)
        {
            //Создание одномерного массива с рейтингами всех игроков.
            double[] rating = new double[CountUsers()]; 
            // Строка для чтения.
            string line; 
            using StreamReader sr = new(path);
            // Номер текущего игрока.
            var j = 0;
            //Цикл работает ,пока не конец файла.
            for (var i = 0; (line = sr.ReadLine() ?? "") != null; i++)
            {
                if (i % 5 == 4)
                {
                    // Строка является рейтигом.
                    rating[j] = Convert.ToDouble(line);
                    j++;
                }
            }
            sr.Close();
            // Массив рейтингов отсортирован по возрастанию.
            Array.Sort(rating);
            // Массив рейтингов отсортирован по убыванию.
            Array.Reverse(rating); 
            // Счётчик повторений.
            var repeat = 0; 
            for (var i = 0; i < rating.Length; i++)
            {
                if ((i > 0) && (rating[i] == rating[i - 1])) 
                {
                    // Если присутствует одинаковый рейтинг.
                    for (var l = i; l != 0; l--)
                    {
                        // Считаем количество повторов этого рейтинга до текущего.
                        if (rating[l] == rating[l - 1]) repeat++; 
                        else break;
                    }

                }
                // Имя пользователя.
                string name = "";
                using StreamReader sr1 = new(path);
                // Цикл для поиска текущего рейтинга.
                for (var k = 0; (line = sr1.ReadLine() ?? "") != null; k++) 
                {
                    if (k % 5 == 0)
                    {
                        // Запоминание игрового имени.
                        name = line;
                    }
                    if ((k % 5 == 4) && (Convert.ToDouble(line) == rating[i]))
                    {
                        if (repeat > 0)
                        {
                            repeat--;
                            continue;
                        }
                        // Запись игрового имени с текущим рейтингом.
                        result[i, 0] = name;
                        // Запись текущего рейтинга.
                        result[i, 1] = Convert.ToString(rating[i]); 
                        break;
                    }
                }
                sr1.Close();
            }
            return result;
        }
    }
}
