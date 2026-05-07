using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces
{
  public interface IUserService
  {
    Task<Users> GetUserByName(string Name);
    Task<Users> GetUserByEmail(string Email);
    Task<Users> GetUsersAsync(string email, string name);
    }
}

