namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            string bull = @"
             (__)
             (oo)
       /------\/
      / |    ||
     *  ||----||
        ^^    ^^";
            // Приветствие.
            Person.Time();
            Console.WriteLine(bull);
            // Объявление объекта структуры Info.
            Info info = new Info(); 
            // Инициализация пользователя.
            Person person = new(ref info);
            person.Menu(ref info);
        }
    }
}