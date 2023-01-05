using ContactManagementWebApp.Models.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManagementWebApp.Models.Contact
{
    [Index(nameof(EmailAddress), Name = "UQ_EmailAddress", IsUnique = true)]
    [Index(nameof(Contact), Name = "UQ_Contact", IsUnique = true)]
    public class ContactEntity : AuditableEntity<int, string>, ISoftDelete<string>
    {
        [Required]
        [MinLength(6,  ErrorMessage = "Name should be a string of any size greater or equal to {0}")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\d{9}", ErrorMessage = "Contact should be 9 digits")]
        public string Contact { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email should be a valid email")]
        public string EmailAddress { get; set; }

        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}
