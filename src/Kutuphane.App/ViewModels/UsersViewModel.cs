using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Core.Validation;

namespace Kutuphane.App.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private readonly IUserRepository _userRepository;

    public ObservableCollection<LibraryUser> Users { get; } = new();

    [ObservableProperty]
    private LibraryUser? _selectedUser;

    [ObservableProperty]
    private string _ad = string.Empty;

    [ObservableProperty]
    private string _soyad = string.Empty;

    [ObservableProperty]
    private string _tc = string.Empty;

    [ObservableProperty]
    private string _tel = string.Empty;

    [ObservableProperty]
    private string _mail = string.Empty;

    [ObservableProperty]
    private string _cinsiyet = "E";

    [ObservableProperty]
    private string _ceza = "0";

    [ObservableProperty]
    private string? _statusMessage;

    [ObservableProperty]
    private bool _isError;

    public UsersViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        LoadUsers();
    }

    partial void OnSelectedUserChanged(LibraryUser? value)
    {
        if (value == null) return;
        Ad = value.Ad;
        Soyad = value.Soyad;
        Tc = value.Tc;
        Tel = value.Tel;
        Mail = value.Mail;
        Cinsiyet = value.Cinsiyet;
        Ceza = value.Ceza.ToString("N2");
    }

    [RelayCommand]
    private void LoadUsers()
    {
        Users.Clear();
        foreach (var u in _userRepository.GetAll())
            Users.Add(u);
    }

    [RelayCommand]
    private void AddUser()
    {
        var error = ValidateForm();
        if (error != null) { SetStatus(error, true); return; }

        try
        {
            _userRepository.Add(BuildUser(0));
            SetStatus("Kullanıcı başarıyla eklendi.", false);
            ClearForm();
            LoadUsers();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void UpdateUser()
    {
        if (SelectedUser == null) { SetStatus("Güncellenecek kullanıcı seçin.", true); return; }
        var error = ValidateForm();
        if (error != null) { SetStatus(error, true); return; }

        try
        {
            var user = BuildUser(SelectedUser.Id);
            _userRepository.Update(user);
            SetStatus("Kullanıcı güncellendi.", false);
            LoadUsers();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void DeleteUser()
    {
        if (SelectedUser == null) { SetStatus("Silinecek kullanıcı seçin.", true); return; }
        try
        {
            _userRepository.Delete(SelectedUser.Id);
            SetStatus("Kullanıcı silindi.", false);
            ClearForm();
            LoadUsers();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, true);
        }
    }

    [RelayCommand]
    private void ClearFormCommand() => ClearForm();

    private string? ValidateForm()
    {
        return UserValidator.ValidateRequired(Ad, "Ad")
               ?? UserValidator.ValidateRequired(Soyad, "Soyad")
               ?? UserValidator.ValidateTc(Tc)
               ?? UserValidator.ValidateEmail(Mail);
    }

    private LibraryUser BuildUser(int id) => new()
    {
        Id = id,
        Ad = Ad.Trim(),
        Soyad = Soyad.Trim(),
        Tc = Tc.Trim(),
        Tel = Tel.Trim(),
        Mail = Mail.Trim(),
        Cinsiyet = Cinsiyet,
        Ceza = double.TryParse(Ceza, out var c) ? c : 0
    };

    private void ClearForm()
    {
        SelectedUser = null;
        Ad = Soyad = Tc = Tel = Mail = string.Empty;
        Cinsiyet = "E";
        Ceza = "0";
    }

    private void SetStatus(string message, bool isError)
    {
        StatusMessage = message;
        IsError = isError;
    }
}
