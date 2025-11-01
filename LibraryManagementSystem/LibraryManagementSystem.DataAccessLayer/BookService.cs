using LibraryManagementSystem.DataAccessLayer.Entities;
using LibraryManagementSystem.DataAccessLayer.Repositories;
using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;
using Book = LibraryManagementSystem.DataAccessLayer.Entities.Book;
using Models_Book = LibraryManagementSystem.BusinessLogicLayer.Models.Book;

namespace LibraryManagementSystem.DataAccessLayer;
public class BookService : IBookService
{
   private readonly IBookRepository _bookRepository;
   private readonly IAuthorRepository _authorRepository;

   public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
   {
      _bookRepository = bookRepository;
      _authorRepository = authorRepository;
   }

   public async Task<IEnumerable<Models_Book>> GetAllBooksAsync()
   {
      var books = await _bookRepository.GetAllAsync();
      return books.Select(FromEntityToModel);
   }

   public async Task<Models_Book?> GetBookByIdAsync(Guid id)
   {
      var book = await _bookRepository.GetByIdAsync(id);
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
      }
      return FromEntityToModel(book);
   }

   public async Task<Models_Book> CreateBookAsync(Models_Book book)
   {
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
      }
      
      var author = await _authorRepository.GetByIdAsync(book.AuthorId);
      if (author is null)
      {
         throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
      }

      var newBook = ToEntity(book);
      newBook.Author = author;
      newBook = await _bookRepository.CreateAsync(newBook);
      
      book.Id = newBook.Id;
      return book;
   }

   public async Task UpdateBookAsync(Models_Book book)
   {
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
      }

      var bookToUpdate = await _bookRepository.GetByIdAsync(book.Id);
      
      if (bookToUpdate is null)
      {
         throw new ArgumentNullException(nameof(bookToUpdate), $"{nameof(bookToUpdate)} is null");
      }
      
      var author = await _authorRepository.GetByIdAsync(book.AuthorId);
      if (author is null)
      {
         throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
      }
      
      bookToUpdate.Title = book.Title;
      bookToUpdate.PublisherYear = book.PublisherYear;
      bookToUpdate.Author = author;
      
      await _bookRepository.UpdateAsync(bookToUpdate);
   }

   public async Task DeleteBookAsync(Guid id)
   {
      var deleteBook = await _bookRepository.GetByIdAsync(id);
      if (deleteBook is null)
      {
         throw new ArgumentNullException(nameof(deleteBook), $"{nameof(deleteBook)} is null");
      }
      
      await _bookRepository.DeleteAsync(id);
   }

   public async Task<IEnumerable<Models_Book>> GetBooksPublishedAfterAsync(int year)
   {
      if (year < 0)
      {
         throw new ArgumentException("Incorrect year");
      }
      var books = await _bookRepository.GetBooksPublishedAfterAsync(year);
      var booksList = books.Select(FromEntityToModel).ToList();

      if (!booksList.Any())
      {
         throw new ArgumentNullException(nameof(books), $"{nameof(books)} is null");
      }
      return booksList;
   }

   private static Book ToEntity(Models_Book book) =>
      new()
      {
         Id = book.Id,
         Title = book.Title,
         PublisherYear = book.PublisherYear
      };

   private static Models_Book FromEntityToModel(Book book) =>
      new()
      {
         Id = book.Id,
         Title = book.Title,
         PublisherYear = book.PublisherYear,
         AuthorId = book.Author.Id
      };
}