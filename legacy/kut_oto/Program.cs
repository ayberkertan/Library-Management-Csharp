namespace kut_oto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // DataDirectory'yi uygulama klasörüne ayarlıyoruz (LocalDB .mdf dosyası için)
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", appDir);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // İlk çalıştırmada LocalDB veritabanını otomatik oluştur
            DatabaseHelper.EnsureDatabaseExists();

            Application.Run(new Form1());
        }
    }
}