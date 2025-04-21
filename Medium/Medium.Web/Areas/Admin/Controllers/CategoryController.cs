using Medium.Domain;
using Medium.Domain.Entities;
using Medium.Domain.ServicesInterface;
using Medium.Web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace Medium.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryManagementService categoryManagementService,
            IApplicationTime applicationTime,
            ILogger<CategoryController> logger)
        {
            _categoryManagementService = categoryManagementService;
            _applicationTime = applicationTime;
            _logger = logger;
        }

        public async Task<IActionResult> CategoryList(int pageIndex = 1, int pageSize = 5)
        {
            var pagedResult = await _categoryManagementService.GetCategoriesAsync(pageIndex, pageSize);

            ViewBag.CurrentPage = pagedResult.CurrentPage;
            ViewBag.TotalPages = pagedResult.TotalPages;
            ViewBag.TotalItems = pagedResult.TotalItems;

            var categories = pagedResult.Items;

            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryCreatModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                { 
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    CreatedDate = _applicationTime.GetCurrentDateTime(),
                    UpdatedDate = _applicationTime.GetCurrentDateTime(),
                };

                await _categoryManagementService.AddCategoryAsync(category);
            }
            return View(model);
        }
    }
}
