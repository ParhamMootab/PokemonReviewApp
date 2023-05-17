using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext context;
        
        public CountryRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
        }
        public bool CountryExists(int id)
        {
            return context.Countries.Any(country => country.Id == id);
        }

        public ICollection<Country> GetAllCountries()
        {
            return context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwnerId(int OwnerId)
        {
            return context.Owners.Where(o => o.Id == OwnerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool CreateCountry(Country country)
        {
            context.Add(country);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            context.Update(country);
            return Save();
        }
    }
}
