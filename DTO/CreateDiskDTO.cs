using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CreateDiskDTO
    {
        public string ip_address { get; set; }
        public string name { get; set; }
        public double total_disk_space { get; set; }
        public double free_disk_space { get; set; }

        public CreateDiskDTO() { }

        public CreateDiskDTO(string ipAddress, string name, double totalDiskSpace, double freeDiskSpace)
        {
            this.ip_address = ipAddress;
            this.name = name;
            this.total_disk_space = totalDiskSpace;
            this.free_disk_space = freeDiskSpace;
        }
    }
}
