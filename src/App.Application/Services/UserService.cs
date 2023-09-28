using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Models;
using App.Infrastructure.Persistence.UnitOfWork;
using App.Infrastructure.Persistence.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Services
{
    /// <summary>
    /// Represents the user service.
    /// Inherits from GenericRepository and implements IUserService.
    /// </summary>
    public class UserService : GenericRepository<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        
        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="serviceProvider">The IServiceProvider to resolve dependencies.</param>
        public UserService(IServiceProvider serviceProvider) 
            : base(serviceProvider.GetRequiredService<IUnitOfWork>().Context)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _serviceProvider = serviceProvider;
        }
    }
}
