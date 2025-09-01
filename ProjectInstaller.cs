using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace DatabaseBackupService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            // Service account
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Service configuration
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "DatabaseBackupService",
                DisplayName = "Automated Database Backup Service",
                Description = "This service is responsible for backing up my database every x minutes",
                StartType = ServiceStartMode.Manual, //NOTE: Start it manually for testing, go to Services.msc to start it

                // Define dependencies
                ServicesDependedOn = new string[]
                {
                    "MSSQLSERVER", // SQL Server default instance (adjust for named instances)
                    "RpcSs",       // Remote Procedure Call
                    "EventLog"     // Windows Event Log
                }


            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);

        }
    }
}
