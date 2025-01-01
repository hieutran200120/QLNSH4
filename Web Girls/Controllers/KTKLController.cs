using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.Context;
using Web_Girls.Models.ModelsView;
using Web_Girls.App_Start;

namespace Web_Girls.Controllers
{
    public class KTKLController : Controller
    {
        MyContext obj = new MyContext();
        // GET: KTKL
        [UserAuthorize(quyen = new[] { 1, 2})]
        public ActionResult KhenThuong()
        {
                var khenthuong = (from a in obj.KhenThuongs
                                  join b in obj.HoiViens on a.MaHV equals b.MaHV
                                  join c in obj.DonVis on b.MaDV equals c.MaDV
                                  //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                                  where a.XacNhan == true
                                  orderby a.Nam descending
                                  select new KhenThuongView()
                                  {
                                      Ma = a.Ma,
                                      TenHV = b.TenHV,
                                      Nam = a.Nam,
                                      TenKhenThuong = a.TenKhenThuong,
                                      LyDo = a.LyDo,
                                      DonVi = c.TenDV,
                                      //HPN = d.TenHPN,
                                      GhiChu = a.GhiChu
                                  }).ToList();
                ViewBag.khenthuong = khenthuong;
          
            return View();
        }
        [UserAuthorize(quyen = new[] { 1, 2 })]
        public ActionResult KyLuat()
        {
                var kyluat = (from a in obj.KyLuats
                              join b in obj.HoiViens on a.MaHV equals b.MaHV
                              join c in obj.DonVis on b.MaDV equals c.MaDV
                              //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                              orderby a.Nam descending
                              select new KyLuatView()
                              {
                                  Ma = a.Ma,
                                  TenHV = b.TenHV,
                                  Nam = a.Nam,
                                  HinhThucKL = a.HinhThucKyLuat,
                                  LyDo = a.LyDo,
                                  DonVi = c.TenDV,
                                  //HPN = d.TenHPN,
                                  GhiChu = a.GhiChu
                              }).ToList();
                ViewBag.kyluat = kyluat;           

            return View();
        }
        [UserAuthorize(quyen = new[] { 3 })]
        public ActionResult KyLuatCaNhan(string ma)
        {
            var kyluat = (from a in obj.KyLuats
                          join b in obj.HoiViens on a.MaHV equals b.MaHV
                          join c in obj.DonVis on b.MaDV equals c.MaDV
                          //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                          where a.MaHV == ma
                          orderby a.Nam descending
                          select new KyLuatView()
                          {
                              Ma = a.Ma,
                              TenHV = b.TenHV,
                              Nam = a.Nam,
                              HinhThucKL = a.HinhThucKyLuat,
                              LyDo = a.LyDo,
                              DonVi = c.TenDV,
                              //HPN = d.TenHPN,
                              GhiChu = a.GhiChu
                          }).ToList();
            ViewBag.kyluat = kyluat;
            return View();
        }
        [UserAuthorize(quyen = new[] { 3 })]
        public ActionResult KhenThuongCaNhan(string ma)
        {
            var khenthuong = (from a in obj.KhenThuongs
                              join b in obj.HoiViens on a.MaHV equals b.MaHV
                              join c in obj.DonVis on b.MaDV equals c.MaDV
                              //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                              where a.MaHV == ma 
                              orderby a.Nam descending
                              select new KhenThuongView()
                              {
                                  Ma = a.Ma,
                                  TenHV = b.TenHV,
                                  Nam = a.Nam,
                                  TenKhenThuong = a.TenKhenThuong,
                                  LyDo = a.LyDo,
                                  DonVi = c.TenDV,
                                  //HPN = d.TenHPN,
                                  XacNhan = a.XacNhan,
                                  GhiChu = a.GhiChu
                              }).ToList();
            ViewBag.khenthuong = khenthuong;
            return View();
        }
        public ActionResult XacNhan(int ma)
        {
            var kt = obj.KhenThuongs.Find(ma);
            kt.XacNhan = true;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [UserAuthorize(quyen = new[] { 2 })]
        public ActionResult ThemKL()
        {
            var hoivien = (from a in obj.HoiViens
                           join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                           where a.GioiTinh == false && b.Khoa == false
                           select a).ToList();
            ViewBag.hoivien = hoivien;
            return View();
        }
        [HttpPost]
        public ActionResult ThemKT(KhenThuong kt)
        {
            var ktnew = new KhenThuong();
            ktnew.TenKhenThuong = kt.TenKhenThuong;
            ktnew.LyDo = kt.LyDo;
            ktnew.Nam = kt.Nam;
            ktnew.GhiChu = kt.GhiChu;
            ktnew.MaHV = kt.MaHV;
            ktnew.XacNhan = false;
            obj.KhenThuongs.Add(ktnew);
            obj.SaveChanges();
            return RedirectToAction("KhenThuongCaNhan", new { ma = ktnew.MaHV });
        }
        public ActionResult XoaKT(int ma)
        {
            var kt = obj.KhenThuongs.Find(ma);
            obj.KhenThuongs.Remove(kt);
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult XacNhanKhenThuong()
        {
            var kt =(from a in obj.KhenThuongs
                     join b in obj.HoiViens on a.MaHV equals b.MaHV
                     where  a.XacNhan == false
                     select new KhenThuongView()
                     {
                         Ma = a.Ma,
                         TenHV = b.TenHV,
                         Nam = a.Nam,
                         TenKhenThuong = a.TenKhenThuong,
                         LyDo = a.LyDo,                       
                         GhiChu = a.GhiChu
                     }).ToList();
            ViewBag.kt = kt;
            return View();
        }
        [HttpPost]
        public ActionResult ThemKL(KyLuat kl)
        {
            KyLuat klnew = new KyLuat();
            klnew.MaHV = kl.MaHV;
            klnew.HinhThucKyLuat = kl.HinhThucKyLuat;
            klnew.Nam = kl.Nam;
            klnew.LyDo = kl.LyDo;
            klnew.GhiChu = kl.GhiChu;
            obj.KyLuats.Add(klnew);
            obj.SaveChanges();
            return RedirectToAction("KyLuat", new { ma = Session["MaHoi"] });
        }
        public ActionResult XoaKL(int ma)
        {
            try
            {
                var kl = obj.KyLuats.Find(ma);
                obj.KyLuats.Remove(kl);
                obj.SaveChanges();
                return Json(new
                {
                    status = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}