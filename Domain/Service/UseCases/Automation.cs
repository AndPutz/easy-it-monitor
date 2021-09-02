﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Infra;

namespace Domain.Service.UseCases
{
    public class Automation : UseCase
    {
        public void DeleteTempFiles()
        {
            string tempPath = Path.GetTempPath();
            
            double MemorySizeMB = GetFolderSize(tempPath);

            AlertHelper.Alert(AlertConsts.AGENT_AUTOMATION_CLEAN_TEMP_DIR_INFO, MemorySizeMB.ToString() + " MB WILL BE CLEANED IN TEMP FOLDER", EAlertLevel.INFO);

            CleanTempFolder(tempPath);
                
            AlertHelper.Alert(AlertConsts.AGENT_AUTOMATION_CLEAN_TEMP_DIR_INFO, "TEMP FOLDER WAS CLEANED - " + MemorySizeMB.ToString() + " MB", EAlertLevel.INFO);            
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
            
            //TODO: CleanTempFolder
            
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
