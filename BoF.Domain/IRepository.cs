using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Domain.Entities;

namespace BoF.Domain.Repository
{
    public interface IRepository<T>
    {

        /// <summary>
        /// Begins the transaction.
        /// 
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits and closes the transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back and closes the transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Gets an IQueryable result of all items in the database.
        /// Use linq queries to filter output.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetList();

        IQueryable<T> GetList<T>();

        /// <summary>
        /// Gets a single entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);
        T GetById<T>(int id);

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Save<T>(T entity) where T : Entity;

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity"></param>
        void Update<T>(T entity) where T : Entity;

        /// <summary>
        /// Saves or updates an entity depending on the identifier.
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(T entity);
        void Delete(T entity);
        /// <summary>
        /// Gets a single entity based on the NHibernate linq query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        T GetOne(QueryBase<T> query);

        /// <summary>
        /// Gets an IQueryable collection based on the NHibernate linq query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable<T> GetList(QueryBase<T> query);

        /// <summary>
        /// Generic GetByName query
        /// </summary>
        /// <param name="t"></param>
        /// <param name="criteria">Field Name</param>
        /// <param name="name">Value to search for</param>
        /// <returns></returns>
        IList<T> GetByName(Type t, string criteria, string name);

        /// <summary>
        /// Generic Find anywhere query
        /// </summary>
        /// <param name="t"></param>
        /// <param name="criteria"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<T> FindByName(Type t, string criteria, string name);

        /// <summary>
        /// Finds by criteria being start of the string
        /// </summary>
        /// <param name="t"></param>
        /// <param name="criteria"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<T> FindByNameFromStart(Type t, string criteria, string name);

        /// <summary>
        /// Gets an IList of reference data of choice.
        /// </summary>
        /// <returns></returns>
        IList<T> GetListofReferenceData();


    }
}