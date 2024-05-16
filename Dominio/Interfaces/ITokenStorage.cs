namespace Dominio.Interfaces;

public interface ITokenStorage
{
    public Task<string> Get();
    public Task Set(string token);
}