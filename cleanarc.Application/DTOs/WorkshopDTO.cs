using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class WorkshopDTO
    {
        public string address { get; set; }
        public string imageUrl { get; set; }
        public string code { get; set; }
        public string Phone { get; set; }
        //------------------------------
        public ICollection<Rank> ranks { get; set; }
        public ICollection<ServiceSession> serviceSessions { get; set; }
        public ICollection<ServiceType> serviceTypes { get; set; }
    }
}
