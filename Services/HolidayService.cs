using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IServices;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Longjubank.FrameWorkCore.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Services
{
    public class HolidayService : IHolidayService
    {
        private readonly AppDbContext DbContext;
        public HolidayService(AppDbContext appDb) => DbContext = appDb;

        //public async Task<int> AddHoliday(string start, string end, int days)
        //{
        //    var result = await DbContext.Database.ExecuteSqlCommandAsync("call proc_date_count(@in_begindate,@in_days)", parameters: new { start, days });
        //    //HolidayEntity entity = new HolidayEntity() { StartDate = start, EndDate = end, Days = days };
        //    //DbContext.Tb_holiday.Add(entity);
        //    return await DbContext.SaveChangesAsync();
        //}

        //public async Task<int> DelHoliday(string date)
        //{
        //    HolidayDateEntity entity = DbContext.Tb_holiday.Where(m => m.date == date).FirstOrDefault();
        //    if (entity == null || string.IsNullOrEmpty(entity.date))
        //        return 0;
        //    DbContext.Tb_holiday.Remove(entity);
        //    return await DbContext.SaveChangesAsync();
        //}

        //public PageList<HolidayDateEntity> GetList(int pageindex, int pagesize)
        //{
        //    PageList<HolidayDateEntity> list = DbContext.Tb_holiday.OrderByDescending(m => m.date).ToPageList(pageindex, pagesize);
        //    return list;
        //}

        public async Task<IEnumerable<ProcDateCountModel>> GetProcDateList(string start, int days)
        {
            // var list = DbContext.Database.GetDbConnection().Database.FromSql($"call proc_date_count({start},{days})").ToList();
            MySqlParameter[] param = { new MySqlParameter("@in_begindate",MySqlDbType.VarChar,10),
                new MySqlParameter("@in_days",MySqlDbType.Int32)};
            param[0].Value = start;
            param[1].Value = days;

            var list = await GetTAsync<ProcDateCountModel>("proc_date_count", param, CommandType.StoredProcedure);

            return list;
        }

        private readonly object _conObj = new object();
        public async Task<IEnumerable<T>> GetTAsync<T>(string cmdText, MySqlParameter[] sqlParameter, CommandType cmdType = CommandType.Text) where T : class, new()
        {
            try
            {
                var connection = DbContext.Database.GetDbConnection();

                lock (_conObj)
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                }
                List<T> datalist = new List<T>();
                Type entity = typeof(T);
                System.Reflection.PropertyInfo[] propertylist = entity.GetProperties();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(sqlParameter);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var item = new T();
                            foreach (var m in propertylist)
                            {
                                if (reader[m.Name] != DBNull.Value)
                                {
                                    m.SetValue(item, reader[m.Name]);
                                }
                            }
                            datalist.Add(item);
                        }
                    }
                } 
                return datalist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
