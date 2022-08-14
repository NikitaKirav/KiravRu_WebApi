using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using KiravRu.Controllers.v1.Model;
using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Mediator.Commands.Notes;
using KiravRu.Logic.Mediator.Queries.Notes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/notes")]
    [ApiController]
    public class NotesApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("getAllWithFilter")]
        public async Task<IActionResult> GetNotesAsync(NoteFilter noteFilter, CancellationToken ct)
        {
            if (noteFilter.Search == null) { noteFilter.Search = ""; }
            var userRoles = GetCurrentRolesOfUser();
            var query = new GetNotesQuery() 
            {
                NoteFilter = noteFilter,
                UserRoles = userRoles,
                DefaultSort = "Name"
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetNoteById(int id, CancellationToken ct)
        {
            var userRoles = GetCurrentRolesOfUser();
            var query = new GetNoteByIdQuery()
            {
                NoteId = id,
                UserRoles = userRoles
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("editing/{id}")]
        public async Task<IActionResult> GetNoteForEditingById(int id, CancellationToken ct)
        {
            var query = new GetNoteForEditingByIdQuery()
            {
                NoteId = id
            };

            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("editing")]
        public async Task<ActionResult> SaveNote(SaveNoteRequestModel param, CancellationToken ct)
        {
            if((param != null) && (param.Article != null) && (param.Roles != null))
            {
                var command = new SaveNoteCommand()
                {
                    Note = new Note()
                    {
                        Id = param.Article.Id,
                        Name = param.Article.Name,
                        ImagePath = param.Article.ImagePath,
                        ImageText = param.Article.ImageText,
                        Text = param.Article.Text,
                        ShortDescription = param.Article.ShortDescription,
                        CategoryId = param.Article.CategoryId,
                        IsFavorite = param.Article.IsFavorite,
                        Visible = param.Article.Visible,
                        DifficultyLevel = param.Article.DifficultyLevel
                    },
                    Roles = param.Roles.ToList()
                };

                var noteId = await _mediator.Send(command, ct);

                var query = new GetNoteForEditingByIdQuery()
                {
                    NoteId = noteId
                };

                var result = await _mediator.Send(query, ct);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            var command = new DeleteNoteCommand()
            {
                NoteId = id
            };

            await _mediator.Send(command, ct);

            return Ok();
        }

        private IList<string> GetCurrentRolesOfUser()
        {
            var rolesClaims = HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.Role);
            List<string> roles = new List<string>();
            foreach (var item in rolesClaims)
            {
                roles.Add(item.Value);
            }
            //// default is an user
            if (roles.Count == 0) { return new List<string>() { "user" }; }
            return roles;
        }
    }
}