using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Cat
    {
        public string Name;
        public DateTime Birthday;
        public sbyte _hungryStatus;
        public event EventHandler HungryStatusChanged;
        public sbyte HungryStatus
        {
            get
            {
                return _hungryStatus;
            }
            set
            {
                sbyte a = 0;
                a = value;
                if (value < 0) { value = 0; }
                if (value > 100) { value = 100; }
                if (a < value) { HungryStatusChanged?.Invoke(this, null); }
                _hungryStatus = value;
            }
        }
        public Cat(string name, DateTime birthday, sbyte _hungryStatus) { Name = name; Birthday = birthday; HungryStatus = _hungryStatus; Task.Run(LifeCircle); }
        public void GetAge()
        {
            int a;
            a = DateTime.Now.Year - Birthday.Year;
        }
        public string GetStatus()
        {
            int BirthDay;
            BirthDay = DateTime.Now.Year - Birthday.Year;
            string col = null;
            string s1 = null;
            if (HungryStatus <= 10)
            {
                col = "1";
                s1 = "Кошка умирает от голода";
            }
            if (HungryStatus > 10 & HungryStatus <= 40)
            {
                col = "1";
                s1 = "Кошка очень голодна";
            }
            if (HungryStatus > 40 & HungryStatus <= 70)
            {
                col = "2";
                s1 = "Кошка хочет кушать";
            }
            if (HungryStatus > 70 & HungryStatus <= 90)
            {
                col = "2";
                s1 = "Кошка не против перекусить";
            }
            if (HungryStatus > 90)
            {
                col = "3";
                s1 = "Кошка недавно поела";
            }
            string status = col + " " + Name + " " + BirthDay + " " + s1;
            return status;
        }
        async Task LifeCircle()
        {
            await Task.Delay(10000);
            HungryStatus -= 10;
            await LifeCircle();
        }
        public void Feed(sbyte feed = 50)
        {
            HungryStatus += feed;
        }
    }
    class CatSmartHouse
    {
        static object printing = true;
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
                sbyte needFood = (sbyte)(100 - cat.HungryStatus);
                if (FoodResource > needFood)
                    FoodResource -= needFood;
                else
                {
                    needFood = (sbyte)FoodResource;
                    FoodResource = 0;
                }
                cat.Feed(needFood);
                PrintStatus();
            }
        }

        public void PrintStatus()
        {
            lock (printing)
            {
                int leftPosition = Console.CursorLeft;
                int topPosition = Console.CursorTop;
                for (int i = 0; i < cats.Count; ++i)
                {
                    string message = cats[i].GetStatus();
                    string color = message.Substring(0, 1);
                    color = color.Replace(" ", "");
                    int coloR = Convert.ToInt32(color);
                    Console.SetCursorPosition(0, i);
                    if (coloR == 3) { Console.ForegroundColor = ConsoleColor.Green; }
                    if (coloR == 2) { Console.ForegroundColor = ConsoleColor.Yellow; }
                    if (coloR == 1) { Console.ForegroundColor = ConsoleColor.Red; }
                    Console.Write(message.Substring(2, message.Length));
                    message = message.PadRight(50);
                    Console.ResetColor();
                }
                Console.SetCursorPosition(0, cats.Count);
                Console.Write(FoodResource);
                Console.SetCursorPosition(leftPosition, topPosition);
            }
        }
        public int CatsCount
        {
            get
            {
                return cats.Count;
            }
        }
    }
    class CommandCenter
    {
        public CommandCenter(int CatSmartHouse)
        {
            int _CatSmartHouse;
            _CatSmartHouse = CatSmartHouse;
            WaitCommands();
        }
        public void WaitCommands()
        { 
          string command = null;
            while (command != "exit")
            {
                Console.SetCursorPosition(0,2 + 1);
                command = Console.ReadLine();
                string[] array = command.Split();
                if (array[0] == "store")
                {
                    int foodresource = Convert.ToInt32(array[2]);
                    FoodResource += foodresource;
                    Console.WriteLine($"Кошка покормлена: {array[1]}");
                }
                if (array[0] == "help")
                {
                    Console.WriteLine($"Доступные команды: store <еда> <кол-во порций>");
                }
            }
        } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            CatSmartHouse cat = new CatSmartHouse(300);
            Cat Barsik = new Cat("Барсик",new DateTime(2015, 7, 20),100);
            cat.AddCat(Barsik);
            Barsik.GetStatus();
            Cat Matroskin = new Cat("Матроскин", new DateTime(2012, 8, 23),100);
            cat.AddCat(Matroskin);
            Matroskin.GetStatus();
            Console.SetCursorPosition(1,2 + 1) ;
            Console.ReadLine();
        }
    }
}
