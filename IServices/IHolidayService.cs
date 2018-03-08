using Entity;
using Longjubank.FrameWorkCore.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface IHolidayService
    {
        //Task<int> AddHoliday(string start, string end, int days);
        //Task<int> DelHoliday(string date);
        //PageList<HolidayDateEntity> GetList(int pageindex, int pagesize);
        Task<IEnumerable<ProcDateCountModel>> GetProcDateList(string start, int days);
    }
}
