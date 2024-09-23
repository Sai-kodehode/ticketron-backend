﻿using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int userId);

         bool UserExists(int userId);
    

        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool Save();


    }
}
