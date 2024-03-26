using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public class ClientService : IClientService
    {
        IClientRepository _clientRepository;
        IUserRepository _userRepository;
        public ClientService(IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }
        public void Create(Client client)
        {
            _clientRepository.CreateAsync(client);
        }

        public void Delete(int id)
        {
            _clientRepository.DeleteAsync(id);
        }

        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }
        public List<Client> Filter(string FirstName, string LastName, string Phone)
        {
            List<Client> clients = _clientRepository.GetAll();
            if(FirstName != null)
                clients = clients.Where(c => c.FirstName.Equals(FirstName)).ToList();
            if (LastName != null)
                clients = clients.Where(c => c.LastName.Equals(LastName)).ToList();
            if (Phone != null)
                clients = clients.Where(c => c.Phone.Equals(Phone)).ToList();
            return clients;
        }

        public Client GetById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public ClientView GetByUserId(int id)
        {
            Client client = _clientRepository.GetByUserId(id);
            User user = _userRepository.GetById(id);
            ClientView clientView = new ClientView()
            {
                ClientID = client.ClientID,
                UserID = client.UserID,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                Gender = client.Gender,
                Age = client.Age,
                UserName = user.UserName,
                Photo = client.Photo,
            };
            return clientView;
        }

        public void Update(Client client)
        {
            _clientRepository.UpdateAsync(client);
        }
    }
}
