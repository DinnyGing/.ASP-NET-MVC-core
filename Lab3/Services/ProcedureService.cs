using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public class ProcedureService : IProcedureService
    {
        IProcedureRepository _procedureRepository;
        ICategoryRepository _categoryRepository;
        IMasterRepository _masterRepository;
        IProcedureTypeRepository _procedureTypeRepository; 
        public ProcedureService(ICategoryRepository categoryRepository, IProcedureRepository procedureRepository, IMasterRepository masterRepository, IProcedureTypeRepository procedureTypeRepository)
        {
            _categoryRepository = categoryRepository;
            _procedureRepository = procedureRepository;
            _masterRepository = masterRepository;
            _procedureTypeRepository = procedureTypeRepository;
        }
        public void Create(Procedure procedure)
        {
            _procedureRepository.CreateAsync(procedure);
        }

        public void Delete(int id)
        {
            _procedureRepository.DeleteAsync(id);
        }

        public List<ProcedureView> GetAll()
        {
            List<ProcedureView> procedureViews = new List<ProcedureView>();
            List<Procedure> procedures = _procedureRepository.GetAll();
            procedures.ForEach(procedure => {
                Master master = _masterRepository.GetById(procedure.MasterID);
                Category category = _categoryRepository.GetById(master.CategoryID);
                ProcedureType procedureType = _procedureTypeRepository.GetById(procedure.ProcedureTypeID);
                ProcedureView procedureView = new ProcedureView()
                {
                    ProcedureID = procedure.ProcedureID,
                    Name = procedureType.Name,
                    Description = procedureType.Description,
                    Duration = procedure.Duration,
                    Price = procedure.Price,
                    Rating = procedure.Rating,
                    CategoryName = category.Name,
                    MasterFirstName = master.FirstName,
                    MasterLastName = master.LastName,
                };
                procedureViews.Add(procedureView); 
            });
            return procedureViews;
        }
        public List<ProcedureView> GetAllByProcedureTypeId(int procedureTypeId)
        {
            List<ProcedureView> procedureViews = new List<ProcedureView>();
            List<Procedure> procedures = _procedureRepository.GetAllByProcedureTypeId(procedureTypeId);
            procedures.ForEach(procedure => {
                Master master = _masterRepository.GetById(procedure.MasterID);
                Category category = _categoryRepository.GetById(master.CategoryID);
                ProcedureType procedureType = _procedureTypeRepository.GetById(procedure.ProcedureTypeID);
                ProcedureView procedureView = new ProcedureView()
                {
                    ProcedureID = procedure.ProcedureID,
                    Name = procedureType.Name,
                    Description = procedureType.Description,
                    Duration = procedure.Duration,
                    Price = procedure.Price,
                    Rating = procedure.Rating,
                    CategoryName = category.Name,
                    MasterFirstName = master.FirstName,
                    MasterLastName = master.LastName,
                };
                procedureViews.Add(procedureView);
            });
            return procedureViews;
        }
        public List<ProcedureView> Filter(int MinPrice, int MaxPrice, int Type)
        {
            List<ProcedureView> procedures;
            if (Type == -1)
            {
                procedures = GetAll();
            }
            else
            {
                procedures = GetAllByProcedureTypeId(Type);
            }
            if (MaxPrice == 0)
            {
                procedures = procedures.Where(p => p.Price >= MinPrice).ToList();
            }
            else
            {
                procedures = procedures.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            }
            return procedures;
        }

        public Procedure GetById(int id)
        {
            return _procedureRepository.GetById(id);
        }

        public void Update(Procedure procedure)
        {
            _procedureRepository.UpdateAsync(procedure);
        }

        public void UpdateRating(int ProcedureID, double Rating)
        {
            Procedure procedure = _procedureRepository.GetById(ProcedureID);
            double rating = Rating + procedure.Rating * procedure.Votes;
            procedure.Votes++;
            procedure.Rating = rating / procedure.Votes;
            _procedureRepository.UpdateAsync(procedure);
        }
    }
}
