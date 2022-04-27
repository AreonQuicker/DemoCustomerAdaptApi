using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCustomerAdaptApi.DataAccess;
using DemoCustomerAdaptApi.Domain.Exceptions;
using DemoCustomerAdaptApi.Domain.Interfaces;
using DemoCustomerAdaptApi.Domain.Models;
using DemoCustomerAdaptApi.Domain.Models.DomainModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DemoCustomerAdaptApi.Domain.Logic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;

        public CustomerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CustomerDomainModel> GetAsync(int id)
        {
            return await _dataContext.Customers.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<CustomerDomainModel>> GetAllAsync()
        {
            return await _dataContext.Customers.OrderBy(o => o.Created).ToListAsync();
        }

        public async Task<int> CreateAsync(CustomerCreateRequest customerCreateRequest)
        {
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(f =>
                f.Email == customerCreateRequest.Email);

            if (customer is not null)
                throw new AlreadyExistException($"Customer already exists with email {customerCreateRequest.Email}", null);

            var newCustomer = customerCreateRequest.Adapt<CustomerDomainModel>();
            await _dataContext.Customers.AddAsync(newCustomer);
            await _dataContext.SaveChangesAsync();

            return newCustomer.Id;
        }

        public async Task<int> UpdateAsync(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(f => f.Id == id);

            if (customer is null)
                throw new NotFoundException(nameof(customer), id);

            customerUpdateRequest.Adapt(customer);

            _dataContext.Customers.Update(customer);

            await _dataContext.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(f => f.Id == id);

            if (customer is null)
                throw new NotFoundException(nameof(customer), id);

            _dataContext.Customers.Remove(customer);

            await _dataContext.SaveChangesAsync();

            return customer.Id;
        }
    }
}