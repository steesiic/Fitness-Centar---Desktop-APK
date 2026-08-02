using NHibernate;

namespace FitnessCentar.Data
{
    public interface IDataLayer
    {
        ISession OpenSession();
    }
}
