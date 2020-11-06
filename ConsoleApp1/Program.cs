using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;


namespace ConsoleApp1
{
    class Cat
    {
        public string Name;
        public DateTime Birthday;
        public byte _hungryStatus;
        public event EventHandler HungryStatusChanged;
        public byte HungryStatus
        {
            get
            {
                return _hungryStatus;
            }
            set
            {
                byte a = 0;
                a = value;
                if (value < 0) {value = 0;}
                if (value > 100) { value = 100; }
                if (a != value) { HungryStatusChanged?.Invoke(this, null); }
                _hungryStatus = value;
            }
        }
        public Cat(string name, DateTime birthday, byte _hungryStatus) { Name = name; Birthday = birthday; HungryStatus = _hungryStatus; Task.Run(LifeCircle); }
        public void GetAge()
        {
            int a;
            a = DateTime.Now.Year - Birthday.Year;
            Console.WriteLine($"Возраст: {a}");
        }
        public void GetStatus()
        {
            Console.WriteLine(Name);
            GetAge();
            if (HungryStatus <= 10)
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
            await LifeCircle();
        }
        public void Feed(byte feed = 50)
        {
            HungryStatus += feed;
        }
    }
    class CatSmartHouse
    {
        public int FoodResource;
        public List<Cat> cats = new List<Cat>();
        public CatSmartHouse(int foodResource) { FoodResource = foodResource; }
        public void AddCat(Cat cat)
        {
            cats.Add(cat);
            cat.HungryStatusChanged += Cat_HungryStatusChanged;
        }
        private void Cat_HungryStatusChanged(object sender, EventArgs e)
        {
            var cat = (Cat)sender;
            if (cat.HungryStatus <= 20 && FoodResource > 0)
            {
                byte needFood = (byte)(100 - cat.HungryStatus);
                if (FoodResource > needFood)
                    FoodResource -= needFood;
                else
                {
                    needFood = (byte)FoodResource;
                    FoodResource = 0;
                }
                cat.Feed(needFood);
                Console.WriteLine($"Покормлена кошка: {cat.Name}\nОстаток еды в вольере: {FoodResource}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cat Barsik = new Cat("Барсик",new DateTime(2015, 7, 20),150);
            Barsik.GetStatus();
            Cat Matroskin = new Cat("Матроскин", new DateTime(2012, 8, 23),150);
            Matroskin.GetStatus();
            CatSmartHouse cat = new CatSmartHouse(200);
            cat.AddCat(Barsik);
            cat.AddCat(Matroskin);
            Console.ReadLine();
        }
    }
}
