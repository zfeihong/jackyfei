using Iot.Application.Products.Command.CreateProduct;
using Iot.Application.Products.Command.DeleteProduct;
using Iot.Application.Products.Commands.UpdateProduct;
using Iot.Application.Products.Queries.ExportProduct;
using Iot.Application.Products.Queries.GetProducts;
using Iot.Data;
using Iot.Domain.Entities;
using Iot.WebApi.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Iot.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<ProductsVm>> Get()
        {
            return await Mediator.Send(new GetProductsQuery());
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportProductQuery { Id = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
