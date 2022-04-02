namespace SeniorManager.Crosscutting.Interfaces
{
    public interface IUrlBuilder
    {
        IUrlBuilder AddParameter(string key, string value);
        string Build();
        IUrlBuilder ToUrl(string url);
    }
}