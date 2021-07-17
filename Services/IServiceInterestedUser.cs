using InfectedAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfectedAPI.Services
{
    public interface IServiceInterestedUser : IDisposable
    {
        Task AddInterestedUser(InterestedUserViewModel viewModel);
        Task RemoveVaccinatedUser(long IdNumber);
        InterestedUserViewModel GetInterestedUser(InterestedUserViewModelInput input);
        List<InterestedUserViewModel> GetNNextToBeVaccinatedUsers(long N);
        List<InterestedUserViewModel> GetAllNextToBeVaccinatedUsers();
        List<InterestedUserViewModel> GetAllOrderedByAge();

    }
}
