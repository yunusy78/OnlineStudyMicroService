namespace OnlineStudyShared.Services;

public interface ISharedIdentity
{
    public string GetUserId { get; }
    public string Email { get; }
}
