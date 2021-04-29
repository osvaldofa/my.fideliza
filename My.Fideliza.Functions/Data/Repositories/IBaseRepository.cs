using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Fideliza.Functions.Data
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        List<TEntity> FindAll();

        TEntity FindById(int Id);
    }
}
