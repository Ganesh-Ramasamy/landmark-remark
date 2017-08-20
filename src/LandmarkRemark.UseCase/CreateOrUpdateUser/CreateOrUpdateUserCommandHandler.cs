using LandmarkRemark.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace LandmarkRemark.UseCase.CreateOrUpdateUser
{
    class CreateOrUpdateUserCommandHandler : IRequestHandler<CreateOrUpdateUserCommand>
    {
        private IConnectionFactory connectionFactory;


        public CreateOrUpdateUserCommandHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Handle(CreateOrUpdateUserCommand message)
        {
            string sqlUserExists = @"
select count(*)
from [User] u
where u.Email = @Email
";

            string sqlInsertUser = @"
insert into [User] (Id, Email, Name)
values (@Id, @Email, @Name)
";

            string sqlUpdateUser = @"
update [User] set Name = @Name 
where Email = @Email
";

            using (var con = connectionFactory.CreateConnection())
            {
                using (var trans = con.BeginTransaction())
                {
                    var numberOfUsers = (int)con.ExecuteScalar(sqlUserExists,
                        new
                        {
                            Email = message.Email
                        }, trans);


                    if(numberOfUsers == 0)
                    {
                        con.Execute(sqlInsertUser, new {
                           Id = Guid.NewGuid().ToString(),
                           Email = message.Email,
                           Name = message.Name
                        }, trans);
                    }
                    else
                    {
                        con.Execute(sqlUpdateUser, new
                        {
                            Email = message.Email,
                            Name = message.Name
                        }, trans);
                    }
                    trans.Commit();
                }
            } 
        }
    }
}
