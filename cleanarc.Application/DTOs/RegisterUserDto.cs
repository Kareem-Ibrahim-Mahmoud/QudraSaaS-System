using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace QudraSaaS.Application.DTOs
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string whats { get; set; }
        public string Rank { get; set; }
        public int numberOfVisits { get; set; }
        public string notes { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        //------------------------------

        public int RankId { get; set; }
        public string workShopId { get; set; }
        public List<int> carid { get; set; } = new List<int>(); 
        public List<int> ServiceSessionid { get; set; } = new List<int>();
    }
}
