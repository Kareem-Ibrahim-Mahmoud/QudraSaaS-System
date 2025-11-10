using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class ServiceSession
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string UserName { get; set; }
        public int KmReading { get; set; }// at the time of service
        public int NumberOfKilometers { get; set; }// How many kilometers does the oil get you?
        public bool FilterChanged { get; set; }
        public List< string> AdditionalServices { get; set; }=new List<string>();
        public int NextChange { get; set; }//after how many kilometers the next service is due
        public string description { get; set; }
        public double cost { get; set; }
        //------------------------------
        
        public int carId { get; set; }
        [ForeignKey("carId")]
        public Car car { get; set; }   
        public string customerId { get; set; }
        [ForeignKey("customerId")]
        public Customer customer { get; set; }
        public string workShopId { get; set; }

    }
}
