namespace Proyecto.Modelos.Repositorios
{
    public interface IEntradaRepository
    {
        IEnumerable<Entrada> GetAll();
        Entrada? GetById(int idEntrada);
        void Anular(int idEntrada);
        void Update(Entrada entrada);
    }
}