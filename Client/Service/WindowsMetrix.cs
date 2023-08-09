using Client.Interfaces;
using Domain;
using DTO;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;

namespace Client.Service
{
    public class WindowsMetrix : IMetrixService
    {
        public static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");


        public CreateMetrixDTO GetMetrix()
        {

            int processorCount = Environment.ProcessorCount;
            long totalMemory = GetTotalPhysicalMemory();
            long usedMemory = Process.GetProcesses().Sum(x => x.WorkingSet64);

            int totalRam = (int)ramCounterAvailable.NextValue();

            long freeRam = totalMemory - usedMemory;

            Console.WriteLine("Processors count: " + processorCount);
            Console.WriteLine("Total RAM: " + totalMemory / (1024 * 1024) + " MB");
            Console.WriteLine("Used memory: " + usedMemory / (1024 * 1024) + " MB");
            Console.WriteLine("Free memory: " + freeRam / (1024 * 1024) + " MB");
            Console.WriteLine("Available memory: " + totalRam + " MB");

            DriveInfo[] drives = DriveInfo.GetDrives();
            List<DiskSpace> disks = new List<DiskSpace>();
            string ipAddress = GetIPAddress();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    double freeSpaceGB = drive.TotalFreeSpace / (1024.0 * 1024 * 1024);
                    double totalSizeGB = drive.TotalSize / (1024.0 * 1024 * 1024);
                    Console.WriteLine($"Drive {drive.Name}: Free Space {freeSpaceGB:0.00} GB | Total Size {totalSizeGB:0.00} GB");
                    var disk = new DiskSpace
                    {
                        ip_address = ipAddress,
                        disk_name = drive.Name,
                        free = freeSpaceGB,
                        total = totalSizeGB,
                    };
                    disks.Add(disk);
                }
            }
            CreateMetrixDTO metricDto = new CreateMetrixDTO(
            ipAddress,
                disks,
                cpuCounter.NextValue(),
                freeRam/1024/1024/1024,
                totalMemory/1024/1024/1024
            );

            return metricDto;
        }

        private long GetTotalPhysicalMemory()
        {
            ObjectQuery query = new ObjectQuery("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                ManagementObjectCollection results = searcher.Get();
                foreach (ManagementObject result in results)
                {
                    return Convert.ToInt64(result["TotalPhysicalMemory"]);
                }
            }
            return 0;
        }

        private string GetIPAddress()
        {
            string? ipAddress = "";

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    ipAddress = networkInterface.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address.ToString();

                    if (!string.IsNullOrEmpty(ipAddress))
                        break;
                }
            }
            return ipAddress;
        }
    }
}
