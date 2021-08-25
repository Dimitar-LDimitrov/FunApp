namespace FunApp.Web.Areas.Admin.Controllers
{
    using Data;
    using Data.Models;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class JokesController : Controller
    {
        private readonly FunAppDbContext context;
        private readonly IJokeService service;

        public JokesController(FunAppDbContext context, IJokeService service)
        {
            this.context = context;
            this.service = service;
        }

        public async Task<IActionResult> All()
        {
            var result = await this.service.All();

            return this.View(result);
        }

        // GET: Admin/Jokes
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var result = await this.context.Jokes.ToListAsync();
            return View(result);
        }

        // GET: Admin/Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = await this.context.Jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // GET: Admin/Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Jokes/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Likes")] Joke joke)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(joke);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Admin/Jokes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = await this.context.Jokes.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: Admin/Jokes/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Likes")] Joke joke)
        {
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(joke);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeExists(joke.Id))
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
            return View(joke);
        }

        // GET: Admin/Jokes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = await this.context.Jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: Admin/Jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var joke = await this.context.Jokes.FindAsync(id);
            this.context.Jokes.Remove(joke);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeExists(int id)
        {
            return this.context.Jokes.Any(e => e.Id == id);
        }
    }
}
