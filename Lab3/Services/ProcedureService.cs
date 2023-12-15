using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public class ProcedureService : IProcedureService
    {
        IProcedureRepository _procedureRepository;
        ICategoryRepository _categoryRepository;
        public ProcedureService(ICategoryRepository categoryRepository, IProcedureRepository procedureRepository)
        {
            _categoryRepository = categoryRepository;
            _procedureRepository = procedureRepository;
        }
        public void Create(ProcedureView procedureView)
        {
            Category category = _categoryRepository.GetByName(procedureView.CategoryName);
            Procedure procedure = new Procedure()
            {
                ProcedureID = procedureView.ProcedureID,
                Name = procedureView.Name,
                Description = procedureView.Description,
                Price = procedureView.Price,
                CategoryId = category.CategoryID
            };
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
                procedureViews.Add(GetById(procedure.ProcedureID)); 
            });
            return procedureViews;
        }

        public ProcedureView GetById(int id)
        {
            Procedure procedure = _procedureRepository.GetById(id);
            Category category = _categoryRepository.GetById(procedure.CategoryId);
            ProcedureView procedureView = new ProcedureView()
            {
                ProcedureID = procedure.ProcedureID,
                Name = procedure.Name,
                Description = procedure.Description,
                Price = procedure.Price,
                CategoryName = category.Name
            };
            return procedureView;
        }

        public void Update(ProcedureView procedureView)
        {
            Category category = _categoryRepository.GetByName(procedureView.CategoryName);
            Procedure procedure = new Procedure()
            {
                ProcedureID = procedureView.ProcedureID,
                Name = procedureView.Name,
                Description = procedureView.Description,
                Price = procedureView.Price,
                CategoryId = category.CategoryID
            };
            _procedureRepository.UpdateAsync(procedure);
        }
    }
}
