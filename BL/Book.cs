using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;

namespace BL
{
    public class Book
    {
        private static List<ML.Book> _books;
        private static string filePath;

        static Book()
        {
            //Aqui va la ruta donde tengas guardado el archivo JSON
            filePath = "D:\\Archivos\\Proyectos & Practicas\\C#\\PruebaKranon\\JSON\\book.json";

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                _books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ML.Book>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al leer el archivo. {ex.Message}");
                _books = new List<ML.Book>();
            }
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var getAll = _books.ToList();

                result.Objects = new List<object>();

                foreach (var item in getAll)
                {
                    ML.Book books = new ML.Book();
                    books.IdBook = item.IdBook;
                    books.BookName = item.BookName;
                    books.Author = item.Author;
                    books.ReleaseDate = item.ReleaseDate;

                    result.Objects.Add(books);
                }

                result.Correct = true;

            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetById(int idBook)
        {
            ML.Result result = new ML.Result();

            try
            {
                var getBook = _books
                    .Where(book =>
                        (book.IdBook != null && book.IdBook == idBook) ||
                        (book.IdBook == null && idBook == null)
                    )
                    .ToList();

                result.Objects = new List<object>();

                foreach (var item in getBook)
                {
                    ML.Book books = new ML.Book();
                    books.IdBook = item.IdBook;
                    books.BookName = item.BookName;
                    books.Author = item.Author;
                    books.ReleaseDate = item.ReleaseDate;

                    result.Object = books;
                }

                result.Correct = true;

            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetByAuthor(string author)
        {
            ML.Result result = new ML.Result();

            try
            {
                var getByAuthor = _books
                    .Where(book =>
                        (book.Author != null && book.Author.ToLower() == author.ToLower()) ||
                        (book.Author == null && author == null)
                    )
                    .ToList();

                result.Objects = new List<object>();

                foreach ( var item in getByAuthor )
                {
                    ML.Book books = new ML.Book();
                    books.BookName = item.BookName;
                    books.Author = item.Author;
                    books.ReleaseDate = item.ReleaseDate;
                    
                    result.Objects.Add( item );
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetByBookName(string bookName)
        {
            ML.Result result = new ML.Result();

            try
            {
                var getByBookName = _books
                    .Where(book =>
                        (book.BookName != null && book.BookName.ToLower() == bookName.ToLower()) ||
                        (book.BookName == null && bookName == null)
                    )
                    .ToList();

                result.Objects = new List<object>();

                foreach (var item in getByBookName)
                {
                    ML.Book books = new ML.Book();
                    books.BookName = item.BookName;
                    books.Author = item.Author;
                    books.ReleaseDate = item.ReleaseDate;

                    result.Objects.Add(item);
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetByReleaseDate(List<ML.Book> book, DateTime releaseDate)
        {
            ML.Result result = new ML.Result();

            try
            {
                var getByReleaseDate = book
                    .Where(book =>
                        (book.ReleaseDate != null && book.ReleaseDate == releaseDate) ||
                        (book.ReleaseDate == null && releaseDate == null)
                    )
                    .ToList();

                result.Objects = new List<object>();

                foreach (var item in getByReleaseDate)
                {
                    ML.Book books = new ML.Book();
                    books.BookName = item.BookName;
                    books.Author = item.Author;
                    books.ReleaseDate = item.ReleaseDate;

                    result.Objects.Add(item);
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Book book)
        {
            ML.Result result = new ML.Result();

            try
            {
                var addBook = book;

                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result UpdateByAuthor(string author, ML.Book book)
        {
            ML.Result result = new ML.Result();

            try
            {
                var existingBook = _books.FirstOrDefault(book =>
                book.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

                if (existingBook != null)
                {
                    existingBook.BookName = book.BookName ?? existingBook.BookName;
                    existingBook.Author = book.Author ?? existingBook.Author;
                    existingBook.ReleaseDate = book.ReleaseDate ?? existingBook.ReleaseDate;

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(_books);
                    File.WriteAllText(filePath, jsonContent);

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.Message = "No se encontro ningun libro con ese autor.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result UpdateByBookName(string bookName, ML.Book book)
        {
            ML.Result result = new ML.Result();

            try
            {
                var existingBook = _books.FirstOrDefault(book =>
                book.BookName.Equals(bookName, StringComparison.OrdinalIgnoreCase));

                if (existingBook != null)
                {
                    existingBook.BookName = book.BookName ?? existingBook.BookName;
                    existingBook.Author = book.Author ?? existingBook.Author;
                    existingBook.ReleaseDate = book.ReleaseDate ?? existingBook.ReleaseDate;

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(_books);
                    File.WriteAllText(filePath, jsonContent);

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.Message = "No se encontro ningun libro con ese titulo.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result Update(int idBook, ML.Book book)
        {
            ML.Result result = new ML.Result();

            try
            {
                var existingBook = _books.FirstOrDefault(book => book.IdBook == idBook);

                if (existingBook != null)
                {
                    existingBook.BookName = book.BookName ?? existingBook.BookName;
                    existingBook.Author = book.Author ?? existingBook.Author;
                    existingBook.ReleaseDate = book.ReleaseDate ?? existingBook.ReleaseDate;

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(_books);
                    File.WriteAllText(filePath, jsonContent);

                    result.Object = existingBook;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.Message = "No se encontro ningun libro con ese Id.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result DeleteByAuthor(string author)
        {
            ML.Result result = new ML.Result();

            try
            {
                var bookDeleted = _books.Where(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();

                if (bookDeleted.Count == 0)
                {
                    result.Message = "No se han encontrado libros con ese autor.";
                }

                foreach (ML.Book book in bookDeleted)
                {
                    _books.Remove(book);
                }

                SaveChanges();

                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result DeleteByBookName(string bookName)
        {
            ML.Result result = new ML.Result();

            try
            {
                var bookDeleted = _books.RemoveAll(book => book.BookName.Equals(bookName, StringComparison.OrdinalIgnoreCase));

                if (bookDeleted == 0)
                {
                    result.Message = "No se han encontrado libros con ese titulo.";
                }

                SaveChanges();

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result DeleteByReleaseDate(DateTime releaseDate)
        {
            ML.Result result = new ML.Result();

            try
            {
                var bookDeleted = _books.Where(book =>
                book.ReleaseDate.HasValue && book.ReleaseDate.Value.Year == releaseDate.Year).ToList();

                if (bookDeleted.Count == 0)
                {
                    result.Message = "No se han encontrado libros con esa fecha.";
                }

                foreach (ML.Book book in bookDeleted)
                {
                    _books.Remove(book);
                }

                SaveChanges();

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        private static void SaveChanges()
        {
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(_books);
            System.IO.File.WriteAllText(filePath, jsonContent);
        }
    }
}
