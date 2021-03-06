using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class TaskEntryController : Controller
    {
        private readonly AppDbContext _context;

        public TaskEntryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaskEntry
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var res = _context.TaskEntries.Include(t => t.TaskType).AsQueryable();
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            
            Console.WriteLine(sortOrder);
            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(s => s.TaskType!.Name.Contains(searchString)
                                               || s.Date.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    res = res.OrderBy(t => t.Name);
                    break;
                case "Type":
                    res = res.OrderBy(t => t.TaskType!.Name);
                    break;
                case "type_desc":
                    res = res.OrderByDescending(t => t.TaskType!.Name);
                    break;
                case "Date":
                    res = res.OrderBy(t => t.Date);
                    break;
                case "date_desc":
                    res = res.OrderByDescending(t => t.Date);
                    break;
                default: 
                    res = res.OrderByDescending(t => t.Name);
                    break;
            }
            return View(await res.ToListAsync());
        }

        // GET: TaskEntry/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntry = await _context.TaskEntries
                .Include(t => t.TaskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskEntry == null)
            {
                return NotFound();
            }

            return View(taskEntry);
        }

        // GET: TaskEntry/Create
        public IActionResult Create()
        {
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "Name");
            return View();
        }

        // POST: TaskEntry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TaskTypeId,Date,From,To,Id")] TaskEntry taskEntry)
        {
            if (ModelState.IsValid)
            {
                taskEntry.Id = Guid.NewGuid();
                _context.Add(taskEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "Name", taskEntry.TaskTypeId);
            return View(taskEntry);
        }

        // GET: TaskEntry/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntry = await _context.TaskEntries.FindAsync(id);
            if (taskEntry == null)
            {
                return NotFound();
            }
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "Name", taskEntry.TaskTypeId);
            return View(taskEntry);
        }

        // POST: TaskEntry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,TaskTypeId,Date,From,To,Id")] TaskEntry taskEntry)
        {
            if (id != taskEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskEntryExists(taskEntry.Id))
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
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "Name", taskEntry.TaskTypeId);
            return View(taskEntry);
        }

        // GET: TaskEntry/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntry = await _context.TaskEntries
                .Include(t => t.TaskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskEntry == null)
            {
                return NotFound();
            }

            return View(taskEntry);
        }

        // POST: TaskEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskEntry = await _context.TaskEntries.FindAsync(id);
            _context.TaskEntries.Remove(taskEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskEntryExists(Guid id)
        {
            return _context.TaskEntries.Any(e => e.Id == id);
        }
    }
}
