using System;

namespace StoreUI
{
    public class MainMenu : IMenu
    {
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Our Store!");
                Console.WriteLine("Some stuff about store here");
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("[1] Browse Items");
                Console.WriteLine("[2] See My Profile");
                Console.WriteLine("[0] Exit");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "0":
                        Console.WriteLine("Goodbye, come back soon!");
                        repeat = false;
                    break;

                    case "1":
                        MenuFactory.GetMenu("browse").Start();
                    break;
                    
                    case "2":
                        MenuFactory.GetMenu("profile").Start();
                    break;
                    
                    case "42":
                        MenuFactory.GetMenu("admin").Start();
                    break;
                    
                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }
    }
}