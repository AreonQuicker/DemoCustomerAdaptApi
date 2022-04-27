using DemoCustomerAdaptApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCustomerAdaptApi.DataAccess.Configurations
{
    public class CustomerDomainModelConfiguration : IEntityTypeConfiguration<CustomerDomainModel>
    {
        public void Configure(EntityTypeBuilder<CustomerDomainModel> builder)
        {
            builder.ToTable("Customer");

            builder.Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Phone)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.InvoiceTotal)
                .HasColumnType("decimal(29,10)");
        }
    }
}