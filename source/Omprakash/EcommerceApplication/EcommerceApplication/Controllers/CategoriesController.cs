using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceApplication.Context;
using EcommerceApplication.Models;
using EcommerceApplication.Service.Interface;
using EcommerceApplication.Service.Infrastructure;

namespace EcommerceApplication.Controllers
{
    public class CategoriesController : Controller
    {
        private IGenericRepository<Category> _category;
        private readonly ECommDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(ECommDbContext context, IUnitOfWork unitOfWork,
            IGenericRepository<Category> category)
        {
            _category = category;            
            _unitOfWork = unitOfWork;
			_context = context;
        }

		// GET: Categories
        public ViewResult Index()
        {
            var category = _unitOfWork.Category.GetAll().Where(x => x.DeletedAt == null);
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
		{
			Category Category = new Category();	
			ViewBag.CategoryId = GetCategories();
			return View(Category);
		}

		// POST: Categories/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,colour,description")] Category category)
		{
			if (ModelState.IsValid)
			{
				category.CreatedAt = DateTime.Now;				
				_unitOfWork.Category.Add(category);				
				_unitOfWork.Save();

				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}
		
	     private Category GetCategory(int id)
		{		
			return _unitOfWork.Category.GetById(id);
		}

		private List<SelectListItem> Getcategorybyid(int CategoryId )
		{
            
			 List<SelectListItem> listCategory = _context.categories
				.Where(c => c.CategoryId == CategoryId)
				.OrderBy(c => c.CategoryName)
				.Select(n =>
			new SelectListItem
			{
				Value = n.CategoryId.ToString(),
				Text = n.CategoryName
			}).ToList();        

			return listCategory;
        }
        private List<SelectListItem> GetCategories()
		{
			var lstcategory = new List<SelectListItem>();
			List<Category> categories = _context.categories.ToList();			
			lstcategory = categories.Select(ct => new SelectListItem()
			{
				Value = ct.CategoryId.ToString(),
				Text = ct.CategoryName
			}).ToList();

			var defItem = new SelectListItem()
			{
				Value = "",
				Text = "-----Select------"
			};
			lstcategory.Insert(0, defItem);
			return lstcategory;

		}
		// GET: Categories/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			
			Category Category = GetCategory(id);
			ViewBag.CategoryId = Getcategorybyid(Category.CategoryId);
		    return View(Category);
		}

		// POST: Categories/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CreatedAt,colour,description")] Category category)
		{
		    if (id != category.CategoryId)
		    {
		        return NotFound();
		    }

		    if (ModelState.IsValid)
		    {
		        try
		        {

                    Category cat = GetCategory(id);
					cat.description = category.description;
					cat.colour = category.colour;
					cat.CategoryName = category.CategoryName;
                    cat.UpdatedAt = DateTime.Now;
		            _unitOfWork.Category.Update(cat);
		           _unitOfWork.Save();
		        }
		        catch (DbUpdateConcurrencyException)
		        {					
					throw;
		        }
		        return RedirectToAction(nameof(Index));
		    }
		    return View(category);
		}

		// GET: Categories/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _context.categories
				.FirstOrDefaultAsync(m => m.CategoryId == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		// POST: Categories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{

			Category category = _context.categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			category.DeletedAt = DateTime.Now;
			//_unitOfWork.Category.Delete(category);			
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryExists(int id)
		{
			return (_context.categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
		}

        [HttpGet, ActionName("GetById")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetById(int id)
        {
			_unitOfWork.Category.GetById(id);
			return View();
        }
    }
}
