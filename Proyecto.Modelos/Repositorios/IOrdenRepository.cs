using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;


namespace Proyecto.Modelos.Repositorios;

public interface IOrdenRepository
{
    IEnumerable<Orden> GetAll();
    Orden? GetById(int idOrden);
    void Add(Orden orden);
    void Update(Orden orden);
    void Pagar(int IdOrden);   
    void Cancelar(int IdOrden); 
    
}
