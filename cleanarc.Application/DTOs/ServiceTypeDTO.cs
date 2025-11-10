using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class ServiceTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool available { get; set; }
        //-------------
        public string workShopId { get; set; }
    }
}
