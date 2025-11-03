using LibraryManagementSystem.BusinessLogicLayer.Services;
using LibraryManagementSystem.WebApi.Mapping;
using LibraryManagementSystem.WebApi.Models;

namespace LibraryManagementSystem.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        var viewModels = ViewModelMapper.ToAuthorWithBooksViewModel(authors);
        return Ok(viewModels);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        var viewModel = ViewModelMapper.ToAuthorViewModel(author);
        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorsAsync(AuthorViewModel authorViewModel)
    {
        var author = ViewModelMapper.ToBusinessModel(authorViewModel);
        var createdAuthor = await _authorService.CreateAuthorAsync(author);
        var resultViewModel = ViewModelMapper.ToAuthorViewModel(createdAuthor);
        return Ok(resultViewModel);

    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthorAsync(AuthorViewModel authorViewModel)
    {
        var author = ViewModelMapper.ToBusinessModel(authorViewModel);
        await _authorService.UpdateAuthorAsync(author);
        return await GetAuthorByIdAsync(author.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> FindAuthorsByNameAsync(string name)
    {
        var authorsByName = await _authorService.FindAuthorsByNameAsync(name);
        var viewModels = ViewModelMapper.ToAuthorViewModel(authorsByName);
        return Ok(viewModels);
    }
}