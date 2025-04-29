using Mapster;
using MapsterMapper;
using Medium.Application.DTO;
using Medium.Domain;
using Medium.Domain.Entities;
using Medium.Domain.ServicesInterface;
using Medium.Domain.Utilities;
using Medium.Web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Mvc;


namespace Medium.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryManagementService categoryManagementService,
            IApplicationTime applicationTime,
            IMapper mapper,
            ILogger<CategoryController> logger)
        {
            _categoryManagementService = categoryManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryCreatModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await model.BuildAdapter().AdaptToTypeAsync<Category>();
            category.Id = IdentityGenerator.NewSequentialGuid();
            category.CreatedDate = _applicationTime.GetCurrentDateTime();
            category.UpdatedDate = _applicationTime.GetCurrentDateTime();

            await _categoryManagementService.AddCategoryAsync(category);

            return RedirectToAction("CategoryList", "Category");
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

        public async Task<IActionResult> CategoryDetails(Guid id)
        {
            var category = await _categoryManagementService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = await category.BuildAdapter().AdaptToTypeAsync<CategoryDto>();

            return View(categoryDto);
        }

        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var category = await _categoryManagementService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(category);
            }
            var categoryDto = await category.BuildAdapter().AdaptToTypeAsync<CategoryDto>();
            return View(categoryDto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel model)
        {
            var dto = _mapper.Map<CategoryDto>(model);

            if (!ModelState.IsValid)
            {
                return View(nameof(UpdateCategory), dto);
            }

            var category = await _categoryManagementService.GetCategoryById(model.Id);
            if (category == null)
            {
                return NotFound(category);
            }
            category = _mapper.Map(model, category);

            category.UpdatedDate = _applicationTime.GetCurrentDateTime();
            await _categoryManagementService.UpdateCategoryAsync(category);

            return RedirectToAction(nameof(CategoryList), "Category");
        }

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryManagementService.GetCategoryById(id);

            if (category != null)
            {
                await _categoryManagementService.DeleteCategoryAsync(category);
                return RedirectToAction(nameof(CategoryList), "Category");
            }
            return NotFound(category);
        }
    }
}
