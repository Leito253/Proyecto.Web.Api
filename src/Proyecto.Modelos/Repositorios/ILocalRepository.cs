using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios
{
    public interface ILocalRepository
    {
        IEnumerable<Local> GetAll();
        Local? GetById(int idLocal);
        int Add(Local local);
        void Update(Local local);
        bool Delete(int idLocal);
        bool TieneFuncionesVigentes(int idLocal);
    }
}
