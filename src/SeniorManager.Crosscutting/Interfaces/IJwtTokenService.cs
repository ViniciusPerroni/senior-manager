namespace SeniorManager.Crosscutting.Interfaces
{
    public interface IJwtTokenService
    {
        string CarregarDado(string claimName, string token);
        string GerarToken(string usuarioId, string username, string tipoUsuario);
    }
}