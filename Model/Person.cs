using RestWithASPNET.db.Base;
using System.ComponentModel.DataAnnotations;
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
        public string? Address { get; set; }
        [Column("gender")]
        public string? Gender { get; set; }
    }
}
