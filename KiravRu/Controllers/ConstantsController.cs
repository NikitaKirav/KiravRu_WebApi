using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiravRu.Interfaces;
using KiravRu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    public class ConstantsController : Controller
    {
        private readonly IConstant _constants;

        public ConstantsController(IConstant constants)
        {
            _constants = constants;
        }

        // GET: Constant
        public ActionResult Index()
        {
            var consts = _constants.AllConstants;
            return View(consts);
        }

        [HttpPost]
        public ActionResult Save(Constant constant)
        {
            var result = false;
            if ((constant.Value != null) && (constant.Name != null))
            {
                result = _constants.UpdateConstant(constant);
            }
            return Ok(result);
        }

        // GET: Constant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Constant/Create
        [HttpPost]
        public ActionResult Create(Constant constant)
        {
            var result = false;
            if ((constant.Value != null) && (constant.Name != null) &&
                (constant.Value != "") && (constant.Name != ""))
            {
                result = _constants.CreateConstant(constant);
            }
            return Ok(result);
        }

        // GET: Constant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Constant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Constant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Constant/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}