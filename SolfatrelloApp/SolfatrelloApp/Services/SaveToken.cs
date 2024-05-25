﻿using Dominio.Interfaces;

namespace SolfatrelloApp.Services;

public class SaveToken : ITokenStorage
{
    private string _token { get; set; } = string.Empty;
    public async Task<string> Get()
    {
        return await Task.FromResult(_token);
    }

    public Task Set(string token)
    {
        _token = token;
        return Task.CompletedTask;
    }
}