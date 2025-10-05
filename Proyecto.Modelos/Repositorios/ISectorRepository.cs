using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;


namespace Proyecto.Modelos.Repositorios;

public interface ISectorRepository
{
    IEnumerable<Sector> GetAll();
    Sector? GetById(int idSector);
    void Add(Sector sector);
    void Update(Sector sector);
    void Delete(int idSector);
    object? GetByLocal(int idLocal);
    void Add(int idLocal, Sector sector);
}