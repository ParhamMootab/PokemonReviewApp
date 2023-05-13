using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();
        Country GetCountry(int id);
        Country GetCountryByOwnerId(int OwnerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);

    }
}
