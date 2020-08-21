using Microsoft.AspNet.Identity;
using MoshCourseWebApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoshCourseWebApplicationMVC.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Attend([FromBody]int gigId)
        {
            var UserId = User.Identity.GetUserId();
            if( _context.Attendances.Any(u => u.AttendeeId == UserId && u.GigId == gigId)) { 
                return BadRequest("The attendance is alreadu exist");
            }

            Attendance attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
        };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
