namespace DataAccessLayer.GenericRepository.Interface
{
    public interface IGenericRepository<T>where T:class
    {
        Task<T> AddNewData(T entity);
        Task<T> UpdateDate(T entity);
        Task DeleteData(T entity);
        Task<List<T>> GetAllData();
        Task<T> GetDataById(int? id); 
    }
}
