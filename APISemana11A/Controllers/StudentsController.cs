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
    public class StudentsController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public StudentsController(InvoiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        [HttpPost]
        public IActionResult InsertStudent(StudentRequestV1 request)
        {
            Student student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                GradeID = request.GradeID,
                IsActive = true
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(new { message = "Estudiante insertado correctamente." });
        }



        [HttpGet]
        public IActionResult GetStudentsByGrade([FromQuery] int gradeID)
        {
            // Verificar si el grado existe
            var gradeExists = _context.Grades.Any(g => g.GradeID == gradeID);
            if (!gradeExists)
            {
                return BadRequest("El GradeID proporcionado no existe.");
            }

            // Obtener los estudiantes que pertenecen al GradeID
            var students = _context.Students
                                   .Where(s => s.GradeID == gradeID)
                                   .ToList();

            // Verificar si hay estudiantes para ese grado
            if (students.Count == 0)
            {
                return NotFound("No se encontraron estudiantes para este grado.");
            }

            return Ok(students);
        }






        [HttpPut]
        public IActionResult UpdateContact(StudentRequestV2 request)
        {
            Student student = _context.Students.FirstOrDefault(x => x.StudentID == request.Id);

            if (student == null)
            {
                return NotFound(new { message = "Estudiante no encontrado." });
            }

            student.Phone = request.Phone;
            student.Email = request.Email;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Datos de contacto actualizados correctamente." });
        }

        [HttpPut]
        public IActionResult UpdatePersonalData(StudentRequestV3 request)
        {
            Student student = _context.Students.FirstOrDefault(x => x.StudentID == request.Id);

            if (student == null)
            {
                return NotFound(new { message = "Estudiante no encontrado." });
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Datos personales actualizados correctamente." });
        }


        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            // Busca el estudiante por su ID
            Student student = _context.Students.FirstOrDefault(x => x.StudentID == id);

            if (student == null)
            {
                return NotFound(new { message = "Estudiante no encontrado." });
            }

            // Cambia el estado de IsActive a false
            student.IsActive = false;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Estudiante eliminado correctamente (IsActive = false)." });
        }

    }
}
