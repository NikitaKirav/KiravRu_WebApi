using System;
using System.Threading;
using System.Threading.Tasks;
using KiravRu.Infrastructure;
using KiravRu.Logic.Domain;
using KiravRu.Logic.Domain.HistoryChanges;
using KiravRu.Logic.Mediator.Commands.Arts;
using KiravRu.Logic.Mediator.Queries.Arts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/projects/artcanvas")]
    [ApiController]
    public class ArtApiController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ArtApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("uploadImage")]
        public async Task<ActionResult> UploadImage([FromBody] object imageData, CancellationToken ct)
        {
            try
            {
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                Program.Logger.Info("remoteIpAddress: " + remoteIpAddress);
                // Get last history saving
                var query = new CheckRemoteIpAddressOfUserQuery(remoteIpAddress);
                var result = await _mediator.Send(query, ct);

                if (result != "Ok") { return Ok(new { result = result }); }
                var image = JsonConvert.DeserializeObject<Image>(imageData.ToString());
                string pathFileImage = ImageDrawing.SaveImage(image.ImageData);

                // Save history
                var command = new AddNewHistoryNoteCommand()
                {
                    HistoryChange = new HistoryChange()
                    {
                        RemoteIpAddress = remoteIpAddress,
                        ModuleChanged = "DrawEveryOne",
                        Operation = "AddNewFile",
                        Changes = pathFileImage,
                        DateChanges = DateTime.Now
                    }
                };
                await _mediator.Send(command, ct);

                var files = ImageDrawing.GetFilesList();
                return Ok(ImageDrawing.BubbleSort(files));
            }
            catch (Exception ex)
            {
                Program.Logger.Error(ex.Message);
                return Ok(new { error="Bad request!" });
            }
        }

        [HttpGet]
        [Route("imagePaths")]
        public ActionResult ImagePaths()
        {
            var files = ImageDrawing.GetFilesList();
            return Ok(ImageDrawing.BubbleSort(files));
        }

    }
}
