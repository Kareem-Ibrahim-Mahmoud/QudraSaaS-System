using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool available { get; set; }
        //-------------
        public string workShopId {  get; set; }
        [ForeignKey("workShopId")]
        public Workshop workShop { get; set; }
    }
}
