using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Web.Models;
using CountriesAndCities.Web.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndCities.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _cs;
        public CountryController(ICountryService cs)
        {
            _cs = cs;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _cs.CountriesCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var countries = await _cs.GetAllAsync(currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(countries);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var newViewModel = new CountryViewModel();
            return this.View(newViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CountryViewModel model)
        {
            var country = model.GetDTO();
            await _cs.PostAsync(country);

            return this.RedirectToAction("Index", "Country");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var country = await _cs.GetById(id);
            var model = country.GetModel();

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel model)
        {
            var country = model.GetDTO();
            await _cs.UpdateAsync(model.Id, country);

            return this.RedirectToAction("Index", "Country");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var country = await _cs.GetById(id);
            var model = country.GetModel();

            return this.View(model);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _cs.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
