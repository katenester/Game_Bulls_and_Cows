using System;
using ConsoleApp;
using System.IO;
using System.Linq;

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
            StreamWriter sw = new StreamWriter(path,true); // true добавляем в конструктор, чтобы записовалась в конец файла , а не заменять содержимое
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
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла
            StreamReader sr = new StreamReader(path);
            StreamWriter sw = new StreamWriter(path, true);
        }

        public static int Counter() //количество игроков
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла

            StreamReader sr = new StreamReader(path);

            int counter = 0;
            for (int i = 0; (line = sr.ReadLine()) != null; i++) //почему просто нельзя пойти с шагом 5?
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
        public static string[,] Rating(string[,] result)
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла
            StreamReader sr = new StreamReader(path);
            int k = 0;
            //Пока не дойдём до конца файла
            for (int i = 0; (line = sr.ReadLine()) != null; i++) //почему просто нельзя пойти с шагом 5?
            {
                if (i % 5 == 0)
                {
                    result[k,0] = line; // 0 столбец - имя . Заполняем имя пользователя 
                }
                if (i % 5 == 4)
                {
                    result[k,1] = line; // 1 столбец - рейтинг . Заполняем рейтинг пользователя
                    k++; // переходим к следующей строке. Т.е заполняем данные для другого пользовалътеля 
                }
            }
            // Сортировка пузырьком. Можно для оптимизации взять другую сортировку 
            int rows = result.GetLength(0); // метод возвращает кол-во строк 
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < rows - 1 - i; j++)
                {
                    int rating1 = int.Parse(result[j, 1]);
                    int rating2 = int.Parse(result[j + 1, 1]);

                    if (rating1 < rating2)
                    {
                        // Обмен значений
                        string tempName = result[j, 0];
                        string tempRating = result[j, 1];
                        result[j, 0] = result[j + 1, 0];
                        result[j, 1] = result[j + 1, 1];
                        result[j + 1, 0] = tempName;
                        result[j + 1, 1] = tempRating;
                    }
                }
            }
            return result;


            /*double[] rating = new double[Counter()];  //создать массив и заполнить его рейтингами всех игроков, потом отсортировать его

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
            int numberLine = 0;
            for (int i = 0;i<rating.Length;i++)
            {
                numberLine = 0;
                StreamReader sr1 = new StreamReader(path);
                for (int k = 0; (line = sr1.ReadLine()) != null; k++) //бежим по файлику и ищем нужный нам рейтинг
                {
                    numberLine++; //считаем на какой мы строчке
                    if (k % 5 == 4 && Convert.ToDouble(line) == rating[i])
                    {
/*                        if (i > 0 && rating[i] == rating[i - 1]) НЕ РАБОТАЕТ, в этом случае вообще не находит 
                        {
                            //numberLine--; 
                            continue; //если рейтинги совпадают у кого то
                        }*/ /*
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
                        result[i,0] = line; //имя
                        result[i, 1] = Convert.ToString(rating[i]); //рейтинг
                        break; //выходим из цикла
                    }
                }
            }
            return result;*/

        }
    }
}
