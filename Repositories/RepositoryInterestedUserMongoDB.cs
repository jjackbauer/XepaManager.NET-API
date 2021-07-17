using InfectedAPI.Data.Collections;
using InfectedAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfectedAPI.Repositories
{
    public class RepositoryInterestedUserMongoDB : IRepositoryInterestedUser
    {
        public IMongoDatabase DB { get; }
        private readonly IMongoCollection<InterestedUser> _InterestedUserCollection;
        public RepositoryInterestedUserMongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }

            _InterestedUserCollection = DB.GetCollection<InterestedUser>(typeof(InterestedUser).Name.ToLower());
        }
        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(InterestedUser)))
            {
                BsonClassMap.RegisterClassMap<InterestedUser>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task AddInterestedUser(InterestedUserViewModel viewModel)
        {
            var user = new InterestedUser(viewModel.IdNumber, viewModel.Name, viewModel.Birthday, viewModel.IsComorbidity, viewModel.Longitude, viewModel.Latitude);
            await _InterestedUserCollection.InsertOneAsync(user);
        }

        public void Dispose()
        {
            
        }

        public List<InterestedUserViewModel> GetAllNextToBeVaccinatedUsers()
        {
            var users = GetAllOrderedByAge();
            var ComorbidityUsers = users.Where(p => p.IsComorbidity == true).ToList();
            var NonComorbidityUsers = users.Where(p => p.IsComorbidity == false).ToList();

            return ComorbidityUsers.Concat(NonComorbidityUsers).ToList();
        }

        public List<InterestedUserViewModel> GetAllOrderedByAge()
        {
            var users = _InterestedUserCollection.Find(Builders<InterestedUser>.Filter.Empty).ToList().Select<InterestedUser, InterestedUserViewModel>(p =>
             {
                 return new InterestedUserViewModel
                 {
                     IdNumber = p.IdNumber,
                     Name = p.Name,
                     Birthday = p.Birthday,
                     IsComorbidity = p.IsComorbidity,
                     Longitude = p.Location.Longitude,
                     Latitude = p.Location.Latitude
                 };
             }).ToList();

            return users.OrderByDescending(p => (DateTime.Now - p.Birthday)).ToList();
        }

        public List<InterestedUserViewModel> GetNNextToBeVaccinatedUsers(long N)
        {
            var allNextToBeVaccinatedUsers = GetAllNextToBeVaccinatedUsers();

            return allNextToBeVaccinatedUsers.Take((int) N).ToList();
        }

        public async Task RemoveInterestedUser(long IdNumber)
        {
           await  _InterestedUserCollection.DeleteOneAsync(p => p.IdNumber == IdNumber);
        }

        public InterestedUserViewModel GetInterestedUser(InterestedUserViewModelInput input)
        {
            var userList = _InterestedUserCollection.Find(Builders<InterestedUser>.Filter.Empty).ToList().Where(p => p.IdNumber == input.IdNumber ).Where(p => p.Name == input.Name).ToList();
            //
            
            if (userList.Count == 0)
                throw new Exception($"There is no register with this IdNumber = {input.IdNumber} and Name = {input.Name}");

            var user = userList.ElementAt(0);
            var userViewModel = new InterestedUserViewModel
            {
                IdNumber = user.IdNumber,
                Name = user.Name,
                Birthday = user.Birthday,
                IsComorbidity = user.IsComorbidity,
                Latitude = user.Location.Latitude,
                Longitude = user.Location.Longitude
            };

            return userViewModel;
        }
    }
}
