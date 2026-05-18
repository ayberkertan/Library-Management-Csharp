using Kutuphane.Core.Models;

namespace Kutuphane.Core.Interfaces;

public interface ILoanRepository
{
    IReadOnlyList<LoanRecord> GetAllLoans();
    IReadOnlyList<LoanRecord> GetActiveLoans();
    void LendBook(int userId, int bookId, int loanDays);
    ReturnResult ReturnBook(int loanId);
}

public class ReturnResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public int LateDays { get; init; }
    public double PenaltyApplied { get; init; }
}
