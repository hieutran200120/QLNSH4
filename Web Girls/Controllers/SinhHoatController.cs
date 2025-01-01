using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.ModelsView;
using Web_Girls.Models.Context;
using System.Data.Entity;

namespace Web_Girls.Controllers
{
    public class SinhHoatController : Controller
    {
        MyContext obj = new MyContext();

        public ActionResult Index(int ma, string id)
        {
            return View();

        }
        //public ActionResult ThamGia(int ma)
        //{
        //    var noidung = obj.SinhHoats.FirstOrDefault(item => item.MaSinhHoat == ma);
        //    var thamgia = (from a in obj.SinhHoats
        //                   where a.MaSinhHoat == ma
        //                   select a).ToList();
        //    ViewBag.noidung = noidung;
        //    ViewBag.thamgia = thamgia;
        //    return PartialView();
        //}


        //[HttpPost]
        //public ActionResult ThemSinhHoat(int TenSH, DateTime NgaySH, string noidung, int MaHPN)
        //{
        //    try
        //    {
        //        if (MaHPN != 0)
        //        {
        //            SinhHoat sh = new SinhHoat();
        //            sh.MaHPN = MaHPN;
        //            sh.MaLoaiSinhHoat = TenSH;
        //            sh.NgaySinhHoat = NgaySH;
        //            sh.NoiDung = noidung;
        //            sh.SoLuongVang = 0;
        //            obj.SinhHoats.Add(sh);
        //            obj.SaveChanges();
        //        }
        //         else
        //        {
        //            SinhHoat sh = new SinhHoat();
        //            sh.MaHPN = 0;
        //            sh.MaLoaiSinhHoat = TenSH;
        //            sh.NgaySinhHoat = NgaySH;
        //            sh.NoiDung = noidung;
        //            sh.SoLuongVang = 0;
        //            obj.SinhHoats.Add(sh);
        //            obj.SaveChanges();
        //        }
                   
        //        return Json(new
        //        {
        //            status = true
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult SuaSinhHoat(int ma)
        //{
        //    var sh = obj.SinhHoats.Find(ma);
        //    ViewBag.sh = sh;
        //    var loaish = obj.LoaiSinhHoats.ToList(); 
        //    ViewBag.loaish = loaish;
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult SuaSinhHoat(SinhHoat sh, int MaHoi)
        //{
        //    var shSua = obj.SinhHoats.Find(sh.MaSinhHoat);
        //    shSua.NgaySinhHoat = sh.NgaySinhHoat;
        //    shSua.NoiDung = sh.NoiDung;
        //    obj.SaveChanges();
        //    return RedirectToAction("Index", new { ma = MaHoi });
        //}
        //public ActionResult XoaSinhHoat(int ma)
        //{
        //    try
        //    {
        //        var sinhhoat = obj.SinhHoats.Find(ma);
        //        obj.SinhHoats.Remove(sinhhoat);
        //        obj.SaveChanges();
        //        return Json(new
        //        {
        //            status = true,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult IndexLoai(int ma)
        //{
        //        var loaisinhhoat = obj.LoaiSinhHoats.ToList();
        //        ViewBag.loaisinhhoat = loaisinhhoat;
        //        return View();
        //}
        //[HttpPost]
        //public ActionResult ThemLoaiSH(string TenSH)
        //{
        //    try
        //    {
               
        //            LoaiSinhHoat sh = new LoaiSinhHoat();
        //            sh.TenSinhHoat = TenSH;
        //            obj.LoaiSinhHoats.Add(sh);
        //            obj.SaveChanges();
        //        return Json(new
        //        {
        //            status = true
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult SuaLoaiSinhHoat(int ma)
        //{
        //    var loaish = obj.LoaiSinhHoats.Find(ma);
        //    ViewBag.loaish = loaish;
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult SuaLoaiSinhHoat(LoaiSinhHoat loaish, int MaHoi)
        //{
        //    var shSua = obj.LoaiSinhHoats.Find(loaish.MaLoaiSinhHoat);
        //    shSua.TenSinhHoat = loaish.TenSinhHoat;
        //    obj.SaveChanges();
        //    return RedirectToAction("IndexLoai", new { ma = MaHoi });
        //}
        //public ActionResult XoaLoaiSinhHoat(int ma)
        //{
        //    try
        //    {
        //        var sinhhoat = obj.LoaiSinhHoats.Find(ma);
        //        obj.LoaiSinhHoats.Remove(sinhhoat);
        //        obj.SaveChanges();
        //        return Json(new
        //        {
        //            status = true,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult ThemHoiVienVang(int ma, int mahoi)
        //{
        //    var sh = obj.SinhHoats.Find(ma);
        //    var listHV = (from a in obj.HoiViens
        //                  //join b in obj.SinhHoat_Vang on a.MaHV equals b.MaHV
        //                  join c in obj.SinhHoats on b.MaSinhHoat equals c.MaSinhHoat
        //                  where b.MaSinhHoat == ma
        //                  select new HoiVienVangView()
        //                  {
        //                      MaSH_V = b.MaSH_V,
        //                      TenHV = a.TenHV,
        //                      NgaySinh = a.NgaySinh,
        //                      SDT = a.SDT,
        //                      MaHV = b.MaHV,
        //                      //MaSinhHoat = b.MaSinhHoat
        //                  }).ToList();

        //    if (mahoi != 0)
        //    {
        //        var hvHoi = (from a in obj.HoiViens
        //                 join b in obj.HoiViens on a.MaHV equals b.MaHV
        //                 where b.MaHPN == mahoi
        //                 select a).ToList();
        //        ViewBag.hvHoi = hvHoi;
        //    }
        //    else
        //    {
        //        var hvHoi = obj.HoiViens.ToList();
        //        ViewBag.hvHoi = hvHoi;
        //    }

        //    ViewBag.hv = listHV;
        //    ViewBag.sh = sh;
        //    string successMessage = TempData["Loi"] as string;
        //    string xoa = TempData["Xoa"] as string;
        //    ViewBag.message = successMessage;
        //    ViewBag.xoa = xoa;
        //    return View();
        //}
        //public ActionResult ThemVang(int MaSinhHoat, string MaHV)
        //{
        //    var shvang = obj.SinhHoat_Vang.Where(s => s.MaSinhHoat == MaSinhHoat).ToList();
        //    var shoat = obj.SinhHoats.Find(MaSinhHoat);
        //    try
        //    {
        //        foreach (var item in shvang)
        //        {
        //            if (item.MaHV == MaHV)
        //            {
        //                TempData["Loi"] = "Hội viên bạn vừa chọn đã vắng.";
        //                return RedirectToAction("ThemHoiVienVang", new { ma = MaSinhHoat, mahoi = Int32.Parse(Session["MaHoi"].ToString()) });
        //            }
        //        }
        //        SinhHoat_Vang shv = new SinhHoat_Vang();
        //        shv.MaHV = MaHV;
        //        shoat.SoLuongVang = shvang.Count + 1;
        //        shv.MaSinhHoat = MaSinhHoat;
        //        obj.SinhHoat_Vang.Add(shv);
        //        obj.SaveChanges();
        //        return Json(new
        //        {
        //            status = true
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult XoaVang(int ma, int MaSinhHoat)
        //{
        //    try
        //    {
        //        var sinhhoatvang = obj.SinhHoat_Vang.Find(ma);
        //        var shoat = obj.SinhHoats.Find(MaSinhHoat);
        //        obj.SinhHoat_Vang.Remove(sinhhoatvang);
        //        shoat.SoLuongVang = shoat.SoLuongVang - 1;
        //        obj.SaveChanges();
        //        return Json(new
        //        {
        //            status = true,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }

}
