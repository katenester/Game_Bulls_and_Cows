using System.Security.Cryptography;

namespace ConsoleApp
{
    enum Day
    {
        morning,
        afternoon,
        evening,
    }
    internal class Program
    {
        // Авторизация.
        // P.S. тут будет проверка есть ли пользователь в системе , регистрация нового пользователя, авторизация предыдущего
        public static string CheckUserName()
        {
            Console.Write("Введите игровое имя: ");
            string UserName = Console.ReadLine();
            // дописать проверки + что-то сделать с not null - метод isnullorempty 
            return UserName;
        }

        // Предлагаю проверку делать тут, чтобы сделать код менее грамоздким и избежать повторения кода
        public static int TryInt()
        {
            // мб следует сделать через try catch
            while (true)
            {
                Console.WriteLine("Введите число:");
                if (int.TryParse(Console.ReadLine(), out var value)) { return value; }
                else { Console.WriteLine("Некорректный формат введенных данных."); }
            }
        }
        // Метод новой игры
        public static void NewGame(string userName)
        {
            int x = Game.HiddenNumber(userName);
            bool newflag = true;
            while (newflag)
            {
                Console.WriteLine("Выберите номер из списка :");
                Console.WriteLine("1. Ввести число");
                // Если пользователь хочет вернуться к этой игре позже 
                Console.WriteLine("0. Сохранение игры/выход");
                int n = TryInt();
                switch (n)
                {
                    case 0:
                        newflag = false;
                        break;
                    case 1:
                        // попытка пользователя отгадать число
                        int trying = TryInt();
                        // количество быков
                        int bull;
                        //количество коров
                        int cow;
                        if (Game.Check(ref bull,ref cow,)
                }
            }
        }
        // Загрузка текущей игры 
        public static void OldGame(string userName)
        {

        }
        static void Main()
        {
            // Получение текущего времени
            DateTime dateTime = DateTime.Now;
            // Получение текущего часа
            int hour = dateTime.Hour;
            if (hour > 5 && hour < 12)
            {
                Console.WriteLine($"Good {Day.morning}");
            }
            else if (hour >= 12 && hour < 19)
            {
                Console.WriteLine($"Good {Day.afternoon}");
            }
            else
            {
                Console.WriteLine($"Good {Day.evening}");
            }
            // авторизация / регистрация имени произошла успешно.
            string UserName = CheckUserName();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Выберите номер из списка :");
                Console.WriteLine("1. Загрузка прошлой игры");
                Console.WriteLine("2. Создание новой игры");
                Console.WriteLine("3. Отображение таблицы лучших игроков");
                Console.WriteLine("0. Выход");
                int n = TryInt();
                switch (n)
                {
                    case 0:
                        Console.WriteLine("Закрытие игры");
                        flag = false;
                        break;
                    case 1:
                        OldGame(UserName);
                        break;
                    case 2:
                        NewGame(UserName);
                        break;
                    case 3:
                        // метод в game вывода таблицы лучших игроков 
                        Game.BestPlayers();
                        break;
                    default:
                        Console.WriteLine("Данный пункт отсутствует. Попробуйте ещё раз:");
                        break;

                }
            }
        }
    }
}