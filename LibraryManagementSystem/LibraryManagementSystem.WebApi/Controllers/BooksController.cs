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
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetBookByIdAsync(long id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(Book book)
    {
        try
        {
            var createdBook = await _bookService.CreateBookAsync(book);
            return Ok(createdBook);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateBookAsync(Book book, long id)
    {
        if (id != book.Id)
        {
            return BadRequest("Route ID and book ID do not match");
        }
        try
        {
            await _bookService.UpdateBookAsync(book);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBookAsync(long id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}