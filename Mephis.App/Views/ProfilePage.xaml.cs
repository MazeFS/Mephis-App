using Mephis.App.ViewModels;

namespace Mephis.App.Views;

public partial class ProfilePage : ContentPage
{
    // Добавили бэкап для музыки
    private string _backupName, _backupNickname, _backupStatus, _backupBio, _backupMusic;

    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new ProfileViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AnimateBackground();
    }

    private async void AnimateBackground()
    {
        var random = new Random();
        while (true)
        {
            // Плавное дыхание фона
            var t1 = Blob1.TranslateTo(random.Next(-100, 100), random.Next(-100, 100), 10000, Easing.SinInOut);
            var t2 = Blob2.TranslateTo(random.Next(-150, 150), random.Next(-100, 200), 12000, Easing.SinInOut);
            var t3 = Blob3.TranslateTo(random.Next(-100, 100), random.Next(-150, 100), 14000, Easing.SinInOut);

            var s1 = Blob1.ScaleTo(random.NextDouble() * 0.5 + 1.0, 10000, Easing.SinInOut);
            var s2 = Blob2.ScaleTo(random.NextDouble() * 0.4 + 1.0, 12000, Easing.SinInOut);
            var s3 = Blob3.ScaleTo(random.NextDouble() * 0.5 + 1.0, 14000, Easing.SinInOut);

            await Task.WhenAll(t1, t2, t3, s1, s2, s3);
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var vm = (ProfileViewModel)BindingContext;

        _backupName = vm.Name;
        _backupNickname = vm.Nickname;
        _backupStatus = vm.Status;
        _backupBio = vm.Bio;
        _backupMusic = vm.CurrentMusic; // Бэкап музыки

        // ПЛАВНЫЙ ПЕРЕВОРОТ (увеличили время до 400мс)
        await ProfileCard.RotateYTo(90, 400, Easing.CubicIn);
        FrontView.IsVisible = false;
        BackView.IsVisible = true;
        ProfileCard.RotationY = -90;
        await ProfileCard.RotateYTo(0, 400, Easing.CubicOut);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        await FlipCardBack();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        var vm = (ProfileViewModel)BindingContext;

        vm.Name = _backupName;
        vm.Nickname = _backupNickname;
        vm.Status = _backupStatus;
        vm.Bio = _backupBio;
        vm.CurrentMusic = _backupMusic; // Откат музыки

        await FlipCardBack();
    }

    private async Task FlipCardBack()
    {
        await ProfileCard.RotateYTo(-90, 400, Easing.CubicIn);
        BackView.IsVisible = false;
        FrontView.IsVisible = true;
        ProfileCard.RotationY = 90;
        await ProfileCard.RotateYTo(0, 400, Easing.CubicOut);
    }
}