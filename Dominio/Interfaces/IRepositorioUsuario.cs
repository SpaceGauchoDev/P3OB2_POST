using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioUsuario<T> where T:class
    {
        bool Add(T objeto);
        T FindById(int id);
        IEnumerable<T> FindAll();
        bool Remove(object objeto);
        bool Update(T objeto);
    }
}
