# Database Backup Service

A Windows Service developed in C# (.NET) to automate SQL Server database backups with log generation and storage management.

## 📋 Key Features

- 🔄 **Automated Backup** for SQL Server databases
- ⏰ **Flexible Scheduling** for backups (daily, weekly, monthly)
- 📁 **Backup File Management** with compression and storage options
- 📝 **Detailed Logging** for all operations and errors
- ⚙️ **Customizable Settings** via configuration files

## 🛠️ System Requirements

### Required Software
- **Operating System**: Windows 10/11 or Windows Server 2016+
- **.NET Framework**: 4.8 or later
- **SQL Server**: 2016 or later (Express, Standard, Enterprise)
- **Permissions**: Administrator privileges to install the service

## 📥 Download and Install Project

### 1. Download Project

```bash
# Clone project from GitHub
git clone https://github.com/AbdallahBadawi/Database-Backup-Service.git

# Navigate to project directory
cd Database-Backup-Service
```

### 2. Build Project

#### Using Visual Studio
1. Open the `.sln` file in Visual Studio
2. Select **Build Solution** from Build menu
3. Ensure **Release** configuration is selected

#### Using Command Line
```bash
# Build the project
dotnet build --configuration Release

# Or using MSBuild
msbuild DatabaseBackupService.sln /p:Configuration=Release
```

## 🚀 Deploy and Run Service

### 1. Install Service

#### Using sc command (Recommended)
```cmd
# Open Command Prompt as Administrator
# Navigate to build directory
cd C:\path\to\your\Database-Backup-Service\bin\Release

# Install the service
sc create "DatabaseBackupService" binPath="C:\path\to\your\Database-Backup-Service\bin\Release\DatabaseBackupService.exe" start=auto

# Set service description
sc description "DatabaseBackupService" "Automated Database Backup Service"
```

### 3. Run Service

#### Via Services Console
1. Press `Win + R` and type `services.msc`
2. Find "DatabaseBackupService"
3. Right-click → **Start**

#### Via Command Line
```cmd
# Start service
sc start "DatabaseBackupService"

# Stop service
sc stop "DatabaseBackupService"

# Query service status
sc query "DatabaseBackupService"
```

#### Via PowerShell
```powershell
# Start service
Start-Service -Name "DatabaseBackupService"

# Stop service
Stop-Service -Name "DatabaseBackupService"

# Get service status
Get-Service -Name "DatabaseBackupService"
```

## 📊 Monitoring and Log Tracking

### View Logs
```cmd
# View recent backup logs
type "C:\DatabaseBackups\Logs\backup_log.txt"

# Monitor logs in real-time
powershell Get-Content "C:\DatabaseBackups\Logs\backup_log.txt" -Wait -Tail 10
```

### Monitor Service Performance
```powershell
# Detailed service status
Get-WmiObject -Class Win32_Service -Filter "Name='DatabaseBackupService'" | 
  Select-Object Name, State, StartMode, ProcessId
```

## 🔧 Troubleshooting

### Common Issues

#### 1. Service Installation Failed
```cmd
# Check Administrator privileges
whoami /priv

# Check .NET Framework existence
reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release
```

#### 2. Database Connection Failed
- Verify connection string
- Ensure SQL Server is running
- Check user permissions

#### 3. Permission Issues
```cmd
# Grant full permissions to backup directory
icacls "C:\DatabaseBackups" /grant "NT AUTHORITY\LOCAL SERVICE":(OI)(CI)F
```

### Common Error Messages

| Error Message | Probable Cause | Solution |
|---------------|----------------|----------|
| "Service failed to start" | Insufficient permissions | Run as Administrator |
| "Database connection failed" | Connection String error | Review connection settings |
| "Backup path not accessible" | Path issue | Check directory existence and permissions |

## 🗑️ Uninstall Service

```cmd
# Stop service first
sc stop "DatabaseBackupService"

# Uninstall service
sc delete "DatabaseBackupService"

# Or using InstallUtil
InstallUtil.exe /u DatabaseBackupService.exe
```

## 👨‍💻 Developer

**Abdallah Badawi**
- GitHub: [@AbdallahBadawi](https://github.com/AbdallahBadawi)
---

⭐ If you like this project, don't hesitate to give it a star!
