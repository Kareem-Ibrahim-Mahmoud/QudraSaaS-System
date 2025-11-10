using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class Rank
    {
        public int id { get; set; }
        public string name { get; set; }
        public int minVisits { get; set; }
        public int discountPercentage { get; set; }// 5 means 5%
       
        //------------------------------
        public string workShopId { get; set; }
        [ForeignKey("workShopId")]
        public Workshop workShop { get; set; }

    }
}
