using System;
using System.Security.Cryptography.X509Certificates;


namespace ConsoleApp1
{
    class Cat
    {
        
        public string Name;
        public int Birthday;
        public Cat(string name, int birthday) { Name = name; Birthday = birthday; }
        public void MakeNoise()
        {
            Console.WriteLine($"{Name} мяукает");
        }
        public void GetAge()
        {
            Console.WriteLine($"Кошке по имени {Name} уже {Birthday} лет") ; 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cat Boris = new Cat("Boris", 10);
            Boris.MakeNoise();
            Boris.GetAge();
        }
    }
}
