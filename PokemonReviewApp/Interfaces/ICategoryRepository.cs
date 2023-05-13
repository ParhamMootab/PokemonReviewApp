using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        ICollection<Category> GetAllCategories();
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExists(int id);
    }
}
