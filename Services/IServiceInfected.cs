using Api_Mongo.Models;
using System;
using System.Collections.Generic;

namespace Api_Mongo.Services
{
    public interface IServiceInfected : IDisposable
    {
        void AddInfected(InfectedViewModel viewModel);
        List<InfectedViewModel> GetInfectedList();
    }
}