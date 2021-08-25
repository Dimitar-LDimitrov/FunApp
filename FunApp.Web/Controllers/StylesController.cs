namespace FunApp.Web.Controllers
{
    using Extentions;
    using Models.Style;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Collections.Generic;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class StylesController : Controller
    {
        private readonly IStyleService styles;
        private readonly ICategoryService categories;

        public StylesController(IStyleService styles, ICategoryService categories)
        {
            this.styles = styles;
            this.categories = categories;
        }

        public IActionResult All()
        => View(new Styles
        {
            AllStyles = this.styles.All()
        });

        public IActionResult ById(int categoryId)
        {
            var category = this.styles.ById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(new Styles
            {
                CategoryId = categoryId,
                AllStyles = category
            });
        }

        private IEnumerable<SelectListItem> GetCategoriesListItem()
        {
            return this.categories
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
        }

        public IActionResult Create() => View(new Styles
        {
            Categories = this.GetCategoriesListItem()
        });

        [HttpPost]
        public IActionResult Create(Styles model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = this.GetCategoriesListItem();
                return View(model);
            }

            this.styles.Create(model.Id, model.Title, model.CategoryId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var style = this.styles.FindById(id);

            if (style == null)
            {
                return NotFound();
            }

            return View(new StyleEditModel
            {
                Id = id,
                Title = style.Title,
                CategoryId = style.CategoryId,
                IsEdit = true
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, StyleEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.IsEdit = true;
                return View(model);
            }

            this.styles.Edit(id, model.Title);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        public IActionResult Destroy(int id)
        {
            this.styles.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
