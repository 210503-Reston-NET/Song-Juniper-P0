using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreBL;
using StoreDL;
using StoreDL.Entities;

namespace StoreUI
{
    public class AuthMenuFactory
    {
        public static IAuthMenu GetMenu(string menuType)
        {
            //getting configurations from a config file
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //setting up my db connections
            string connectionString = configuration.GetConnectionString("wssdb");
            //we're building the dbcontext using the constructor that takes in options, we're setting the connection
            //string outside the context def'n
            DbContextOptions<wssdbContext> options = new DbContextOptionsBuilder<wssdbContext>()
            .UseSqlServer(connectionString)
            .Options;
            //passing the options we just built
            var context = new wssdbContext(options);
            IMapper mapper = new StoreMapper();

            switch (menuType.ToLower())
            {
                case "login":
                    return new LoginMenu(new CustomerBL(new CustomerRepoDB(context, mapper)));

                case "signup":
                    return new SignupMenu(new CustomerBL(new CustomerRepoDB(context, mapper)), new ValidationService());

                default:
                    return null;
            }
        }
    }
}