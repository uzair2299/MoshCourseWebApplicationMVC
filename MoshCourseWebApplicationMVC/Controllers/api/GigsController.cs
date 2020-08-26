using Microsoft.AspNet.Identity;
using MoshCourseWebApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoshCourseWebApplicationMVC.Controllers.api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.ArtistId == userId && g.Id==id);
            if (gig.IsCanceled)
                return NotFound();
            gig.IsCanceled = true;
            _context.Entry(gig).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();


        }
    }
}
