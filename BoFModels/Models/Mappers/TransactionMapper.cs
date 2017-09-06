using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.BoFModels.Models.Mappers
{
    public class TransactionMapper
    {
        private readonly ISession _session;

        public TransactionMapper(ISession session)
        {
            this._session = session;
        }

        public TransactionModel TransactionToTransactionModel(Transaction Transaction)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var TransactionModel = new TransactionModel();

            TransactionModel.TransAmount = Transaction.TransAmount;
            TransactionModel.TransDateTime = Transaction.TransDateTime;
            TransactionModel.TransDetails = Transaction.TransDetails;
            TransactionModel.Debit = Transaction.Debit;
            TransactionModel.Credit = Transaction.Credit;
            TransactionModel.RunningCapital = Transaction.RunningCapital;


            return TransactionModel;
        }

        public Transaction TransactionModelToTransaction(TransactionModel TransactionModel)
        {
            var Transaction = new Transaction();

            Transaction.TransAmount = TransactionModel.TransAmount;
            Transaction.TransDateTime = TransactionModel.TransDateTime;
            Transaction.TransDetails = TransactionModel.TransDetails;
            Transaction.Debit = TransactionModel.Debit;
            Transaction.Credit = TransactionModel.Credit;
            Transaction.RunningCapital = TransactionModel.RunningCapital;

            return Transaction;
        }

    }


}