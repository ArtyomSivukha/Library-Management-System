using LibraryManagementSystem.BusinessLogicLayer.Models;
using LibraryManagementSystem.BusinessLogicLayer.Services;
using LibraryManagementSystem.WebApi.Mapping;
using LibraryManagementSystem.WebApi.Models;

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
        var viewModel =  ViewModelMapper.ToBookViewModel(books);
        return Ok(viewModel);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        var viewModel = ViewModelMapper.ToBookViewModel(book);
        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(BookViewModel bookViewModel)
    {
        var book = ViewModelMapper.ToBusinessModel(bookViewModel);
        var createdBook = await _bookService.CreateBookAsync(book);
        var resultViewModel = ViewModelMapper.ToBookViewModel(createdBook);
        return Ok(resultViewModel);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBookAsync(BookViewModel bookViewModel)
    {
        var book = ViewModelMapper.ToBusinessModel(bookViewModel);
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
        var viewModels = ViewModelMapper.ToBookViewModel(books);
        return Ok(viewModels);
    }
}