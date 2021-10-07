using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Data;
using System.Data.OleDb;

namespace Domain.DTO
{
    public class DTOMonitor : DTO
    {
        public DTOMonitor(IAccess access) : base (access)
        {

        }

        public void SaveMachine(Machine oItem)
        {
            VerifyOpenDB();

            if (IsConnectionAvaible())
            {
                //TODO: Alterar a procedure WSQOSTORE_WATCHDOG_ITEM para se não tiver a categoria enviada no novo parâmetro, será inserida antes para que o registro possa ser associado.

                OleDbCommand oCommand = DbConnection.CreateCommand();
                oCommand.CommandText = "WSQOSTORE_WATCHDOG_ITEM";
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@MACHINE_NAME", oItem.MachineName);
                oCommand.Parameters.AddWithValue("@PLATFORM", oItem.Platform);
                oCommand.Parameters.AddWithValue("@VERSION", oItem.Version);
                oCommand.Parameters.AddWithValue("@SERVICE_PACK", oItem.ServicePack);
                oCommand.Parameters.AddWithValue("@PROCESSOR_COUNT", oItem.ProcessorCount);

                //TODO: adicionar o parâmetro de IdCategory que já estará preenchido previamente.

                OleDbParameter oParamId = new OleDbParameter("@ID", oItem.Id);
                oParamId.Direction = ParameterDirection.Output;

                oCommand.Parameters.Add(oParamId);                                

                oCommand.ExecuteNonQuery();

                oItem = new Machine(Convert.ToInt64(oCommand.Parameters["@ID"].Value),
                                    oItem.MachineName,
                                    oItem.Platform,
                                    oItem.Version,
                                    oItem.ServicePack,
                                    oItem.ProcessorCount,
                                    oItem.CategoryMachinesId,
                                    oItem.Category);                

                oCommand.Dispose();

                DbConnection.Close();
            }
        }

        public void SaveMonitoring(MonitorDetail oItem)
        {
            VerifyOpenDB();

            if (IsConnectionAvaible())
            {
                OleDbCommand oCommand = DbConnection.CreateCommand();
                oCommand.CommandText = "WSQOSTORE_WATCHDOG_DETAIL";
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID_WATCHDOG_ITEM", oItem.IdWatchDogItem);
                oCommand.Parameters.AddWithValue("@NAME", oItem.Name);
                oCommand.Parameters.AddWithValue("@RAM_USED_PROCESS", oItem.MemoryUsedPerProcess);
                oCommand.Parameters.AddWithValue("@AVAIBLE_RAM_MACHINE", oItem.AvaibleMemoryMachine);
                oCommand.Parameters.AddWithValue("@CPU_USED_PROCESS", oItem.CpuUsedProcess);
                oCommand.Parameters.AddWithValue("@CPU_USED_MACHINE", oItem.CpuUsedMachine);
                oCommand.Parameters.AddWithValue("@IS_SERVICE", oItem.IsService);
                oCommand.Parameters.AddWithValue("@PATH", oItem.Path);
                oCommand.ExecuteNonQuery();

                oCommand.Dispose();

                DbConnection.Close();
            }
        }

        public void SaveDisk(Disk oItem)
        {
            VerifyOpenDB();

            if (IsConnectionAvaible())
            {
                OleDbCommand oCommand = DbConnection.CreateCommand();
                oCommand.CommandText = "WSQOSTORE_WATCHDOG_DISK";
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID_WATCHDOG_ITEM", oItem.IdWatchDogItem);
                oCommand.Parameters.AddWithValue("@DRIVE", oItem.Drive);
                oCommand.Parameters.AddWithValue("@TYPE", oItem.DriveType);
                oCommand.Parameters.AddWithValue("@VOLUME_LABEL", oItem.VolumeLabel);
                oCommand.Parameters.AddWithValue("@FILE_SYSTEM", oItem.FileSystem);
                oCommand.Parameters.AddWithValue("@AVAILABLE_SPACE_MB", oItem.AvailableSpaceMB);
                oCommand.Parameters.AddWithValue("@TOTAL_SIZE_GB", oItem.TotalSizeGB);
                oCommand.ExecuteNonQuery();

                oCommand.Dispose();

                DbConnection.Close();
            }
        }
    }
}
