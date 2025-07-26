namespace SampleMigrateApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }
}
