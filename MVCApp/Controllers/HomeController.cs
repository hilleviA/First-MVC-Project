using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MVCApp.Models;
using System.IO;

namespace MVCApp.Controllers
{

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //Läser in json-fil och spara till lista
            var jsonString = System.IO.File.ReadAllText("movies.json");
            var jsonMovieObj = JsonConvert.DeserializeObject<IEnumerable<Movies>>(jsonString);

            //Skickar listan till vyn
            return View(jsonMovieObj);

        }

        public IActionResult Movies()
        {
            return View();
        }


        //Hantera formulärdata
        [HttpPost]
        public IActionResult Movies(Movies model)
        {
            if (ModelState.IsValid)
            {
                var jsonString = System.IO.File.ReadAllText("movies.json");
                List<Movies> movieList = JsonConvert.DeserializeObject<List<Movies>>(jsonString);

                //Lägger till ny film i listan
                movieList.Add(new Movies { Title = model.Title, Genre = model.Genre, ReleaseYear = model.ReleaseYear, ViewDate = model.ViewDate, Raiting = model.Raiting });

                //Serialiserar listan tillbaka till json och sparar
                jsonString = JsonConvert.SerializeObject(movieList, Formatting.Indented);
                using (var writer = new StreamWriter("movies.json"))
                {
                    writer.Write(jsonString);
                }

                //Skapar ett objekt av formulärdata
                Movies newMovie = new Movies
                {
                    Title = model.Title,
                    Genre = model.Genre,
                    ReleaseYear = model.ReleaseYear,
                    ViewDate = model.ViewDate,
                    Raiting = model.Raiting
                };

                //Konverterar objekt till JSON och sparar som sessionsvariabel
                string jsonMovieObj = JsonConvert.SerializeObject(newMovie);
                HttpContext.Session.SetString("addedMovie", jsonMovieObj);

                //Skickar vidare till ny vy
                return Redirect("MovieResponse");
            }
            return View();
        }

        public IActionResult MovieResponse()
        {
            //Hämtar in sessionsvariabel och konverterar tillbaka
            string jsonMovieObj = HttpContext.Session.GetString("addedMovie");
            Movies newMovie = JsonConvert.DeserializeObject<Movies>(jsonMovieObj);

            //Skickar meddelande med ViewBad
            ViewBag.ResponseMessage = "Följande film har lagts till: ";

            //Returnerar modellen tillbaka till Vyn
            return View(newMovie);
        }

    }
}
