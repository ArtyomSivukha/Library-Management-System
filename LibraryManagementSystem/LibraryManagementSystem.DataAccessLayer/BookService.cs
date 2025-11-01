using LibraryManagementSystem.DataAccessLayer.Entities;
using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;
using Book = LibraryManagementSystem.DataAccessLayer.Entities.Book;
using Models_Book = LibraryManagementSystem.BusinessLogicLayer.Models.Book;

namespace LibraryManagementSystem.DataAccessLayer;
public class BookService : IBookService
{
   private readonly LibraryDbContext _dbContext;

   public BookService(LibraryDbContext dbContext)
   {
      _dbContext = dbContext;
   }

   public async Task<IEnumerable<Models_Book>> GetAllBooksAsync()
   {
      return await _dbContext.Books
         .Include(book => book.Author)
         .Select(book => FromEntityToModel(book))
         .ToArrayAsync();
   }

   public async Task<Models_Book?> GetBookByIdAsync(Guid id)
   {
      var book = await _dbContext.Books
         .Include(b => b.Author)
         .FirstOrDefaultAsync(b => b.Id == id);
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
      
      var author = await _dbContext.Authors.FindAsync(book.AuthorId);
      if (author is null)
      {
         throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
      }

      var newBook = ToEntity(book);
      newBook.Author = author;
      _dbContext.Books.Add(newBook);
      await _dbContext.SaveChangesAsync();
      
      book.Id = newBook.Id;
      return book;
   }

   public async Task UpdateBookAsync(Models_Book book)
   {
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
      }

      var bookToUpdate = await _dbContext.Books.FindAsync(book.Id);
      
      if (bookToUpdate is null)
      {
         throw new ArgumentNullException(nameof(bookToUpdate), $"{nameof(bookToUpdate)} is null");
      }
      
      var author = await _dbContext.Authors.FindAsync(book.AuthorId);
      if (author is null)
      {
         throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
      }
      
      bookToUpdate.Title = book.Title;
      bookToUpdate.PublisherYear = book.PublisherYear;
      bookToUpdate.Author = author;
      
      await _dbContext.SaveChangesAsync();
   }

   public async Task DeleteBookAsync(Guid id)
   {
      var deleteBook = await _dbContext.Books.FindAsync(id);
      if (deleteBook is null)
      {
         throw new ArgumentNullException(nameof(deleteBook), $"{nameof(deleteBook)} is null");
      }
      
      _dbContext.Books.Remove(deleteBook);
      await _dbContext.SaveChangesAsync();
   }

   public async Task<IEnumerable<Models_Book>> GetBooksPublishedAfterAsync(int year)
   {
      if (year < 0)
      {
         throw new ArgumentException("Incorrect year");
      }
      var books = await _dbContext.Books.
         Include(b => b.Author).
         Where(b => b.PublisherYear > year).
         Select(b => FromEntityToModel(b)).
         ToArrayAsync();

      if (!books.Any())
      {
         throw new ArgumentNullException(nameof(books), $"{nameof(books)} is null");
      }
      return books;
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