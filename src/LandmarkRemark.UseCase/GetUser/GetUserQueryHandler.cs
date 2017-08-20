using LandmarkRemark.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;


namespace LandmarkRemark.UseCase.GetUser
{
    class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private IConnectionFactory connectionFactory;


        public GetUserQueryHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public User Handle(GetUserQuery message)
        {
            string sqlGetUser = @"
select *
from [User] u
where u.Email = @Email
";
            using (var con = connectionFactory.CreateConnection())
            {
                using (var reader = con.ExecuteReader(sqlGetUser, message))
                {
                    if(reader.Read())
                    {
                        var user = new User();
                        user.Email = reader["Email"] as string;
                        user.Name = reader["Name"] as string;
                        user.Id = reader["Id"] as string;
                        user.Password = reader["Password"] as string;

                        return user;
                    }
                }
            }

            return null;
        }
    }
}
