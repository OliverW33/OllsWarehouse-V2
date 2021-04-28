using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OllsWarehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OllsWarehouse.ViewComponents
{
    public class MovieShoppingCartViewComponent : ViewComponent
    {
        const string userCart = "_Cart";

        private readonly IHttpContextAccessor _contextAccesor;

        public MovieShoppingCartViewComponent(IHttpContextAccessor newContextAccessor)
        {
            _contextAccesor = newContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MovieShoppingCart> newItem = new List<MovieShoppingCart>();

            if(HttpContext.Session.GetString(userCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(userCart);
                newItem = JsonSerializer.Deserialize<List<MovieShoppingCart>>(serialJSON);
            }

            return View(newItem);
        }
    }
}
