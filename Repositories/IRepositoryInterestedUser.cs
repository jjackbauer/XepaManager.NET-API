using InfectedAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfectedAPI.Repositories
{
    public interface IRepositoryInterestedUser : IDisposable
    {
        Task AddInterestedUser(InterestedUserViewModel viewModel);
        Task RemoveInterestedUser(long IdNumber);
        InterestedUserViewModel GetInterestedUser(InterestedUserViewModelInput input);
        List<InterestedUserViewModel> GetNNextToBeVaccinatedUsers(long N);
        List<InterestedUserViewModel> GetAllNextToBeVaccinatedUsers();
        List<InterestedUserViewModel> GetAllOrderedByAge();
    }
}
