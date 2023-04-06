using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public interface IRepository <T>
    {
        int NextId();
        List<T> GetAll();
        T GetById(int id);
        void Save(T entity);
        void Delete(T entity);
        void SaveAll(List<T> entities);
        void Update(T entity);  
    }
}
