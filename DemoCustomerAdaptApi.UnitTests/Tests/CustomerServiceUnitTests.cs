using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using DemoCustomerAdaptApi.DataAccess;
using DemoCustomerAdaptApi.Domain.Exceptions;
using DemoCustomerAdaptApi.Domain.Logic.Services;
using DemoCustomerAdaptApi.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoCustomerAdaptApi.UnitTests.Tests
{
    public class CustomerServiceUnitTests
    {
        private DataContext DataContext { get; }

        public CustomerServiceUnitTests()
        {
            DataContext = TestHelpers.CreateDbContext();
            TestHelpers.SeedDataAsync(DataContext).GetAwaiter().GetResult();
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldThrowException_WhenEmailAlreadyExists(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();
            
            Func<Task> f = async () =>
            {
                await customerService.CreateAsync(new CustomerCreateRequest()
                {
                    Email = "Email"
                });
            };

            await f.Should().ThrowAsync<AlreadyExistException>();
        }
        
        [Theory]
        [AutoData]
        public async Task Create_ShouldCreateCustomer(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var request = fixture.Create<CustomerCreateRequest>();
            request.Email = "Email2";

            var result = await customerService.CreateAsync(request);

            var totalCustomers = DataContext.Customers.Count();
            
            result.Should().BeGreaterThan(0);
            totalCustomers.Should().Be(2);
        }
        
        [Theory]
        [AutoData]
        public async Task GetAll_ShouldReturnOneCustomer(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var result = await customerService.GetAllAsync();

            result.Should().NotBeNull();
            result.Count().Should().Be(1);
        }
        
        [Theory]
        [AutoData]
        public async Task Get_ShouldReturnOneCustomer(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var result = await customerService.GetAsync(1);

            result.Should().NotBeNull();
        }
        
        [Theory]
        [AutoData]
        public async Task Get_ShouldReturnNull_WithInvalidId(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var result = await customerService.GetAsync(2);

            result.Should().BeNull();
        }
        
        [Theory]
        [AutoData]
        public async Task Update_ShouldUpdateCustomer(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var request = fixture.Build<CustomerUpdateRequest>().With(w => w.Phone, "Phone2").Create();

            var result = await customerService.UpdateAsync(1,request);

            var customers = DataContext.Customers;
            
            result.Should().BeGreaterThan(0);
            customers.Count().Should().Be(1);
            customers.FirstOrDefault().Should().NotBeNull();
            customers.FirstOrDefault().Phone.Should().Be("Phone2");
        }
        
        [Theory]
        [AutoData]
        public async Task Delete_ShouldDeleteCustomer(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            var result = await customerService.DeleteAsync(1);

            var customers = DataContext.Customers;
            
            result.Should().BeGreaterThan(0);
            customers.Count().Should().Be(0);
        }
        
        [Theory]
        [AutoData]
        public async Task Delete_ShouldThrowException(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            fixture.Inject(DataContext);

            var customerService = fixture.Create<CustomerService>();

            Func<Task> f = async () =>
            {
                await customerService.DeleteAsync(2);
            };

            await f.Should().ThrowAsync<NotFoundException>();
        }
    }
}