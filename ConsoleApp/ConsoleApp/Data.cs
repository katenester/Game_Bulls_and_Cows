using System;
using System.IO;

namespace ConsoleApp
{
    class Data
    {

        // Метод, возвращающий информацию о пользователе в виде массива ОНО РАБОТАЕТ!!!
        public static string[] CheckUser(string UserName)
        {
            string[] info = new string[5];
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            string line; //для чтения файла

            StreamReader sr = new StreamReader(path);

            //Пока не дойдём до конца файла или не найдём имя
            for (int i = 0; (line = sr.ReadLine()) != null; i++)
            {
                if (i % 5 == 0 && line == UserName)
                {
                    // строка является каждой пятой строкой, и она равна "UserName"
                    break;
                }
            }

            if (line == null) //если пользователя нет
            {
                info[0] = UserName;
                for (int i = 1; i<info.Length;i++)
                {
                    info[i] = "0";
                }
                sr.Close();
                AddNewUser(info);// метод добавления нового Пользователя в бд
                return info;
            }

            else //если пользователь есть 
            {
                for (int i = 0; i < info.Length; i++)
                {
                    info[i] = line;
                    line = sr.ReadLine();
                }
                sr.Close();
                return info;
            }
            
        }
        // метод добавления нового Пользователя в бд. Заполняем имя + 4 пустые строки
        public static void AddNewUser(string[] info)
        {
            string path = @"c:\temp\1.txt"; //Перед этим нужно создать папку temp на диске С и в ней блокнот 1.txt
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < info.Length; i++)
            {
                sw.WriteLine(info[i]);
            }
            sw.Close();
        }
        // метод обновления данных
        public static void Update(string[] info)
        {
            
        }
    }
}
