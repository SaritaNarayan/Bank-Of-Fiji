using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.Web.Models.Mappers
{
    public class AccountTypeMapper
    {
        private readonly ISession _session;

        public AccountTypeMapper(ISession session)
        {
            this._session = session;
        }

        public AccountTypeModel AccountTypeToAccountTypeModel(AccountType AccountType)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var AccountTypeModel = new AccountTypeModel();

            AccountTypeModel.AccountName = AccountType.AccountName;
            AccountTypeModel.InterestRate= AccountType.InterestRate;

            return AccountTypeModel;
        }

        public AccountType AccountTypeModelToAccountType(AccountTypeModel AccountTypeModel)
        {
            var AccountType = new AccountType();

            AccountType.AccountName = AccountTypeModel.AccountName;
            AccountType.InterestRate = AccountTypeModel.InterestRate;

            return AccountType;
        }

    }


}