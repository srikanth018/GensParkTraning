using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Like
{
    [Key]
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }

    [ForeignKey("PostId")]
    public Post? Post { get; set; }
    public string? Status { get; set; } = "Active";

}
}