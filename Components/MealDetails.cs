using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppBasics.Models;

namespace WebCoreAppBasics.Components
{
    public class MealDetails : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            UserOrder model = new UserOrder();
            model.CatId = Id;
            return View(model);
        }
    }
}