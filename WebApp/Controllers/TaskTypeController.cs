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
    public class TaskTypeController : Controller
    {
        private readonly AppDbContext _context;

        public TaskTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaskType
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskTypes.ToListAsync());
        }

        // GET: TaskType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _context.TaskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // GET: TaskType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] TaskType taskType)
        {
            if (ModelState.IsValid)
            {
                taskType.Id = Guid.NewGuid();
                _context.Add(taskType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskType);
        }

        // GET: TaskType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _context.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return NotFound();
            }
            return View(taskType);
        }

        // POST: TaskType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] TaskType taskType)
        {
            if (id != taskType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskTypeExists(taskType.Id))
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
            return View(taskType);
        }

        // GET: TaskType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _context.TaskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // POST: TaskType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskType = await _context.TaskTypes.FindAsync(id);
            _context.TaskTypes.Remove(taskType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskTypeExists(Guid id)
        {
            return _context.TaskTypes.Any(e => e.Id == id);
        }
    }
}
