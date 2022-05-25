using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppBasics.Models;

namespace WebCoreAppBasics.Components
{
    public class OrderHeaderComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            UserOrder model = new UserOrder();
            model.Id = Id;
            return View(model);
        }
    }
}
