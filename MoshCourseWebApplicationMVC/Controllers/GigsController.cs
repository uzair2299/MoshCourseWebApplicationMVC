using Microsoft.AspNet.Identity;
using MoshCourseWebApplicationMVC.Models;
using MoshCourseWebApplicationMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Genres = _context.Genres.ToList(),
                Heading="Add a gig"
            };
            return View("GigForm",gigFormViewModel);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(GigFormViewModel gigFormViewModel)
        {
            if (!ModelState.IsValid)
            {
               gigFormViewModel.Genres= _context.Genres.ToList();
                return View("GigForm", gigFormViewModel);

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
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigViewModel = new GigsViewModel
            {
                UpComingGigs = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig).Include(g => g.Genre).Include(g => g.Artist).ToList(),
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };
             
            return View("Gigs",gigViewModel);
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now).Include(g=>g.Genre).ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Single(g => g.Id == id && g.ArtistId ==userId);
            GigFormViewModel gigFormViewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gigs.Id,
                Date = gigs.DateTime.ToString("d MMM yyyy"),
                Time=gigs.DateTime.ToString("HH:mm"),
                Venue = gigs.Venue,
                Genre= gigs.GenreId,
                Heading="Edit a gig"

                
            };
            return View("GigForm",gigFormViewModel);
        }

        public ActionResult Update(GigFormViewModel gigFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                gigFormViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", gigFormViewModel);

            }
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == gigFormViewModel.Id && g.ArtistId == userId);
            gig.Venue = gigFormViewModel.Venue;
            gig.GenreId = gigFormViewModel.Genre;
            gig.DateTime = gigFormViewModel.GetDateTime();
            _context.Entry(gig).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Mine","Gigs");
        }
    }
}