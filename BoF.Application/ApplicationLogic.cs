using System;
using System.Collections.Generic;
using BoF.Domain.Entities;
using BoF.Domain.Repository;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Loader.Criteria;

namespace BoF.Application
{
    public partial class ApplicationLogic : IApplicationLogic
    {

        #region Members      

        private readonly ISession _session;
        private IRepository<SystemRoles> _UserRole;
        private IRepository<Customer> _Customer;
        private IRepository<Account> _Account;
        private IRepository<AccountType> _AccountType;
        private IRepository<UserProfile> _UserProfile;
        private IRepository<Transaction> _Transaction;

        // private IRepository<UserLoginDetails> _UserLoginDetailsRepository;

        #endregion

        #region Constructors

        //protected ISession _session;

        public ApplicationLogic()
        {

        }
        public ApplicationLogic(ISession session)
        {
            _session = session;
            // _importApplicationRepository = new Repository<ImportPermitApplication>(_session);
        }

        #endregion

        public T GetEntityById<T>(int Id)
        {
            return new Repository<T>(_session).GetById(Id);
        }

        public int AuthenticateUser(string Username, string Password)
        {
            return _session.QueryOver<Customer>().Where(c => c.Username == Username && c.Password == Password).RowCount();
        }

        public int getCustomerId(string Username)
        {
            int customerId = _session.QueryOver<Customer>().Select(v => v.Id).Where(c => c.Username == Username).Take(1).SingleOrDefault<int>();
            return customerId;
        }
        public string getRole(int customerId)
        {
            String Role = _session.QueryOver<UserProfile>().Select(v => v.Role).Where(c => c.Customer.Id == customerId).Take(1).SingleOrDefault<string>();
            return Role;
        }

        public Customer CreateUser(Customer Customer)
        {
            _Customer = new Repository<Customer>(_session);
            _Customer.BeginTransaction();

            try
            {
                _Customer.Save(Customer);
                _Customer.CommitTransaction();
            }
            catch (Exception)
            {
                _Customer.RollbackTransaction();
                throw;
            }

            return Customer;
        }

        public Account AddAccount(Account Account)
        {
            _Account = new Repository<Account>(_session);
            _Account.BeginTransaction();

            try
            {
                _Account.Save(Account);
                _Account.CommitTransaction();
            }
            catch (Exception)
            {
                _Account.RollbackTransaction();
                throw;
            }

            return Account;
        }

        public AccountType AddAccountType(AccountType AccountType)
        {
            _AccountType = new Repository<AccountType>(_session);
            _AccountType.BeginTransaction();

            try
            {
                _AccountType.Save(AccountType);
                _AccountType.CommitTransaction();
            }
            catch (Exception)
            {
                _AccountType.RollbackTransaction();
                throw;
            }

            return AccountType;
        }

        public UserProfile InsertRole(UserProfile UserProfile)
        {
            _UserProfile = new Repository<UserProfile>(_session);
            _UserProfile.BeginTransaction();

            try
            {
                _UserProfile.Save(UserProfile);
                _UserProfile.CommitTransaction();
            }
            catch (Exception)
            {
                _UserProfile.RollbackTransaction();
                throw;
            }

            return UserProfile;
        }

        public IList<Customer> GetCustomerDetails()
        {
            return _session.QueryOver<Customer>()
                //.Where(x => x.Id == CostingSheetMasterId)
                .List();
        }
        public IList<Customer> GetCustomerDetail(int id)
        {
            return _session.QueryOver<Customer>()
                .Where(x => x.Id == id)
                .List();
        }
        //public IList<Account> GetCustomerAccounts(int id)
        //{
        //    var query = _session.CreateSQLQuery(@"Update OPFMaster set OPFSequenceNumber_Full = :opfnum where Id = :id")
        //        .SetParameter("opfnum", OPFNumber, NHibernateUtil.String)
        //        .SetParameter("id", id, NHibernateUtil.Int32);
        //    int result = query.ExecuteUpdate();

        //    return result();
        //}
        public IList<Account> GetAccountDetails(int id)
        {
            return _session.QueryOver<Account>()
                .Where(x => x.Customer.Id == id)
                .List();
        }
        public int GetAccountId(int id)
        {
            int accountId = _session.QueryOver<Account>().Select(Q => Q.Id).Where(x => x.Customer.Id == id).Take(1).SingleOrDefault<int>();
            return accountId;
        }

        public int GetaccountId(int id)
        {
            int accountId = _session.QueryOver<Account>().Select(Q => Q.Id).Where(x => x.AccountNumber == id).Take(1).SingleOrDefault<int>();
            return accountId;
        }
        public void insertTransaction(decimal TransAmount, string TransDetails, int Account_id, int TransTypeId)
        {
                var query = _session.CreateSQLQuery(@"insert into [dbo].[Transaction](TransAmount,TransDetails,TransDateTime,Account_id,TransactionType_id) values(:TransferDetails,:TransferFrom,:Date,:DepartmentManager,:OPFMaster_id)")
                        .SetParameter("TransferDetails", TransAmount, NHibernateUtil.Decimal)
                        .SetParameter("TransferFrom", TransDetails, NHibernateUtil.String)
                        .SetParameter("Date", DateTime.Now, NHibernateUtil.DateTime)
                        .SetParameter("DepartmentManager", Account_id, NHibernateUtil.Int32)
                        .SetParameter("OPFMaster_id", TransTypeId, NHibernateUtil.Int32);
                int result = query.ExecuteUpdate();
        }

        public void insertBillPay(int Account_id, string biller, string billerAccNo, string desc, decimal TransAmount, string nextRunDate, string Freq)
        {
            var query = _session.CreateSQLQuery(@"insert into [dbo].[ScheduleTrans] ([FromAccount],[Biller],[BillerAccNo],[Desc],[Amount],[NextRunDate],[Frequency]) values(:fromaccount,:biller,:billeraccno,:desc,:amount,:nextrundate,:frequency)")
                    .SetParameter("fromaccount", Account_id, NHibernateUtil.Int32)
                    .SetParameter("biller", biller, NHibernateUtil.String)
                    .SetParameter("billeraccno", billerAccNo, NHibernateUtil.String)
                    .SetParameter("desc", desc, NHibernateUtil.String)
                    .SetParameter("amount", TransAmount, NHibernateUtil.Decimal)
                    .SetParameter("nextrundate", nextRunDate, NHibernateUtil.String)
                    .SetParameter("frequency", Freq, NHibernateUtil.String);
            int result = query.ExecuteUpdate();
        }
        public void updateAccountBalance(int Account_id, decimal TransAmount)
        {
            var query = _session.CreateSQLQuery(@"Update Account set Balance = Balance - :qa_Status where Id = :id")
                .SetParameter("qa_Status", TransAmount, NHibernateUtil.Decimal)
                .SetParameter("id", Account_id, NHibernateUtil.Int32);
            int result = query.ExecuteUpdate();
        }
        public AccountType AddSimpleInterest(AccountType AccountType)
        {
            _AccountType = new Repository<AccountType>(_session);
            _AccountType.BeginTransaction();

            try
            {
                _AccountType.Update(AccountType);
                _AccountType.CommitTransaction();
            }
            catch (Exception)
            {
                _AccountType.RollbackTransaction();
                throw;
            }

            return AccountType;
        }
        public AccountType AddSavingsInterest(AccountType AccountType)
        {
            _AccountType = new Repository<AccountType>(_session);
            _AccountType.BeginTransaction();

            try
            {
                _AccountType.Update(AccountType);
                _AccountType.CommitTransaction();
            }
            catch (Exception)
            {
                _AccountType.RollbackTransaction();
                throw;
            }

            return AccountType;
        }
        public Transaction AddTransaction(Transaction Transaction)
        {
            _Transaction = new Repository<Transaction>(_session);
            _Transaction.BeginTransaction();

            try
            {
                _Transaction.Save(Transaction);
                _Transaction.CommitTransaction();
            }
            catch (Exception)
            {
                _Transaction.RollbackTransaction();
                throw;
            }

            return Transaction;
        }
        public decimal GetSimpleInterest()
        {
            decimal simpleInterest = _session.QueryOver<AccountType>().Select(Q => Q.InterestRate).Where(x => x.AccountName == "Simple").Take(1).SingleOrDefault<decimal>();
            return simpleInterest;
        }

        public decimal GetSavingsInterest()
        {
            decimal savingsInterest = _session.QueryOver<AccountType>().Select(Q => Q.InterestRate).Where(x => x.AccountName == "Savings").Take(1).SingleOrDefault<decimal>();
            return savingsInterest;
        }
        public IList<Account> GetListofAccounts()
        {
            return _session.QueryOver<Account>()
                //.Where(x => x.Id == id)
                .List();
        }
        public int GetTypeAccountId(int id)
        {
            int accountId = _session.QueryOver<Account>().Select(Q => Q.AccountType.Id).Where(x => x.Id == id).Take(1).SingleOrDefault<int>();
            return accountId;
        }
        public IList<AccountType> GetAccountTypeDetails(int id)
        {
            return _session.QueryOver<AccountType>()
                .Where(x => x.Id == id)
                .List();
        }
    }
    //public IList<Customer> GetCustomerDetails()
    //{
    //    IList<Customer> CustomerList = this._session.QueryOver<Customer>()
    //                        .SelectList(list => list
    //                            .Select(p => p.FirstName)
    //                            .Select(p => p.LastName)
    //                            .Select(p => p.DateOfBirth)
    //                            .Select(p => p.Email)
    //                            .Select(p => p.Gender)
    //                            .Select(p => p.MobileNumber)
    //                            .Select(p => p.HomeAddress)
    //                            .Select(p => p.City)
    //                            .Select(p => p.TinNumber)
    //                            .Select(p => p.Occupation)
    //                            .Select(p => p.EmployerName))
    //                            .TransformUsing(Transformers.AliasToBean<Customer>())
    //                            .List<Customer>();
    //    return CustomerList;
    //}
}