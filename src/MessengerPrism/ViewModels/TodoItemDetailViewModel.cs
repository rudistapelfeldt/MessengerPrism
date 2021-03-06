using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using MessengerPrism.Models;
using MessengerPrism.Strings;

namespace MessengerPrism.ViewModels
{
    public class TodoItemDetailViewModel : ViewModelBase
    {
        public TodoItemDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
                                       IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = Resources.TodoItemDetailTitle;
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
        }

        public TodoItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

        private bool _isNew;

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
            Model = parameters.GetValue<TodoItem>("todoItem");
        }

        private async void OnSaveCommandExecuted()
        {
            if(_isNew)
            {
                await _navigationService.GoBackAsync(new NavigationParameters{ { "todoItem", Model } });
            }
            else
            {
                await _navigationService.GoBackAsync();
            }
        }
    }
}