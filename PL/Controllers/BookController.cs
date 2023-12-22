using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

namespace PL.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private IHostingEnvironment environment;
        public BookController(IConfiguration _configuration, IHttpClientFactory _httpClientFactory)
        {
            configuration = _configuration;
            httpClientFactory = _httpClientFactory;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (IsAuthenticated(username, password))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
        };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.ErrorMessage = "Credenciales inválidas";
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login");
            }

            ML.Book book = new ML.Book();

            ML.Result resultBook = new ML.Result();
            resultBook.Objects = new List<Object>();

            using (HttpClient cliente = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                cliente.BaseAddress = new Uri(webApi);

                string authString = "admin:pass123";
                string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var responseTask = cliente.GetAsync("book/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Book resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Book>(resultItem.ToString());
                        resultBook.Objects.Add(resultItemList);
                    }
                }

                book.Bookss = resultBook.Objects;
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult FormAdd()
        {
            ML.Book book = new ML.Book();

            return View(book);
        }

        [HttpPost]
        public ActionResult FormAdd(ML.Book book)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login");
            }

            ML.Result result = new ML.Result();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                string authString = "admin:pass123";
                string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Book>("book/add", book);
                postTask.Wait();

                HttpResponseMessage resultTask = postTask.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    result.Correct = true;
                    ViewBag.Titulo = "El registro se inserto correctamente.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al insertar el registro.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult FormUpdate(int idBook)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login");
            }
            ML.Book book = new ML.Book();

            ML.Result result = BL.Book.GetById(idBook);
            book = (ML.Book)result.Object;
            return View(book);
        }

        [HttpPost]
        public ActionResult FormUpdate(ML.Book book)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login");
            }

            ML.Result result = new ML.Result();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                string authString = "admin:pass123";
                string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                Task<HttpResponseMessage> postTask = client.PutAsJsonAsync<ML.Book>("book/update/" + book.IdBook, book);
                postTask.Wait();

                HttpResponseMessage resultTask = postTask.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    result.Correct = true;
                    ViewBag.Titulo = "El registro se actualizo correctamente.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al actualizar el registro.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }

        public ActionResult Delete(ML.Book book)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login");
            }

            ML.Result resultBook = new ML.Result();
            string bookName = book.BookName;

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                string authString = "admin:pass123";
                string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var responseTask = client.DeleteAsync("book/deletebybookname/" + bookName);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "El registro se elimino correctamente.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al eliminar el registro.";
                    return View("Modal");
                }
            }
            return View("Modal");
        }

        private bool IsAuthenticated(string username, string password)
        {
            return username == "admin" && password == "pass123";
        }

        private bool IsUserAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
