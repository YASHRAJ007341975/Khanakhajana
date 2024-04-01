using Khanakhajana.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Khanakhajana.Data;
namespace Khanakhajana.Controllers
{
    public class KhanakhajanaController : Controller
    {
        private dbcontext s2;

      
        private readonly HttpClient _httpClient;

        public KhanakhajanaController(IHttpClientFactory httpClientFactory, dbcontext dbContext)
        {
            s2 = dbContext;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44395");
        }


        public IActionResult Header()
        {
            return View();
        }
        public IActionResult Footer()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpGet]
        public IActionResult Sign()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(RegisterModel abc)
        {
            var users = s2.Users.FirstOrDefault(u => u.Email == abc.Email && u.Password == abc.Password);
            if (users != null)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Your Email or password is incorrect");
            }
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sign(RegisterModel abc)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return the same view with validation errors
                return View(abc);
            }
            s2.Users.Add(abc);
            s2.SaveChanges();
            return RedirectToAction("Login");
        }



        public async Task<IActionResult> Homecategory()
        {
            var response = await _httpClient.GetAsync("api/Recipes/Category");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                return View(recipes);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }

            

        }




        //public async Task<IActionResult> Home()
        //{    var response0 = await _httpClient.GetAsync("/api/Recipes");

        //    var response1 = await _httpClient.GetAsync("/api/Recipes");
        //    if (response0.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response0.Content.ReadAsStringAsync();
        //        var recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonString);
        //        return View(recipes);
        //    }
        //    else
        //    {
        //        return StatusCode((int)response0.StatusCode);
        //    }



        //}
        public async Task<IActionResult> Home()
        {
            var response0 = await _httpClient.GetAsync("api/Recipes/Category");
            var response1 = await _httpClient.GetAsync("/api/Recipes");

            if (response0.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                var jsonString0 = await response0.Content.ReadAsStringAsync();
                var jsonString1 = await response1.Content.ReadAsStringAsync();

                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString0);
                var recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonString1);

                var model = new HomeViewModel
                {
                    Categories = categories,
                    Recipes = recipes
                };

                return View(model);
            }
            else
            {
                // Handle if any of the requests fail
                return StatusCode((int)response0.StatusCode);
            }
        }



        public async Task<IActionResult> ViewRecipe(int id)
        {
            var response = await _httpClient.GetAsync("/api/Recipes");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonString);

                // Filter recipes to only include the one with ID equal to the provided id
                var filteredRecipe = recipes.FirstOrDefault(recipe => recipe.Id == id);

                if (filteredRecipe != null)
                {
                    // Return the view with the filtered recipe
                    return View(filteredRecipe);
                }
                else
                {
                    // Recipe with provided id not found
                    return NotFound();
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        public async Task<IActionResult> Category()
        {
            var response = await _httpClient.GetAsync("api/Recipes/Category");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                return View(recipes);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }



        }



        
        public async Task<IActionResult> ViewCategory(string categoryName)
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Recipes");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonString);

                    // Filter recipes by category
                    var filteredRecipes = recipes.Where(recipe => recipe.Category == categoryName).ToList();

                    return View(filteredRecipes);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet]
        public IActionResult Profile()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileModel abc)
        {
            s2.Profile.Add(abc);
            s2.SaveChanges();

            return RedirectToAction("Profile");
        }


        public IActionResult Favourite()
        {
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }


    }
}
