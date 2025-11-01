using LibraryManagementSystem.Services;
using LibraryManagementSystem.Services.Local;
using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
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
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(Book book)
    {
        var createdBook = await _bookService.CreateBookAsync(book);
        return Ok(createdBook);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateBookAsync(Book book, long id)
    {
        // if (id != book.Id)
        // {
        //     return BadRequest("Route ID and book ID do not match");
        // }

        await _bookService.UpdateBookAsync(book);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBookAsync(long id)
    {
        await _bookService.DeleteBookAsync(id);
        return Ok();
    }

    [HttpGet("after/{year:int}")]
    public async Task<IActionResult> GetBooksAfter(int year)
    {
        var books = await _bookService.GetBooksPublishedAfterAsync(year);
        return Ok(books);
    }
}