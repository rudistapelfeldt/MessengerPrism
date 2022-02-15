using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using MessengerPrism.Models;
using MessengerPrism.Strings;
using MessengerPrism.Sqlite;
using System.Threading.Tasks;
using MessengerPrism.Constants;
using Newtonsoft.Json;

namespace MessengerPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        #region Private members
        ItemDatabase _itemDatabase;
        Action<string> _handler;
        string _consoleString;
        #endregion

        #region Public members

        
        public string ConsoleString
        {
            get => _consoleString;
            set
            {
                SetProperty(ref _consoleString, value);
                SaveMessage(_consoleString);
            }
        }
        #endregion

        #region Constructors
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Messenger";
            SetDatabase();
            _handler = delegate (string i)
            {
                ConsoleString = i;
            };
            new MessageStream().Subscribe(_handler);

        }
        #endregion

        #region Commands
        public DelegateCommand NavigateToGroupCommand => new DelegateCommand(async() => await NavigateToGroup());

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }
        #endregion

        #region Methods
        public async Task SetDatabase()
        {
            _itemDatabase = await ItemDatabase.Instance;
        }


        private async Task NavigateToGroup()
        {
            var np = new NavigationParameters { { NavigationPerameterKeys.ItemDatabase, _itemDatabase} };
            await _navigationService.NavigateAsync(NavigationPaths.GroupPage, np);
        }

        private async Task SaveMessage(string message)
        {
            var str = JsonConvert.DeserializeObject<object>(message);

            if (str is Group)
            {
                var obj = str as Group;
                await _itemDatabase.SaveGroupAsync(obj);
            }

            if (str is Text)
            {
                var obj = str as Text;
                await _itemDatabase.SaveTextAsync(obj);
            }

            if (str is Image)
            {
                var obj = str as Image;
                await _itemDatabase.SaveImageAsync(obj);
            }
        }
        #endregion

        #region Navigation
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            IsBusy = true;

            //_itemDatabase = await ItemDatabase.Instance;
            


            IsBusy = false;
        }
        #endregion
    }
}
