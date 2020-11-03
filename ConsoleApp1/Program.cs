using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading;


namespace ConsoleApp1
{
    class Cat
    {
        public string Name;
        public DateTime Birthday;
        public byte _hungryStatus;
        public byte HungryStatus
        {
           get
           {
               return _hungryStatus;
           }

           set
           {
                if (value < 0) { value = 0; }
                if (value > 100) { value = 100; }
                _hungryStatus = value;
           }
        }
        public Cat(string name, DateTime birthday, byte _hungryStatus) { Name = name; Birthday = birthday; HungryStatus = _hungryStatus; Task.Run(LifeCircle); }
        public void GetAge()
        {
            int a;
            a = DateTime.Now.Year - Birthday.Year;
            Console.WriteLine($"Возраст: {a}") ;  
        }
        public void GetStatus()
        {
            Console.WriteLine(Name);
            GetAge();
            if (HungryStatus<=10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Кошка умирает от голода");
                Console.ResetColor();
            }
            if (HungryStatus > 10 & HungryStatus <= 40)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Кошка очень голодна");
                Console.ResetColor();
            }
            if (HungryStatus > 40 & HungryStatus <= 70)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Кошка хочет кушать");
                Console.ResetColor();
            }
            if (HungryStatus > 70 & HungryStatus <= 90)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Кошка не против перекусить");
                Console.ResetColor();
            }
            if (HungryStatus > 90)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Кошка недавно поела");
                Console.ResetColor();
            }
        }
        async Task LifeCircle()
        {
            await Task.Delay(10000);
            HungryStatus -= 10;
            GetStatus();
            await LifeCircle();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cat Barsik = new Cat("Барсик",new DateTime(2015, 7, 20),150);
            Barsik.GetStatus();
            Console.ReadLine();
        }
    }
}
