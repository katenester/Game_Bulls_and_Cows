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
            Person.GetTime();
            Console.WriteLine(bull);
            //Объявление объекта структуры Info.
            Info info = new();
            // Инициализация пользователя.
            _ = new Person(ref info);
            Person.Menu(ref info);

        }
    }
}