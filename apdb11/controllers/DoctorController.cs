using apdb11.models;
using apdb11.services;
using Microsoft.AspNetCore.Mvc;

namespace apdb11.controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController: ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }
        
        
        [HttpGet("get")]
        public IActionResult GetDoctorsData()
        {
            var doctors = _service.GetDoctors();
            if (doctors != null)
                return Ok(doctors);
            else return BadRequest("an error occured");
        }

        [HttpPost("modify")]
        public IActionResult ModifyDoctor(DoctorRequest request)
        {
            var success = _service.ModifyDoctor(request);
            if (success)
                return Ok("modified the doctor");
            else return BadRequest("an error occured");
        }

        [HttpPost("add")]
        public IActionResult AddDoctor(DoctorRequest request)
        {
            var success = _service.AddDoctor(request);
            if (success)
                return Ok("added the doctor");
            else return BadRequest("an error occured");

        }

        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            var success = _service.DeleteDoctor(id);
            if (success)
                return Ok("deleted the doctor");
            else return BadRequest("an error occured");
        }

        [HttpGet("populate")]
        public IActionResult PopulateData()
        {
            _service.PopulateData();
            return Ok();
        }
        
        
        
        
    }
}