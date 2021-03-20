using AutoMapper;
using StatsWebApp.Data;
using StatsWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IUserRepository UserRepository => new UserRepository(_context);
        public ICategoryRepository CategoryRepository => new CategoryRepository(_context,_mapper);
        public ISubCategoryRepository SubCategoryRepository => new SubCategoryRepository(_context, _mapper);
        public IAppDataRepository AppDataRepository => new AppDataRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
