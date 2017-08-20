using LandmarkRemark.UseCase.CreateRemark;
using LandmarkRemark.UseCase.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using LandmarkRemark.Infrastructure;



namespace LandmarkRemark.UseCase.SearchRemark
{
    class SearchRemarkQueryHandler : IRequestHandler<SearchRemarkQuery, IList<RemarkResponse>>
    {

        private IConnectionFactory connectionFactory;

        public SearchRemarkQueryHandler(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }


        public IList<RemarkResponse> Handle(SearchRemarkQuery message)
        {
            string sqlSearch = @"
select r.Id RemarkId, r.Remark,
r.Longitude, r.Latitude,
u.Name UserName, u.Id UserId
from LandmarkRemark r 
inner join [User] u
    on r.UserId = u.Id
where u.Name like '%' +  @SearchText + '%' or
r.Remark like '%' +  @SearchText + '%'
";
              
            var response = new List<RemarkResponse>();

            using (var con = connectionFactory.CreateConnection())
            {
                using (var reader = con.ExecuteReader(sqlSearch, message))
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
