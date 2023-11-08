using GGJWeb.Data;
using GGJWeb.Models;
using GGJWeb.Utils;
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
            // Return if session is not authorized
            if ((HttpContext.Session.GetInt32("Authorized") ?? 0) == 0)
            {
                return RedirectToAction(nameof(AdminController.Auth), "Admin");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            // Return if session is not authorized
            if ((HttpContext.Session.GetInt32("Authorized") ?? 0) == 0)
            {
                return RedirectToAction(nameof(AdminController.Auth), "Admin");
            }

            var post = await _context.Posts!
                .Include(p => p.PostInfo)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post is null ? NotFound() : View(new NewPostBody { Title=post.Title, Subtitle=post.Subtitle, Body=post.PostInfo?.Body});
        }

        public async Task<IActionResult> Delete(int? id)
        {
            // Return if session is not authorized
            if ((HttpContext.Session.GetInt32("Authorized") ?? 0) == 0)
            {
                return RedirectToAction(nameof(AdminController.Auth), "Admin");
            }

            var post = await _context.Posts!
                .Include(p => p.PostInfo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post is null || post.PostInfo is null) return NotFound();

            _context.Remove(post);
            _context.Remove(post.PostInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind(nameof(NewPostBody.Title),nameof(NewPostBody.Subtitle),nameof(NewPostBody.Body))]NewPostBody info)
        {
            // Return if session is not authorized
            if ((HttpContext.Session.GetInt32("Authorized") ?? 0) == 0)
            {
                return RedirectToAction(nameof(AdminController.Auth), "Admin");
            }

            if (ModelState.IsValid)
            {
                var postInfo = new PostInfo { Body = info.Body };
                var post = new Post { Title = info.Title, Subtitle = info.Subtitle, PostInfo = postInfo, PublishedOn = DateTime.UtcNow };
                await _context.AddAsync(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminController.Index), "Admin");
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind(nameof(NewPostBody.Title), nameof(NewPostBody.Subtitle), nameof(NewPostBody.Body))] NewPostBody info)
        {
            // Return if session is not authorized
            if ((HttpContext.Session.GetInt32("Authorized") ?? 0) == 0)
            {
                return RedirectToAction(nameof(AdminController.Auth), "Admin");
            }

            if (ModelState.IsValid)
            {
                var post = await _context.Posts!
                .Include(p => p.PostInfo)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (post is null || post.PostInfo is null) return NotFound();

                post.Title = info.Title;
                post.Subtitle = info.Subtitle;
                post.PublishedOn = post.PublishedOn;
                post.PostInfo.Body = info.Body;

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
        }
    }
}
