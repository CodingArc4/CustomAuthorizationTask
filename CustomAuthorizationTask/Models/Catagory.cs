using System.ComponentModel.DataAnnotations.Schema;

namespace CustomAuthorizationTask.Models
{
    public class Catagory
    {
        public int Id { get; set; }
        public string CatagoryName { get; set; }

        public int ProductsId { get; set; }

        [ForeignKey("ProductsId")]
        public Products Products { get; set; }
    }
}
