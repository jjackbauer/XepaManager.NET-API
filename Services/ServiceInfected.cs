using Api_Mongo.Models;
using Api_Mongo.Repositories;
using System.Collections.Generic;

namespace Api_Mongo.Services
{
    public class ServiceInfected : IServiceInfected
    {
        private readonly IRepositoryInfected _repositoryInfected;

        public ServiceInfected (IRepositoryInfected repositoryInfected)
	    {
            _repositoryInfected = repositoryInfected;
	    }
        public  void AddInfected(InfectedViewModel viewModel)
        {
            _repositoryInfected.AddInfected(viewModel);
        }

        public void Dispose()
        {
            
        }

        public List<InfectedViewModel> GetInfectedList()
        {
            return _repositoryInfected.GetInfectedList();
        }
    }
}