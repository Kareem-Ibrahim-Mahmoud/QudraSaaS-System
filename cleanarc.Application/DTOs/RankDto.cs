using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class RankDto
    {
        public string name { get; set; }
        public int minVisits { get; set; }
        public int discountPercentage { get; set; }// 5 means 5%

        //------------------------------
        public string workShopId { get; set; }
    }
}
