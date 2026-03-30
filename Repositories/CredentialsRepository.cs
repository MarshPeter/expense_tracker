using expense_tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Repository;

public class CredentialRepository : ICredentialsRepository
{
    private readonly ExpenseDBContext _context;

    public CredentialRepository(ExpenseDBContext context)
    {
        _context = context;
    }
    public async Task<Credentials?> GetUserById(Guid id)
    {
        return await _context.Credentials.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Credentials> CreateAsync(Credentials credentials)
    {
        DateTime currentTime = DateTime.Now;
        credentials.Created = currentTime;
        await _context.AddAsync(credentials);
        return credentials;
    }

    // Returns false if row is not found
    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        int rowsDeleted = await _context.Credentials
            .Where(c => c.Id ==  id)
            .ExecuteDeleteAsync();

        return rowsDeleted == 1;
    }

    public async Task<bool> IdExists(Guid id)
    {
        return await _context.Credentials.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> UsernameExists(string username)
    {
        return await _context.Credentials.AnyAsync(c => c.Username == username);
    }

    // Returns null if no matching credentials are found
    public async Task<Credentials?> GetCredentials(string username, string hashed_password)
    {
        return await _context.Credentials.FirstOrDefaultAsync(c => c.Username == username && c.HashedPassword == hashed_password);
    }
}
