using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.Services;
using CountriesAndCities.Web.Models;
using CountriesAndCities.Web.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CountriesAndCities.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cs;
        private readonly GetObjectsService _gos;
        public CityController(ICityService cs, GetObjectsService gos)
        {
            _cs = cs;
            _gos = gos;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _cs.CitiesCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var cities = await _cs.GetAllAsync(currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(cities);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByCountryId(long id, int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _cs.CitiesCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var cities = await _cs.GetCitiesInACountryAsync(id, currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View("Index", cities);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var newViewModel = new CityViewModel();
            newViewModel.Countries = this.GetCountries();
            return this.View(newViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel model)
        {
            var city = model.GetDTO();
            await _cs.PostAsync(city);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var city = await _cs.GetById(id);
            var model = city.GetModel();
            model.Countries = this.GetCountries();

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CityViewModel model)
        {
            var city = model.GetDTO();
            await _cs.UpdateAsync(model.Id, city);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var city = await _cs.GetById(id);
            var model = city.GetModel();

            return this.View(model);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _cs.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }

        private SelectList GetCountries()
        {
            var countries = this._gos.GetCountries();
            return new SelectList(countries, "Id", "Name");
        }

    }
}
