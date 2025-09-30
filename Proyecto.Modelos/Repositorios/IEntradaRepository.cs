using Proyecto.Modelos.Entidades;
using System.Collections.Generic;

namespace Proyecto.Modelos.Interfaces
{
    public interface IEntradaRepository
    {
        IEnumerable<Entrada> GetAll();
        Entrada? GetById(int id);
        void Anular(int id);

    }
}