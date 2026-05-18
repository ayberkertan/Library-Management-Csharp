using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Core.Validation;

namespace Kutuphane.App.ViewModels;

public partial class BooksViewModel : ObservableObject
{
    private readonly IBookRepository _bookRepository;

    public ObservableCollection<Book> Books { get; } = new();

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private string _ad = string.Empty;

    [ObservableProperty]
    private string _yazar = string.Empty;

    [ObservableProperty]
    private string _yayinci = string.Empty;

    [ObservableProperty]
    private int _sayfaSayisi = 100;

    [ObservableProperty]
    private DateTime _basimTarihi = DateTime.Today;

    [ObservableProperty]
    private string? _statusMessage;

    [ObservableProperty]
    private bool _isError;

    public BooksViewModel(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        LoadBooks();
    }

    partial void OnSelectedBookChanged(Book? value)
    {
        if (value == null) return;
        Ad = value.Ad;
        Yazar = value.Yazar;
        Yayinci = value.Yayinci;
        SayfaSayisi = value.SayfaSayisi;
        BasimTarihi = value.BasimTarihi;
    }

    [RelayCommand]
    private void LoadBooks()
    {
        Books.Clear();
        foreach (var b in _bookRepository.GetAll())
            Books.Add(b);
    }

    [RelayCommand]
    private void AddBook()
    {
        var error = UserValidator.ValidateRequired(Ad, "Kitap adı");
        if (error != null) { SetStatus(error, true); return; }

        try
        {
            _bookRepository.Add(BuildBook(0));
            SetStatus("Kitap eklendi.", false);
            ClearForm();
            LoadBooks();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void UpdateBook()
    {
        if (SelectedBook == null) { SetStatus("Güncellenecek kitap seçin.", true); return; }
        try
        {
            _bookRepository.Update(BuildBook(SelectedBook.Id));
            SetStatus("Kitap güncellendi.", false);
            LoadBooks();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void DeleteBook()
    {
        if (SelectedBook == null) { SetStatus("Silinecek kitap seçin.", true); return; }
        if (_bookRepository.IsOnLoan(SelectedBook.Id))
        {
            SetStatus("Ödünçte olan kitap silinemez.", true);
            return;
        }
        try
        {
            _bookRepository.Delete(SelectedBook.Id);
            SetStatus("Kitap silindi.", false);
            ClearForm();
            LoadBooks();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void ClearFormCommand() => ClearForm();

    private Book BuildBook(int id) => new()
    {
        Id = id,
        Ad = Ad.Trim(),
        Yazar = Yazar.Trim(),
        Yayinci = Yayinci.Trim(),
        SayfaSayisi = SayfaSayisi,
        BasimTarihi = BasimTarihi
    };

    private void ClearForm()
    {
        SelectedBook = null;
        Ad = Yazar = Yayinci = string.Empty;
        SayfaSayisi = 100;
        BasimTarihi = DateTime.Today;
    }

    private void SetStatus(string message, bool isError)
    {
        StatusMessage = message;
        IsError = isError;
    }
}
