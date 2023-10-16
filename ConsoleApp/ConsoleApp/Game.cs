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
            int n;
            while (true)
            {
                n = random.Next(1000, 10000); // генерация рандомного четырехзначного числа 
                if (CheckUnique(n.ToString())) { break; }
            }
            return n;
        }

        // Метод, проверяющий подходит ли число пользователя под условия игры ( проверка явл ли четырехзначным и цифры уникальными)
        public static bool CheckUnique(string trial)
        {
            //Distinct() -  возвращает последовательность, содержащую только уникальные элементы из исходной последовательности.
            // Можно попробовать вручную , но пока пусть будет так. Проанализовать скорость работы !!!!!!!!!!
            return trial.Distinct().Count()==trial.Length;
        }

        // Игра быки и коровы . ПРОПИСАТЬ ИГРУ , КОТОРАЯ ВЫЧИСЛЯЕТ КОЛ-ВО БЫКОВ И КОРОВ
        public static void BullsАndCowsGame(string trial, string number, out int bull, out int cow)
        {
            bull = 0;
            cow = 0;
            for (int i = 0; i < 4; i++) //бежим по числу (строке)
            {
                // если полное совпадение, то бык
                if (trial[i] == number[i])
                {
                    bull++;
                }
                // если позиция не совпадает, но такая цифра есть в числе
                // number.Contains(trial[i]) - метод типа bool. Возвращает значение, указывающее, встречается ли указанный символ внутри этой строки.
                else if (number.Contains(trial[i]))
                {
                    cow++;
                }

            } 
        }
        public static void Update(Info info)
        {
            Data.Update(info);
        }
    }
}
