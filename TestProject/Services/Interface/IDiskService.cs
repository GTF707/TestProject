using Domain;

namespace TestProject.Services.Interface
{
    public interface IDiskService
    {
        public List<DiskSpace> GetAll(string ip);
    }
}
