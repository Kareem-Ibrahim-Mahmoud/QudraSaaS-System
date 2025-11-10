using System.ComponentModel.DataAnnotations.Schema;

namespace QudraSaaS.Dmain
{
    public class Car
    {
        public int Id { get; set; }
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
