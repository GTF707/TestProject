

using Domain;

namespace DTO
{
    public class CreateMetrixDTO
    {
        public string IpAddress { get; set; }
        public List<DiskSpace> DiskSpaces { get; set; }
        public double Cpu { get; set; }
        public double RamSpaceFree { get; set; }
        public double RamSpaceTotal { get; set; }

        public CreateMetrixDTO(string ipAddress, List<DiskSpace> diskSpace, double cpu, double ramSpaceFree, double ramSpaceTotal)
        {
            IpAddress = ipAddress;
            DiskSpaces = diskSpace;
            Cpu = cpu;
            RamSpaceFree = ramSpaceFree;
            RamSpaceTotal = ramSpaceTotal;
        }
        public CreateMetrixDTO() { }
    }
}
