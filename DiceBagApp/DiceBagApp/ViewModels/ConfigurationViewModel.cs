using DiceBagApp.Datas;
using DiceBagApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{

    class ConfigurationViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public ConfigurationViewModel(IDiceService diceService, IDiceDataBase diceDataBase)
        {
            //first step
            _diceService = diceService;
            _diceDataBase = diceDataBase;
            ResetBagCommand = new Command(ExecuteResetBagCommand);
        }

        #region Commands
        public Command ResetBagCommand { get; }

        public async void ExecuteResetBagCommand()
        {
            await Task.Run(() =>
            {
                _diceDataBase.ResetDataBase();
                var Bag = _diceService.GetDefaultBag();
                _diceDataBase.SaveBag(Bag);
                Task.WaitAll();
                base.RefreshMasterPage();
            }
            );
        }
        #endregion Commands
    }
}
