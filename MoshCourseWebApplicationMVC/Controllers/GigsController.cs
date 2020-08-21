using Microsoft.AspNet.Identity;
using MoshCourseWebApplicationMVC.Models;
using MoshCourseWebApplicationMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoshCourseWebApplicationMVC.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            GigFormViewModel gigFormViewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(gigFormViewModel);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(GigFormViewModel gigFormViewModel)
        {
            if (!ModelState.IsValid)
            {
               gigFormViewModel.Genres= _context.Genres.ToList();
                return View("Create", gigFormViewModel);

            }
            Gig gig = new Gig
            {
                GenreId = gigFormViewModel.Genre,
                ArtistId = User.Identity.GetUserId(),
                Venue = gigFormViewModel.Venue,
                DateTime =gigFormViewModel.GetDateTime(),
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}