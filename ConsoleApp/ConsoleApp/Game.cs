using System;

namespace ConsoleApp
{
    class Game
    {
        /// <summary>
        /// Метод для составления таблицы лучших игроков.
        /// </summary>
        /// <returns>Двухмерный массив, состоящий из имен и соответствующего рейтинга игровов, в порядке убывания рейтинга.</returns>
        public static string[,] BestPlayers()
        {
            // Инициализация двухмерного массива.
            string[,] result = new string[Data.Counter(), 2];
            // Присваивание двухмерному массиву итоговый результат.
            result = Data.Rating(result);
            return result;
        }

        /// <summary>
        /// Метод для авторизации/авторизации пользователя.
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void User(ref Info info)
        {
            Data.CheckUser(ref info);
        }

        /// <summary>
        /// Метод генерации рандомного числа. 
        /// </summary>
        /// <returns>Рандомное четырехзначное число.</returns>
        public static int GeneratingNumber()
        {
            Random random = new Random();
            int n;
            while (true)
            {
                // Генерация рандомного четырехзначного числа от 1000 до 9999.
                n = random.Next(1000, 10000); 
                // Проверка уникальности всех цифр в числе.
                if (CheckUnique(n.ToString())) { break; }
            }
            return n;
        }

        /// <summary>
        /// Проверка уникальности/различности цифр в числе.
        /// </summary>
        /// <param name="trial">Число для проверки.</param>
        /// <returns>Если число с уникальными цифрами возвращаем true, иначе false.</returns>
        public static bool CheckUnique(string trial)
        {
            // Distinct() -  возвращает последовательность, содержащую только уникальные элементы из исходной последовательности.
            return trial.Distinct().Count()==trial.Length;
        }

        /// <summary>
        /// Игра быки и коровы.
        /// </summary>
        /// <param name="trial">Введеное число пользователем/попытка.</param>
        /// <param name="number">Загаданное число.</param>
        /// <param name="bull">Количество полных совпадений (количество быков).</param>
        /// <param name="cow">Количество угаданного без совпадения с их позициями цифр (количество коров).</param>
        public static void BullsАndCowsGame(string trial, string number, out int bull, out int cow)
        {
            bull = 0;
            cow = 0;
            // Проход по строке, состоящей из цифр загаданного числа.
            for (int i = 0; i < 4; i++) 
            {
                // При полном совпадении цифры загаданного числа с числом, введенным пользователем прибавляем число быков.
                if (trial[i] == number[i])
                {
                    bull++;
                }
                // При несовпадении позиции, но наличием цифры загаданного числа с числом, введенным пользователем прибавляем число коров.
                // number.Contains(trial[i]) - метод типа bool. Возвращает значение, указывающее, встречается ли указанный символ внутри этой строки.
                else if (number.Contains(trial[i]))
                {
                    cow++;
                }

            } 
        }

        /// <summary>
        /// Обновление данных.
        /// </summary>
        /// <param name="info">Структура, в которой хранится информация о пользователе.</param>
        public static void Update(Info info) 
        {
            Data.Update(info);
        }

        /// <summary>
        /// Количество пользователей из базы данных.
        /// </summary>
        /// <returns>Количество пользователей.</returns>
        public static int Counter() 
        {
            return Data.Counter();
        }
    }
}
