using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Services
{
    public interface IApiRequest <T>
    {
        List<T> GetAll(string controllerName, string endpoint = "");
        T GetSingle(string controllerName, int id);
        T Create(string controllerName, T entity);
        T Edit(string controllerName, int id, T entity);
        void Delete(string controllerName, int id);

        List<T> GetChildrenForParentID(string controllerName, string endpoint, int id);
    }
}
