/* using Xunit;
using Proyecto.Web.Api.Controllers;
using Proyecto.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Proyecto.Tests
{
    public class EntradaControllerTests
    {
        private readonly EntradaController _controller;

        public EntradaControllerTests()
        {
            _controller = new EntradaController();
        }

        [Fact]
        public void Get_ReturnsListOfEntradas()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var entradas = Assert.IsType<List<Entrada>>(okResult.Value);
            Assert.NotEmpty(entradas);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenEntradaExists()
        {
            var result = _controller.Get(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenEntradaDoesNotExist()
        {
            var result = _controller.Get(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_CreatesEntrada_ReturnsCreatedAtAction()
        {
            var nuevaEntrada = new Entrada
            {
                Numero = "A003",
                FuncionId = 1,
                TarifaId = 1,
                Anulada = false
            };

            var result = _controller.Post(nuevaEntrada);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var entradaCreada = Assert.IsType<Entrada>(createdResult.Value);
            Assert.Equal("A003", entradaCreada.Numero);
        }

        [Fact]
        public void Put_UpdatesEntrada_ReturnsNoContent()
        {
            var entradaActualizada = new Entrada
            {
                Id = 1,
                Numero = "A001-EDIT",
                FuncionId = 1,
                TarifaId = 1,
                Anulada = false
            };

            var result = _controller.Put(1, entradaActualizada);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Anular_MarksEntradaAsAnulada()
        {
            var result = _controller.Anular(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var entrada = Assert.IsType<Entrada>(okResult.Value);
            Assert.True(entrada.Anulada);
        }

        [Fact]
        public void Delete_RemovesEntrada_ReturnsNoContent()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
 */