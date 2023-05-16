using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAssesment.Controllers
{
    [Route("appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private FinalAssesmentContext _context;

        public AppointmentController(FinalAssesmentContext context)
        {
            _context = context;
        }


        [HttpGet("getAppointment")]
        public IEnumerable<object> GetAppointments() 
        {
            var appointment = from a in _context.Appointments
                              select a;

            return appointment;
        }

        [HttpPut("updateAppointment/{id}")]

        public async Task<ActionResult> updateAppointment(int id, [FromBody] AppointmentUpdate app)
        {
            var present = await _context.Appointments.FirstOrDefaultAsync(a => a.AppId == id);

            if(present == null)
            {
                return NotFound("Appointment Not found");
            }

            present.UserName = app.UserName;
            present.CreatedDate = app.CreatedDate;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("createAppointment")]

        public async Task<ActionResult> CreateAppointment([FromBody] Appointment app)
        {
            _context.Appointments.Add(app);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteAppointment/{id}")]

        public async Task<ActionResult> deleteAppointment(int id)
        {
            try
            {
                var present = await _context.Appointments.FirstOrDefaultAsync(a => a.AppId == id);
                if (present == null)
                {
                    return NotFound("appointment Not found");
                }

                _context.Appointments.Remove(present);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
