using Domain;
using DTO;
using Microsoft.AspNetCore.SignalR;
using Repository.Repository;
using Repository.Repository.Interface;
using System.Text.Json;
using System.Text.RegularExpressions;
using TestProject.Services.Interface;

namespace TestProject.Services
{
    public class MetrixService : IMetrixService
    {
        private readonly static string SendTime = Directory.GetCurrentDirectory() + "\\SendTime.txt";
        private readonly IMetrixRepository MetricsRepository;
        private readonly IHubContext<SignalRChat> HubContext;
        private readonly IDiskSpaceRepository DiskRepository;
        public MetrixService(IMetrixRepository metricsRepository, IHubContext<SignalRChat> hubContext, IDiskSpaceRepository diskRepository)
        {
            MetricsRepository = metricsRepository;
            HubContext = hubContext;
            DiskRepository = diskRepository;
        }


        public async Task SendMetrics(string message)
        {
            try
            {
                var metrics = JsonSerializer.Deserialize<CreateMetrixDTO>(message);

                if (metrics != null && ValidateMetrics(metrics))
                {
                    var metric = new Metrix
                    {
                        ip_address = metrics.IpAddress,
                        cpu = metrics.Cpu,
                        ram_free = metrics.RamSpaceFree,
                        ram_total = metrics.RamSpaceTotal,
                    };
                    var objec = MetricsRepository.GetAll()
                   .Where(x => x.ip_address == metrics.IpAddress)
                   .FirstOrDefault();

                    if (objec == null)
                    {
                        MetricsRepository.Create(metric);
                    }
                    else
                    {
                        MetricsRepository.Update(metric, objec.id.ToString());
                    }

                    var alldisks = DiskRepository
                        .GetAll()
                        .ToList()
                        ?? throw new Exception();

                    foreach (var disk in metrics.DiskSpaces)
                    {
                        disk.disk_name = disk.disk_name.Replace("\\", "//");
                        var check = alldisks
                         .Where(x => x.ip_address == disk.ip_address && x.disk_name == disk.disk_name)
                         .FirstOrDefault();


                        if (check == null)
                        {
                            DiskRepository.Create(disk);
                        }
                        else
                        {
                            DiskRepository.Update(disk, check.id.ToString());
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
        }
        private bool ValidateMetrics(CreateMetrixDTO metrics)
        {
            // Perform necessary validation checks on the metrics DTO
            return !string.IsNullOrWhiteSpace(metrics?.IpAddress) && metrics.Cpu >= 0 && metrics.RamSpaceFree >= 0 && metrics.RamSpaceTotal >= 0;
        }
        public void Create(CreateMetrixDTO request)
        {
            var metric = new Metrix
            {
                ip_address = request.IpAddress,
                cpu = request.Cpu,
                ram_free = Math.Round(request.RamSpaceFree, 3),
                ram_total = Math.Round(request.RamSpaceTotal, 3)
            };
            MetricsRepository.Create(metric);
        }

        public void Delete(Guid id)
        {
            var objec = MetricsRepository.GetAll()
                           .Where(x => x.id == id)
                           .FirstOrDefault()
                           ?? throw new Exception();
            MetricsRepository.Delete(objec);
        }

        public IQueryable<Metrix> Get()
        {
            return MetricsRepository.GetAll();
        }

        public Metrix Get(Guid id)
        {
            var metrics = MetricsRepository.GetAll()
                           .Where(x => x.id == id)
                           .FirstOrDefault()
                           ?? throw new Exception();
            return metrics;
        }

        public Metrix Get(string ip)
        {
            var metrics = MetricsRepository.GetAll()
                          .Where(x => x.ip_address == ip)
                          .FirstOrDefault()
                          ?? throw new Exception();

            return metrics;
        }

        public async Task SendTimeClient(int time)
        {
            CheckFile();
            StreamReader reader = new StreamReader(SendTime);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, content, time.ToString());

            StreamWriter writer = new StreamWriter(SendTime);
            writer.Write(content);
            writer.Close();

            await HubContext.Clients.All.SendAsync("ReceiveMessage", time);
        }
        public static void CheckFile()
        {
            if (!File.Exists(SendTime))
            {
                File.Create(SendTime).Dispose();

                using (TextWriter tw = new StreamWriter(SendTime))
                {
                    tw.WriteLine("5");
                }
            }
        }

        public void Update(string ip, CreateMetrixDTO request)
        {
            var metric = new Metrix
            {
                ip_address = request.IpAddress,
                cpu = request.Cpu,
                ram_free = Math.Round(request.RamSpaceFree, 3),
                ram_total = Math.Round(request.RamSpaceTotal, 3)
            };

            MetricsRepository.Update(metric, ip);
        }
    }
}
