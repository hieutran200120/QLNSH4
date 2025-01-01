using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.Context;
using Web_Girls.Models.ModelsView;

namespace Web_Girls.Controllers
{
    public class ChiHoiController : Controller
    {
        MyContext obj = new MyContext();
        // GET: ChiHoi
        public ActionResult Index()
        {
          
            return View();
        }
        //[HttpPost]
        //public ActionResult ThemChiHoi(string TenHPN)
        //{
        //    HoiPhuNu hpn = new HoiPhuNu();
        //    hpn.TenHPN = TenHPN;
        //    obj.HoiPhuNus.Add(hpn);
        //    obj.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Edit(int Ma)
        //{

        //    var HPN = obj.HoiPhuNus.Find(Ma);
        //    var hoitruong = (from a in obj.HoiViens
        //                     select a).FirstOrDefault();
        //    var listhvhoi = (from a in obj.HoiViens
        //                     join b in obj.TaiKhoans on a.MaHV equals b.MaHV
        //                     where b.Khoa == false && a.MaHPN == Ma
        //                     select a).ToList();          
        //    ViewBag.listhv = listhvhoi;        
        //    //ViewBag.HPN = HPN;
        //    ViewBag.HoiTruong = hoitruong;
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult Edit(HoiPhuNu hpn, string mahoitruongcu, string mahoitruongmoi)
        //{
        //    if(mahoitruongcu == "0")
        //    {
        //        var data = obj.HoiPhuNus.Find(hpn.MaHPN);
        //        data.TenHPN = hpn.TenHPN;
        //        data.MaHV = mahoitruongmoi;
        //        var tk1 = obj.TaiKhoans.Where(s => s.MaHV == mahoitruongmoi).FirstOrDefault();
        //        tk1.Quyen = 2;
        //        obj.SaveChanges();               
        //    }
        //    else if( mahoitruongcu == mahoitruongmoi)// nếu không thay dổi hội trưởng
        //    {
        //        var data = obj.HoiPhuNus.Find(hpn.MaHPN);
        //        data.TenHPN = hpn.TenHPN;
        //        obj.SaveChanges();

        //    }
        //    else if(mahoitruongcu != "0" && mahoitruongcu != mahoitruongmoi)// nếu thay đổi hội trưởng
        //    {
        //        var data = obj.HoiPhuNus.Find(hpn.MaHPN);
        //        data.TenHPN = hpn.TenHPN;
        //        data.MaHV = mahoitruongmoi;
        //        //var it = obj.BCH_HPN.Where(s => s.MaHV == mahoitruongcu).FirstOrDefault();// thay đổi vị trí trong ban chấp hành hội 
        //        //it.ViTri = 2;
        //        //var it1 = obj.BCH_HPN.Where(s => s.MaHV == mahoitruongmoi).FirstOrDefault();
        //        //if( it1!= null)
        //        //{
        //        //    it1.ViTri = 1;
        //        //}else if( it1 == null)
        //        //{
        //        //    BCH_HPN bch = new BCH_HPN();
        //        //    bch.MaHPN = hpn.MaHPN;
        //        //    bch.MaHV = mahoitruongmoi;
        //        //    bch.ViTri = 1;
        //        //    bch.NamBatDau = DateTime.Now.Year;
        //        //    obj.BCH_HPN.Add(bch);
        //        //}
        //        var tk = obj.TaiKhoans.Where(s => s.MaHV == mahoitruongcu).FirstOrDefault(); // thay đổi quyền của tài khoản 1 là quản trị cấp học viên, 2 là quản trị cấp hội tức hội trưởng, 3 là hội viên
        //        tk.Quyen = 3;
        //        var tk1 = obj.TaiKhoans.Where(s => s.MaHV == mahoitruongmoi).FirstOrDefault();
        //        tk1.Quyen = 2;
        //        obj.SaveChanges();

        //    }
        //    return RedirectToAction("Index");

        //}

        public string GetChucDanhGanNhat(string maHV)
        {

            var cd = obj.LichSuChucDanhs.Where(s => s.MaHV == maHV).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (cd != null)
            {
                string cdgn = obj.ChucDanhs.Find(cd.MaCD).TenCD;
                return cdgn;
            }
            else
            {
                return null;
            }


        }
        public string GetDanhHieuGanNhat(string maHV)
        {


            var dh = obj.LichSuDanhHieux.Where(s => s.MaHV == maHV).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (dh != null)
            {
                string dhgn = obj.DanhHieux.Find(dh.MaDH).TenDH;
                return dhgn;
            }
            else
            {
                return null;
            }
        }

        public string GetTrinhDoGanNhat(string maHV)
        {
            var td = obj.LichSuTrinhDoes.Where(s => s.MaHV == maHV).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (td != null)
            {
                string dhgn = obj.TrinhDoes.Find(td.MaTD).TenTD;
                return dhgn;
            }
            else
            {
                return null;
            }
        }
        public ActionResult ThongtinHoiVien()
        {
            var ds = obj.HoiViens.Where(s =>  s.GioiTinh == false).ToList();
            int i = 1;
            List<HoiVienView> listView = new List<HoiVienView>();
            foreach (var it in ds)
            {
                var hv = new HoiVienView()
                {
                    STT = i,
                    MaHV = it.MaHV,
                    TenHV = it.TenHV,
                    NgaySinh = it.NgaySinh,
                    MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = '" + it.MaHV + "' ORDER BY NgayQD DESC").FirstOrDefault(),
                    TrinhDo = GetTrinhDoGanNhat(it.MaHV),
                    DonVi = obj.DonVis.Find(it.MaDV).TenDV
                };
                if (GetChucDanhGanNhat(it.MaHV) != null) hv.ChucDanh = GetChucDanhGanNhat(it.MaHV);
                else hv.ChucDanh = "-";

                if (GetDanhHieuGanNhat(it.MaHV) != null) hv.DanhHieu = GetDanhHieuGanNhat(it.MaHV);
                else hv.DanhHieu = "-";

                if (hv.MaCB == 1) hv.CapBac = "H1";
                else if (hv.MaCB == 2) hv.CapBac = "H2";
                else if (hv.MaCB == 3) hv.CapBac = "H3";
                else if (hv.MaCB == 4) hv.CapBac = "1/";
                else if (hv.MaCB == 5) hv.CapBac = "2/";
                else if (hv.MaCB == 6) hv.CapBac = "3/";
                else if (hv.MaCB == 7) hv.CapBac = "4/";
                else if (hv.MaCB == 8) hv.CapBac = "1//";
                else if (hv.MaCB == 9) hv.CapBac = "2//";
                else if (hv.MaCB == 10) hv.CapBac = "3//";
                else if (hv.MaCB == 11) hv.CapBac = "4//";
                else if (hv.MaCB == 12) hv.CapBac = "Thiếu tướng";
                else if (hv.MaCB == 13) hv.CapBac = "Trung tướng";
                else if (hv.MaCB == 14) hv.CapBac = "Thượng tướng";
                else if (hv.MaCB == 15) hv.CapBac = "Đại tướng";

                if (it.MaLHV == 1) hv.CapBac += "SQ";
                else if (it.MaLHV == 2) hv.CapBac += "CN";
                else if (it.MaLHV == 3) hv.CapBac += "CNVQP";
                else if (it.MaLHV == 4) hv.CapBac += "LĐHD";
                listView.Add(hv);
                i++;
            }
            ViewBag.list = listView;
            //var tench = obj.HoiPhuNus.Find(id).TenHPN;
            //ViewBag.ten = tench;
            string OK = TempData["OK"] as string;
            ViewBag.OK = OK;
            return View();
        }

    }
}