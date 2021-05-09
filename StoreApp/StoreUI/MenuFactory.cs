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
                    return new BrowseMenu();

                case "profile":
                    return new ProfileMenu();

                case "admin":
                    return new AdminMenu();
                
                case "location":
                    return new LocationMenu();

                case "product":
                    return new ProductMenu();

                default:
                    return null;
            }
        }
    }
}