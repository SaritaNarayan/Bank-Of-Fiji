using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.Web.Models.Mappers
{
    public class AccountMapper
    {
        private readonly ISession _session;

        public AccountMapper(ISession session)
        {
            this._session = session;
        }

        public AccountModel AccountToAccountModel(Account Account)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var AccountModel = new AccountModel();

            //AccountModel.StartDate = Account.StartDate;
            AccountModel.Balance = Account.Balance;
            AccountModel.Status = Account.Status;
            AccountModel.AccountNumber = Account.AccountNumber;
            return AccountModel;
        }

        public Account AccountModelToAccount(AccountModel AccountModel)
        {
            var Account = new Account();

            //Account.StartDate = AccountModel.StartDate;
            Account.Balance = AccountModel.Balance;
            Account.Status = AccountModel.Status;
            Account.AccountNumber = AccountModel.AccountNumber;

            return Account;
        }

    }


}