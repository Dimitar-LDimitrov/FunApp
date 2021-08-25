namespace FunApp.Web.Controllers
{
    using Extentions;
    using Models.Category;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class CategoriesController : Controller
    {
        private const int PageSize = 15;

        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        public IActionResult All(int page = 1)
        => View(new CategoryPageListingModel
        {
            CurrentPage = page,
            Categories = this.service.All(page, PageSize),
            TotalPage = (int)Math.Ceiling(this.service.Total() / (double)PageSize)
        });

        [Route("categories/stylesbycategory/{categoryId}")]
        public IActionResult StylesByCategory(int categoryId)
        {
            var styles = this.service.StylesByCategory(categoryId);

            if (styles == null)
            {
                return NotFound();
            }

            return View(styles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BasicCategoryModel model)
        {
            this.service.Create(model.Title, model.ThumbnailImagePath);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Route("categories/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.service.FindById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(new BasicCategoryModel
            {
                Id = id,
                Title = customer.Title,
                ThumbnailImagePath = customer.ThumbnailImagePath
            });
        }

        [HttpPost]
        [Route("categories/edit/{id}")]
        public IActionResult Edit(int id, CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            var categoryExist = this.service.CategoryExist(id);

            if (categoryExist == false)
            {
                return NotFound();
            }

            this.service
                .Edit(id, categoryModel.Title, categoryModel.ThumbnailImagePath);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        public IActionResult Destroy(int id)
        {
            this.service.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}