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
            //Читаем первую строчку файла, это нужно, чтобы в цикле дойти до конца файла
            line = sr.ReadLine();
            //Пока не дойдём до конца файла или не найдём имя 
            while ((line != null) && (line != UserName))
            {
                //выводим строку
                Console.WriteLine(line);
                //переходим на следующую
                line = sr.ReadLine();
            }
            
            if (line == null) //если пользователя нет
            {
                info[0] = UserName;
                for (int i = 1; i<info.Length;i++)
                {
                    info[i] = "0";
                }
                for (int i = 0; i < info.Length; i++)
                {
                    Console.WriteLine(info[i]);
                }
                sr.Close();
                return info;
            }

            else //если пользователь есть 
            {
                for (int i = 0; i < info.Length; i++)
                {
                    info[i] = line;
                    line = sr.ReadLine();
                }
                for (int i = 0; i < info.Length; i++)
                {
                    Console.WriteLine(info[i]);
                }
                sr.Close();
                return info;
            }
            
        }
        // метод добавления нового Пользователя в бд. Заполняем имя + 4 пустые строки
        public static void AddNewUser(string UserName)
        {

        }
    }
}
