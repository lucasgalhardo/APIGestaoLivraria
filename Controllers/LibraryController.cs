using GestaoLivraria.Communication.Requests;
using GestaoLivraria.Communication.Responses;
using GestaoLivraria.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoLivraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private static List<Book> books = new List<Book>();
        private static Int32 BookID = 0;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseAddedBookJson), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] RequestAddBookJson request)
        {
            Book book = new Book();
            book.Id = BookID;
            book.Author = request.Author;
            book.Title = request.Title;
            book.Price = request.Price;
            book.Genre = request.Genre;
            book.Balance = request.Balance;
            books.Add(book);

            BookID++;

            var response = new ResponseAddedBookJson()
            {
                Id = book.Id,
                Title = book.Title
            };
            return Created("Cadastro realizado com sucesso!", response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(books);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] RequestUpdateBookJson request)
        {
            Book bookToRemove = books.FirstOrDefault(b => b.Id == request.Id);

            if (bookToRemove == null)
            {
                return NotFound();
            }

            books[request.Id].Author = request.Author;
            books[request.Id].Price = request.Price;
            books[request.Id].Genre = request.Genre;
            books[request.Id].Title = request.Title;

            return Ok("Alreação realizada com sucesso!");
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        public IActionResult Delete(RequestDeleteBookJson request)
        {
            Book bookToRemove = books.FirstOrDefault(b => b.Id == request.Id);

            if (bookToRemove == null)
            {
                return NotFound();
            }

            books.RemoveAt(request.Id);
            return Ok($"O livro com ID {request.Id} foi removido com sucesso!");
        }
    }
}
