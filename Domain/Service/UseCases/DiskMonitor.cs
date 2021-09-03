using Domain.Service.DTO;
using Domain.Service.Entities;
using Infra;
using System;
using System.Collections.Generic;
using System.IO;

namespace Domain.Service.UseCases
{
    public class DiskMonitor : UseCase
    {
        private DTOMonitor DTO = null;
        private List<Disk> ListDiskMonitoring = null;

        public DiskMonitor()
        {            
            ListDiskMonitoring = new List<Disk>();
            DTO = new DTOMonitor();            
        }

        /// <summary>
        /// Collect data from Disk on Machine and save in DB         
        /// </summary>        
        public void Monitoring(Int64 nIdWatchDogItem)
        {
            try
            {
                ListDiskMonitoring.Clear();

                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo d in allDrives)
                {
                    Disk _Disk = new Disk();
                    _Disk.IdWatchDogItem = nIdWatchDogItem;
                    _Disk.Drive = d.Name;
                    _Disk.DriveType = d.DriveType.ToString();

                    if (_Disk.DriveType.Equals("CDRom")) continue;

                    _Disk.VolumeLabel = d.VolumeLabel;
                    _Disk.FileSystem = d.DriveFormat;
                    _Disk.AvailableSpaceMB = Math.Round(Convert.ToDouble(d.TotalFreeSpace / 1024 / 1024), 2);
                    _Disk.TotalSizeGB = Math.Round(Convert.ToDouble(d.TotalSize / 1024 / 1024 / 1024), 2);

                    ListDiskMonitoring.Add(_Disk);
                }
            }
            catch (Exception ex)
            {
                AlertHelper.Alert(AlertConsts.AGENT_MONITOR_DISK_ERROR, "COLLECT DISK: " + ex.Message, EAlertLevel.CRITICAL);                
            }
        }

        public void Save()
        {
            try
            {
                foreach (Disk _Disk in ListDiskMonitoring)
                {
                    DTO.SaveDisk(_Disk);
                }
            }
            catch (Exception ex)
            {
                AlertHelper.Alert(AlertConsts.AGENT_MONITOR_SAVE_DISK_ERROR, "COLLECT DISK: " + ex.Message, EAlertLevel.CRITICAL);                
            }
        }
    }
}
