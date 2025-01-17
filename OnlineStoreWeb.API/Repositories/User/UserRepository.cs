using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly StoreDbContext _context;

    public UserRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to fetch users", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task<User?> GetUserWithIdAsync(int id)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to fetch user", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task AddUserAsync(CreateUserDto createUserDto)
    {
        try
        {
            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                UserCreated = createUserDto.UserCreated,
                UserUpdated = createUserDto.UserUpdated
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to save user", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new Exception("User not found");

            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;
            user.UserUpdated = updateUserDto.UserUpdated;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to update user", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new Exception("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to delete user", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }
}