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
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }
}