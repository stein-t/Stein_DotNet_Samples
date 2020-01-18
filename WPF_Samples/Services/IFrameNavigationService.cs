using GalaSoft.MvvmLight.Views;

namespace WPF_Samples.Services
{
    /// <summary>
    /// IFrameNavigationService Interface
    /// see https://github.com/SamTheDev/SampleMvvmLightNavigation
    /// see https://stackoverflow.com/questions/28966819/mvvm-light-5-0-how-to-use-the-navigation-service
    /// </summary>
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
