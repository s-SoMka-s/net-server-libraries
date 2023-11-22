namespace Net.Server.Libraries.SignalR.Interfaces.Models;

internal class ConnectionContainer
{
    internal ConnectionContainer(string id)
    {
        Id = id;
    }


    internal string Id { get; }
    internal long? AccountId { get; set; }
}
