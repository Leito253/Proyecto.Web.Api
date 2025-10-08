using Proyecto.Modelos.Entidades;
using System.Collections.Generic;

namespace Proyecto.Modelos.Interfaces
{
    public interface IEntradaRepository
    {
        IEnumerable<Entrada> GetAll();
        Entrada? GetById(int idEntrada);
        void Anular(int idEntrada);
        void Update(Entrada entrada);
    }
}