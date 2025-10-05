using Microsoft.AspNetCore.Mvc;
using Moq;
using Proyecto.Modelos.Controllers;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using Xunit;

namespace Proyecto.Tests
{
    public class OrdenControllerTests
    {
        [Fact]
        public void GetById_ReturnsNotFound_WhenOrdenNull()
        {
            // Arrange
            var mockRepo = new Mock<IOrdenRepository>();
            mockRepo.Setup(r => r.GetById(1)).Returns((Orden)null!);
            var controller = new OrdenController(mockRepo.Object);

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenOrdenFound()
        {
            // Arrange
            var orden = new Orden { NumeroOrden = 1,  Estado = "Pendiente" };
            var mockRepo = new Mock<IOrdenRepository>();
            mockRepo.Setup(r => r.GetById(1)).Returns(orden);
            var controller = new OrdenController(mockRepo.Object);

            // Act
            var result = controller.GetById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Orden>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedOrden = Assert.IsType<Orden>(okResult.Value);
            Assert.Equal(orden.NumeroOrden, returnedOrden.NumeroOrden);
            Assert.Equal(orden.Total, returnedOrden.Total);
        }
    }
}