// namespace SampleMigrateApp.Models
// {
//     using System;
//     using System.Collections.Generic;

//     public partial class Product
//     {
//         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
//         public Product()
//         {
//             this.OrderDetails = new HashSet<OrderDetail>();
//         }

//         public int ProductId { get; set; }
//         public string ProductName { get; set; }
//         public string Image { get; set; }
//         public Nullable<double> Price { get; set; }
//         public Nullable<int> UserId { get; set; }
//         public Nullable<int> CategoryId { get; set; }
//         public Nullable<int> ColorId { get; set; }
//         public Nullable<int> ModelId { get; set; }
//         public Nullable<int> StorageId { get; set; }
//         public Nullable<System.DateTime> SellStartDate { get; set; }
//         public Nullable<System.DateTime> SellEndDate { get; set; }
//         public Nullable<int> IsNew { get; set; }

//         public virtual Category Category { get; set; }
//         public virtual Color Color { get; set; }
//         public virtual Model Model { get; set; }
//         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//         public virtual ICollection<OrderDetail> OrderDetails { get; set; }
//         public virtual User User { get; set; }
//     }
// }

using System.ComponentModel.DataAnnotations;

namespace SampleMigrateApp.Models
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double? Price { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? ColorId { get; set; }
        public int? ModelId { get; set; }
        public int? StorageId { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public int? IsNew { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Model? Model { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User? User { get; set; }
    }
}