using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("disk_spaces", Schema = "public")]
    public class DiskSpace : PersistentObject
    {
        [Column("ip_address")]
        public string ip_address { get; set; }
        [Column("disk_name")]

        public string disk_name { get; set; }
        [Column("total")]

        public double total { get;set; }
        [Column("free")]

        public double free { get; set; }
        public DiskSpace(string name, long total, long free)
        {
            this.disk_name = name;
            this.total = total;
            this.free = free;
        }
        public DiskSpace() { }
    }
}
