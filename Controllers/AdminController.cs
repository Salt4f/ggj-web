using GGJWeb.Data;
using GGJWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static GGJWeb.Controllers.PostController;

namespace GGJWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public AdminController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "page")] int page)
        {
            var list = await _context.Posts!.OrderByDescending(p => p.PublishedOn).Skip(page * 5).Take(5).ToListAsync();
            HomeModel homeModel;

            try
            {
                homeModel = await _context.Home!.FirstAsync();
                if (homeModel.JamStart == null) throw new InvalidOperationException("JamStart is Null");
            } catch (InvalidOperationException) {
                homeModel = new HomeModel();
                homeModel.JamStart = DateTime.Now.AddDays(7);
            }

            homeModel.posts = list;

            return View(homeModel);
        }


        // I left it off here, now I gota do the saving to db stuff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind(nameof(NewHomeData.JamStart))] NewHomeData data)
        {
            if (ModelState.IsValid)
            {
                // TODO AUTH
                /*if (info.Password != _config.GetValue<string>("Password"))
                {
                    return Unauthorized();
                }*/
                Debug.Print((data.JamStart == null).ToString());
                var home = new HomeModel { Id = 0, JamStart = data.JamStart };
                await _context.AddAsync(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index), "Admin");
            }
            return BadRequest();
        }

        public class NewHomeData
        {
            public DateTime? JamStart { get; set; }
        }
    }
}
