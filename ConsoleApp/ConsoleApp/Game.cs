using System;

namespace ConsoleApp
{
    class Game
    {
        public int number;
        public int bull;
        public int cow;

        // метод отображения лучших игровов
        public static void BestPlayers()
        {
            
        }
        // метод для новой игры
        public static void NewPlay(string userName)
        {
            
        }
        // метод для создания числа ( здесь компьютер загадывает число). userName нужен для занесения в бд при сохранении незавершенной игры
        public static int HiddenNumber(string userName)
        {
            return 0;
        }
        // Метод, возвращающий true - если пользователь найден , false - если отсутствует
        public static bool Check(string UserName)
        {
            //ТУТ ЛОГИКА ТОЖЕ СЛОМАЛАСЬ 
            //if (Data.CheckUser(UserName)) { return true; }
            //else
            return false;
        }
        /*/ метод инициализации старого пользователя
        public static void Init(string userName)
        {
            
        }
        // метод создания нового пользвоателя в бд*/
        // метод авторизации/ регистрации пользователя 
        public static string[] User(string UserName)
        {
            return Data.CheckUser(UserName);
        }
    }
}
