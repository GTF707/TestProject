using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("metrix", Schema = "public")]
    public class Metrix : PersistentObject
    {
        [Column("ip_address")]
        public string ip_address { get; set; }
        [Column("ram_free")]
        public double ram_free { get; set; }

        [Column("ram_total")]
        public double ram_total { get; set; }
        
        [Column("cpu")]
        public double cpu { get; set; }
        public Metrix(string ipAddress, double remFree, double ramTotal, double cpu) 
        {
            this.ip_address = ipAddress;
            this.ram_free = remFree;
            this.ram_total = ramTotal;
            this.cpu = cpu;
        }
        public Metrix() { }
    }
}