using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using Newtonsoft.Json;

namespace ETradeMvcWebUI.Controllers
{
    [Route("[Controller]")] // ~/Location
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // şehir listesini Json olarak alacağız ve sayfamızda alanında dolduracağız
        // hiç route tanımları değiştirmeden query string ile kullanımı : ~/Location/GetCities?countryId=1
        // Route tanımlarını değiştirerek kullanımı :  ~/Location/GetCities/1
        [Route("GetCities/{countryId?}")]
        // [HttpPost] // Eğer action sonucunda dönülen veriler kritik yani herkesin ulaşmasını istemediğimiz veriler ise Http methodu Post olmalıdır
        public IActionResult GetCitiesByCountryId(int? countryId) // id = country id
        {
            try
            {
                if (countryId == null)
                {
                    return NotFound();
                }

                CountryModel country = _locationService.GetById(countryId.Value);
                return Json(country.Cities);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
