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
    public class DiskSpaceRepository : AbstractRepository<DiskSpace>, IDiskSpaceRepository
    {
        protected override DiskSpace Map(DbDataReader reader)
        {
            var result = new DiskSpace
            {
                id = reader.GetGuid(0),
                ip_address = reader.GetString(1),
                disk_name = reader.GetString(2),
                total = reader.GetDouble(3),
                free = reader.GetDouble(4),
                is_deleted = reader.GetBoolean(5),
                create_date = reader.GetDateTime(6),
                update_date = !reader.IsDBNull(7) ? reader.GetDateTime(7) : null,
                delete_date = !reader.IsDBNull(8) ? reader.GetDateTime(8) : null
            };
            return result;
        }
    }
}
