using Domain;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class MetricRepository : AbstractRepository<Metrix>, IMetrixRepository
    {
       

        protected override Metrix Map(DbDataReader reader)
        {
          
            var result = new Metrix
            {
                id = reader.GetGuid(0),
                ip_address = reader.GetString(1),
                cpu = reader.GetDouble(2),
                ram_free = reader.GetDouble(3),
                ram_total = reader.GetDouble(4),
                is_deleted = reader.GetBoolean(5),
                create_date = reader.GetDateTime(6),
                update_date = !reader.IsDBNull(7) ? reader.GetDateTime(7) : null,
                delete_date = !reader.IsDBNull(8) ? reader.GetDateTime(8) : null
            };
            return result;
        
    }
    }
}
