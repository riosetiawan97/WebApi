using WebApi.Data.Models;

namespace WebApi.Data.Repositories
{
    public interface IProfilRepository
    {
        Task AddProfil(Profil Profil);
        Task DeleteProfil(int id);
        Task<IEnumerable<Profil>> GetProfil();
        Task<Profil?> GetProfilById(int id);
        Task UpdateProfil(Profil Profil);
    }
}