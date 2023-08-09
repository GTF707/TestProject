using Domain;
using Repository.Repository.Interface;
using TestProject.Services.Interface;

namespace TestProject.Services
{
    public class DiskService : IDiskService
    {
        private readonly IDiskSpaceRepository DiskRepository;
        public DiskService(IDiskSpaceRepository diskService)
        {
            DiskRepository = diskService;
        }
        public List<DiskSpace> GetAll(string ip)
        {
            var listDisk = DiskRepository.GetAll().
                            Where(x => x.ip_address == ip).
                            ToList();

            return listDisk;
        }
    }
}
