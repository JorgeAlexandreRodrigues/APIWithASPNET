using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace RestWithASPNET.Model
{
    [Index(nameof(UserName), IsUnique = true)]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Column("password")]
        public string? Password { get; set; }
        
        [Column("refresh_token")] 
        public string? RefreshToken { get; set; } 
        
        [Column("refresh_token_expiry_time")]       
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
