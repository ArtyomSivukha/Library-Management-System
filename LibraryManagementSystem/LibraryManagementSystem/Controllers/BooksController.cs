using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _bookService.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById(long id)
    {
        var book = _bookService.GetBookById(id);
        if (book is null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost("createBook")]
    public IActionResult CreateBook(Book book)
    {
        if (ModelState.IsValid)
        {
            var createdBook = _bookService.CreateBook(book);
            return Ok(createdBook);
        }
        return BadRequest();
    }

    [HttpPut("{id:long}")]
    public IActionResult UpdateBook(Book book, long id)
    {
        if (id == book.Id)
        {
            _bookService.UpdateBook(book);
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        _bookService.DeleteBook(id);
        return Ok();
    }
}