using Blog.Areas.Admin.Helpers;
using Blog.Areas.Admin.ViewModels.HeaderVM;
using Blog.Contexts;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIO = System.IO;
namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        BlogDbContexts _db { get; }
        IWebHostEnvironment _ev { get; }
        public HomeController(BlogDbContexts db, IWebHostEnvironment ev)
        {
            _db = db;
            _ev = ev;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _db.Headers.Select(x => new AdminHeaderVm
            {
                Id = x.Id,
                Description = x.Description,
                ImgUrl = x.ImgUrl,
                Text = x.Text
            }).ToListAsync();
            return View(item.Count == 0 ? null : item[0]);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateHeaderVm data)
        {
            if (data.ImgUrl != null)
            {
                if (data.ImgUrl.IsValidSize())
                {
                    ModelState.AddModelError("ImgUrl", "Wrong Img Size");
                }

                if (data.ImgUrl.IsValidType())
                {
                    ModelState.AddModelError("ImgUrl", "Wrong File Type");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            await _db.Headers.AddAsync(new Header { Description = data.Description, ImgUrl = data.ImgUrl.SaveAsync().Result, Text = data.Text });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            
            var item = await _db.Headers.FindAsync(id);

            return View(new AdminCreateHeaderVm
            {
                Description = item.Description,
                Text = item.Text
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,AdminCreateHeaderVm data)
        {
            if (id == null || id < 1) return BadRequest();

            var item = await _db.Headers.FindAsync(id);

            if(item == null) return BadRequest();

            if(data.ImgUrl != null)
            {
                if (data.ImgUrl.IsValidSize())
                {
                    ModelState.AddModelError("ImgUrl", "Wrong Img Size");
                }

                if (data.ImgUrl.IsValidType())
                {
                    ModelState.AddModelError("ImgUrl", "Wrong File Type");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            item.Description = data.Description;
            item.Text = data.Text;
            item.ImgUrl = data.ImgUrl.SaveAsync().Result;

            await _db.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id < 1) { return BadRequest(); }

            Header header = await _db.Headers.FindAsync(id);

            if (header == null) return BadRequest();

            string path = Path.Combine(PathConstant.RootPath,header.ImgUrl);

            if (SIO.File.Exists(path))
                SIO.File.Delete(path);

            _db.Headers.Remove(header);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
