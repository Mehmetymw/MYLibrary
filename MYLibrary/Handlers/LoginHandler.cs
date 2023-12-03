namespace MYLibrary.Handlers
{
    public static class LoginHandler
    {
        public static bool IsLogined(IConfiguration configuration,string password)
        {
            string apiSecret = configuration["LibraryService:Pass"];

            return password == apiSecret;
        }
        public static bool IsSessionValid(string sessionid)
        {
            return false; //TODO Session kontrol edilecek;
        }
    }
}
