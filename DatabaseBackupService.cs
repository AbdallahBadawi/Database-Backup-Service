using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseBackupService
{
    public partial class DatabaseBackupService : ServiceBase
    {
        private Timer backupTimer;
        private string connectionString;
        private string backupFolder;
        private string logFolder;
        private int backupIntervalMinutes;
     
        public DatabaseBackupService()
        {
            InitializeComponent();
            
           
            // Read configuration values
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            backupFolder = ConfigurationManager.AppSettings["BackupFolder"];
            logFolder = ConfigurationManager.AppSettings["LogFolder"];
            backupIntervalMinutes = int.TryParse(ConfigurationManager.AppSettings["BackupIntervalMinutes"], out int interval) ? interval : 60;


            // Ensure directories exist
            Directory.CreateDirectory(backupFolder);
            Directory.CreateDirectory(logFolder);

        }
        protected override void OnStart(string[] args)
        {

            Log("Service Started.");

            // Set up a timer to trigger backups periodically
            // backupTimer = new Timer(PerformBackup, null, TimeSpan.Zero, TimeSpan.FromMinutes(backupIntervalMinutes));
            // Initialize Timer to trigger PerformBackup
           
            backupTimer = new Timer(
                callback: PerformBackup,                  // Callback method
                state: null,                              // State object (not used here)
                dueTime: TimeSpan.Zero,                   // Start immediately
                period: TimeSpan.FromMinutes(backupIntervalMinutes) // Interval
            );


            Log($"Backup schedule initiated: every {backupIntervalMinutes} minute(s).");


        }
        protected override void OnStop()
        {

            backupTimer?.Dispose();
            Log("Service Stopped.");


        }



        private void Log(string message)
        {
            string logFilePath = Path.Combine(logFolder, "ServiceLog.txt");
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";

            File.AppendAllText(logFilePath, logMessage);

            // Output to console if running in debug mode
            if (Environment.UserInteractive)
            {
                Console.WriteLine(logMessage);
            }
        }
        private void PerformBackup(object state)
        {
            try
            {
                string backupFileName = Path.Combine(backupFolder, $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");

                // Perform database backup
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string backupQuery = $@"BACKUP DATABASE [{connection.Database}] TO DISK = '{backupFileName}' WITH INIT";
                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Log successful backup
                Log($"Database backup successful: {backupFileName}");

            }
            catch (Exception ex)
            {
                Log($"Error during backup: {ex.Message}");
            }
        }
        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();
            OnStop();
        }
    }
}
