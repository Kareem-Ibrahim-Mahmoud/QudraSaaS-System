using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class CarDTO
    {
        public string CarModel { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public int CurrentKm { get; set; }
        public string OilType { get; set; }
        //------------------------------
        public string customerId { get; set; }
        [ForeignKey("customerId")]
        public Customer customer { get; set; }
        //-------------------------------
        public ICollection<ServiceSession> serviceSessions { get; set; }
    }
}
