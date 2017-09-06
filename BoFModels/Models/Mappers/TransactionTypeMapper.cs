using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.BoFModels.Models.Mappers
{
    public class TransactionTypeMapper
    {
        private readonly ISession _session;

        public TransactionTypeMapper(ISession session)
        {
            this._session = session;
        }

        public TransactionTypeModel TransactionTypeToTransactionTypeModel(TransactionType TransactionType)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var TransactionTypeModel = new TransactionTypeModel();

            TransactionTypeModel.TransName = TransactionType.TransName;

            return TransactionTypeModel;
        }

        public TransactionType TransactionTypeModelToTransactionType(TransactionTypeModel TransactionTypeModel)
        {
            var TransactionType = new TransactionType();

            TransactionType.TransName = TransactionTypeModel.TransName;

            return TransactionType;
        }

    }


}