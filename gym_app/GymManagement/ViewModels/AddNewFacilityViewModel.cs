using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewFacilityViewModel : BaseViewModel
    {
        private bool _flag = true;
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }
        private GymManagementDbContext Context { get; set; }
        private List<string> _typeList { get; set; }

        public List<string> TypeList
        {
            get => _typeList;
            set { _typeList = value; OnPropertyChanged(); }
        }
        private string _selectedType { get; set; }
        public string SelectedType
        {
            get => _selectedType;
            set { _selectedType = value; OnPropertyChanged(); }
        }
        private string _facilityName { get; set; }
        public string FacilityName
        {
            get => _facilityName;
            set { _facilityName = value; OnPropertyChanged(); }
        }
        private string _quantity { get; set; }
        public string Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }
        private string _pricePerUnit { get; set; }
        public string PricePerUnit
        {
            get => _pricePerUnit;
            set { _pricePerUnit = value; OnPropertyChanged(); }
        }
        private string _description { get; set; }
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public void AddNewFacility()
        {
            _ = AddNewFacilityAsync();
        }

        public Task AddNewFacilityAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymManagementDbContext())
                {
                    var type = Context.TypesOfFacilities.Where(s => s.Name == SelectedType).FirstOrDefault();


                    if (type != null)
                    {
                        // This will validate whether a customer have exists ?



                        //// Add new customer
                        Facility facility = new()

                        {
                            Name = FacilityName,
                            Description = Description,
                            TypeId = type.TypeId
                        };

                        if (Quantity != null)
                        {
                            facility.Quantity = Int32.Parse(Quantity);
                        }
                        if (PricePerUnit != null)
                        {
                            facility.PricePerUnit = Int32.Parse(PricePerUnit);
                        }

                        Context.Add<Facility>(facility);
                        Context.SaveChanges();
                        FacilityContext.Add(facility);

                    }
                    else
                    {
                        Flag = false;
                    }
                }
            });
        }

        private Task LoadDataAsync(List<string> typeList)
        {
            return Task.Factory.StartNew(() =>
            {
                _typeList = typeList;
            });
        }

        public async Task LoadData(List<string> typeList)
        {
            _ = LoadDataAsync(typeList);
        }

        public ObservableCollection<Facility> Sync()
        {
            return FacilityContext;
        }

        private ObservableCollection<Facility> FacilityContext;
        public AddNewFacilityViewModel(ObservableCollection<Facility> _facilityContext, List<string> typeList)
        {
            FacilityContext = _facilityContext;
            LoadData(typeList);
        }
    }
}

