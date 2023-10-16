using System;

namespace ConsoleApp
{
    class Game
    {
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
        public static void User(ref Info info)
        {
            Data.CheckUser(ref info);
        }
        // Метод генерации рандомного числа 
        public static int GeneratingNumber()
        {
            Random random = new Random();
            int n=random.Next(1000,10000); // генерация рандомного четырехзначного числа 
            return n;
        }
        // Игра быки и коровы . ПРОПИСАТЬ ИГРУ , КОТОРАЯ ВЫЧИСЛЯЕТ КОЛ-ВО БЫКОВ И КОРОВ
        public static void BullsАndCowsGame(int trial,int number,out int bull, out int cow)
        {
            
        }

        public static void Update(Info info)
        {
            Data.Update(info);
        }
    }
}
