
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("owner")]
    public class Owner
    {
        [Column("OwnerId")]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }= string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Address { get; set; } = string.Empty;


        public ICollection<Account>? Accounts { get; set; }
}
}
