using RestWithASPNET.db.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Model
{
    [Table("Person")]
    public class Person : BaseEntity
    {

        [Column("firstName")]
        public string? FirstName { get; set; }
        [Column("lastName")]
        public string? LastName { get; set; }
        [Column("address")] 
        public string Address { get; set; } = string.Empty;
        [Column("gender")] 
        public string Gender { get; set; } = string.Empty;
        [Column("enable")]
        public bool Enable { get; set; }
    }
}
