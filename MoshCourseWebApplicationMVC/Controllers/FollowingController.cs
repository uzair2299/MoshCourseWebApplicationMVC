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
    [Authorize]
    public class FollowingController : ApiController
    {

        private ApplicationDbContext _context;
        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO followingDTO)
        {
            var UserId = User.Identity.GetUserId();
            if (_context.Followings.Any(f => f.FolloweeId == UserId && f.FollowerId == followingDTO.FolloweeId))
            {
                return BadRequest("Following already exist");
            }   
            var following = new Following
                {
                    FollowerId = UserId,
                    FolloweeId = followingDTO.FolloweeId
                };
                _context.Followings.Add(following);
                _context.SaveChanges();

            return Ok();


        }
    }
}
