using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETradeBusiness.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ETradeMvcWebUI.Models.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ViewViewComponentResult Invoke() // bir sayfada başka bir kısmı da görmemizi sağlıyor 
        {
            return View(_categoryService.GetQuery().ToList());
        }
    }
}
