using System;
using Microsoft.EntityFrameworkCore;
using Torico.Server.Data;
using Torico.Shared.Models;
using Torico.Shared.Responses;

namespace Torico.Server.Repositories.Abstracts;

public abstract class BaseRepository
{
    private readonly AppDbContext _appDbContext;

    protected BaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    protected AppDbContext GetAppDbContext() => _appDbContext;
    protected DbSet<T> GetCurrentScope<T>() where T : class => GetAppDbContext().Set<T>();

    protected async Task Commit() => await _appDbContext.SaveChangesAsync();

    public async Task<ServiceResponse> CheckName<T>(string name) where T : class
    {
        var dbSet = GetCurrentScope<T>();
        var entity = await dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name").ToLower() == name.ToLower());
        return entity == null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Name already exists");
    }
}
