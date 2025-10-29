using LibraryManagementSystem.Services.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.EntityFramework;
public class BookService : IBookService
{
   private readonly LibraryDbContext _dbContext;

   public BookService(LibraryDbContext dbContext)
   {
      _dbContext = dbContext;
   }

   public async Task<IEnumerable<Models.Book>> GetAllBooksAsync()
   {
      return await _dbContext.Books
         .Include(book => book.Author)
         .Select(book => FromEntityToModel(book))
         .ToArrayAsync();
   }

   public async Task<Models.Book?> GetBookByIdAsync(long id)
   {
      var book = await _dbContext.Books
         .Include(b => b.Author)
         .FirstOrDefaultAsync(b => b.Id == id);
      return book is null ? null : FromEntityToModel(book);
   }

   public async Task<Models.Book> CreateBookAsync(Models.Book book)
   {
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
      }
      
      var author = await _dbContext.Authors.FindAsync(book.AuthorId);
      if (author is null)
      {
         throw new NullReferenceException($"Author with id {book.AuthorId} does not exist");
      }

      var newBook = ToEntity(book);
      newBook.Author = author;
      _dbContext.Books.Add(newBook);
      await _dbContext.SaveChangesAsync();
      
      return book;
   }

   public async Task UpdateBookAsync(Models.Book book)
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
         throw new NullReferenceException($"Author with id {book.AuthorId} does not exist");
      }
      
      bookToUpdate.Title = book.Title;
      bookToUpdate.PublisherYear = book.PublisherYear;
      bookToUpdate.Author = author;
      
      await _dbContext.SaveChangesAsync();
   }

   public async Task DeleteBookAsync(long id)
   {
      var deleteBook = await _dbContext.Books.FindAsync(id);
      if (deleteBook is null)
      {
         throw new ArgumentNullException(nameof(deleteBook), $"{nameof(deleteBook)} is null");
      }
      
      _dbContext.Books.Remove(deleteBook);
      await _dbContext.SaveChangesAsync();
   }

   private static Book ToEntity(Models.Book book) =>
      new()
      {
         Id = book.Id,
         Title = book.Title,
         PublisherYear = book.PublisherYear
      };

   private static Models.Book FromEntityToModel(Book book) =>
      new()
      {
         Id = book.Id,
         Title = book.Title,
         PublisherYear = book.PublisherYear,
         AuthorId = book.Author.Id
      };
}