namespace Proyecto.Core.Repositorios;

public interface IFuncionRepository
{
    IEnumerable<Funcion> GetAll();
    Funcion? GetById(int IdFuncion);
    void Add(Funcion funcion);
    void Update(Funcion funcion);
    void Delete(int IdFuncion);

}
