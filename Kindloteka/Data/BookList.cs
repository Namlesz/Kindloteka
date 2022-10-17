using Kindloteka.Models;

namespace Kindloteka.Data;

public class BookList
{

    public static readonly List<Book> Books = new()
    {
        new Book("The Hobbit", "J.R.R. Tolkien"),
        new Book("Kobzar", "Taras Shevchenko"),
        new Book("A Song of Ice and Fire", "George R.R. Martin")
    };
    
}