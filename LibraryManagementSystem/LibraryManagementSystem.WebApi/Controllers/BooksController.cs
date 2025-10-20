using LibraryManagementSystem.Services.Local;
using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.WebApi.Controllers;

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
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
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
            try
            {
                _bookService.UpdateBook(book);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        return BadRequest();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }
}