using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;

namespace src.Proyecto.Modelos.Repositorios
{
    public interface IRolRepository
    {
        IEnumerable<Rol> GetAll();
        void Add(Rol rol);
        Rol? GetById(int idRol);
         void Delete(int idRol);
    }
}