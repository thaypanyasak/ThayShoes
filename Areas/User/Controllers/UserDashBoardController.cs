using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Models;

namespace TMDTFINAL.Areas.User.Controllers
{
    [Area("User")]
    public class UserDashBoardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserDashBoardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            // Lấy thông tin người dùng hiện tại
            var user = _userManager.GetUserAsync(User).Result;

            return View(user);
        }
    }
}
