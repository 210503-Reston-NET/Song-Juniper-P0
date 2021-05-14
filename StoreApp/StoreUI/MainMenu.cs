using System;
using StoreBL;
using StoreDL;

namespace StoreUI
{
    public class MainMenu : IMenu
    {
        private bool _login;

        public MainMenu()
        {
            _login = false;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                string input;
                if(!_login)
                {
                    Console.WriteLine("Welcome to the Wild Side Story!");
                    Console.WriteLine("We specialize in sourdough supplies and products");
                    Console.WriteLine("Have you shopped with us before? [y/n]");
                    input = Console.ReadLine();
                    switch(input.ToLower())
                    {
                        case "y":
                            _login = AuthMenuFactory.GetMenu("login").Start();
                        break;

                        case "n":
                            Console.WriteLine("Please sign up before continuing");
                            _login = AuthMenuFactory.GetMenu("signup").Start();
                        break;

                        case "42":
                            _login = true;
                            MenuFactory.GetMenu("admin").Start();
                        break;

                        default:
                            Console.WriteLine("I don't understand your input, please try again.");
                        break;
                    }
                }

                
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("[1] Browse Items");
                Console.WriteLine("[0] Exit");
                input = Console.ReadLine();
                switch(input)
                {
                    case "0":
                        Console.WriteLine("Goodbye, come back soon!");
                        repeat = false;
                    break;

                    case "1":
                        MenuFactory.GetMenu("browse").Start();
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