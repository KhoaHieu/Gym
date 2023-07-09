using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewFacilityTypeViewModel : BaseViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }

        }
        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        private GymManagementDbContext _dbcontext;
        private ObservableCollection<TypesOfFacility> TypeOfFacilityContext;
        public void AddNewTypeOfFacility()
        {
            using (_dbcontext = new GymManagementDbContext())
            {
                if (Name != null)
                {
                    TypesOfFacility type = new TypesOfFacility();
                    type.Name = Name;
                    _dbcontext.TypesOfFacilities.Add(type);
                    TypeOfFacilityContext.Add(type);
                    _dbcontext.SaveChanges();
                }
            }
        }
        public AddNewFacilityTypeViewModel(FacilityViewModel viewmodel)
        {
            TypeOfFacilityContext = viewmodel.TypeList;
        }
    
    }
}
