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
            Info info = new (); 
            // Инициализация пользователя.
            Person person = new(ref info);
            Person.Menu(ref info);
        }
    }
}