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
        static void Main()
        {
            // Получение текущего времени
            DateTime dateTime = new DateTime();
            // Получение текущего часа
            int hour=dateTime.Hour;
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
            string UserName= CheckUserName(); // авторизация / регистрация имени произошла успешно.
            Console.WriteLine("Выберите из списка номер:");
            Console.WriteLine("1. Загрузка предыдущей игры");
            Console.WriteLine("2. Создание новой игры");
            int x = TryInt();




        }
    }
}