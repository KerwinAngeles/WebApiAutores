using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Range(18, 85)]
        public int Edad { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }

        public List<Libro> Libros { get; set; }
    }
}
