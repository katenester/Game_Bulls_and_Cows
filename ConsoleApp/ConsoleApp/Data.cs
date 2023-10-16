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
            for (int i = 0; (line = sr.ReadLine()) != null; i++)
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
            
        }
    }
}
