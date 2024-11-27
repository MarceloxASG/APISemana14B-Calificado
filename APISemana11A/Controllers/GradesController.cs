using APISemana11A.Models;
using APISemana11A.Requests;
using APISemana11A.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISemana11A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public GradesController(InvoiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Grade> GetAll()
        {
            return _context.Grades.ToList();
        }

        [HttpPost]
        public IActionResult InsertGrade(GradeRequest request)
        {
            Grade grade = new Grade
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = true
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return Ok(new { message = "Grado insertado correctamente." });
        }


        [HttpPut]
        public IActionResult UpdateGrade(GradeRequest request)
        {
            // Busca el grado por su ID
            Grade grade = _context.Grades.FirstOrDefault(x => x.GradeID == request.Id);

            if (grade == null)
            {
                return NotFound(new { message = "Grado no encontrado." });
            }

            // Actualiza las propiedades
            grade.Name = request.Name;
            grade.Description = request.Description;

            _context.Entry(grade).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Grado actualizado correctamente." });
        }


        [HttpDelete]
        public IActionResult DeleteGrade(int id)
        {
            // Busca el grado por su ID
            Grade grade = _context.Grades.FirstOrDefault(x => x.GradeID == id);

            if (grade == null)
            {
                return NotFound(new { message = "Grado no encontrado." });
            }

            // Cambia el estado de IsActive
            grade.IsActive = false;

            _context.Entry(grade).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Grado eliminado correctamente (IsActive = false)." });
        }

    }
}
