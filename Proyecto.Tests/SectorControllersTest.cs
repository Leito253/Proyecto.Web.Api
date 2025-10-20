/* using Microsoft.AspNetCore.Mvc;
using Moq;
using Proyecto.Modelos.Controllers;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using Xunit;
using System.Collections.Generic;

namespace Proyecto.Tests
{
    public class SectorControllerTests
    {
        [Fact]
        public void GetByLocal_ReturnsListOfSectores()
        {
            var sectores = new List<Sector> {
                new Sector { idSector = 1, Nombre = "VIP", Capacidad = 100, idLocal  = 1, Precio = 500 },
                new Sector { idSector = 2, Nombre = "General", Capacidad = 200, idLocal = 1, Precio = 300 }
            };

            var mock = new Mock<ISectorRepository>();
            mock.Setup(r => r.GetByLocal(1)).Returns(sectores);

            var controller = new SectorController(mock.Object);

            var result = controller.GetByLocal(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Sector>>(okResult.Value);
            Assert.Equal(2, ((List<Sector>)returned).Count);
        }

        [Fact]
        public void Create_ReturnsCreatedAtAction()
        {
            var sector = new Sector { idSector = 1, Nombre = "VIP", Capacidad = 100, idLocal = 1, Precio = 500 };

            var mock = new Mock<ISectorRepository>();
            var controller = new SectorController(mock.Object);

            var result = controller.Create(1, sector);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<Sector>(created.Value);
            Assert.Equal(sector.idSector, returned.idSector);
        }

        [Fact]
        public void Update_ReturnsNoContent_WhenIdsMatch()
        {
            var sector = new Sector { Id = 1, Nombre = "VIP", Capacidad = 100, LocalId = 1 };

            var mock = new Mock<ISectorRepository>();
            var controller = new SectorController(mock.Object);

            var result = controller.Update(1, sector);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var sector = new Sector { Id = 1, Nombre = "VIP", Capacidad = 100, LocalId = 1 };

            var mock = new Mock<ISectorRepository>();
            var controller = new SectoresController(mock.Object);

            var result = controller.Update(2, sector);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent()
        {
            var mock = new Mock<ISectorRepository>();
            var controller = new SectoresController(mock.Object);

            var result = controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
} */