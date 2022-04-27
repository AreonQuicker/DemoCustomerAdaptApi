namespace DemoCustomerAdaptApi.Domain.Models.DomainModels
{
    public class CustomerDomainModel : AuditDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal InvoiceTotal { get; set; }
    }
}