using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using IServices;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHolidayService HolidayService;

        public HomeController(IHolidayService service) => HolidayService = service;
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Index(string startdate, int days = 10)
        {

            List<ProcDateCountModel> list = new List<ProcDateCountModel>();

            if (!string.IsNullOrEmpty(startdate))
                list = (await HolidayService.GetProcDateList(startdate, days)).ToList();

            return View(list);
        }

        //public IActionResult Index(int pageindex = 1, int pagesize = 10)
        //{
        //    PageList<HolidayDateEntity> list = HolidayService.GetList(pageindex, pagesize);

        //    return View(list);
        //}

        //public IActionResult AddMatHoliday()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<JsonResult> Delete(string date)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(date)) return Json("日期选择错误");
        //        var count = await HolidayService.DelHoliday(date);
        //        if (count > 0)
        //            return Json("成功");
        //        return Json("删除失败");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        ////}
        //[HttpPost]
        //public async Task<JsonResult> SaveMatHoliday(string startdate, string enddate, int days)
        //{
        //    try
        //    {
        //        string stroutput = "";
        //        if (string.IsNullOrEmpty(startdate)) stroutput = "开始日期不能为空，";
        //        if (string.IsNullOrEmpty(enddate)) stroutput = "结束日期不能为空，";

        //        if (DateTime.Parse(enddate) < DateTime.Parse(startdate))
        //        {
        //            stroutput = "结束日期不能小于开始日期";
        //        }

        //        if (!string.IsNullOrEmpty(stroutput)) return Json(stroutput);
        //        var count = await HolidayService.AddHoliday(startdate, enddate, days);
        //        if (count <= 0) return Json("添加失败");
        //        return Json("成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
    }
}
