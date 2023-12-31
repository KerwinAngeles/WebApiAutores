using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Data;
using WebApiAutores.Models;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
           return await _context.Autores.Include(l => l.Libros).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var primerAutor = await _context.Autores.FirstOrDefaultAsync(i => i.Id == id);
            if (primerAutor == null)
            {
                return NotFound();
            }
            return Ok(primerAutor);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> GetAutorNombre(string nombre)
        {
            var nombreAutor = await _context.Autores.FirstOrDefaultAsync(n => n.Nombre == nombre);
            if(nombreAutor == null)
            {
                return NotFound();
            }
            return Ok(nombreAutor);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok("Autor creado.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id !=  autor.Id)
            {
                return BadRequest("El id de la url no coincide con el id del autor");
            }
            var existe = await _context.Autores.AnyAsync(i => i.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok("Autor actualizado.");

        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> Delete(int id)
        {
            var exixte = await _context.Autores.AnyAsync(i => i.Id == id);
            if(!exixte)
            {
                return NotFound();
            }
         
            _context.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return Ok("Autor Eliminado");
        }
    }
}
