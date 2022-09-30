using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBKMNET_WebApps.Context;
using SIBKMNET_WebApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_WebApps.Controllers
{
    public class ProvinceController : Controller
    {
        MyContext myContext;

        public ProvinceController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // GET ALL
        // GET
        public IActionResult Index()
        {
            var data = myContext.Provinces.Include(x => x.Region).ToList();
            return View(data);
        }

        // GET BY ID
        //GET
        public IActionResult Details(int id)
        {
            var data = myContext.Provinces.Include(x => x.Region).FirstOrDefault(x => x.Id.Equals(id));
            return View(data);
        }

        // CREATE 
        // GET
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Province province)
        {
            if (ModelState.IsValid)
            {
                myContext.Provinces.Add(province);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View();
        }

        // UPDATE
        // GET
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = myContext.Provinces.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        // POST
        [HttpPost]
        public ActionResult Edit(Province Model)
        {
            var data = myContext.Provinces.Where(x => x.Id == Model.Id).FirstOrDefault();
            if (data != null)
            {
                data.Id = Model.Id;
                data.Name = Model.Name;
                data.RegionId = Model.RegionId;

                myContext.SaveChanges();
            }

            return RedirectToAction("index");
        }

        // Delete
        // DELETE
        public ActionResult Delete(int id)
        {
            var data = myContext.Provinces.Where(x => x.Id == id).FirstOrDefault();
            myContext.Provinces.Remove(data);
            myContext.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("index");
        }
    }
}
