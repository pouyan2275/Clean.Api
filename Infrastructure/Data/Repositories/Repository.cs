﻿using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Infrastructure.Data.Repositories;

public class Repository<Tentity, Tid> : IRepository<Tentity, Tid>
    where Tentity :class, new()
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Tentity> Entity;
    public IQueryable Table { get {return _dbContext.Set<Tentity>().AsTracking(); } }
    public IQueryable TableAsNoTracking { get { return _dbContext.Set<Tentity>().AsNoTracking(); } }

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Entity = _dbContext.Set<Tentity>();
    }

    public virtual async Task<Tentity> CreateAsync(Tentity Tentity,bool saveChange = true,CancellationToken ct = default)
    {   
        var result = await Entity.AddAsync(Tentity,ct);
        if (saveChange)
            await _dbContext.SaveChangesAsync();
        return Tentity;
    }

    public virtual async Task SaveChangesAsync( CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<List<Tentity>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await Entity.ToListAsync();

        return result;
    }
    public virtual async Task<Tentity> GetByIdAsync(Tid id, CancellationToken ct = default)
    {
        var record = await Entity.FirstOrDefaultAsync(x => x.GetType().GetProperty("Id")!.GetValue(x)!.ToString() == id!.ToString(),ct);
        return new Tentity();
    }

    public virtual async Task<Tentity> UpdateAsync(Tid id, Tentity Tentity, bool saveChange = true, CancellationToken ct = default)
    {
        var record = await GetByIdAsync(id);
        var result = Entity.Update(Tentity);
        if (saveChange)
            await _dbContext.SaveChangesAsync();
        return Tentity;
    }

    public virtual async Task DeleteAsync(Tid id, CancellationToken ct = default)
    {
        var record = await GetByIdAsync(id);
        var result = Entity.Remove(record);
    }
}
