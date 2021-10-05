using System;

namespace Domain.Entities
{
    public sealed class Disk : Entity
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
