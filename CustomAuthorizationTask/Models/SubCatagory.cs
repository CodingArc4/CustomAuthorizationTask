using System.ComponentModel.DataAnnotations.Schema;

namespace CustomAuthorizationTask.Models
{
    public class SubCatagory
    {
        public int Id { get; set; }
        public string SubName { get; set; }

        public int CatagoryId { get; set; }

        [ForeignKey("CatagoryId")]
        public Catagory Catagory { get; set; }
    }
}
