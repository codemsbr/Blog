using Blog.Areas.Admin.Helpers;
using Blog.Contexts;
using Blog.Models;
using Blog.ViewModels.HomeVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        BlogDbContexts _db { get; }
        public HomeController(BlogDbContexts db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var item = _db.Headers.ToListAsync().Result;

            if (item == null)
                return View();

            Header header = item[item.Count - 1];
            return View(new UserHeaderVM()
            {
                Description = header.Description,
                ImgUrl = header.ImgUrl,
                Text = header.Text,
            });
        }
    }
}
