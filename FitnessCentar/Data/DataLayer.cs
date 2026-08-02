using FitnessCentar.Infrastructure.Mappers;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Configuration;

namespace FitnessCentar.Data
{
    public class DataLayer : IDataLayer
    {
        private readonly ISessionFactory _sessionFactory;

        public DataLayer()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SBP"].ConnectionString;

            var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ConnectionString(c => c.Is(connectionString));

            _sessionFactory = Fluently.Configure()
                .Database(cfg.ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<KorisnikMapper>())
                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
