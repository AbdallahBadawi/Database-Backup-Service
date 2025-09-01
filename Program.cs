using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // Run as a console application
                DatabaseBackupService service = new DatabaseBackupService();
                service.StartInConsole();
            }
            else
            {
                // Run as a Windows Service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DatabaseBackupService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
