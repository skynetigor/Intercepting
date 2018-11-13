using System.Collections.Generic;
using Core.Models;

namespace Core.DAL
{
    public interface IGenericRepository<T> where T: BaseModel
    {
        void Add(T model);

        bool Remove(T model);

        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
