using System;
namespace StoreUI
{
    public class ValidationService
    {
        public string ValidateString(string prompt)
        {
            string response;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Console.ReadLine();
                repeat = String.IsNullOrWhiteSpace(response);
                if (repeat) Console.WriteLine("Please input a non empty string");
            } while(repeat);
            return response;
        }

        public double ValidateDouble(string prompt)
        {
            double response;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Double.Parse(Console.ReadLine());
                repeat = response < 0;
                if (repeat) Console.WriteLine("Please input a value greater than 0");
            } while(repeat);

            return response;
        }
    }
}