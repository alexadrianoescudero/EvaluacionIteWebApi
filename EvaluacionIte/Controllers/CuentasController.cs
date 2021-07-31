using DatosEvaluacion.Data;
using DatosEvaluacion.Model;
using DatosEvaluacion.ViewModel;
using EvaluacionIte.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionIte.Controllers
{
    [Authorize]
    public class CuentasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void Combox()
        {
            ViewData["CodigoSocio"] = new SelectList(_context.Socios.Select(x => new SocioCuenta
            {
                CedulasSocio = x.Cedula,
                NombreSocio = $"{x.Nombre} {x.Apellido} ",
                Estado = x.Estado
            }).Where(x => x.Estado == 1).ToList(), "CedulasSocio", "NombreSocio");
        }
        [Authorize(Roles = "Admin,User")]
        // GET: CuentasController
        public ActionResult Index()
        {
            List<SocioCuenta> ltsCuenta = new List<SocioCuenta>();
            ltsCuenta = _context.Cuenta.Select(x => new SocioCuenta
            {
                NumeroCuenta = x.Numero,
                Saldo = x.SaldoTotal,
                NombreSocio = $"{x.CodigoSocioNavigation.Nombre} {x.CodigoSocioNavigation.Apellido} ",
                Estado = x.Estado
            }).ToList();
            return View(ltsCuenta);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: CuentasController/Details/5
        public ActionResult Details(string id)
        {
            Cuenta cuenta = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            return View(cuenta);
        }
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            try
            {
                cuenta.Estado = 1;
                _context.Add(cuenta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Edit/5
        public ActionResult Edit(string id)
        {
            Cuenta cuenta = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            Combox();
            return View(cuenta);
        }
        [Authorize(Roles = "Admin")]
        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Cuenta cuenta)
        {
            if (id != cuenta.CodigoSocio)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Update(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(cuenta);
            }
        }

        /// <summary>
        /// Accion que Activa a las cuentas
        /// </summary>
        /// <param name="id">Codigo de la cuenta </param>
        /// <returns>Activacion de la cuenta</returns>        
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Delete/5        
        public ActionResult Activar(string id)
        {
            var datos = id != null ? 10 : 0;


            Cuenta cuenta = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            cuenta.Estado = 1;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Desactivar(string id)
        {
            Cuenta cuenta = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            cuenta.Estado = 0;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
