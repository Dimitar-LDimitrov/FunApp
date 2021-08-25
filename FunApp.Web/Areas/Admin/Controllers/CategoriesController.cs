namespace FunApp.Web.Areas.Admin.Controllers
{
    using Data;
    using Data.Models;
    using Models;
    using Models.Category;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private const int PageSize = 15;

        private readonly ICategoryService service;
        private readonly FunAppDbContext context;

        public CategoriesController(FunAppDbContext context, ICategoryService service)
        {
            this.context = context;
            this.service = service;
        }

        // GET: Admin/AllCategories
        [Route("AllCategories")]
        public IActionResult AllCategories(int page = 1)
        {
            //var allCategories = await this.context.Categories.ToListAsync();

            //return View(allCategories);
            
            return View(new CategoryPageListingModel
            {
                CurrentPage = page,
                Categories = this.service.All(page, PageSize),
                TotalPage = (int)Math.Ceiling(this.service.Total() / (double)PageSize)
            });
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(BasicCategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            this.service.Create(categoryModel.Title, categoryModel.ThumbnailImagePath);

            return RedirectToAction(nameof(AllCategories));
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(category);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(AllCategories));
            }

            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await this.context.Categories.FindAsync(id);
            this.context.Categories.Remove(category);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(AllCategories));
        }

        private bool CategoryExists(int id)
        {
            return this.context.Categories.Any(e => e.Id == id);
        }
    }
}
