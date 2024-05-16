using Dominio.Models;

namespace Dominio.Interfaces;

public interface IAutenticacaoService
{
    public Task<LoginResponse> LoginAsync(string password);    
}