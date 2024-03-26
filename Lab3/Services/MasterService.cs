using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;
using Microsoft.Win32;
using System.Diagnostics.Metrics;

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

        public void Create(RegisterMaster masterView, IFormFile upload)
        {
            User user = new User()
            {
                UserName = masterView.UserName,
                Password = masterView.Password,
                Role = "master",
            };
            Master master = new Master()
            {
                User = user,
                MasterID = masterView.MasterID,
                FirstName = masterView.FirstName,
                LastName = masterView.LastName,
                Gender = masterView.Gender,
                Phone = masterView.Phone,
                Age = masterView.Age,
                AgeInCategory = masterView.AgeInCategory,
                Level = masterView.Level,
                CategoryID = masterView.Category,
                Photo = masterView.Photo,
            };
            if (upload != null && upload.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    upload.CopyToAsync(memoryStream);
                    master.Photo = memoryStream.ToArray();
                }
            }
            _masterRepository.CreateAsync(master);
        }

        public void Delete(int id)
        {
            _masterRepository.DeleteAsync(id);
        }

        public List<MasterView> GetAll()
        {
            List<MasterView> masterViews = new List<MasterView>();
            List<Master> masters = _masterRepository.GetAll();
            foreach (var master in masters)
            {
                masterViews.Add(GetById(master.MasterID));
            }
            return masterViews;
        }
        public List<MasterView> Filter(int AgeInCategory, string Level, int Category)
        {
            List<MasterView> masterViews = new List<MasterView>();
            List<Master> masters;
            if (Category != -1)
            {
                masters = _masterRepository.GetAllByCategoryId(Category);
            }
            else
                masters = _masterRepository.GetAll();
            foreach (var master in masters)
            {
                masterViews.Add(GetById(master.MasterID));
            }
            if(Level != null)
            {
                masterViews = masterViews.Where(m => m.Level == Level).ToList();
            }
            if (AgeInCategory != 0)
            {
                masterViews = masterViews.Where(m => m.AgeInCategory == AgeInCategory).ToList();
            }
            return masterViews;
        }

        public MasterView GetById(int id)
        {
            Master master = _masterRepository.GetById(id);
            Category category = _categoryRepository.GetById(master.CategoryID);
            User user = _userRepository.GetById(master.UserID);
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
                CategoryID= master.CategoryID,
                CategoryName = category.Name,
                UserID= master.UserID,
                UserName = user.UserName,
                Photo = master.Photo
            };
            return masterView;
        }

        public MasterView GetUserById(int id)
        {
            Master master = _masterRepository.GetByUserId(id);
            Category category = _categoryRepository.GetById(master.CategoryID);
            User user = _userRepository.GetById(master.UserID);
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
                CategoryID = master.CategoryID,
                CategoryName = category.Name,
                UserID = master.UserID,
                UserName = user.UserName,
                Photo = master.Photo
            };
            return masterView;
        }

        public void Update(Master master, IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    upload.CopyToAsync(memoryStream);
                    master.Photo = memoryStream.ToArray();
                }
            }
            _masterRepository.UpdateAsync(master);
        }
    }
}
