namespace DataHelpers
{
    public class DataHelper
    {
        private static DataHelper instance = null;
        public static DataHelper getInstance()
        {
            if (instance == null)
            {
                instance = new DataHelper();
            }

            return instance;
        }
        
        private const string SERVER_URL = "http://192.168.3.8:5000";
        private DataHelper() {}
        
        
    }   
}