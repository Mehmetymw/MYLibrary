using Microsoft.EntityFrameworkCore;
using MYLibraryService.Models;

namespace MYLibraryService.Context
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        public DbSet<BookDto> Books { get; set; }
    }
}
