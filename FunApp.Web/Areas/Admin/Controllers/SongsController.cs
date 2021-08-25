namespace FunApp.Web.Areas.Admin.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class SongsController : Controller
    {
        private readonly FunAppDbContext context;

        public SongsController(FunAppDbContext context)
        {
            this.context = context;
        }

        // GET: Admin/Songs
        public async Task<IActionResult> Index()
        {
            var funAppDbContext = this.context.Songs.Include(s => s.Style);
            return View(await funAppDbContext.ToListAsync());
        }

        // GET: Admin/Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await this.context.Songs
                .Include(s => s.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Admin/Songs/Create
        public IActionResult Create()
        {
            ViewData["StyleId"] = new SelectList(this.context.Styles, "Id", "Title");
            return View();
        }

        // POST: Admin/Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Artist,Name,Duration,ThumbnailImage,VideoLink,ReleaseDate,StyleId")] Song song)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(song);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StyleId"] = new SelectList(this.context.Styles, "Id", "Title", song.StyleId);
            return View(song);
        }

        // GET: Admin/Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await this.context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["StyleId"] = new SelectList(this.context.Styles, "Id", "Title", song.StyleId);
            return View(song);
        }

        // POST: Admin/Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Artist,Name,Duration,ThumbnailImage,VideoLink,ReleaseDate,StyleId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(song);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["StyleId"] = new SelectList(this.context.Styles, "Id", "Title", song.StyleId);
            return View(song);
        }

        // GET: Admin/Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await this.context.Songs
                .Include(s => s.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Admin/Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await this.context.Songs.FindAsync(id);
            this.context.Songs.Remove(song);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return this.context.Songs.Any(e => e.Id == id);
        }
    }
}
