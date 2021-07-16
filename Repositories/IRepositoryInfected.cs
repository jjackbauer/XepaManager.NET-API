using Api_Mongo.Models;
using System;
using System.Collections.Generic;

namespace Api_Mongo.Repositories
{
    public interface IRepositoryInfected : IDisposable
    {
        void AddInfected(InfectedViewModel viewModel);
        List<InfectedViewModel> GetInfectedList();
        
    }
}