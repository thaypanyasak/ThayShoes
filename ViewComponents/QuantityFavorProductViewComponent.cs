using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMDTFINAL.Data;

namespace TMDTFINAL.ViewComponents
{
    public class QuantityFavorProductViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuantityFavorProductViewComponent(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            // Get the current user's ID
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                // Get the count of favorited products for the current user
                var favorProductCount = _db.FavorProducts
                    .Where(fp => fp.ApplicationUserId == userId)
                    .Count();

                return View(favorProductCount);
            }

            return View(0); // User is not authenticated, so no favorited products.
        }
    }
}
