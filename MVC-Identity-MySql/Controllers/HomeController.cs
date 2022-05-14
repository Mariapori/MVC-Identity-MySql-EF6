using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Identity_MySql.Data;
using MVC_Identity_MySql.Models;
using System.Diagnostics;

namespace MVC_Identity_MySql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            var tiedotteet = _db.Tiedotteet.OrderByDescending(o => o.DT).Take(10).ToList();
            return View(tiedotteet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        public IActionResult LuoTiedote(string otsikko, string kuvaus)
        {
            Tiedote tiedote = new Tiedote();
            tiedote.Name = otsikko;
            tiedote.Desc = kuvaus;
            tiedote.DT = DateTime.Now;
            _db.Tiedotteet.Add(tiedote);
            _db.SaveChanges();
            return View();
        }
    }
}