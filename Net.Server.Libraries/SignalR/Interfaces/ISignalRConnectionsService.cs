namespace Net.Server.Libraries.SignalR.Interfaces;

public interface ISignalRConnectionsService
{
    public void Add(string connectionId);
    public void Remove(string connectionId);

    public Task LinkWithAccountAsync(string connectionId, long accountId);

    public IReadOnlyList<string> GetByAccount(long accountId);
}
