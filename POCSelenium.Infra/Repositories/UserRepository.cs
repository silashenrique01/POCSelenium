using Dapper;
using Microsoft.Extensions.Configuration;
using POCSelenium.Domain;
using POCSelenium.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace POCSelenium.Infra
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Delete(User user)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLServerConnectionString")))
            {
                string sqlQuery = @"DELETE FROM usuarios WHERE usuario_id = @Id ";
                int result = conn.Execute(sqlQuery, new
                {
                    Id = user.Id
                });

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IList<User> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLServerConnectionString")))
            {
                string sqlQuery = @"SELECT usuario_id AS Id, nomeCompleto AS FullName, idade AS Age, email AS EMAIL, role AS Role FROM usuarios";
                List<User> result = conn.Query<User>(sqlQuery).ToList();
                return result;
            }
        }

        public IList<User> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public User GetRolesUser(string email)
        {
         
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLServerConnectionString")))
            {
                string sqlQuery = @"SELECT role as Role FROM usuarios where email = @email";
                var result = conn.Query<User>(sqlQuery, new
                {
                    Email = email,
                });
                return result.Single();
            }
        }

        public bool Insert(User user)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLServerConnectionString")))
            {
                string sqlQuery = @"INSERT INTO usuarios(nomeCompleto, idade, email) VALUES (@FullName, @Age, @Email)";
                int result = conn.Execute(sqlQuery, new
                {
                    FullName = user.Fullname,
                    Age = user.Age,
                    Email = user.Email
                });

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLServerConnectionString")))
            {
                string sqlQuery = @"UPDATE usuarios SET nomeCompleto = @FullName, idade = @Age, email = @Email WHERE usuario_id = @Id ";

                int result = conn.Execute(sqlQuery, new
                {
                    Id = user.Id,
                    FullName = user.Fullname,
                    Age = user.Age,
                    Email = user.Email
                });

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
