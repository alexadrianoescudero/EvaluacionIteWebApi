using DatosEvaluacion.Data;
using DatosEvaluacion.Model;
using EvaluacionIte.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionIte.Controllers
{
    [Authorize]
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin,User")]
        // GET: SociosController
        public ActionResult Index()
        {
            List<Socio> ltssocios = new List<Socio>();
            ltssocios = _context.Socios.ToList();
            return View(ltssocios);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: SociosController/Details/5
        public ActionResult Details(string id)
        {
            Socio socio = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socio);
        }
        [Authorize(Roles = "Admin")]
        // GET: SociosController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: SociosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Socio socio)
        {
            try
            {
                socio.Estado = 1;
                _context.Add(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socio);
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: SociosController/Edit/5
        public ActionResult Edit(string id)
        {
            Socio socio = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socio);
        }

        // POST: SociosController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Socio socio)
        {
            if(id != socio.Cedula)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socio);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Activar(string id)
        {
            Socio socio = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            socio.Estado = 1;
            _context.Update(socio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Desactivar(string id)
        {
            Socio socio = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            socio.Estado = 0;
            _context.Update(socio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
