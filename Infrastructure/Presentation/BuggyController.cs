using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] // GET : /api/Buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            // Code
            return NotFound(); // 404
        }


        [HttpGet("servererror")] // GET : /api/Buggy/servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }


        [HttpGet("badrequest")] // GET : /api/Buggy/badrequest
        public IActionResult GetBadRequestRequest()
        {
            // Code
            return BadRequest(); // 400
        }


        [HttpGet("badrequest/{id}")] // GET : /api/Buggy/badrequest/1
        public IActionResult GetBadRequestRequest(int id) // Validation Error
        {
            // Code
            return BadRequest(); // 400
        }


        [HttpGet("unauthorized")] // GET : /api/Buggy/unauthorized
        public IActionResult GetUnauthorizedRequest()
        {
            // Code
            return Unauthorized(); // 401
        }
    }
}
