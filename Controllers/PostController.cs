using GGJWeb.Data;
using GGJWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GGJWeb.Controllers
{
    public class PostController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public PostController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IActionResult> Details(int? id)
        {
            var post = await _context.Posts!
                .Include(p => p.PostInfo)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post is null ? NotFound() : View(post);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind(nameof(NewPostBody.Title),nameof(NewPostBody.Subtitle),nameof(NewPostBody.Body),nameof(NewPostBody.Password))]NewPostBody info)
        {
            if (ModelState.IsValid)
            {
                if (info.Password != _config.GetValue<string>("Password"))
                {
                    return Unauthorized();
                }
                var postInfo = new PostInfo { Body = info.Body };
                var post = new Post { Title = info.Title, Subtitle = info.Subtitle, PostInfo = postInfo, PublishedOn = DateTime.UtcNow };
                await _context.AddAsync(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminController.Index), "Admin");
            }
            return BadRequest();
        }

        public class NewPostBody
        {
            public string? Title { get; set; }
            public string? Subtitle { get; set; }
            public string? Body { get; set; }

            [DataType(DataType.Password)]
            public string? Password { get; set; }
        }
    }
}
