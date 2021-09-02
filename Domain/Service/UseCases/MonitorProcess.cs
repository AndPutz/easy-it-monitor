using Domain.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.UseCases
{
    public class MonitorProcess : Monitor
    {
        public MonitorProcess() : base ()
        {

        }

        public override void Save()
        {
            foreach (MonitorDetail Detail in MonitoringItems)
            {
                Detail.IsService = false;
                DTO.SaveMonitoring(Detail);
            }
        }
    }
}
