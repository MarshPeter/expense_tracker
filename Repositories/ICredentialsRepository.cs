using expense_tracker.Models;

namespace expense_tracker.Repository;

public interface ICredentialsRepository
{
    public Task<Credentials?> GetUserById(Guid id);
    public Task<Credentials?> GetCredentials(string username, string hashed_password);
    public Task<Credentials> CreateAsync(Credentials credentials);
    public Task<bool> DeleteByIdAsync(Guid id);
    public Task<bool> IdExists(Guid id);
    public Task<bool> UsernameExists(string username);
}
