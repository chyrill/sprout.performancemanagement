using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using al.performancemanagement.DAL.Helpers;
using al.performancemanagement.DAL.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace al.performancemanagement.DAL.Repository
{
    public class BaseRepository<TData> : IRepository<TData> 
        where TData : BaseEntity
    {
        private string _repoName = typeof(TData).Name;

        public IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public bool Insert(TData data)
        {
            try
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();

                var dataEntry = GetProperties(data);

                var param = new DynamicParameters();

                for (int cn = 0; cn < dataEntry.Properties.Count - 1; cn++)
                {
                    param.Add("@" + dataEntry.Properties[cn], dataEntry.Values[cn]);
                }

                string sp = "sp_" + _repoName + "_add";
                var create = this._db.Execute(sp, param, commandType: CommandType.StoredProcedure);

                _db.Close();

                return true;
            }
            catch (Exception e)
            {
                _db.Close();
                return false;
            }
        }

        public bool Update(TData data)
        {
            try
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();

                var dataEntry = GetProperties(data);

                var param = new DynamicParameters();

                for (int cn = 0; cn < dataEntry.Properties.Count; cn++)
                {
                    param.Add("@" + dataEntry.Properties[cn], dataEntry.Values[cn]);
                }

                string sp = "sp_" + _repoName + "_update";

                var update = _db.Execute(sp, param, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception e)
            {
                _db.Close();
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();

                string sqlQuery = "DELETE FROM " + _repoName + " WHERE Id=" + id;

                _db.Execute(sqlQuery);

                _db.Close();

                return true;
            }
            catch(Exception e)
            {
                _db.Close();
                return false;
            }
        }

        public TData GetById(object id)
        {
            try
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();

                string sqlQuery = "select * from " + _repoName + " where Id=" + id;

                var data = _db.Query<TData>(sqlQuery).FirstOrDefault();

                return data;
            }
            catch(Exception e)
            {
                _db.Close();
                return default(TData);
            }
        }

        public SearchResult<TData> Search(SearchRequest<TData> request)
        {
            try
            {
                SearchResult<TData> result = new SearchResult<TData>();

                var records = _db.Query<TData>("select * from " + _repoName);

                var recordCount = records.Select(x => x);

                if(request.Filter != null)
                {
                    records = records.AsQueryable<TData>().Where(request.Filter);
                    recordCount = recordCount.AsQueryable<TData>().Where(request.Filter);
                }

                result.SearchTotal = recordCount.Count();

                if(result.SearchTotal == 0)
                {
                    result.Items = new List<TData>().AsQueryable();
                    result.Message = string.Format("No " + _repoName + " matched search filters");
                    return result;
                }

                if(request.Sort != null && request.Sort.Count() > 0)
                {
                    IOrderedQueryable<TData> sortedRecords = null;
                    foreach (var sortExpression in request.Sort.ToList())
                    {
                        var sort = sortExpression.Sort.RemapForType<TData, TData, object>();
                        if (sortedRecords == null)
                        {
                            sortedRecords = records.AsQueryable().ObjectSort<TData>(sort, sortExpression.IsAscending, false);
                        }
                        else
                        {
                            sortedRecords = sortedRecords.ObjectSort<TData>(sort, sortExpression.IsAscending, true);
                        }
                    }

                    records = sortedRecords;
                }
                else
                {
                    records = records.AsQueryable().OrderBy(x => x.Id);
                }

                int actualPageSize = request.PageSize == 0 ? result.SearchTotal : request.PageSize;
                result.SearchPages = result.SearchTotal / actualPageSize;
                int div = result.SearchTotal % actualPageSize;
                result.SearchPages = div != 0 ? ++result.SearchPages : result.SearchPages;

                var finalRecords = records.Skip(request.PageIndex * request.PageSize).Take(actualPageSize).AsEnumerable();

                result.Items = finalRecords.Select(x => x).AsQueryable();

                return result;
            }
            catch(Exception e)
            {
                _db.Close();
                return null;
            }
        }

        public class PropertiesPair
        {
            public Dictionary<int, string> Properties { get; set; }

            public Dictionary<int, object> Values { get; set; }
        }

        // get properties and values
        public PropertiesPair GetProperties(TData data)
        {
            Dictionary<int, string> props = new Dictionary<int, string>();

            Dictionary<int, object> values = new Dictionary<int, object>();

            var propertyNames = typeof(TData).GetProperties();

            int count = 0;

            foreach (var prop in propertyNames)
            {
                props.Add(count, prop.Name);

                values.Add(count, typeof(TData).GetProperty(prop.Name).GetValue(data, null));

                count++;
            }

            return new PropertiesPair() { Properties = props, Values = values };
        }
    }
}
