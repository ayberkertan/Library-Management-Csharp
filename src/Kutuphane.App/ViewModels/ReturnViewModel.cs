using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;

namespace Kutuphane.App.ViewModels;

public partial class ReturnViewModel : ObservableObject
{
    private readonly ILoanRepository _loanRepository;

    public ObservableCollection<LoanRecord> ActiveLoans { get; } = new();

    [ObservableProperty]
    private LoanRecord? _selectedLoan;

    [ObservableProperty]
    private string? _statusMessage;

    [ObservableProperty]
    private bool _isError;

    public ReturnViewModel(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
        LoadLoans();
    }

    [RelayCommand]
    private void LoadLoans()
    {
        ActiveLoans.Clear();
        foreach (var l in _loanRepository.GetActiveLoans())
            ActiveLoans.Add(l);
    }

    [RelayCommand]
    private void ReturnBook()
    {
        if (SelectedLoan == null)
        {
            SetStatus("İade edilecek kaydı seçin.", true);
            return;
        }

        var result = _loanRepository.ReturnBook(SelectedLoan.KayitId);
        SetStatus(result.Message, !result.Success);
        if (result.Success)
        {
            SelectedLoan = null;
            LoadLoans();
        }
    }

    private void SetStatus(string message, bool isError)
    {
        StatusMessage = message;
        IsError = isError;
    }
}
