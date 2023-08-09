// See https://aka.ms/new-console-template for more information
using Client.Service;
using Domain;
using DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace TestProject
{
    public class Program
    {
        private static readonly string Url = "http://localhost:7007/chatHub";
        public static int Time = 5;
        public static async Task Main(string[] args)
        {
            CultureInfo customCulture = new CultureInfo("ru-RU");
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = customCulture;
            CultureInfo.DefaultThreadCurrentUICulture = customCulture;

            HubConnection connection = Connect(Url);


            connection.On<int>("ReceiveMessage", (message) =>
            {
                Console.WriteLine(message);
                Time = message;
            });

            while (true)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    WindowsMetrix winMetrixService = new WindowsMetrix();
                    CreateMetrixDTO metrixInfo = winMetrixService.GetMetrix();
                    if (connection != null)
                    {
                        await SendMessage(connection, metrixInfo);
                    }
                    Thread.Sleep(Time * 1000);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    LinuxMetrix linMetrixService = new LinuxMetrix();
                    CreateMetrixDTO metrixInfo = linMetrixService.GetMetrix();
                    if (connection != null)
                    {
                        await SendMessage(connection, metrixInfo);
                    }
                    Thread.Sleep(Time * 1000);
                }
            }

        }
        private static HubConnection Connect(string url)
        {
            var connection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();

            connection.StartAsync().Wait();

            return connection;
        }
        private static async Task SendMessage(HubConnection connect, object message)
        {
            string metrixJson = JsonSerializer.Serialize(message);
            await connect.InvokeCoreAsync("SaveMetrix", args: new[] { metrixJson });
        }


    }

}
