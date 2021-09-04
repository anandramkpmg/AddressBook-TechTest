using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.WebAPI.Models
{
    public class Contact
    {
        [Key]
        //TODO : make it Guid
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string SurName { get; set; }
        [Required]
        public DateTime DateOfBirth  { get; set; }
        public string Email { get; set; }
    }
}
