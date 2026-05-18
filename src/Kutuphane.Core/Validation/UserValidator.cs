namespace Kutuphane.Core.Validation;

public static class UserValidator
{
    public static string? ValidateTc(string? tc)
    {
        if (string.IsNullOrWhiteSpace(tc))
            return "TC kimlik numarası zorunludur.";
        if (tc.Length != 11 || !tc.All(char.IsDigit))
            return "TC kimlik numarası 11 haneli olmalıdır.";
        return null;
    }

    public static string? ValidateRequired(string? value, string fieldName)
    {
        return string.IsNullOrWhiteSpace(value) ? $"{fieldName} zorunludur." : null;
    }

    public static string? ValidateEmail(string? mail)
    {
        if (string.IsNullOrWhiteSpace(mail))
            return null;
        return mail.Contains('@') ? null : "Geçerli bir e-posta adresi girin.";
    }
}
