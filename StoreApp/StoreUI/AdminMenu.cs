using System;

namespace StoreUI
{
    public class AdminMenu : IMenu
    {
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to the admin menu");
                
                string input = Console.ReadLine();
                switch(input)
                {

                }
            } while(repeat);
        }
    }
}