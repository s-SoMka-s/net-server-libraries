using Net.Server.Libraries.SignalR.Interfaces;
using Net.Server.Libraries.SignalR.Interfaces.Models;

namespace Net.Server.Libraries.SignalR.Implementations;

public class SignalRConnectionsService : ISignalRConnectionsService
{
    private static readonly List<ConnectionContainer> _connections;

    static SignalRConnectionsService()
    {
        _connections = new List<ConnectionContainer>();
    }

    void ISignalRConnectionsService.Add(string connectionId)
    {
        if (_connections.Any(c => c.Id == connectionId))
        {
            return;
        }

        _connections.Add(new ConnectionContainer(connectionId));
    }

    IReadOnlyList<string> ISignalRConnectionsService.GetByAccount(long accountId)
    {
        return _connections.Where(c => c.AccountId.HasValue)
            .Where(c => c.AccountId == accountId)
            .Select(c => c.Id)
            .ToList();
    }

    async Task ISignalRConnectionsService.LinkWithAccountAsync(string connectionId, long accountId)
    {
        var container = _connections.FirstOrDefault(c => c.Id == connectionId);
        if (default == container)
        {
            _connections.Add(new ConnectionContainer(connectionId));
        }
        else
        {
            container.AccountId = accountId;
        }
    }

    void ISignalRConnectionsService.Remove(string connectionId)
    {
        var item = _connections.FirstOrDefault(c => c.Id == connectionId);
        if (item == default)
        {
            return;
        }

        _connections.Remove(item);
    }
}
