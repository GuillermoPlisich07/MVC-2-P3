using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVC_2_P3.DTOs;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MVC_2_P3.Controllers
{
    public class MovimientoStockController : Controller
    {
        
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:5192/api/";

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
            try
            {
                //Realizo la consulta
                var response1 = _httpClient.GetAsync("Articulo").Result;
                var response2 = _httpClient.GetAsync("MovimientoTipo").Result;

                if (response1.StatusCode == System.Net.HttpStatusCode.Unauthorized || response2.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Usuario", "Login");
                }

                List<DTOArticulo> art = null;
                if (response1.IsSuccessStatusCode)
                {
                    var content1 = response1.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(content1))
                    {
                        ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                        return View();
                    }
                    art = JsonSerializer.Deserialize<List<DTOArticulo>>(content1, _jsonOptions);

                }
                List<DTOMovimientoTipo> tip = null;
                if (response2.IsSuccessStatusCode)
                {
                    var content2 = response2.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(content2))
                    {
                        ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                        return View();
                    }
                    tip = JsonSerializer.Deserialize<List<DTOMovimientoTipo>>(content2, _jsonOptions);

                }

                DTOMovimientoStockCreate vista = new DTOMovimientoStockCreate()
                {
                    articulos = art,
                    tipos = tip,
                    email = HttpContext.Session.GetString("Email")
                };

                return View(vista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex;
            }
            
            return View();
        }

        // POST: MovimientoStockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTOMovimientoStockCreate movStock)
        {
            if (movStock == null)
            {
                ViewBag.Error = "El Movimiento Stock es Vacio";
                return View();
            }
            try
            {
                DTOMovimientoStockPost nuevo = new DTOMovimientoStockPost()
                {
                    id = 0,
                    idArticulo = movStock.idArticulo,
                    idMovimientoTipo = movStock.idMovimientoTipo,
                    email = movStock.email,
                    cantidadMovidas = movStock.cantidadMovidas
                };


                //Pregunto el Token que esta cargado en el session
                var token = HttpContext.Session.GetString("Token");

                //En el caso de que sea nulo, se tiene que loguear devuelta
                if ((token == null))
                {
                    return RedirectToAction("Usuario", "Login");
                }

                //Inserto el token al Autorizacion para la consulta
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);

                var json = JsonSerializer.Serialize(nuevo);

                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = _httpClient.PostAsync("MovimientoStock", body).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    SetError(respuesta);
                    return View(movStock);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: MovimientoStockController/ListadoArticuloRangoPorFecha
        public ActionResult ListadoArticuloRangoPorFecha()
        {
            var response1 = _httpClient.GetAsync("Articulo").Result;

            if (response1.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Usuario", "Login");
            }

            List<DTOArticulo> art = null;
            if (response1.IsSuccessStatusCode)
            {
                var content1 = response1.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(content1))
                {
                    ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                    return View();
                }
                art = JsonSerializer.Deserialize<List<DTOArticulo>>(content1, _jsonOptions);

            }

            List<DTOMovimientoStock> mov = null;

            DTOListadoArticuloRangoPorFecha nuevo = new DTOListadoArticuloRangoPorFecha()
            {
                movimientos = mov,
                articulos = art,
            };
            return View(nuevo);
        }

        // POST: MovimientoStockController/ListadoArticuloRangoPorFecha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListadoArticuloRangoPorFecha(DateTime? inicio, DateTime? final, List<int> idArticulos, int pagina)
        {
            return null;
        }


        // GET: MovimientoStockController/ListadoArticuloTipoDescendente
        public ActionResult ListadoArticuloTipoDescendente()
        {
            var response1 = _httpClient.GetAsync("Articulo").Result;
            var response2 = _httpClient.GetAsync("MovimientoTipo").Result;

            if (response1.StatusCode == System.Net.HttpStatusCode.Unauthorized || response2.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Usuario", "Login");
            }

            List<DTOArticulo> art = null;
            if (response1.IsSuccessStatusCode)
            {
                var content1 = response1.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(content1))
                {
                    ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                    return View();
                }
                art = JsonSerializer.Deserialize<List<DTOArticulo>>(content1, _jsonOptions);

            }
            List<DTOMovimientoTipo> tip = null;
            if (response2.IsSuccessStatusCode)
            {
                var content2 = response2.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(content2))
                {
                    ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                    return View();
                }
                tip = JsonSerializer.Deserialize<List<DTOMovimientoTipo>>(content2, _jsonOptions);

            }

            List<DTOMovimientoStock> mov = null;

            DTOListadoArticuloRangoPorFecha nuevo = new DTOListadoArticuloRangoPorFecha()
            {
                movimientos = mov,
                articulos = art,
            };
            return View(nuevo);

        }

        // POST: MovimientoStockController/ListadoArticuloTipoDescendente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListadoArticuloTipoDescendente(int idArticulo, int idTipo, int pagina)
        {
            return null;
        }


        // GET: MovimientoStockController/ListadoAnualesPorTipo
        public ActionResult ListadoAnualesPorTipo()
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


            var response = _httpClient.GetAsync("MovimientoStock/ListadoAnualesPorTipo").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Usuario", "Login");
            }

            List<DTOResumenAnio> mov = null;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(content))
                {
                    ViewBag.Mensaje = "Ocurrio un error inesperado al cargar Movimiento Stock.";
                    return View();
                }
                mov = JsonSerializer.Deserialize<List<DTOResumenAnio>>(content, _jsonOptions);

            }
            
            return View(mov);
        }

        private void SetError(HttpResponseMessage respuesta)
        {
            var contenidoError = respuesta.Content.ReadAsStringAsync().Result;
            dynamic mensajeJson = JObject.Parse(@"{'Message':'" + contenidoError + "'}");
            ViewBag.Error = $"Hubo un error. {respuesta.ReasonPhrase} " + mensajeJson.Message;
        }

    }
}
