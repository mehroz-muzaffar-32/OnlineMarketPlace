using System;
using Torico.Server.Data;
using Torico.Server.Repositories.Implementations;
using Torico.Server.Repositories.Interfaces;

namespace Torico.Server.Repositories.Core;

public class Services
{
    private readonly AppDbContext _appDbContext;

    public Services(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public IProductRepository ProductRepository => new ProductRepository(_appDbContext);
    public ICategoryRepository CategoryRepository => new CategoryRepository(_appDbContext);
}
