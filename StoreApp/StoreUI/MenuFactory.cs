using StoreBL;
using StoreDL;

namespace StoreUI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuType)
        {
            switch (menuType.ToLower())
            {
                case "main":
                    return new MainMenu();

                case "browse":
                    return new BrowseMenu(new LocationBL(new LocationRepo()), new ProductBL(new ProductRepo()));

                case "admin":
                    return new AdminMenu();
                
                case "location":
                    return new LocationMenu(new LocationBL(new LocationRepo()), new ValidationService());

                case "product":
                    return new ProductMenu(new ProductBL(new ProductRepo()), new ValidationService());
                
                case "inventory":
                    return new InventoryMenu();

                default:
                    return null;
            }
        }
    }
}