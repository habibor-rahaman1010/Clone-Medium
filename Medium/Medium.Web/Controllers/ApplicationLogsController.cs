using Medium.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Web.Controllers
{
    public class ApplicationLogsController : Controller
    {
        private readonly MediumDbContext _context;
        private readonly int _pageSize = 10;

        public ApplicationLogsController(MediumDbContext context)
        {
            _context = context;
        }

        public IActionResult LogList(int page = 1)
        {
            var totalLogs = _context.ApplicationLogs.Count();
            var totalPages = (int)Math.Ceiling(totalLogs / (double)_pageSize);

            var logs = _context.ApplicationLogs
                .OrderBy(x => x.Id)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(logs);
        }

        public IActionResult ClearLogs()
        {
            try
            {
                _context.ApplicationLogs.RemoveRange(_context.ApplicationLogs);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Logs cleared successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while clearing logs: " + ex.Message;
            }

            return RedirectToAction("LogList", "ApplicationLogs");
        }
    }
}
