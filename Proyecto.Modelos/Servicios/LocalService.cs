using Proyecto.Modelos.Entidades;

namespace Servicios;
public class LocalService : ILocalService
{

    private readonly List<Local> _locales = new List<Local>();

    /* public LocalService()
    {
        // Inicializar con algunos datos de ejemplo
        _locales.AddRange(new[]
        {
            new Local { idLocal = 1, Nombre = "Cancha Monumental", Direccion = "Av. Figueroa Alcorta 7597, CABA", Capacidad = 83000 },
            new Local { idLocal = 2, Nombre = "Estadio Libertadores de América", Direccion = "Av. Pres. Figueroa Alcorta 7597, CABA", Capacidad = 66000 },
            new Local { idLocal = 3, Nombre = "Estadio Ciudad de La Plata", Direccion = "Av. 25 y 32, La Plata, Buenos Aires", Capacidad = 53000 },
            new Local { idLocal = 4, Nombre = "Luna Park", Direccion = "Av. Eduardo Madero 470, C1106 Cdad. Autónoma de Buenos Aires", Capacidad = 8400 },
            new Local { idLocal = 5, Nombre = "Estadio Único Diego Armando Maradona", Direccion = "Av. Pres. Juan Domingo Perón 3500, La Plata, Buenos Aires", Capacidad = 53000 }
        });
    } */

    public Local Create(Local newLocal)
    {
        throw new NotImplementedException();
        
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Local> GetAll()
    {
        throw new NotImplementedException();
    }

    public Local? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Local? Update(int id, Local updatedLocal)
    {
        throw new NotImplementedException();
    }
}
