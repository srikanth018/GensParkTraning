
using System.ComponentModel.DataAnnotations;

namespace SampleMigrateApp.Models
{

    public class Model
    {
        public Model()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int ModelId { get; set; }
        public string Model1 { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }
}
