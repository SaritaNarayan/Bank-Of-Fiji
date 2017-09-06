using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using EOPS.Domain.Entities;
using EOPS.Infrastructure.EOPS.Persistence.Fluent.NHibernate;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace EOPS.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable
    {

        #region Members

        protected ISession _session;
        protected ITransaction _transaction = null;

        #endregion

        #region Constructors

        public Repository()
        {
            _session = FluentNHibernateSessionManager.OpenSession();
        }

        public Repository(ISession session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session = session;
        }

        #endregion

        #region Transaction and Session Management Methods
        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            // _transaction will be replaced with a new transaction
            // by NHibernate, but we will close to keep a consistent state.
            _transaction.Commit();

            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            // _session must be closed and disposed after a transaction
            // rollback to keep a consistent state.
            if (_transaction != null)
            {
                _transaction.Rollback();
                CloseTransaction();
            }

            //CloseTransaction();
            //CloseSession();
        }

        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        private void CloseSession()
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }

        #endregion

        #region IRepository Members

        public IQueryable<T> GetList()
        {
            return (from entity in _session.Query<T>() select entity);
        }

        public IQueryable<T> GetList<T>()
        {
            var query = from entity in _session.Query<T>() select entity;
            return query;
        }

        public IList<T> FindByNameFromStart(Type t, string criteria, string name)
        {
            return _session.CreateCriteria(typeof(T))
                    .Add(Restrictions.Like(criteria, name, MatchMode.Start))
                    .List<T>();
        }

        public IList<T> GetListofReferenceData()
        {
            return GetList().ToList();
        }

        public T GetById(int id)
        {
            return _session.Get<T>(id);
        }
        public T GetById<T>(int id)
        {
            return _session.Get<T>(id);
        }

        public int Save<T>(T entity) where T : Entity
        {
            entity.CreateTimestamp();
            return (int)_session.Save(entity);
        }

        public void Update<T>(T entity) where T : Entity
        {
            entity.UpdateTimestamp();
            _session.Update(entity);
        }

        public void SaveOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public T GetOne(QueryBase<T> query)
        {
            return query.SatisfyingElementFrom(_session.Query<T>());
        }

        public IQueryable<T> GetList(QueryBase<T> query)
        {
            return query.SatisfyingElementsFrom(_session.Query<T>());
        }

        /// <summary>
        /// Gets by value of the field name specified.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="criteria">Field Name</param>
        /// <param name="name">Value to search for</param>
        /// <returns></returns>
        public IList<T> GetByName(Type t, string criteria, string name)
        {
            return _session.CreateCriteria(typeof(T))
                    .Add(Restrictions.Eq(criteria, name))
                    .List<T>();
        }

        public IList<T> FindByName(Type t, string criteria, string name)
        {
            return _session.CreateCriteria(typeof(T))
                    .Add(Restrictions.Like(criteria, name, MatchMode.Anywhere))
                    .List<T>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_transaction != null)
            {
                // Commit transaction by default, unless user explicitly rolls it back.
                // To rollback transaction by default, unless user explicitly commits,
                // comment out the line below.
                //CommitTransaction();
            }

            // Commented out because we don't want automatically flush and close session
            //if (_session != null)
            //{
            //    _session.Flush(); // commit session transactions
            //    CloseSession();
            //}
        }

        #endregion
    }

    public abstract class QueryBase<T>
    {
        public abstract Expression<Func<T, bool>> MatchingCriteria { get; }

        public T SatisfyingElementFrom(IQueryable<T> candidates)
        {
            return SatisfyingElementsFrom(candidates).Single();
        }

        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return candidates.Where(MatchingCriteria).AsQueryable();
        }
    }





}