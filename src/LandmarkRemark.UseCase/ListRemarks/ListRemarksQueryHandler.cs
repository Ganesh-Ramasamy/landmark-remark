using LandmarkRemark.UseCase.CreateRemark;
using LandmarkRemark.UseCase.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using LandmarkRemark.Infrastructure;



namespace LandmarkRemark.UseCase.ListRemarks
{
    class ListRemarksQueryHandler : IRequestHandler<ListRemarksQuery, IList<RemarkResponse>>
    {

        private IConnectionFactory connectionFactory;

        public ListRemarksQueryHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }


        public IList<RemarkResponse> Handle(ListRemarksQuery message)
        {
            string sqlMyRemarks = @"
select r.Id RemarkId, r.Remark,
r.Longitude, r.Latitude,
u.Name UserName, u.Id UserId
from LandmarkRemark r 
inner join [User] u
    on r.UserId = u.Id
where u.Email = @Email
";

            string sqlNotMyRemarks = @"
select r.Id RemarkId, r.Remark,
r.Longitude, r.Latitude,
u.Name UserName, u.Id UserId
from LandmarkRemark r 
inner join [User] u
    on r.UserId = u.Id
where u.Email <> @Email
";

            string sql = sqlNotMyRemarks;
            if(message.MyRemarks)
            {
                sql = sqlMyRemarks;
            }

            var response = new List<RemarkResponse>();

            using (var con = connectionFactory.CreateConnection())
            {
                using (var reader = con.ExecuteReader(sql, message))
                {
                    while (reader.Read())
                    {
                        var remark = new RemarkResponse();
                        remark.Remark = reader["Remark"] as string;
                        remark.Location = new Location();
                        remark.Location.Longitude = (decimal)reader["Longitude"];
                        remark.Location.Latitude = (decimal)reader["Latitude"];
                        remark.RemarkId = reader["RemarkId"] as string;
                        remark.UserName = reader["UserName"] as string;
                        remark.UserId = reader["UserId"] as string;
                        response.Add(remark);
                    }
                }
            }

            return response;
        }
    }
}
