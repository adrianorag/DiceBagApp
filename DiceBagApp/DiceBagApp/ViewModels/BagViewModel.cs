using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class BagViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public BagViewModel(IDiceService diceService, IDiceDataBase diceDataBase, Bag bag)
        {
            //first step
            _diceService = diceService;
            _diceDataBase = diceDataBase;
            Bag = bag;

            ListGroupDice = new ObservableCollection<GroupDice>();

            //Commands 
            GroupDicePageCommand = new Command(ExecuteGroupDicePageCommand);

            //Set proprety
            CreateBagState();
            LoadDataToObject();

        }
        
        private void CreateBagState()
        {
            if (Bag == null || Bag.ID == 0)
            {
                var taskBag = _diceDataBase.GetBagAsync(false);
                taskBag.Wait();

                var list = taskBag.Result;
                if (list != null && list.Count > 0)
                {
                    Bag = list.FirstOrDefault();
                }
                else
                {
                    Bag = new Bag();
                    Bag.Active = false;
                    SaveBag();
                }
                LoadDataToObject();
            }
        }


        private void LoadDataToObject()
        {
            //Public Data
            Name = Bag.Name;
        }
        

        #region Public Data
        public ObservableCollection<GroupDice> ListGroupDice { get; set; }
        public Bag Bag {get; set;}

        private string _name;
        public string  Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                if(value != Bag.Name)
                    SaveBag();
            }
        }

        #endregion Public Data


        public void SaveBag()
        {
            if (!string.IsNullOrEmpty(Bag.Name))
                Bag.Active = true;

            Task.Run(() => {
                //Simple information
                Bag.Name = Name;

                var groupDice = new List<GroupDice>();
                foreach (var item in ListGroupDice)
                {
                    groupDice.Add(item);
                }
                Bag.GroupDices = groupDice;

                _diceDataBase.SaveBag(Bag);

                RefreshMasterPage();
            });
        }


        public Task<List<GroupDice>> RefreshList()
        {
            return Task.Run(() => { return _diceDataBase.GetGroupDice(Bag.ID); });
        }

        #region Command

        public Command GroupDicePageCommand { get; }
        async void ExecuteGroupDicePageCommand()
        {
            await PushModalAsync<GroupDiceViewModel>(_diceService, _diceDataBase, Bag);
        }
        
        public async Task DeleteGroupDiceAsync(GroupDice groupDice)
        {
            await _diceDataBase.DeleteItemAsync(groupDice);
            await _diceDataBase.DeleteGroupDiceAsync(groupDice);
        }
        #endregion Command

    }
}
