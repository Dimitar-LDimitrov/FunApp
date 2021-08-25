namespace FunApp.Web.Areas.Admin.Controllers
{
    using Data;
    using Data.Models;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FunApp.Web.Areas.Admin.Models.Styles;
    using FunApp.Services.Models.Style;

    [Area("Admin")]
    public class StylesController : Controller
    {
        private readonly FunAppDbContext context;
        private readonly IStyleService service;

        public StylesController(FunAppDbContext context, IStyleService service)
        {
            this.context = context;
            this.service = service;
        }

        // GET: Admin/Styles
        [Route("Admin/Index")]
        public async Task<IActionResult> Index(int categoryId)
        {
            //var all = await this.context.Styles.ToListAsync();

            List<StyleFormModel> styles = await (from catItem in this.context.Styles
                                        where catItem.CategoryId == categoryId
                                        select new StyleFormModel
                                        {
                                            Id = catItem.Id,
                                            CategoryId = categoryId,
                                            Title = catItem.Title
                                        })
                                        .ToListAsync();

            //List<Style> list = await this.context.Styles
            //    .Where(st => st.CategoryId == categoryId)
            //    .Select(s => new Style
            //    {
            //        Id = s.Id,
            //        Title = s.Title,
            //        CategoryId = categoryId,
            //        Category = s.Category,
            //        Songs = s.Songs
            //    })
            //    .ToListAsync();

            //var funAppDbContext = this.context.Styles.Include(s => s.Category);
            //return View(await funAppDbContext.ToListAsync());

            return View(styles);
        }


        private IEnumerable<SelectListItem> GetCategoriesSelectedItems()
        {
            return this.context.Categories
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                });
        }

        // GET: Admin/Styles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await this.context.Styles
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }

        // GET: Admin/Styles/Create
        public IActionResult Create(int id, string title, int categoryId)
        {
            return View();
        }

        // POST: Admin/Styles/Create
        [HttpPost]
        public IActionResult Create(StyleFormModel style)
        {
            //if (ModelState.IsValid)
            //{
            //    this.context.Add(style);
            //    this.context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ThumbnailImagePath", style.CategoryId);
            //return View(style);

            if (!ModelState.IsValid)
            {
                return View(style);
            }

            this.service.Create(style.Id, style.Title, style.CategoryId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Styles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await this.context.Styles.FindAsync(id);
            if (style == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ThumbnailImagePath", style.CategoryId);
            return View(style);
        }

        // POST: Admin/Styles/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId")] Style style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(style);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StyleExists(style.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ThumbnailImagePath", style.CategoryId);
            return View(style);
        }

        // GET: Admin/Styles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await this.context.Styles
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }

        // POST: Admin/Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var style = await this.context.Styles.FindAsync(id);
            this.context.Styles.Remove(style);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StyleExists(int id)
        {
            return this.context.Styles.Any(e => e.Id == id);
        }
    }
}
