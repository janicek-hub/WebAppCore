using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreData;

namespace WebAppCore.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly WebAppCoreContext _context;

        public TaskController(ILogger<TaskController> logger, WebAppCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TaskTypeSortParm"] = sortOrder == "TaskType" ? "tasktype_desc" : "TaskType";
            ViewData["FromSortParm"] = sortOrder == "From" ? "from_desc" : "From";
            ViewData["ToSortParm"] = sortOrder == "To" ? "to_desc" : "To";
            var tasks = from t in _context.Tasks
                           select t;
            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(t => t.Name);
                    break;
                case "TaskType":
                    tasks = tasks.OrderBy(t => t.TaskType);
                    break;
                case "tasktype_desc":
                    tasks = tasks.OrderByDescending(t => t.TaskType);
                    break;
                case "From":
                    tasks = tasks.OrderBy(t => t.From);
                    break;
                case "from_desc":
                    tasks = tasks.OrderByDescending(t => t.From);
                    break;
                case "To":
                    tasks = tasks.OrderBy(t => t.To);
                    break;
                case "to_desc":
                    tasks = tasks.OrderByDescending(t => t.To);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Name);
                    break;
            }

            return View(await tasks.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new WebAppCoreData.Task());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TaskType,From,To")] WebAppCoreData.Task task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(task);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Tasks.FirstOrDefaultAsync(s => s.Id == id));
        }

        //alternative edit
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TaskType,From,To")] WebAppCoreData.Task task)
        //{
        //    if (id != task.Id)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(task);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //    }
        //    return View(task);
        //}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<WebAppCoreData.Task>(
                taskToUpdate,
                "",
                t => t.Name, t => t.TaskType, t => t.From, t => t.To))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(taskToUpdate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
