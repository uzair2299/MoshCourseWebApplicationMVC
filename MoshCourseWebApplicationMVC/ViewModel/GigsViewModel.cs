using MoshCourseWebApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoshCourseWebApplicationMVC.ViewModel
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpComingGigs { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }

    }
}