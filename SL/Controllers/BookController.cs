using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Authorize]
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public List<ML.Book> _books;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string filePath;

        public BookController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            //Aqui va la ruta donde tengas guardado el archivo JSON
            filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "D:\\Archivos\\Proyectos & Practicas\\C#\\PruebaKranon\\JSON\\book.json");

            var jsonContent = System.IO.File.ReadAllText(filePath);
            _books = System.Text.Json.JsonSerializer.Deserialize<List<ML.Book>>(jsonContent);
        }

        [HttpGet]
        [Route("getall")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Book.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpGet]
        [Route("getbyauthor/{author}")]
        public IActionResult GetByAuthor(string author)
        {
            ML.Result result = BL.Book.GetByAuthor(author);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpGet]
        [Route("getbyid/{idBook}")]
        public IActionResult GetById(int idBook)
        {
            ML.Result result = BL.Book.GetById(idBook);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpGet]
        [Route("getbybookname/{bookName}")]
        public IActionResult GetByBookName(string bookName)
        {
            ML.Result result = BL.Book.GetByBookName(bookName);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpGet]
        [Route("getbyreleasedate/{releaseDate}")]
        public IActionResult GetByReleaseDate(DateTime releaseDate)
        {
            ML.Result result = BL.Book.GetByReleaseDate(_books, releaseDate);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Book book)
        {
            ML.Result result = BL.Book.Add(book);

            _books.Add(book);

            if (result.Correct)
            {
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(_books);
                System.IO.File.WriteAllText(filePath, jsonContent);

                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpPut]
        [Route("updatebyauthor/{author}")]
        public IActionResult UpdateByAuthor(string author, [FromBody] ML.Book book)
        {
            ML.Result result = BL.Book.UpdateByAuthor(author, book);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpPut]
        [Route("updatebybookname/{bookName}")]
        public IActionResult UpdateByBookName(string bookName, [FromBody] ML.Book book)
        {
            ML.Result result = BL.Book.UpdateByBookName(bookName, book);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpPut]
        [Route("update/{idBook}")]
        public IActionResult Update(int idBook, [FromBody] ML.Book book)
        {
            ML.Result result = BL.Book.Update(idBook, book);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpDelete]
        [Route("deletebyauthor/{author}")]
        public IActionResult DeleteByAuthor(string author)
        {
            ML.Result result = BL.Book.DeleteByAuthor(author);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpDelete]
        [Route("deletebybookname/{bookName}")]
        public IActionResult DeleteByBookName(string bookName)
        {
            ML.Result result = BL.Book.DeleteByBookName(bookName);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }

        [HttpDelete]
        [Route("deletebyreleasedate/{releaseDate}")]
        public IActionResult DeleteByReleaseDate(DateTime releaseDate)
        {
            ML.Result result = BL.Book.DeleteByReleaseDate(releaseDate);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Error: {result.Message}");
            }
        }
    }
}
