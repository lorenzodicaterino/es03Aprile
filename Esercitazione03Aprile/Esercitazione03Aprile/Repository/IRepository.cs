namespace Esercitazione03Aprile.Repository
{
    public interface IRepository<T>
    {
        bool Insert(T t);
        bool Update(T t);
        bool DeleteByCodice(string codice);
        List<T> GetAll();
        T GetById(int id);
        T GetByCodice(string codice);

    }
}
