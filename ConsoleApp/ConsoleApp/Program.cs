namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            //приветствие
            Person.Time();
            // авторизация / регистрация имени произошла успешно.
            Person person = new();
            person.Init();
            person.Menu();
        }

    }
}