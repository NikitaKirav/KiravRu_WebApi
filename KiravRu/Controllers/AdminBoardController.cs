using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    public class AdminBoardController : Controller
    {
        // GET: AdminBoard
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Users()
        //{
            //UserViewModel model = new UserViewModel();

            //model.Roles = new List<IdentityRoles>();

            //return View(model);
        //}
 
    }
}