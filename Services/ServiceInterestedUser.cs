using InfectedAPI.Models;
using InfectedAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfectedAPI.Services
{
    public class ServiceInterestedUser : IServiceInterestedUser
    {
        private readonly IRepositoryInterestedUser _repositoryInterestedUser;
        public ServiceInterestedUser(IRepositoryInterestedUser repositoryInterestedUser)
        {
            _repositoryInterestedUser = repositoryInterestedUser;
        }
        public async Task AddInterestedUser(InterestedUserViewModel viewModel)
        {
           await _repositoryInterestedUser.AddInterestedUser(viewModel);
        }

        public void Dispose()
        {
            
        }

        public List<InterestedUserViewModel> GetAllNextToBeVaccinatedUsers()
        {
            return  _repositoryInterestedUser.GetAllNextToBeVaccinatedUsers();
        }

        public List<InterestedUserViewModel> GetAllOrderedByAge()
        {
            return _repositoryInterestedUser.GetAllOrderedByAge();
        }

        public InterestedUserViewModel GetInterestedUser(InterestedUserViewModelInput input)
        {
            return _repositoryInterestedUser.GetInterestedUser(input);
        }

        public List<InterestedUserViewModel> GetNNextToBeVaccinatedUsers(long N)
        {
            return  _repositoryInterestedUser.GetNNextToBeVaccinatedUsers(N);
        }

        public async Task RemoveVaccinatedUser(long IdNumber)
        {
            await _repositoryInterestedUser.RemoveInterestedUser(IdNumber);
        }
    }
}
