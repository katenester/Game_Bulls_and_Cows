using System;
using ConsoleApp;
using System.IO;

namespace ConsoleApp
{
    class Data
    {
        /// <summary>
        /// Метод инициализации структуры.
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void CheckUser(ref Info info)
        {
            // Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt.
            string path = @"c:\temp\1.txt"; 
            string line; 
            StreamReader sr = new StreamReader(path);
            // Пока не дойдём до конца файла или не найдём имя читаем файл построчно.
            for (int i = 0; (line = sr.ReadLine()) != null; i++) 
            {
                if (i % 5 == 0 && line == info.userName)
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
                info.textGame = sr.ReadLine();
                info.countAttempt = int.Parse(sr.ReadLine());
                info.number = int.Parse(sr.ReadLine());
                info.rating = double.Parse(sr.ReadLine());
                sr.Close();
            }

        }

        /// <summary>
        /// Метод добавления нового пользователя в бд. Добавляем имя + 4 пустые строки (в действительности для вэлью типов записывается 0).
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void AddNewUser(Info info)
        {
            string path = @"c:\temp\1.txt";
            // Добавляем true, чтобы информация записывалась в конец файла, а не заменяла содержимое.
            StreamWriter sw = new StreamWriter(path, true); 
            sw.WriteLine(info.userName);
            sw.WriteLine(info.textGame);
            sw.WriteLine(info.countAttempt);
            sw.WriteLine(info.number);
            sw.WriteLine(info.rating);
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
            using (StreamReader sr = new StreamReader(path)) // читаем
            using (StreamWriter sw = new StreamWriter(tempPath)) // и сразу же пишем во временный файл ПОПРОБОВАТЬ БЕЗ ФОЛС 
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (lineIndex == i)
                        sw.WriteLine(newValue);
                    else
                        sw.WriteLine(line);
                    i++;
                }
            }
            File.Delete(path); // удаляем старый файл
            File.Move(tempPath, path); // переименовываем временный файл
        }

        // метод обновления данных
        public static void Update(Info info)
        {
            //для начало находим номер строки, где лежит имя пользователя
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла

            StreamReader sr = new StreamReader(path);
            int numberLine = 0; //для сравнения
            for (int k = 0; (line = sr.ReadLine()) != null; k++) //бежим по файлику
            {
                if (line == info.userName) //нашли имя, оно лежит в line
                {
                    break; //выходим из цикла
                }
                numberLine++; //считаем на какой мы строчке
            }
            sr.Close();
            RewriteLine(path, numberLine + 1, info.textGame);
            RewriteLine(path, numberLine + 2, (info.countAttempt).ToString());
            RewriteLine(path, numberLine + 3, info.number.ToString());
            RewriteLine(path, numberLine + 4, info.rating.ToString());
        }

        public static int Counter() //количество игроков
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла

            StreamReader sr = new StreamReader(path);

            int counter = 0;
            for (int i = 0; (line = sr.ReadLine()) != null; i++)
            {
                if (i % 5 == 0)
                {
                    counter++;
                }
            }
            sr.Close();
            return counter;
        }
        //прописать логику, что делать, если у кого то совпадают рейтинги, если этого не сделать, то всегда будет выводиться игрок, который первый в файлике с заданным рейтингом
        //(счётчик повторений возможно ввести и если в отсортированном массиве два одиноковых рейтинга подряд,
        // то первого пользователя в бд с таким рейтингом пропускаем, ищем следующего и добаляем)
        //сделать юзинг или трай кетч
        public static string[,] Rating(string[,] result)
        {
            double[] rating = new double[Counter()];  //создать массив и заполнить его рейтингами всех игроков, потом отсортировать его

            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла
            StreamReader sr = new StreamReader(path);

            int j = 0;
            //Пока не дойдём до конца файла
            for (int i = 0; (line = sr.ReadLine()) != null; i++) //почему просто нельзя пойти с шагом 5?
            {
                if (i % 5 == 4)
                {
                    rating[j] = Convert.ToDouble(line);
                    j++;
                }
            }
            sr.Close();
            Array.Sort(rating); //массив рейтингов отсортирован по возрастанию
            Array.Reverse(rating); //теперь по убыванию

            //дальше берем первый элемент в массиве (максимальный рейтинг) и ищем в бд имя пользователя с таким рейтингом, заносим в наш двумерный массив и тд
            //только тогда надо продумать логику, что делать, если рейтинги каких-то игроков будут повторяться (счётчик повторений возможно
            //ввести и если в отсортированном массиве два одиноковых рейтинга подряд, то первого пользователя в бд с таким рейтингом пропускаем, ищем следующего и добаляем)
            string name = ""; //имя
            int repeat = 0; //счётчик повторений
            for (int i = 0; i < rating.Length; i++)
            {
                if (i > 0 && rating[i] == rating[i - 1]) //если одинаковый рейтинг у кого-то
                {
                    for (int l = i; l != 0; l--)
                    {
                        if (rating[l] == rating[l - 1]) repeat++; // 5 4 4 4
                        else break;
                    }

                }
                name = "";
                StreamReader sr1 = new StreamReader(path);
                for (int k = 0; (line = sr1.ReadLine()) != null; k++) //бежим по файлику и ищем нужный нам рейтинг
                {
                    if (k % 5 == 0)
                    {
                        name = line;// запомнили имя
                    }
                    if (k % 5 == 4 && Convert.ToDouble(line) == rating[i])
                    {
                        if (repeat > 0)
                        {
                            repeat--;
                            continue;
                        }
                        //мы нашли, в какой строке находится нужный нам рейтинг, теперь ищем имя пользователя, которому принадлежит этот рейтинг
                        //заполняем двумерный массив
                        result[i, 0] = name; //имя
                        result[i, 1] = Convert.ToString(rating[i]); //рейтинг
                        break;
                    }
                }
                sr1.Close();

            }
            return result;
        }
    }
}
