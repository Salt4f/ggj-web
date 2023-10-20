using GGJWeb.Data;
using GGJWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace GGJWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "page")] int page)
        {
            HomeModel homeModel;
            
            try
            {
                homeModel = await _context.Home!.FirstAsync();
            }
            catch (InvalidOperationException)
            {
                homeModel = new HomeModel();
            }
            homeModel.posts = await _context.Posts!.OrderByDescending(p => p.PublishedOn).Skip(page * 5).Take(5).ToListAsync();

            return View(homeModel);
        }

        public async Task<IActionResult> Signup()
        {
            HomeModel homeModel;

            try
            {
                homeModel = await _context.Home!.FirstAsync();
                return Redirect(homeModel.SignupLink ?? "gamejambcn.com");
            }
            catch (InvalidOperationException)
            {
                // Add error redirect
                return Redirect("gamejambcn.com");
            }

        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}