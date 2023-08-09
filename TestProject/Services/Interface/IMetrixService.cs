using Domain;
using DTO;

namespace TestProject.Services.Interface
{
    public interface IMetrixService
    {
        public Task SendTimeClient(int time);
        public IQueryable<Metrix> Get();
        public Metrix Get(Guid id);
        public Metrix Get(string ip);
        public void Update(string ip, CreateMetrixDTO request);
        public void Create(CreateMetrixDTO request);
        public void Delete(Guid id);
        public Task SendMetrics(string message);
    }
}
