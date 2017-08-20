using LandmarkRemark.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;


namespace LandmarkRemark.UseCase.DeleteRemark
{
    class DeleteRemarkCommandHandler : IRequestHandler<DeleteRemarkCommand>
    {
        private IConnectionFactory connectionFactory;


        public DeleteRemarkCommandHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }


        public void Handle(DeleteRemarkCommand message)
        {
            string sqlDeleteRemark = @"
delete from LandmarkRemark
where Id = @RemarkId
";
            using (var con = connectionFactory.CreateConnection())
            {
                con.Execute(sqlDeleteRemark, message);
            }
        }
    }
}
