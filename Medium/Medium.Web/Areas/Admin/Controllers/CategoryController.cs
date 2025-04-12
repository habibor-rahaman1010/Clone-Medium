using MediatR;
using Medium.Application.Features.Categories.Commands;
using Medium.Application.Features.Categories.Queries;
using Medium.Domain;
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

        public async Task<IActionResult> CategoryList(CancellationToken cancellationToken = default)
        {
            var query = new GetAllCategorisQuery();
            var categoryList = await _mediator.Send(query, cancellationToken);
            return View(categoryList);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command, cancellationToken);
            }
            return RedirectToAction(nameof(CategoryList), "Category");
        }

        public async Task<IActionResult> CategoryDetails(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetCategoryByIdQuery { Id = id};
            var category = await _mediator.Send(query, cancellationToken);
            return View(category);
        }


        public async Task<IActionResult> UpdateCategory(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var category = await _mediator.Send(query, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }

            var command = new UpdateCategoryCommand
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                UpdatedDate = category.UpdatedDate,
            };

            return View(command);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            var updateCategory = await _mediator.Send(command, cancellationToken);
            if (updateCategory == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(CategoryList), "Category");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            var query = new DeleteCategoryCommand { Id = id };
            var deleteResult = await _mediator.Send(query, cancellationToken);

            if (deleteResult == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(CategoryList), "Category");
        }
    }
}
