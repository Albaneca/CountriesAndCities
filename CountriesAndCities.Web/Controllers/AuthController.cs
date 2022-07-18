using CountriesAndCities.Common;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.DTOs.AuthDTOs;
using CountriesAndCities.Web.Models;
using CountriesAndCities.Web.Models.Mapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CountriesAndCities.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IUserService _us;

        public AuthController(IAuthService auth,
            IUserService us)
        {
            _auth = auth;
            _us = us;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new RequestAuthDTO());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(RequestAuthDTO model)
        {
            if (!await _auth.IsPasswordValidAsync(model.Email, model.Password))
            {
                ModelState.AddModelError("Password", GlobalConstants.WRONG_CREDENTIALS);
                return View(model);
            }
            var user = await _auth.GetByEmailAsync(model.Email);

            await SignInWithRoleAsync(user.Email);

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _auth.IsExistingAsync(model.Email))
            {
                ModelState.AddModelError("Email", GlobalConstants.USER_EXISTS);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var toUser = model.GetDTO();

            await _us.PostAsync(toUser);

            return RedirectToAction("index", "home");
        }

        private async Task SignInWithRoleAsync(string email)
        {
            //You can add more claims as you wish but keep these KEYS here as is
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, email));


            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }

}
