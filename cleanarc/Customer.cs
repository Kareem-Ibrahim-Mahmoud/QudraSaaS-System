using System.ComponentModel.DataAnnotations.Schema;


namespace QudraSaaS.Dmain
{
    public class Customer: applicationUser
    {
        public string whats { get; set; }
        public string Rank { get; set; }
        public int numberOfVisits { get; set; }
        public string notes { get; set; }
        //------------------------------

        public int RankId { get; set; }
        [ForeignKey("RankId")]
        public Rank rank { get; set; }
        public string workShopId { get; set; }

        //[ForeignKey("workShopId")]
        //public Workshop workShop { get; set; }
        
        //--------------------
        public ICollection<Car> cars { get; set; }
        public ICollection<ServiceSession> serviceSessions { get; set; }

    }
}
