using Domain.Entities;
using Domain.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Domain.UseCases
{
    public sealed class Automation
    {
        private IAlert _Alert;

        public Automation(IAlert alert)
        {
            _Alert = alert;
        }

        public void DeleteTempFiles()
        {
            string tempPath = Path.GetTempPath();
            
            double MemorySizeMB = GetFolderSize(tempPath);

            _Alert.Alert(_Alert.GetAlertTypeForAutomationItCleanTempDirInfo(), MemorySizeMB.ToString() + " MB WILL BE CLEANED IN TEMP FOLDER", EAlertLevel.INFO);

            CleanTempFolder(tempPath);

            double MemorySizeMBAfter = GetFolderSize(tempPath);

            double SizeDeleted = MemorySizeMB - MemorySizeMBAfter;

            _Alert.Alert(_Alert.GetAlertTypeForAutomationItCleanTempDirInfo(), "TEMP FOLDER WAS CLEANED - " + SizeDeleted.ToString() + " MB", EAlertLevel.INFO);            
        }

        public void DeleteInternetHistory()
        {

        }

        public void Reboot()
        {

        }

        private void CleanTempFolder(string path)
        {
            string[] filesName = Directory.GetFiles(path);

            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            string MessageUnauthorizedAccess = DeleteFiles(filesName);

            DirectoryInfo[] directories = directoryInfo.GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                MessageUnauthorizedAccess += DeleteFiles(Directory.GetFiles(directory.FullName));
                MessageUnauthorizedAccess += DeleteDirectoriesRecursive(directory);
            }

            if(string.IsNullOrWhiteSpace(MessageUnauthorizedAccess) == false)
            {
                _Alert.Alert(_Alert.GetAlertTypeForAutomationItCleanTempUnauthorized(), "FILES NOT AUTHORIZED: " + MessageUnauthorizedAccess, EAlertLevel.MEDIUM);
            }            
        }

        private string DeleteFiles(string[] filesName)
        {
            //TODO: Convert to Json to send it
            string FilesMessageUnauthorizedAccess = "";

            foreach (string filePath in filesName)
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (UnauthorizedAccessException)
                {
                    FilesMessageUnauthorizedAccess += filePath + " | ";
                }
                catch 
                { 
                }
            }

            return FilesMessageUnauthorizedAccess;
        }

        private string DeleteDirectoriesRecursive(DirectoryInfo directoryRoot)
        {
            string MessageUnauthorizedAccess = "";

            DirectoryInfo[] directories = directoryRoot.GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                MessageUnauthorizedAccess += DeleteFiles(Directory.GetFiles(directory.FullName));
                MessageUnauthorizedAccess += DeleteDirectoriesRecursive(directory);

                try
                {
                    Directory.Delete(directory.FullName);
                }
                catch
                {

                }
            }

            try
            {
                Directory.Delete(directoryRoot.FullName);
            }
            catch
            {

            }

            return MessageUnauthorizedAccess;
        }

        private double GetFolderSize(string path)
        {
            double MemorySizeMB = 0;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            // root files
            long totalFilesSizeFromFolder = GetFolderFilesSize(directoryInfo);

            DirectoryInfo[] directories = directoryInfo.GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                totalFilesSizeFromFolder += GetFolderFilesSize(directory);
                totalFilesSizeFromFolder += GetDirectoriesRecursiveSize(directory);
            }

            if (totalFilesSizeFromFolder > 0)
                MemorySizeMB = Math.Round((Convert.ToDouble(totalFilesSizeFromFolder / 1024) / 1024), 2);


            return MemorySizeMB;
        }

        public long GetDirectoriesRecursiveSize(DirectoryInfo directoryRoot)
        {
            long totalFilesSizeFromFolder = 0;

            DirectoryInfo[] directories = directoryRoot.GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                totalFilesSizeFromFolder += GetFolderFilesSize(directory);
                totalFilesSizeFromFolder += GetDirectoriesRecursiveSize(directory);
            }

            return totalFilesSizeFromFolder;
        }

        private long GetFolderFilesSize(DirectoryInfo directoryInfo)
        {
            long MemorySizeMB = 0;            

            MemorySizeMB = directoryInfo.EnumerateFiles().Sum(file => file.Length);

            return MemorySizeMB;
        }
    }
}
