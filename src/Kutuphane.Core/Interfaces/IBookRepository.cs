using Kutuphane.Core.Models;

namespace Kutuphane.Core.Interfaces;

public interface IBookRepository
{
    IReadOnlyList<Book> GetAll();
    IReadOnlyList<Book> SearchByTitle(string keyword);
    Book? GetById(int id);
    void Add(Book book);
    void Update(Book book);
    void Delete(int id);
    bool IsOnLoan(int bookId);
}
