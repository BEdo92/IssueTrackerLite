using IssueTrackerLite.Data;

namespace IssueTrackerLite.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
