using Api_Mongo.Data.Collections;
using Api_Mongo.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api_Mongo.Repositories
{
    public class RepositoryMongoDB : IRepositoryInfected
    {
        public IMongoDatabase DB { get; }
        private readonly IMongoCollection<Infected> _infectadosCollection;
        public RepositoryMongoDB(IConfiguration configuration)
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

            _infectadosCollection = DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }
        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
        public void AddInfected(InfectedViewModel viewModel)
        {
            var infectado = new Infected(viewModel.Birthday, viewModel.Sex, viewModel.Latitude, viewModel.Longitude);

             _infectadosCollection.InsertOne(infectado);

            return; 
        }
        public List<InfectedViewModel> GetInfectedList()
        {
            var infectados = _infectadosCollection.Find(Builders<Infected>.Filter.Empty).ToList().Select(p => new InfectedViewModel
            {
                Birthday = p.Birthday,
                Sex = p.Sex,
                Longitude = p.Location.Longitude,
                Latitude = p.Location.Latitude

            }).ToList();

            return infectados;
        }
        public void Dispose()
        {
            
        }
    }
}