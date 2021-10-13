using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Diagnostics;
using System.Management;

namespace Domain.UseCases
{
    public class ManageProcess
    {
        private IAlert _Alert;

        public ManageProcess(IAlert alert)
        {
            _Alert = alert;
        }

        public bool GetStatus(string processName)
        {
            bool IsProcessRunning = false;

            try
            {
                Process[] localByName = Process.GetProcessesByName(processName);

                if (localByName.Length <= 0)
                    IsProcessRunning = false;
                else
                    IsProcessRunning = true;
            }
            catch (Exception ex)
            {
                _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessStatusError(), "CAN NOT ACCESS PROCESS " + processName + ". ERROR MSG: " + ex.Message, EAlertLevel.MEDIUM);                
            }

            return IsProcessRunning;
        }

        public bool StartProcess(string processName, string path, string arguments = "")
        {
            bool IsProcessRunning = false;

            Process.Start(path, arguments);

            System.Threading.Thread.Sleep(5000);

            if (GetStatus(processName) == true)
            {
                _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessOn(), processName + " STARTED", EAlertLevel.INFO);
                IsProcessRunning = true;
            }
            else
            {
                _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessOn(), processName + " NOT STARTED", EAlertLevel.HIGH);
                IsProcessRunning = false;
            }

            return IsProcessRunning;
        }

        public bool StopProcess(string processName)
        {
            bool IsProcessStoped = false;

            Process[] localByName = Process.GetProcessesByName(processName);

            if (localByName.Length > 0)
            {
                Process process = localByName[0];

                try
                {
                    ManagementObject classInstance = new ManagementObject(@"root\CIMV2", "Win32_Process.Handle='" + process.Id.ToString() + "'", null);

                    ManagementBaseObject inParams = classInstance.GetMethodParameters("Terminate");

                    ManagementBaseObject outParams = classInstance.InvokeMethod("Terminate", inParams, null);

                    if (int.Parse(outParams["ReturnValue"].ToString().Trim()) != 0)
                    {
                        _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessNotPossible(), processName + " NOT ABLE TO STOP", EAlertLevel.MEDIUM);
                        IsProcessStoped = false;
                    }
                    else
                    {
                        _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessOff(), processName + " STOPED", EAlertLevel.INFO);
                        IsProcessStoped = true;
                    }
                }
                catch(Exception ex)
                {
                    _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessNotPossible(), "ERROR TO CLOSE PROCESS: " + processName + ". ERROR MSG: " + ex.Message, EAlertLevel.HIGH);
                    IsProcessStoped = false;
                }
            }
            else
                IsProcessStoped = true;

            //double-check

            System.Threading.Thread.Sleep(1000);

            if (GetStatus(processName) == true)
            {
                _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessNotPossible(), processName + " NOT ABLE TO STOP", EAlertLevel.MEDIUM);
                IsProcessStoped = false;
            }

            return IsProcessStoped;
        }
    }
}
