using System;
using ConsoleApp;
using System.IO;

namespace ConsoleApp
{
    class Data
    {

        // Метод, возвращающий информацию о пользователе в виде структуры ОНО РАБОТАЕТ!!!
        public static void CheckUser(ref Info info)
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла

            StreamReader sr = new StreamReader(path);

            //Пока не дойдём до конца файла или не найдём имя
            for (int i = 0; (line = sr.ReadLine()) != null; i++) //почему просто нельзя пойти с шагом 5?
            {
                if (i % 5 == 0 && line == info.userName)
                {
                    // строка является каждой пятой строкой, и она равна "UserName"
                    break;
                }
            }
            //если пользователя нет, то структура остаётся без изменений ( по умолчанию она проинициализируется 0 или null)
            if (line == null)
            {
                sr.Close();
                AddNewUser(info);// метод добавления нового Пользователя в бд
            }

            else //если пользователь есть 
            {   //info.userName уже проинициализоривана. В цикле поиска указатель в sr.ReadLine() поставлен на текст игры
                info.textGame = sr.ReadLine();
                info.countAttempt = int.Parse(sr.ReadLine());
                info.number = int.Parse(sr.ReadLine());
                info.rating = double.Parse(sr.ReadLine());
                sr.Close();
            }

        }
        // метод добавления нового Пользователя в бд. Заполняем имя + 4 пустые строки
        public static void AddNewUser(Info info)
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            StreamWriter sw = new StreamWriter(path, true); // true добавляем в конструктор, чтобы записовалась в конец файла , а не заменять содержимое
            sw.WriteLine(info.userName);
            sw.WriteLine(info.textGame);
            sw.WriteLine(info.countAttempt);
            sw.WriteLine(info.number);
            sw.WriteLine(info.rating);
            sw.Close();
        }
        // метод обновления данных
        public static void Update(Info info)
        {

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
            int numberLine = 0; //строка, на которой находимся 
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
                numberLine = 0;
                StreamReader sr1 = new StreamReader(path);
                for (int k = 0; (line = sr1.ReadLine()) != null; k++) //бежим по файлику и ищем нужный нам рейтинг
                {
                    numberLine++; //считаем на какой мы строчке
                    if (k % 5 == 4 && Convert.ToDouble(line) == rating[i])
                    {
                        if (repeat > 0)
                        {
                            repeat--;
                            continue;
                        }
                        /*                        if (i > 0 && rating[i] == rating[i - 1]) НЕ РАБОТАЕТ, в этом случае вообще не находит 
                                                {
                                                    //numberLine--; 
                                                    continue; //если рейтинги совпадают у кого то
                                                }*/
                        break; //мы нашли, в какой строке находится нужный нам рейтинг, теперь ищем имя пользователя, которому принадлежит этот рейтинг
                    }
                }
                StreamReader sr2 = new StreamReader(path);
                numberLine -= 4; //имя пользователя ледит на 4 строки выше
                int numberLine2 = 0; //для сравнения
                for (int k = 0; (line = sr2.ReadLine()) != null; k++) //снова бежим по файлику
                {
                    numberLine2++; //считаем на какой мы строчке
                    if (numberLine2 == numberLine) //нашли имя, оно лежит в line
                    {

                        //заполняем двумерный массив
                        result[i, 0] = line; //имя
                        result[i, 1] = Convert.ToString(rating[i]); //рейтинг
                        break; //выходим из цикла
                    }
                }
            }
            return result;
        }
    }
}
