

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    public interface IAdministratorDirector
    {
        Task<List<AbsModels.Administratordetails>> GetAllAsync(CancellationToken cancellationToken);

         Task<int> InsertAsync(Administrator administrator, CancellationToken cancellationToken);

        Task<int> UpdateAsync(string id, Administrator administrator, CancellationToken cancellationToken);

        Task<int> DeleteAsync(string id, CancellationToken cancellationToken);

        Task<Administrator> GetDocumentByIdAsync(string id, CancellationToken cancellationToken);

    }
}
