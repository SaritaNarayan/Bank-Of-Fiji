using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using EOPS.Domain.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace EOPS.Infrastructure.EOPS.Persistence.Fluent.NHibernate
{
    public static class FluentNHibernateSessionManager
    {
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Custom mapping sessionfactory.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static ISessionFactory CreateSessionFactory()
        {
            if (_sessionFactory == null)
            {
                var cfg = MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c.FromAppSetting("connectionString"))
                    .AdoNetBatchSize(256);

                _sessionFactory = Fluently.Configure()
                    .Database(cfg)
                    //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<ApplicationBase>().Conventions.Add<CustomForeignKeyConventionNew>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Module>().Conventions.Add<CustomForeignKeyConventionNew>())
                    .BuildSessionFactory();

            }
            return _sessionFactory;
        }

        /// <summary>
        /// Automapping based sessionfactory.
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory CreateSessionFactoryAuto()
        {
            if (_sessionFactory == null)
            {
                var autoPersistenceModel = CreateMapping();

                var cfg = MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromAppSetting("connectionString"))
                        .AdoNetBatchSize(256);

                _sessionFactory = Fluently.Configure()
                    .Database(cfg)
                    .Mappings(m => m.AutoMappings.Add(autoPersistenceModel))
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();

                // .Mappings(m => m.AutoMappings.Add(autoPersistenceModel.Conventions.Add<StringColumnLengthConvention>()))//                       Added convention to set string size at 10000
            }

            return _sessionFactory;
        }

        /// <summary>
        /// Automapping mapper.
        /// </summary>
        /// <returns></returns>
        public static AutoPersistenceModel CreateMapping()
        {
            AutoPersistenceModel autoPersistenceModel = null;

            autoPersistenceModel = AutoMap
            .Assembly(System.Reflection.Assembly.GetCallingAssembly())
            .Where(t => t.IsSubclassOf(typeof(Entity)));
            //.Conventions.AddFromAssemblyOf<CascadeAll>();

            return autoPersistenceModel;
        }

        /// <summary>
        /// Session opener.
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
            //return CreateSessionFactory().OpenSession();
            return CreateSessionFactoryAuto().OpenSession();
        }

        /// <summary>
        /// Builds schema (database tables) if export parameter is set to true.
        /// </summary>
        /// <param name="config"></param>
        public static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, false);
            //new SchemaExport(config).SetOutputFile(@"C:\dev\MyRMIS.sql").Create(true, false);
        }

    }

    /// <summary>
    /// Allows for foreign key as xxxid.
    /// </summary>
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";  // many-to-many, one-to-many, join

            return property.Name + "Id"; // many-to-one
        }
    }

    /// <summary>
    /// Allows for foreign key id as xxx_id.
    /// </summary>
    public class CustomForeignKeyConventionNew : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "_Id";  // many-to-many, one-to-many, join

            return property.Name + "_Id"; // many-to-one
        }
    }

    /// <summary>
    /// Added convention   to set the default length for string properties to 10000
    /// This will be a nvarchar(max) column.
    /// </summary>
    public class StringColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(string)).Expect(x => x.Length == 0);
        }
        public void Apply(IPropertyInstance instance)
        {
            instance.Length(10000);
        }
    }

}