using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id)
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBookAsync(Book book, Guid id)
    {
        if (id != book.Id)
        {
            return BadRequest("Route ID and book ID do not match");
        }

        await _bookService.UpdateBookAsync(book);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBookAsync(Guid id)
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