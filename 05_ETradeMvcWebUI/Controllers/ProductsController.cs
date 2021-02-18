using System;
using System.Collections.Generic;
using ETradeDataAccess.EntityFramework;
using ETradeEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ETradeMvcWebUI.Controllers
{
    
    public class ProductsController : Controller
    {
        //private readonly ETradeContext _context;

        //public ProductsController(DbContext context)
        //{
        //    _context = context as ETradeContext;
        //}

        private readonly IProductService _productService; 
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        //GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var eTradeContext = _context.Products.Include(p => p.Category);
        //    return View(await eTradeContext.ToListAsync());
        //}


        public IActionResult Index(int id) // burada ki aydı benim için category Id dir
        {
            try
            {
                List<ProductModel> products;
                string productJson;
                if (HttpContext.Session.GetString("product") == null)
                {
                    products = _productService.GetQuery().ToList();
                    productJson = JsonConvert.SerializeObject(products);
                    HttpContext.Session.SetString("products", productJson);
                }
                else
                {
                    productJson = HttpContext.Session.GetString("products");
                    products = JsonConvert.DeserializeObject<List<ProductModel>>(productJson);
                }

                if (id != 0)
                {
                    products = products.Where(p => p.CategoryId == id).ToList();
                }

                return View(products);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin, User")]
        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetById(id.Value);
            if (product == null)
            {
                return NotFound(product);
            }

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Categories = new SelectList(_categoryService.GetQuery().ToList(), "Id", "Name");
            var model = new ProductModel();
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name,UnitPrice,StockAmount,CategoryId,CreateDate,UpdateDate,IsDeleted,ImagePath,Id,Guid")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                return RedirectToAction(nameof(Index)); // return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetQuery().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = _productService.GetById(id);
                ViewData["Categories"] =
                    new SelectList(_categoryService.GetQuery().ToList(), "Id", "Name", model.CategoryId);
                return View(model);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,UnitPrice,StockAmount,CategoryId,CreateDate,UpdateDate,IsDeleted,ImagePath,Id,Guid")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productService.Update(product);
                    return RedirectToAction(nameof(Index));
                }

                ViewData["Categories"] =
                    new SelectList(_categoryService.GetQuery().ToList(), "Id", "Name", product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        // GET: Products/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var product = await _context.Products
        //            .Include(p => p.Category)
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(product);
        //    }

        //    // POST: Products/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var product = await _context.Products.FindAsync(id);
        //        _context.Products.Remove(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        //    private bool ProductExists(int id)
        //    {
        //        return _context.Products.Any(e => e.Id == id);
        //    }
    }
}
