using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Entity<TId>
    {
        public virtual TId id { get; set; }
        [Column("is_deleted")]

        public virtual bool is_deleted { get; set; }
        [Column("create_date")]
        public virtual DateTime create_date { get; set; } = DateTime.Now;
        [Column("update_date")]
        public virtual DateTime? update_date { get; set; }
        [Column("delete_date")]
        public virtual DateTime? delete_date { get; set; }
    }
}
