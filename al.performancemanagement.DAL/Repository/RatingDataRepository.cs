using al.performancemanagement.DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace al.performancemanagement.DAL.Repository
{
    public class RatingDataRepository:BaseRepository<RatingData>
    {

        public IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public IEnumerable<RatingData> GetDescription(long id,decimal score)
        {
            try
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();

                var param = new DynamicParameters();

                param.Add("@Id", id, DbType.Int64);
                param.Add("@Score", score, DbType.Decimal);

                var get = _db.Query<RatingData>("sp_RatingData_search", param, commandType:CommandType.StoredProcedure);

                _db.Close();
                return get;
            }
            catch(Exception e)
            {
                _db.Close();
                return null;
            }
        }
    }
}
