using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiravRu.Infrastructure;
using KiravRu.Interfaces;
using KiravRu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KiravRu.Controllers
{
    [Route("api/projects/artcanvas")]
    [ApiController]
    public class ArtApiController : ControllerBase
    {

        private readonly IHistoryChange _historyChange;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public ArtApiController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IHistoryChange historyChange)
        {
            _historyChange = historyChange;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("uploadImage")]
        public ActionResult UploadImage([FromBody] object imageData)
        {
            try
            {            
                var result = CheckRemoteIpAddressOfUser();
                if (result != "Ok") { return Ok(new { result = result }); }
                var image = JsonConvert.DeserializeObject<Image>(imageData.ToString());
                string pathFileImage = ImageDrawing.SaveImage(image.ImageData);
                AddNewHistoryNote(pathFileImage);

                var files = ImageDrawing.GetFilesList();
                return Ok(ImageDrawing.BubbleSort(files));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("imagePaths")]
        public ActionResult ImagePaths()
        {
            var files = ImageDrawing.GetFilesList();
            return Ok(ImageDrawing.BubbleSort(files));
        }

        private void AddNewHistoryNote(string pathFileImage)
        {
            HistoryChange historyChange = new HistoryChange()
            {
                RemoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                ModuleChanged = "DrawEveryOne",
                Operation = "AddNewFile",
                Changes = pathFileImage,
                DateChanges = DateTime.Now
            };
            _historyChange.AddHistoryChange(historyChange);
        }

        private string CheckRemoteIpAddressOfUser()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            DateTime lastDate = _historyChange.GetLastDateTimeUsingIpAddress(remoteIpAddress);
            if (DateTime.Now < lastDate.AddMinutes(15))
            {
                var span = lastDate.AddMinutes(15) - DateTime.Now;
                return string.Format("{0}", span.Minutes);
            }
            return "Ok";
        }

    }
}
