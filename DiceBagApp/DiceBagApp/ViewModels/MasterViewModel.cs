using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace DiceBagApp.ViewModels
{
    class MasterViewModel : BaseViewModel
    {
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public MasterViewModel(IDiceService diceService, IDiceDataBase diceDataBase)
        {
            //first step
            _diceService = diceService;
            _diceDataBase = diceDataBase;

            ListBag = new ObservableCollection<Bag>();
        }

        public async void RefreshListBag()
        {
            await Task.Run(() =>
            {
                var taskListBag = _diceDataBase.GetBagAsync();
                var listBag = taskListBag.Result;
                ListBag.Clear();
                foreach (var item in listBag)
                {
                    ListBag.Add(item);
                }
            });
        }
        
        #region Public Data
        public ObservableCollection<Bag> ListBag { get; set; }
        #endregion Public Data


        public async void SelectBagToRoom(Bag bag)
        {
           await RootPushAsync<RoomViewModel>(_diceService, _diceDataBase, bag);
        }
    }
}
