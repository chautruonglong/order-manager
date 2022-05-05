using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;
using AspNet.Tutorial.Core.Exceptions;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Infrastructure.Contexts;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AspNet.Tutorial.Infrastructure.Repositories
{
    public abstract class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : BaseEntity
    {
        private readonly AspNetTutorialContext _context;

        private readonly DbSet<TEntity> _dbSet;

        private readonly IMapper _mapper;

        public CrudRepository(AspNetTutorialContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }

        public virtual IQueryable<TEntity> GetMany<TOrderKey>(Expression<Func<TEntity, TOrderKey>> order, SortDirections direction)
        {
            if (direction == SortDirections.Asc)
            {
                return _dbSet.Where(e => !e.IsDeleted)
                    .OrderBy(order);
            }

            return _dbSet.Where(e => !e.IsDeleted)
                .OrderByDescending(order);
        }

        public virtual IQueryable<TEntity> GetMany<TOrderKey>(int page, int size, Expression<Func<TEntity, TOrderKey>> order, SortDirections direction)
        {
            if (direction == SortDirections.Asc)
            {
                return _dbSet.Where(e => !e.IsDeleted)
                    .OrderBy(order)
                    .Skip(page * size)
                    .Take(size);
            }

            return _dbSet.Where(e => !e.IsDeleted)
                .OrderByDescending(order)
                .Skip(page * size)
                .Take(size);
        }

        public virtual async Task<TEntity> Get(TKey key)
        {
            TEntity entity = await _dbSet.FindAsync(key);

            if (entity == null || entity.IsDeleted)
            {
                throw new RecordNotFoundException();
            }

            return entity;
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            entity = (await _dbSet.AddAsync(entity)).Entity;

            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task InsertMany(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            TEntity dbEntity = await _dbSet.FindAsync(entity.Id);

            if (entity == null || entity.IsDeleted)
            {
                throw new RecordNotFoundException();
            }

            _mapper.Map(entity, dbEntity);

            await _context.SaveChangesAsync();

            return dbEntity;
        }

        public virtual async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> SoftDelete(TKey key)
        {
            TEntity dbEntity = await _dbSet.FindAsync(key);

            if (dbEntity == null || dbEntity.IsDeleted)
            {
                throw new RecordNotFoundException();
            }

            dbEntity.IsDeleted = true;

            await _context.SaveChangesAsync();

            return dbEntity;
        }

        public virtual async Task<TEntity> HardDelete(TKey key)
        {
            TEntity dbEntity = await _dbSet.FindAsync(key);

            if (dbEntity == null || dbEntity.IsDeleted)
            {
                throw new RecordNotFoundException();
            }

            dbEntity = _dbSet.Remove(dbEntity).Entity;

            await _context.SaveChangesAsync();

            return dbEntity;
        }

        public virtual async Task<long> Count()
        {
            return await _dbSet.Where(e => !e.IsDeleted)
                .LongCountAsync();
        }
    }
}
