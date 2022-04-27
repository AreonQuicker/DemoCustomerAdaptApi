namespace DemoCustomerAdaptApi.Domain.Models
{
    public class CustomerUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public decimal? InvoiceTotal { get; set; }
    }
}