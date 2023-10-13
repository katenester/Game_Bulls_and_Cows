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
            // авторизация / регистрация имени произошла успешно.
            Person person = new();
            //person.Init();
            person.Menu();
        }

    }
}