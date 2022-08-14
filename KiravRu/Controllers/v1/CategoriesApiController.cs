using System.Threading;
using System.Threading.Tasks;
using KiravRu.Controllers.v1.Model;
using KiravRu.Logic.Mediator.Commands.Categories;
using KiravRu.Logic.Mediator.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetCategoryList(CancellationToken ct)
        {
            var query = new GetCategoryListQuery();

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("editing/{id}")]
        public async Task<ActionResult> GetCategoryForEditingById(int id, CancellationToken ct)
        {
            var query = new GetCategoryForEditingQuery()
            {
                CategoryId = id
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("editing")]
        public async Task<ActionResult> SetCategory(SetCategoryRequestModel category, CancellationToken ct)
        {
            if ((category != null) && (category.Category != null))
            {
                var command = new SetCategoryCommand()
                {
                    Id = category.Category.Id,
                    Description = category.Category.Description,
                    ImagePath = category.Category.ImagePath,
                    ImageText = category.Category.ImageText,
                    Name = category.Category.Name,
                    NestingLevelId = category.Category.NestingLevelId,
                    OrderItem = category.Category.OrderItem,
                    Visible = category.Category.Visible
                };

                var result = await _mediator.Send(command, ct);

                return Ok(result);
            }
            return BadRequest();
        }


        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            var command = new DeleteCategoryCommand()
            {
                CategoryId = id
            };

            await _mediator.Send(command, ct);

            return Ok();
        }
    }
}
