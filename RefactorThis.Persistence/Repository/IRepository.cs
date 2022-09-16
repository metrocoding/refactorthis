namespace RefactorThis.Persistence.Repository
{
    public interface IRepository<T>
    {
        T Get(string reference);
        void Save();
        void Add(T entity);
    }
}