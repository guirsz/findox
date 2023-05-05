namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface ICrudRepository<T>
    {
        /// <summary>
        /// It gets only single record, no matter if it was deleted or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// It inserts one single row
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// It update one single row accordingly to id
        /// </summary>
        /// <param name="groupEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T groupEntity);
    }
}
