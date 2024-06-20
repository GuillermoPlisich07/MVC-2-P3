using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_2_P3.DTOs;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace MVC_2_P3.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:5192/api/";
        //Configuracion para deserializar el json y evitar errores por mayusculas y minusculas
        private readonly JsonSerializerOptions _jsonOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public UsuarioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }

        // GET: LoginController/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(DTOLogin login)
        {
            try
            {
                var json = JsonSerializer.Serialize(login);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = _httpClient.PostAsync("Usuario/Login", body).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var content = respuesta.Content.ReadAsStringAsync().Result;

                    var token = JsonSerializer.Deserialize<DTOLogToken>(content, _jsonOptions);
                    if (token == null)
                    {
                        ViewBag.Error = "No se ha podido deserializar el token";
                        return View(login);
                    }
                    HttpContext.Session.SetString("Token", token.token);
                    HttpContext.Session.SetString("Email", token.email);
                    HttpContext.Session.SetString("Rol", token.rol);
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.token}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    SetError(respuesta);
                    return View(login);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(login);
            }
        }

        public ActionResult Logout()
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Remove("Email");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            return RedirectToAction("Usuario", "Login");
        }

        private void SetError(HttpResponseMessage respuesta)
        {
            var contenidoError = respuesta.Content.ReadAsStringAsync().Result;
            dynamic mensajeJson = JObject.Parse(@"{'Message':'" + contenidoError + "'}");
            ViewBag.Error = $"Hubo un error. {respuesta.ReasonPhrase} " + mensajeJson.Message;
        }
    }
}
