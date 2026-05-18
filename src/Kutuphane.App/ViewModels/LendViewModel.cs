using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kutuphane.Core.Constants;
using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Core.Validation;

namespace Kutuphane.App.ViewModels;

public partial class LendViewModel : ObservableObject
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;

    public ObservableCollection<LoanRecord> Loans { get; } = new();
    public ObservableCollection<Book> SearchResults { get; } = new();

    [ObservableProperty]
    private string _tc = string.Empty;

    [ObservableProperty]
    private string _searchKeyword = string.Empty;

    [ObservableProperty]
    private string _foundUserText = "TC ile kullanıcı arayın.";

    [ObservableProperty]
    private bool _userFound;

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private string? _statusMessage;

    [ObservableProperty]
    private bool _isError;

    private LibraryUser? _foundUser;

    public LendViewModel(
        IUserRepository userRepository,
        IBookRepository bookRepository,
        ILoanRepository loanRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
        LoadLoans();
        SearchBooks();
    }

    partial void OnSearchKeywordChanged(string value) => SearchBooks();

    [RelayCommand]
    private void SearchUser()
    {
        var tcError = UserValidator.ValidateTc(Tc);
        if (tcError != null)
        {
            FoundUserText = tcError;
            UserFound = false;
            _foundUser = null;
            return;
        }

        _foundUser = _userRepository.GetByTc(Tc.Trim());
        if (_foundUser != null)
        {
            FoundUserText = $"Bulunan: {_foundUser.TamAd}";
            UserFound = true;
        }
        else
        {
            FoundUserText = "Böyle bir kullanıcı bulunamadı.";
            UserFound = false;
        }
    }

    [RelayCommand]
    private void SearchBooks()
    {
        SearchResults.Clear();
        var results = string.IsNullOrWhiteSpace(SearchKeyword)
            ? _bookRepository.GetAll()
            : _bookRepository.SearchByTitle(SearchKeyword);
        foreach (var b in results)
            SearchResults.Add(b);
    }

    [RelayCommand]
    private void LendBook()
    {
        if (_foundUser == null)
        {
            SetStatus("Önce geçerli bir kullanıcı bulun.", true);
            return;
        }
        if (SelectedBook == null)
        {
            SetStatus("Ödünç verilecek kitabı seçin.", true);
            return;
        }

        try
        {
            _loanRepository.LendBook(_foundUser.Id, SelectedBook.Id, LibrarySettings.LoanDurationDays);
            var due = DateTime.Now.AddDays(LibrarySettings.LoanDurationDays).ToShortDateString();
            SetStatus($"Ödünç verildi. İade tarihi: {due}", false);
            LoadLoans();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void LoadLoans()
    {
        Loans.Clear();
        foreach (var l in _loanRepository.GetAllLoans())
            Loans.Add(l);
    }

    private void SetStatus(string message, bool isError)
    {
        StatusMessage = message;
        IsError = isError;
    }
}
