namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            //Data.CheckUser("a");
            //Console.WriteLine(Data.CheckUser("")); 
            //приветствие
            Person.Time();
            // Объявление объекта структуры Info
            Info info = new Info(); 
            // авторизация / регистрация имени произошла успешно.
            Person person = new(ref info);
            //person.Init();
            person.Menu(ref info);
        }

    }
}