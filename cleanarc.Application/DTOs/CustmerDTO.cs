using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class CustmerDTO
    {
        public string name { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string whats { get; set; }
        public string Rank { get; set; }
        public int numberOfVisits { get; set; }
        public string notes { get; set; }
        //------------------------------
        public int RankId { get; set; }
        public string workShopId { get; set; }
        //public ICollection<Car> cars { get; set; }
        public List<int>? carlistid { get; set; }= new List<int>();
        public List<int>? serviceSessionslistId { get; set; } = new List<int>();
        //public ICollection<ServiceSession> serviceSessions { get; set; }
    }
}
