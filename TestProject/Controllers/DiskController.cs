using Domain;
using Microsoft.AspNetCore.Mvc;
using TestProject.Services.Interface;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiskController : Controller
    {
        private readonly IDiskService DiskService;
        public DiskController(IDiskService diskService)
        {
            DiskService = diskService;
        }

        [HttpGet("get-all")]
        public List<DiskSpace> getAll(string ip)
        {
            

            return DiskService.GetAll(ip);
        }
    }
}
