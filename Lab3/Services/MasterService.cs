using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;
using Microsoft.Win32;

namespace Lab3.Services
{
    public class MasterService : IMasterService
    {
        IMasterRepository _masterRepository;

        ICategoryRepository _categoryRepository;
        IUserRepository _userRepository;

        public MasterService(IMasterRepository masterRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _masterRepository = masterRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public void Create(RegisterMaster masterView)
        {
            int userID = _userRepository.GetAll().Last().UserID + 1;
            Category category = _categoryRepository.GetByName(masterView.CategoryName);
            User user = new User()
            {
                UserID = userID,
                UserName = masterView.UserName,
                Password = masterView.Password,
                Role = "master",
            };
            Master master = new Master()
            {
                UserID = userID,
                MasterID = masterView.MasterID,
                FirstName = masterView.FirstName,
                LastName = masterView.LastName,
                Gender = masterView.Gender,
                Phone = masterView.Phone,
                Age = masterView.Age,
                AgeInCategory = masterView.AgeInCategory,
                Level = masterView.Level,
                CategoryId = category.CategoryID
            };
            _masterRepository.CreateAsync(master);
            _userRepository.CreateAsync(user);
        }

        public void Delete(int id)
        {
            Master master = _masterRepository.GetById(id);
            _userRepository.DeleteAsync(master.UserID);
            _masterRepository.DeleteAsync(id);
        }

        public List<MasterView> GetAll()
        {
            List<MasterView> masterViews = new List<MasterView>();
            List<Master> masters = _masterRepository.GetAll();
            masters.ForEach(master => {
                masterViews.Add(GetById(master.MasterID));
            });
            return masterViews;
        }

        public MasterView GetById(int id)
        {
            Master master = _masterRepository.GetById(id);
            Category category = _categoryRepository.GetById(master.CategoryId);
            MasterView masterView = new MasterView()
            {
                MasterID= master.MasterID,
                FirstName = master.FirstName,
                LastName = master.LastName,
                Age = master.Age,
                Gender = master.Gender,
                Phone= master.Phone,
                AgeInCategory= master.AgeInCategory,
                Level = master.Level,
                CategoryName = category.Name
            };
            return masterView;
        }

        public MasterView GetUserById(int id)
        {
            Master master = _masterRepository.GetUserById(id);
            Category category = _categoryRepository.GetById(master.CategoryId);
            MasterView masterView = new MasterView()
            {
                MasterID = master.MasterID,
                FirstName = master.FirstName,
                LastName = master.LastName,
                Age = master.Age,
                Gender = master.Gender,
                Phone = master.Phone,
                AgeInCategory = master.AgeInCategory,
                Level = master.Level,
                CategoryName = category.Name
            };
            return masterView;
        }

        public void Update(MasterView masterView)
        {
            Category category = _categoryRepository.GetByName(masterView.CategoryName);
            Master master = new Master()
            {
                MasterID = masterView.MasterID,
                UserID = masterView.UserID,
                FirstName = masterView.FirstName,
                LastName = masterView.LastName,
                Age = masterView.Age,
                Gender = masterView.Gender,
                Phone = masterView.Phone,
                AgeInCategory= masterView.AgeInCategory,
                Level = masterView.Level,
                CategoryId = category.CategoryID
            };
            _masterRepository.UpdateAsync(master);
        }
    }
}
