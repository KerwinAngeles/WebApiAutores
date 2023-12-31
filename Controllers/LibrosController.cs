using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Data;
using WebApiAutores.Migrations;
using WebApiAutores.Models;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(l => l.Id == libro.AutorId);
            if(!existeAutor)
            {
                return BadRequest();
            }
            var existeLibro = await _context.Libros.AnyAsync(l => l.Titulo == libro.Titulo);
            if(existeLibro)
            {
                return BadRequest("the book title already exist.");
            }
            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok("Libro creado");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Libro libro, int id)
        {
            if (libro.Id != id)
            {
                return BadRequest("El id de la url no coincide con el id del autor");
            }

            var existe = await _context.Libros.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return BadRequest("El libro no existe");
            }

            _context.Update(libro);
            await _context.SaveChangesAsync();
            return Ok($" El libro con el id {libro.Id} ha sido actualizado");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Libros.AnyAsync(l => l.Id == id);
            if(!existe)
            {
                return BadRequest("El libro no existe");
            }

            _context.Remove(new Libro { Id = id });
            await _context.SaveChangesAsync();
            return Ok("Libro eliminado");
        }
    }
}
