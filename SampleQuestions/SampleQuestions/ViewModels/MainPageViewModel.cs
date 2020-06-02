using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleQuestions.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleQuestions.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Commands
        public DelegateCommand OpenFormCommand { get; set; }
        #endregion

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            OpenFormCommand = new DelegateCommand(async () =>
            {
                string xaml = $"{DynamicPage.Header}" +
                              //$"{CreateWidgets(formSelected)}" +
                              $"{DynamicPage.Footer}";

                ContentPage page = new ContentPage().LoadFromXaml(xaml);

                StackLayout StackLayoutRoot = page.FindByName<StackLayout>("StackLayoutRoot");

                await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushAsync(page);
            });
        }
    }
}
