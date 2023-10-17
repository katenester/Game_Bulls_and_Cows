using System;

namespace ConsoleApp
{
    public struct Info
    {
        public string userName { get; set; } //зачем писать паблик, если это структура? Там все поля паблик по умолчанию вроде
        public string textGame { get; set; }
        public int countAttempt { get; set; }
        public int number { get; set; }
        public double rating { get; set; }
    }
}
