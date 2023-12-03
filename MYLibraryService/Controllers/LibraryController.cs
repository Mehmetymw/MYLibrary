using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYLibraryService.Context;
using MYLibraryService.Models;

namespace MYLibraryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> logger;
        private readonly LibraryDbContext libraryDbContext;
        public LibraryController(ILogger<LibraryController> logger, LibraryDbContext libraryDbContext)
        {
            this.logger = logger;
            this.libraryDbContext = libraryDbContext;
        }

        //Veritabanındaki bütün kitapları getirir.
        [HttpGet("getBooks")]
        public async Task<List<BookDto>> GetBooks()
        {
            try
            {
                logger.LogInformation("Bütün kitaplar getirildi");
                return await libraryDbContext.Books.OrderByDescending(b=>b.Name).Reverse().ToListAsync();
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex,"Kitapları getirirken bir hata oluştu.");
                throw;
            }
        }

        //Veritabanına yeni bir kitap ekler
        [HttpPost("addBook")]
        public async Task<ActionResult> AddBook([FromBody]BookDto newBook)
        {
            try
            {
                if (newBook is null)
                    throw new ArgumentNullException(nameof(newBook));

                logger.LogInformation($"{newBook.Name} isimli Kitap başarıyla eklendi");
                await libraryDbContext.Books.AddAsync(newBook);
                await libraryDbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                string error = "Kitap eklenirken bir hata oluştu";
                logger.LogInformation(ex, error);
                return BadRequest(error);
            }
        }

        //Veritabanında name parametresinden gelen veri ile aynı isimdeki kitapları getirir
        [HttpGet("getBooksByName/{name}")]
        public async Task<List<BookDto>> GetBooksByName(string name)
        {
            try
            {
             
                logger.LogInformation($"{name}'i içeren kitaplar getirildi");
                var books= await libraryDbContext.Books.Where(b => b.Name.Contains(name)).OrderByDescending(b => b.Name).Reverse().ToListAsync();
                return  books;

            }
            catch(Exception ex)
            {
                logger.LogInformation(ex, $"{name} isimli Kitaplar getirilirken bir hata oluştu");
                throw;
            }
        }

        //Parametrede verilen id ile eşleşen kitap getirilir
        [HttpGet("getBookById/{id}")]
        public async Task<BookDto> GetBookById(int id)
        {
            try
            {
                logger.LogInformation($"{id}'li kitap getirildi");
                var book = await libraryDbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
                return book;

            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"{id} isimli kitap getirilirken bir hata oluştu");
                throw;
            }
        }

        //Parametrede verilen id ile eşleşen kitap getirilir
        [HttpGet("lendBook")]
        public async Task<BookDto> LendBook(int bookId,string borrowerName,string expireDate)
        {
            try
            {
                var book = await libraryDbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
                if(book is not null)
                {
                    logger.LogInformation($"{bookId} li kitap {borrowerName} isimli kişiye ödünç verildi");
                    book.Borrower = borrowerName;
                    book.IsAvailable = false;
                    book.ExpireDate = expireDate;
                    await libraryDbContext.SaveChangesAsync();
                }
                return book;

            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"{bookId} isimli kitap ödünç verilirken bir hata oluştu");
                throw;
            }
        }

    };

}