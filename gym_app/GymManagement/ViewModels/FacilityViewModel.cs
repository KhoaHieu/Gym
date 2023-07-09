using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace GymManagement.ViewModels
{
   public  class FacilityViewModel : BaseViewModel
    {

        #region DataFields
        private GymManagementDbContext DbContext { get; set; }
        private ObservableCollection<Facility> _facilityContext { get; set; }

        public ObservableCollection<Facility> FacilityContext
        {
            get => _facilityContext;
            set { _facilityContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Facility> _facilityContextClone { get; set; }

        public ObservableCollection<Facility> FacilityContextClone
        {
            get => _facilityContextClone;
            set { _facilityContextClone = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TypesOfFacility> _typeList { get; set; }

        public ObservableCollection<TypesOfFacility> TypeList
        {
            get => _typeList;
            set { _typeList = value; OnPropertyChanged(); }
        }
        
        private Facility _selectedFacility { get; set; }
        public Facility SelectedFacility
        {
            get => _selectedFacility;
            set
            {
                _selectedFacility = value;
                base.SelectedItem = _selectedFacility;
                OnPropertyChanged();
            }
        }
        private int _selectedType { get; set; }
        public int SelectedType 
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                Debug.WriteLine(value);
                OnPropertyChanged();
            }
        }

        #endregion

        protected async Task ShowErrorDialog()
        {
            var view = new SampleErrorDialog();
            await DialogHost.Show(view, "RootDialog");
        }
        private async void ExtendedClosingEventHandlerForAddNewFacility(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddNewFacilityDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewFacilityViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewFacilityAsync();
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    //OK, lets cancel the close...
                    eventArgs.Cancel();


                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _facilityContext = viewmodel.Sync();
                    }


                    //lets run a fake operation for 3 seconds then close this baby.
                    //Task.Delay(TimeSpan.FromSeconds(3))
                    //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    //        TaskScheduler.FromCurrentSynchronizationContext());

                }

            }
        }
        public override async Task DeleteData(object model)
        {
            using (DbContext = new GymManagementDbContext())
            {
                DbContext.Remove<Facility>(model as Facility);
                DbContext.SaveChanges();
                _facilityContext.Remove(model as Facility);
            }
        }

        public override async Task AddNewData()
        {
            using (DbContext = new GymManagementDbContext())
            {
                List<string> TypeList = new List<string>();
                foreach (TypesOfFacility type in DbContext.TypesOfFacilities.ToList())
                {
                    if (type != null)
                    {
                        TypeList.Add(type.Name);
                    }
                }
                var view = new AddNewFacilityDialog
                {
                    DataContext = new AddNewFacilityViewModel(_facilityContext, TypeList)
                };
                var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandlerForAddNewFacility, null);
            }
        }

        
        public override async Task UpdateData()
        {
            try
            {
                using (DbContext = new GymManagementDbContext())
                {


                    Facility replace = SelectedFacility;

                    Facility current = DbContext.Facilities.Where(t => t.FacilityId.Equals(SelectedFacility.FacilityId)).FirstOrDefault();
                    TypesOfFacility oldtype = DbContext.TypesOfFacilities.Where(t => t.TypeId == current.TypeId).FirstOrDefault();
                    current.Type = oldtype;

                    if (current != null)
                    {
                        if (!(current.Name == replace.Name &&
                            current.PricePerUnit == replace.PricePerUnit &&
                            current.Quantity == replace.Quantity &&
                            current.TypeId == replace.TypeId &&
                            current.Type == replace.Type &&
                            current.Description == replace.Description))
                        {
                            
                            current.Name = replace.Name;
                            current.PricePerUnit = replace.PricePerUnit;
                            
                            current.Quantity = replace.Quantity;
                            current.Description = replace.Description;
                            if (current.Type.Name != replace.Type.Name)
                            {
                                TypesOfFacility newtype = DbContext.TypesOfFacilities.Where(t => t.TypeId == replace.Type.TypeId).FirstOrDefault();
                                current.TypeId = newtype.TypeId;
                                current.Type = replace.Type;
                            }
                            else
                            {
                                current.TypeId = replace.TypeId;
                            }
                            DbContext.SaveChanges();


                            for (int i = 0; i < _facilityContext.Count; i++)
                            {
                                if (_facilityContext[i].FacilityId == replace.FacilityId)
                                    _facilityContext[i] = replace;
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        }

        public ICommand GridChangeCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand AddFacilityTypeCommand { get; set; }
        public FacilityViewModel()
        {
            GridChangeCommand = new GridChangeCommand(this);
            DeleteDataCommand= new DeleteDataCommand(this);
            AddFacilityTypeCommand = new AddFacilityTypeCommand(this);
        }
        
    }
}
