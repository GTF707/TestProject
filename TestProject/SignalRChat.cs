using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using TestProject.Services.Interface;

namespace TestProject
{
    public class SignalRChat : Hub
    {
       private readonly IMetrixService MetrixService;
        public SignalRChat (IMetrixService metrixService)
        {
            MetrixService = metrixService;
        }
        public async Task SaveMetrix(string jsonmetrix)
        {
            await MetrixService.SendMetrics(jsonmetrix);
        }
    }
}
