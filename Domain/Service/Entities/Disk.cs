﻿using System;

namespace Domain.Service.Entities
{
    public class Disk : Entity
    {
        public Int64 IdWatchDogItem { get; set; }
        public string Drive { get; set; }
        public string DriveType { get; set; }
        public string VolumeLabel { get; set; }
        public string FileSystem { get; set; }
        public double AvailableSpaceMB { get; set; }
        public double TotalSizeGB { get; set; }
    }
}
