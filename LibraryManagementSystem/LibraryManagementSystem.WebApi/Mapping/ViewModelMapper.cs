using LibraryManagementSystem.BusinessLogicLayer.Models;
using LibraryManagementSystem.WebApi.Models;

namespace LibraryManagementSystem.WebApi.Mapping;

public static class ViewModelMapper
{
    public static AuthorWithBooksViewModel ToAuthorWithBooksViewModel(Author author)
    {
        return new AuthorWithBooksViewModel
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
            Books = ToBookViewModel(author.Books)
        };
    }

    public static Author ToBusinessModel(AuthorViewModel authorViewModel)
    {
        return new Author
        {
            Id = authorViewModel.Id,
            Name = authorViewModel.Name,
            DateOfBirth = authorViewModel.DateOfBirth
        };
    }

    public static AuthorViewModel ToAuthorViewModel(Author businessModel)
    {
        return new AuthorViewModel
        {
            Id = businessModel.Id,
            Name = businessModel.Name,
            DateOfBirth = businessModel.DateOfBirth
        };
    }

    public static Book ToBusinessModel(BookViewModel bookViewModel)
    {
        return new Book
        {
            Id = bookViewModel.Id,
            Title = bookViewModel.Title,
            PublisherYear = bookViewModel.PublisherYear,
            AuthorId = bookViewModel.AuthorId
        };
    }

    public static BookViewModel ToBookViewModel(Book businessModel)
    {
        return new BookViewModel
        {
            Id = businessModel.Id,
            Title = businessModel.Title,
            PublisherYear = businessModel.PublisherYear,
            AuthorId = businessModel.AuthorId
        };
    }

    public static IEnumerable<AuthorViewModel> ToAuthorViewModel(IEnumerable<Author> businessModels)
    {
        return businessModels.Select(ToAuthorViewModel);
    }

    public static IEnumerable<BookViewModel> ToBookViewModel(IEnumerable<Book>? businessModels)
    {
        return businessModels.Select(ToBookViewModel);
    }

    public static IEnumerable<AuthorWithBooksViewModel> ToAuthorWithBooksViewModel(IEnumerable<Author> businessModels)
    {
        return businessModels.Select(ToAuthorWithBooksViewModel);
    }
}