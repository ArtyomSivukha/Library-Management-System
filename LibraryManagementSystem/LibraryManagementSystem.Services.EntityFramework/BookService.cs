using LibraryManagementSystem.Services.EntityFramework.Entities;
using ServiceEntities = LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.EntityFramework;
public class BookService : IBookService
{
   private readonly LibraryDbContext _dbContext;

   public Task<IEnumerable<ServiceEntities.Book>> GetAllBooksAsync()
   {
      throw new NotImplementedException();
   }

   public Task<ServiceEntities.Book?> GetBookByIdAsync(long id)
   {
      throw new NotImplementedException();
   }

   public Task<ServiceEntities.Book> CreateBookAsync(ServiceEntities.Book book)
   {
      throw new NotImplementedException();
   }

   public Task UpdateBookAsync(ServiceEntities.Book book)
   {
      throw new NotImplementedException();
   }

   public Task DeleteBookAsync(long id)
   {
      throw new NotImplementedException();
   }
}