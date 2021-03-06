using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreBL;
using StoreDL;
using StoreDL.Entities;

namespace StoreUI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuType)
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
                case "main":
                    return new MainMenu();

                case "browse":
                    return new BrowseMenu(new LocationBL(new LocationRepoDB(context, mapper)), new OrderBL(new OrderRepoDB(context, mapper)));

                case "admin":
                    return new AdminMenu();
                
                case "location":
                    return new LocationMenu(new LocationBL(new LocationRepoDB(context, mapper)), new ValidationService());

                case "product":
                    return new ProductMenu(new ProductBL(new ProductRepoDB(context, mapper)), new ValidationService());
                
                case "inventory":
                    return new InventoryMenu(new LocationBL(new LocationRepoDB(context, mapper)), new ProductBL(new ProductRepoDB(context, mapper)), new OrderBL(new OrderRepoDB(context, mapper)), new CustomerBL(new CustomerRepoDB(context, mapper)));

                case "profile":
                    return new ProfileMenu(new OrderBL(new OrderRepoDB(context, mapper)), new ProductBL(new ProductRepoDB(context, mapper)), new LocationBL(new LocationRepoDB(context, mapper)));

                default:
                    return null;
            }
        }
    }
}