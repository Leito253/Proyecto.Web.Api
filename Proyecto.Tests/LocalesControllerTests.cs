/* using Microsoft.AspNetCore.Mvc;
using Moq;
using Proyecto.Modelos.Controllers;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using Xunit;
using System.Collections.Generic;

namespace Proyecto.Tests
{
    public class LocalesControllerTests
    {
        [Fact]
        public void GetById_ReturnsNotFound_WhenLocalDoesNotExist()
        {
            var mock = new Mock<ILocalRepository>();
            mock.Setup(r => r.GetById(1)).Returns((Local)null!);
            var controller = new LocalController(mock.Object);

            var result = controller.GetById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenLocalExists()
        {
            var local = new Local { idLocal = 1, Nombre = "Teatro", Direccion = "Calle 123", Telefono = "123456789" };
            var mock = new Mock<ILocalRepository>();
            mock.Setup(r => r.GetById(1)).Returns(local);
            var controller = new LocalController(mock.Object);

            var result = controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Local>(okResult.Value);
            Assert.Equal(local.idLocal, returned.idLocal);
        }

        [Fact]
        public void GetAll_ReturnsListOfLocales()
        {
            var lista = new List<Local> {
                new Local { idLocal = 1, Nombre = "Teatro", Direccion = "Calle 1", Telefono = "123456789" },
                new Local { idLocal = 2, Nombre = "Estadio", Direccion = "Avenida 2", Telefono = "987654321" }
            };
            var mock = new Mock<ILocalRepository>();
            mock.Setup(r => r.GetAll()).Returns(lista);
            var controller = new LocalController(mock.Object);

            var result = controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Local>>(okResult.Value);
            Assert.Equal(2, ((List<Local>)returned).Count);
        }
    }
} */