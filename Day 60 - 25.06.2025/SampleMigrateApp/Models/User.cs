using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMigrateApp.Models;

public class User
{
    public User()
    {
        News = new HashSet<News>();
        Products = new HashSet<Product>();
        Carts = new HashSet<Cart>();
    }

    [Key]
    public int UserId { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public virtual ICollection<News> News { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<Cart> Carts { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

}
