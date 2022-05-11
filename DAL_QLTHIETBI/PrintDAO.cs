namespace DAL_QLTHIETBI
{
    public class PrintDAO
    {
        private static PrintDAO instance;

        public static PrintDAO Instance
        {
            get { if (instance == null) instance = new PrintDAO(); return instance; }
            private set { instance = value; }
        }

        public PrintDAO() { }
       
    }
}
