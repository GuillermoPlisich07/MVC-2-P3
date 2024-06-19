using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_2_P3.DTOs;
using System.Text.Json;

namespace MVC_2_P3.Controllers
{
    public class MovimientoStockController : Controller
    {
        
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:#/api/";

        private readonly JsonSerializerOptions _jsonOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public MovimientoStockController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }




        // GET: MovimientoStockController/Create
        public ActionResult Create()
        {
            //Pregunto el Token que esta cargado en el session
            var token = HttpContext.Session.GetString("Token");

            //En el caso de que sea nulo, se tiene que loguear devuelta
            if ((token == null))
            {
                return RedirectToAction("Usuario", "Login");
            }

            //Inserto el token al Autorizacion para la consulta
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);

            //Realizo la consulta
            var response1 = _httpClient.GetAsync("Articulo").Result;
            var response2 = _httpClient.GetAsync("MovimientoTipo").Result;






            return View();
        }

        // POST: MovimientoStockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //Pregunto el Token que esta cargado en el session
            var token = HttpContext.Session.GetString("Token");

            //En el caso de que sea nulo, se tiene que loguear devuelta
            if ((token == null))
            {
                return RedirectToAction("Usuario", "Login");
            }

            try
            {
                


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
