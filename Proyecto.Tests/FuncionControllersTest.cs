/* using Xunit;
using Proyecto.Web.Api.Controllers;
using Proyecto.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Proyecto.Tests
{
    public class FuncionControllerTests
    {
        private readonly FuncionController _controller;

        public FuncionControllerTests()
        {
            _controller = new FuncionController();
        }

        [Fact]
        public void Get_ReturnsListOfFunciones()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var funciones = Assert.IsType<List<Funcion>>(okResult.Value);
            Assert.NotNull(funciones);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenFuncionExists()
        {
            var result = _controller.Get(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenFuncionDoesNotExist()
        {
            var result = _controller.Get(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_CreatesFuncion_ReturnsCreatedAtAction()
        {
            var nuevaFuncion = new Funcion
            {
                Id = 10,
                Nombre = "Nueva Función",
                Descripcion = "Función de prueba"
            };

            var result = _controller.Post(nuevaFuncion);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var funcionCreada = Assert.IsType<Funcion>(createdResult.Value);
            Assert.Equal("Nueva Función", funcionCreada.Nombre);
        }

        [Fact]
        public void Put_UpdatesFuncion_ReturnsNoContent()
        {
            var funcionActualizada = new Funcion
            {
                Id = 1,
                Nombre = "Actualizada",
                Descripcion = "Cambio de prueba"
            };

            var result = _controller.Put(1, funcionActualizada);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_RemovesFuncion_ReturnsNoContent()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
 */