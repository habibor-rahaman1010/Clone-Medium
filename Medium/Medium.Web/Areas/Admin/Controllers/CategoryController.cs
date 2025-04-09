using MediatR;
using Medium.Application.Features.Categories.Commands;
using Medium.Domain;
using Medium.Domain.Entities;
using Medium.Web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Mvc;


namespace Medium.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IApplicationTime _applicationTime;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator,
            IApplicationTime applicationTime,
            ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _applicationTime = applicationTime;
            _logger = logger;
        }

        public IActionResult CategoryList()
        {
            return View();
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
            }
            return View(command);
        }
    }
}
