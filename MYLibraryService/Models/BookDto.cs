namespace MYLibraryService.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public bool IsAvailable { get; set; }
        public string? Borrower { get; set; }
        public string? Summary { get; set; }
        public string? ExpireDate { get; set; }

    }
}
