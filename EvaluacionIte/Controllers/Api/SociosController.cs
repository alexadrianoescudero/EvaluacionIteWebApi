using DatosEvaluacion.Model;
using DatosEvaluacion.Data;
using EvaluacionIte.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluacionIte.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<SociosController>
        [Route("ListarSocios")]
        [HttpGet]
        public List<Socio> Get()
        {
            return _context.Socios.ToList();
        }

        // GET api/<SociosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SociosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SociosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SociosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
