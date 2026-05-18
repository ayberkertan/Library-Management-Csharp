using Kutuphane.Core.Models;

namespace Kutuphane.Core.Interfaces;

public interface IUserRepository
{
    IReadOnlyList<LibraryUser> GetAll();
    LibraryUser? GetByTc(string tc);
    LibraryUser? GetById(int id);
    void Add(LibraryUser user);
    void Update(LibraryUser user);
    void Delete(int id);
    void AddPenalty(int userId, double amount);
}
