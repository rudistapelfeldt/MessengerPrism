using System;
using Prism.Navigation;
using Prism.Services;

namespace MessengerPrism.ViewModels
{
    public class GroupPageViewModel : ViewModelBase
    {
        public GroupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Groups";
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

        }
    }
}
