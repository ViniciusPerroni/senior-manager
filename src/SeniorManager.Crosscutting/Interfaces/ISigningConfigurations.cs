namespace SeniorManager.Crosscutting.Interfaces
{
    public interface ISigningConfigurations
    {
        string ConverterHash(string value);
        string GerarHash(string stringValue);
        bool VerificarHash(string stringValue, string hash);
    }
}