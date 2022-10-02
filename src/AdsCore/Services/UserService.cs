using Ads.Core.Entities;
using Ads.Core.Interfaces;

namespace Ads.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(
        IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public virtual async Task Create(string identityId)
    {
        if (string.IsNullOrEmpty(identityId))
            throw new ArgumentNullException(nameof(identityId));

        var user = new User(identityId);
        
        await _userRepository.Add(user);
    }
}