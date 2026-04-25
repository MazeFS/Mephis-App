using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mephis.App.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    // Теперь аватарка это переменная, чтобы мы могли ее менять
    [ObservableProperty]
    private string avatarImage = "dotnet_bot.png";

    [ObservableProperty] private string name = "Mephis Creator";
    [ObservableProperty] private string nickname = "@mephis_lead";
    [ObservableProperty] private string status = "Создаю мессенджер";
    [ObservableProperty] private string bio = "Senior mobile & lerp de.";
    [ObservableProperty] private string currentMusic = "Урал Гайсин - say no mo";

    // Переменная, которая управляет видимостью полноэкранного режима
    [ObservableProperty]
    private bool isAvatarExpanded;

    [RelayCommand]
    private void OpenAvatar() => IsAvatarExpanded = true;

    [RelayCommand]
    private void CloseAvatar() => IsAvatarExpanded = false;

    // Магия MAUI: вызов системной галереи телефона
    [RelayCommand]
    private async Task ChangeAvatar()
    {
        var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Выберите фото профиля"
        });

        if (result != null)
        {
            // Берем путь к выбранной картинке и обновляем интерфейс
            AvatarImage = result.FullPath;
        }
    }

    [RelayCommand]
    private void PlayMusic()
    {
        App.Current.MainPage.DisplayAlert("Плеер", $"Включаю: {currentMusic}", "ОК");
    }
}