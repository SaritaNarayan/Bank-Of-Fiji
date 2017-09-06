using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.Web.Models.Mappers
{
    public class CustomerMapper
    {
        private readonly ISession _session;

        public CustomerMapper(ISession session)
        {
            this._session = session;
        }

        public CustomerModel CustomerToCustomerModel(Customer Customer)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var CustomerModel = new CustomerModel();

            CustomerModel.FirstName = Customer.FirstName;
            CustomerModel.LastName = Customer.LastName;
            CustomerModel.Username = Customer.Username;
            CustomerModel.Password = Customer.Password;
            CustomerModel.DateOfBirth = Customer.DateOfBirth;
            CustomerModel.TinNumber = Customer.TinNumber;
            CustomerModel.Gender = Customer.Gender;
            CustomerModel.Email = Customer.Email;
            CustomerModel.HomeAddress = Customer.HomeAddress;
            CustomerModel.City = Customer.City;
            CustomerModel.MobileNumber = Customer.MobileNumber;
            CustomerModel.Occupation = Customer.Occupation;
            CustomerModel.EmployerName = Customer.EmployerName;

            return CustomerModel;
        }

        public Customer CustomerModelToCustomer(CustomerModel CustomerModel)
        {
            var Customer = new Customer();

            Customer.FirstName = CustomerModel.FirstName;
            Customer.LastName = CustomerModel.LastName;
            Customer.Username = CustomerModel.Username;
            Customer.Password = CustomerModel.Password;
            Customer.DateOfBirth = CustomerModel.DateOfBirth;
            Customer.TinNumber = CustomerModel.TinNumber;
            Customer.Gender = CustomerModel.Gender;
            Customer.Email = CustomerModel.Email;
            Customer.HomeAddress = CustomerModel.HomeAddress;
            Customer.City = CustomerModel.City;
            Customer.MobileNumber = CustomerModel.MobileNumber;
            Customer.Occupation = CustomerModel.Occupation;
            Customer.EmployerName = CustomerModel.EmployerName;

            return Customer;
        }

    }


}