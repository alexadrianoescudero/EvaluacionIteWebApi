using DatosEvaluacion.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionIte.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Devuelve la vista del Index 
        /// </summary>
        /// <returns> Vista</returns>
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration =0, Location =ResponseCacheLocation.None, NoStore =true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }



        
    }
}
