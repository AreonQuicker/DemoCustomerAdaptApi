using System.ComponentModel.DataAnnotations;
using DemoCustomerAdaptApi.Domain.Models.DomainModels;

namespace DemoCustomerAdaptApi.Domain.Models
{
    public class CustomerCreateRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
        [Required]
        public decimal InvoiceTotal { get; set; }
    }
}