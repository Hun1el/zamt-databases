using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PR28
{
    public static class Backup
    {
        public static void BackupDatabase()
        {
            try
            {
                string host = PR28.Properties.Settings.Default.DbHost;
                string user = PR28.Properties.Settings.Default.DbUser;
                string password = PR28.Properties.Settings.Default.DbPassword;
                string database = PR28.Properties.Settings.Default.DbName;

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string folderName = "Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFolderPath = Path.Combine(appDirectory, folderName);
                Directory.CreateDirectory(backupFolderPath);

                string backupFileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

                CreateDump(host, user, password, database, backupFilePath, showMessage: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка резервного копирования:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void BackupDatabaseManual(string backupFilePath)
        {
            try
            {
                string host = PR28.Properties.Settings.Default.DbHost;
                string user = PR28.Properties.Settings.Default.DbUser;
                string password = PR28.Properties.Settings.Default.DbPassword;
                string database = PR28.Properties.Settings.Default.DbName;

                string folder = Path.GetDirectoryName(backupFilePath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                CreateDump(host, user, password, database, backupFilePath, showMessage: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка резервного копирования:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDump(string host, string user, string password, string database, string backupFilePath, bool showMessage)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "mysqldump",
                Arguments = $"-h {host} -u {user} --default-character-set=utf8mb4 --databases {database}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8
            };

            using (Process process = Process.Start(psi))
            {
                string result = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                File.WriteAllText(backupFilePath, result, new System.Text.UTF8Encoding(false));
            }

            if (showMessage)
            {
                MessageBox.Show($"Резервная копия создана:\n{backupFilePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
