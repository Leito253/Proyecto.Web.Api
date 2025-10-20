/* using Xunit;
using Proyecto.Web.Api.Controllers;
using Proyecto.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Proyecto.Tests
{
    public class TarifaControllerTests
    {
        private readonly TarifaController _controller;

        public TarifaControllerTests()
        {
            _controller = new TarifaController();
        }

        [Fact]
        public void Get_ReturnsListOfTarifas()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tarifas = Assert.IsType<List<Tarifa>>(okResult.Value);
            Assert.NotEmpty(tarifas);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenTarifaExists()
        {
            var result = _controller.Get(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenTarifaDoesNotExist()
        {
            var result = _controller.Get(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_CreatesTarifa_ReturnsCreatedAtAction()
        {
            var nuevaTarifa = new Tarifa
            {
                Descripcion = "VIP",
                Precio = 5000
            };

            var result = _controller.Post(nuevaTarifa);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var tarifaCreada = Assert.IsType<Tarifa>(createdResult.Value);
            Assert.Equal("VIP", tarifaCreada.Descripcion);
        }

        [Fact]
        public void Put_UpdatesTarifa_ReturnsNoContent()
        {
            var tarifaActualizada = new Tarifa
            {
                Id = 1,
                Descripcion = "General Actualizada",
                Precio = 3000
            };

            var result = _controller.Put(1, tarifaActualizada);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_RemovesTarifa_ReturnsNoContent()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
 */