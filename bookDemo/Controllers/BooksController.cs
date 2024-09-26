using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContex.Books;
            return Ok(books);


        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id)
        { //LINQ SORGUSU DİLE ENTEGRE SORGU FİLTRELEME YAPIYOR BULDUYSA KİTABI BULAMADIYSA NULL DÖNER
            var book = ApplicationContex
                .Books.
                Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if (book is null)
                return NotFound();

            return Ok(book);


        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();
                ApplicationContex.Books.Add(book);
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            // Var olan kitabı bul
            var entity = ApplicationContex.Books.SingleOrDefault(b => b.Id == id);

            // Eğer kitap bulunamadıysa 404 Not Found döndür
            if (entity == null)
                return NotFound(); // 404

            // Eğer id'ler uyuşmuyorsa 400 Bad Request döndür
            if (id != book.Id)
                return BadRequest(); // 400

            // Mevcut kitabın alanlarını güncelle
            entity.Title = book.Title;
            entity.Price = book.Price;

            // Güncellenmiş kitabı geri döndür
            return Ok(entity);
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContex.Books.Clear();
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id") ]int id)
        {
             var entity = ApplicationContex
            .Books
            .Find(b => b.Id.Equals(id));
          
             if(entity is null)
                return NotFound();
            ApplicationContex.Books.Remove(entity);
            return NoContent();

                       
        }

    }
}
