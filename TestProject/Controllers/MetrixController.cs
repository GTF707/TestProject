using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Interface;
using TestProject.Services.Interface;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetrixController : ControllerBase
    {
        
        private readonly IMetrixService MetricsService;

        public MetrixController(IMetrixService metrixService)
        {
            MetricsService = metrixService;
        }

        [HttpGet("get-list")]
        public IQueryable<Metrix> Get()
        {
            return MetricsService.Get();

        }

        [HttpGet("get-by-id")]
        public Metrix GetById(Guid id)
        {


            return MetricsService.Get(id);
        }

        [HttpGet("get-by-ip")]
        public Metrix GetByIp(string ip)
        {


            return MetricsService.Get(ip);
        }

        [HttpPut("update-by-ip")]
        public OkResult Update(string ip, [FromBody] CreateMetrixDTO request)
        {
            MetricsService.Update(ip, request);
            return Ok();
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] CreateMetrixDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            MetricsService.Create(request);
            return Ok();
        }

        [HttpDelete("id")]
        public OkResult Delete(Guid id)
        {
            MetricsService.Delete(id);

            return Ok();
        }



        [HttpPost("report_Send_Time")]
        public async Task SendTimeToClients(int time)
        {
            await MetricsService.SendTimeClient(time);
        }
    }
}

