using LandmarkRemark.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using LandmarkRemark.UseCase.GetUser;

namespace LandmarkRemark.UseCase.CreateRemark
{
    class CreateRemarkCommandHandler : IRequestHandler<CreateRemarkCommand>
    {
        private IConnectionFactory connectionFactory;
         
        
        public CreateRemarkCommandHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
             
        }

        public void Handle(CreateRemarkCommand command)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                string sqlUser = @"
select top 1 *
from [User] u
where u.Email = @Email
";

                User user = null;

                using (var reader = connection.ExecuteReader(sqlUser, new { Email = command.UserEmail }))
                {
                    reader.Read();
                    user = new User();
                    user.Id = reader["Id"] as string;
                    user.Email = reader["Email"] as string;
                    user.Name = reader["Name"] as string;
                    user.Password = reader["Password"] as string;
                }

                    string sql = @"
insert into LandmarkRemark(Id, Remark, Longitude, Latitude, UserId)
values (@Id, @Remark, @Longitude, @Latitude, @UserId)
";
                connection.Execute(sql, new {
                    Id = Guid.NewGuid().ToString(),
                    Remark = command.Remark,
                    Longitude = command.Location.Longitude,
                    Latitude = command.Location.Latitude,
                    UserId = user.Id
                });
            }
        }
    }
}
