namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            // Приветствие.
            Person.Time();
            // Объявление объекта структуры Info.
            Info info = new Info(); 
            // Инициализация пользователя.
            Person person = new(ref info);
            person.Menu(ref info);
        }
    }
}