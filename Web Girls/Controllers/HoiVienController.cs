using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.Context;
using Web_Girls.Models.ModelsView;
using System.IO;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Web_Girls.App_Start;
using System.Security.Cryptography;

namespace Web_Girls.Controllers
{
    public class HoiVienController : Controller
    {
        MyContext obj = new MyContext();
        // GET: HoiVien
        public ActionResult Index()
        {
            return View();
        }
        public string GetChucDanhGanNhat(string maHV)
        {

            var cd = obj.LichSuChucDanhs.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(x => x.NgayQD).FirstOrDefault();
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


            var dh = obj.LichSuDanhHieux.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(x => x.NgayQD).FirstOrDefault();
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
            var td = obj.LichSuTrinhDoes.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(x => x.NgayQD).FirstOrDefault();
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
        public string GetCapBacGanNhat(string maHV)
        {
            var td = obj.LichSuCapBacs.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(s => s.NgayQD).FirstOrDefault();
            if (td != null)
            {
                string dhgn = obj.CapBacs.Find(td.MaCB).TenCB;
                return dhgn;
            }
            else
            {
                return null;
            }
        }
        //public int GetViTri(string maHV)
        //{
        //    var td = obj.BCH_HPN.Where(s => s.MaHV == maHV).FirstOrDefault();
        //    if (td != null)
        //    {   
        //        int vt = td.ViTri;
        //        return vt;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        public ActionResult TatCaHoiVien()
        {
            var ds = obj.HoiViens.Where(s => s.GioiTinh == false).ToList();
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
                    MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                    //MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = '" + it.MaHV + "' and XacNhan = true ORDER BY NgayQD DESC").FirstOrDefault(),
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
            return View();
        }

        [HttpPost]
        public JsonResult XoaHV(string ma)
        {
            try
            {
                var hocVien = obj.HoiViens.FirstOrDefault(tk => tk.MaHV == ma);
                if (hocVien == null)
                {
                    return Json(new { status = false, message = "Học viên không tồn tại." });
                }

                obj.HoiViens.Remove(hocVien);
                obj.SaveChanges();

                return Json(new { status = true, message = "Xóa học viên thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        public ActionResult ThongTinHoiVien(string id)
        {
            var hvpn = obj.HoiViens.Find(id);
            var hv = new HoiVienView()
            {
                MaHV = hvpn.MaHV,
                TenHV = hvpn.TenHV,
                NgaySinh = hvpn.NgaySinh,
                NamNhapNgu = hvpn.NamNhapNgu,
                DanToc = hvpn.DanToc,
                Email = hvpn.Email,
                SDT = hvpn.SDT,
                DonVi = obj.DonVis.Find(hvpn.MaDV).TenDV,
                BacTho = hvpn.BacTho,
                avatar = hvpn.avatar,
                QueQuan = hvpn.Quequan,
                NgayVaoDang = hvpn.NgayVaoDang,
                NgayChinhThuc = hvpn.NgayChinhThuc,
                NoiOHienNay = hvpn.Noiohiennay,
                LoaiDangVien = obj.LoaiDangViens.Find(hvpn.MaLDV).TenLDV,
                LoaiHoiVien = obj.LoaiHoiViens.Find(hvpn.MaLHV).TenLHV,
                //ChiHoiPN = obj.HoiPhuNus.Find(hvpn.MaHPN).TenHPN,
                //ViTri = GetViTri(id)
            };
            if (GetTrinhDoGanNhat(hvpn.MaHV) != null) hv.TrinhDo = GetTrinhDoGanNhat(hvpn.MaHV);
            else hv.TrinhDo = "Chưa có thông tin";

            if (GetChucDanhGanNhat(hvpn.MaHV) != null) hv.ChucDanh = GetChucDanhGanNhat(hvpn.MaHV);
            else hv.DanhHieu = "Chưa có thông tin";

            if (GetCapBacGanNhat(hvpn.MaHV) != null) hv.CapBac = GetCapBacGanNhat(hvpn.MaHV);
            else hv.TrinhDo = "Chưa có thông tin";

            if (GetDanhHieuGanNhat(hvpn.MaHV) != null) hv.DanhHieu = GetCapBacGanNhat(hvpn.MaHV);
            else hv.DanhHieu = "Chưa có thông tin";
            ViewBag.hv = hv;
            string OK = TempData["OK"] as string;
            ViewBag.OK = OK;
            return View();
        }
        public ActionResult HoiVienDetails(string id)
        {
            var hvpn = obj.HoiViens.Find(id);
            var hv = new HoiVienView()
            {
                MaHV = hvpn.MaHV,
                TenHV = hvpn.TenHV,
                NgaySinh = hvpn.NgaySinh,
                NamNhapNgu = hvpn.NamNhapNgu,
                DanToc = hvpn.DanToc,
                Email = hvpn.Email,
                SDT = hvpn.SDT,
                DonVi = obj.DonVis.Find(hvpn.MaDV).TenDV,
                BacTho = hvpn.BacTho,
                QueQuan = hvpn.Quequan,
                NoiOHienNay = hvpn.Noiohiennay,
                LoaiDangVien = obj.LoaiDangViens.Find(hvpn.MaLDV).TenLDV,
                LoaiHoiVien = obj.LoaiHoiViens.Find(hvpn.MaLHV).TenLHV,
                //ChiHoiPN = obj.HoiPhuNus.Find(hvpn.MaHPN).TenHPN,
                //ViTri = GetViTri(id)
            };


            if (GetTrinhDoGanNhat(hvpn.MaHV) != null) hv.TrinhDo = GetTrinhDoGanNhat(hvpn.MaHV);
            else hv.TrinhDo = "Chưa có thông tin";

            if (GetChucDanhGanNhat(hvpn.MaHV) != null) hv.ChucDanh = GetChucDanhGanNhat(hvpn.MaHV);
            else hv.ChucDanh = "Chưa có thông tin";

            if (GetCapBacGanNhat(hvpn.MaHV) != null) hv.CapBac = GetCapBacGanNhat(hvpn.MaHV);
            else hv.CapBac = "Chưa có thông tin";

            if (GetDanhHieuGanNhat(hvpn.MaHV) != null) hv.DanhHieu = GetDanhHieuGanNhat(hvpn.MaHV);
            else hv.DanhHieu = "Chưa có thông tin";

            //var hvpn = obj.HoiViens.Find(id);
            //var kq = from a in obj.HoiViens
            //         join b in obj.DonVis on a.MaDV equals b.MaDV
            //         join c in obj.HoiPhuNus on a.MaHPN equals c.MaHPN
            //         join d in obj.BCH_HPN on a.MaHV equals d.MaHV
            //         join e in obj.LoaiDangViens on a.MaLDV equals e.MaLDV
            //         join f in obj.LoaiHoiViens on a.MaLHV equals f.MaLHV
            //         where a.MaHV == id
            //         select new HoiVienView()
            //         {
            //             MaHV = a.MaHV,
            //             TenHV = a.TenHV,
            //             NgaySinh = a.NgaySinh,
            //             NamNhapNgu = a.NamNhapNgu,
            //             DanToc = a.DanToc,
            //             Email = a.Email,
            //             SDT = a.SDT,
            //             BacTho = a.BacTho,
            //             QueQuan = a.Quequan,
            //             NoiOHienNay = a.Noiohiennay,
            //             LoaiDangVien = e.TenLDV,
            //             LoaiHoiVien = f.TenLHV,
            //             ChiHoiPN = c.TenHPN,
            //             ViTri = d.ViTri
            //         };
            //HoiVienView hv = new HoiVienView();
            //hv = kq.FirstOrDefault();
            //if (GetTrinhDoGanNhat(hvpn.MaHV) != null) hv.TrinhDo = GetTrinhDoGanNhat(hvpn.MaHV);
            //else hv.TrinhDo = "Chưa có thông tin";

            //if (GetChucDanhGanNhat(hvpn.MaHV) != null) hv.ChucDanh = GetChucDanhGanNhat(hvpn.MaHV);
            //else hv.DanhHieu = "Chưa có thôn tin";

            //if (GetCapBacGanNhat(hvpn.MaHV) != null) hv.CapBac = GetCapBacGanNhat(hvpn.MaHV);
            //else hv.TrinhDo = "Chưa có thông tin";

            //if (GetDanhHieuGanNhat(hvpn.MaHV) != null) hv.DanhHieu = GetCapBacGanNhat(hvpn.MaHV);
            //else hv.DanhHieu = "Chưa có thông tin";
            ViewBag.hv = hv;
            return View();
        }
        public ActionResult SuaThongTin(string Ma)
        {
            var hv = obj.HoiViens.Find(Ma);
            var dv = obj.DonVis.ToList();
            //var hpn = obj.HoiPhuNus.ToList();
            ViewBag.dv = dv;
            ViewBag.hv = hv;
            //ViewBag.hpn = hpn;
            return View();
        }
        [HttpPost]
        public ActionResult SuaThongTin(HttpPostedFileBase image1, HocVien hv)
        {
            var hvsua = obj.HoiViens.Find(hv.MaHV);
            if (image1 != null && image1.ContentLength > 0)
            {
                string Anh1 = Path.GetFileName(image1.FileName);
                hvsua.avatar = Anh1;
                string path = Path.Combine(Server.MapPath("/Assets/Sbadmin/img/"), Anh1);
                if (System.IO.File.Exists(path))
                {
                }
                else
                {
                    image1.SaveAs(path);
                }
            }
            else
            {
                hvsua.avatar = hvsua.avatar;
            }
            hvsua.TenHV = hv.TenHV;
            hvsua.NgaySinh = hv.NgaySinh;
            hvsua.DanToc = hv.DanToc;
            hvsua.Email = hv.Email;
            hvsua.SDT = hv.SDT;
            hvsua.NgayVaoDang = hv.NgayVaoDang;
            hvsua.NgayChinhThuc = hv.NgayChinhThuc;
            hvsua.MaDV = hv.MaDV;
            hvsua.Quequan = hv.Quequan;
            hvsua.Noiohiennay = hv.Noiohiennay;
            hvsua.NamNhapNgu = hv.NamNhapNgu;
            //if (hvsua.MaHPN != hv.MaHPN)
            //{
            //    var bch = obj.BCH_HPN.Where(s => s.MaHPN == hvsua.MaHPN && s.MaHV == hv.MaHV).FirstOrDefault();
            //    if (bch != null)
            //    {
            //        bch.MaHPN = hv.MaHPN;
            //    }
            //    else
            //    {

            //    }
            //}
            //else
            //{

            //}
            obj.SaveChanges();
            TempData["OK"] = "Cập nhật thông tin thành công";
            return RedirectToAction("ThongTinHoiVien", new { id = hv.MaHV });
        }
        public ActionResult CapBacHoiVien(string id)
        {
            var hv = obj.HoiViens.Find(id);
            var listcb = obj.CapBacs.ToList();
            string CapBacGanNhat = "";
            if (GetCapBacGanNhat(id) != null)
            {
                CapBacGanNhat = GetCapBacGanNhat(id);
            }
            else
            {
                CapBacGanNhat = "Chưa có thông tin";
            }
            var cb = (from a in obj.LichSuCapBacs
                      join b in obj.CapBacs on a.MaCB equals b.MaCB
                      where a.MaHV == id
                      orderby a.NgayQD descending
                      select new LichSuCapBacView()
                      {
                          Ma = a.MaLSCD,
                          TenCapBac = b.TenCB,
                          XacNhan = a.XacNhan,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.listcb = listcb;
            ViewBag.hv = hv;
            ViewBag.CapBacHienTai = CapBacGanNhat;
            ViewBag.list = cb;
            return View();
        }
        public ActionResult ThemLichSuCapBac(string MaHV, int MaCB, DateTime NgayQD)
        {
            var chuaxacnhan = obj.LichSuCapBacs.Where(s => s.MaHV == MaHV && s.XacNhan == false).ToList();
            if (chuaxacnhan.Count() > 0)
            {
                return Json(new
                {
                    status = false,
                    data = "0"
                }, JsonRequestBehavior.AllowGet);
            }
            var list = obj.LichSuCapBacs.Where(s => s.MaHV == MaHV && s.XacNhan == true).OrderByDescending(s => s.NgayQD).ToList();
            foreach (var item in list)
            {
                if (item.MaCB == MaCB)
                {
                    return Json(new
                    {
                        status = false,
                        data = "1"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (list.FirstOrDefault().MaCB < MaCB)
            {
                LichSuCapBac ls = new LichSuCapBac();
                ls.MaHV = MaHV;
                ls.MaCB = MaCB;
                ls.NgayQD = NgayQD;
                ls.XacNhan = false;
                obj.LichSuCapBacs.Add(ls);
                obj.SaveChanges();
                return Json(new
                {
                    status = true,
                    data = "0"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                LichSuCapBac ls = new LichSuCapBac();
                ls.MaHV = MaHV;
                ls.MaCB = MaCB;
                ls.NgayQD = NgayQD;
                ls.XacNhan = true;
                obj.LichSuCapBacs.Add(ls);
                obj.SaveChanges();
                return Json(new
                {
                    status = true,
                    data = "1"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SuaLichSuCapBac(int Ma)
        {
            var ls = obj.LichSuCapBacs.Find(Ma);
            ViewBag.ls = ls;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SuaLichSuCapBac(LichSuCapBac ls)
        {
            var lscb = obj.LichSuCapBacs.Find(ls.MaLSCD);
            lscb.NgayQD = ls.NgayQD;
            obj.SaveChanges();
            return RedirectToAction("CapBacHoiVien", new { id = lscb.MaHV });
        }
        public ActionResult CapBacChuaXacNhan()
        {
            var cb = (from a in obj.LichSuCapBacs
                      join b in obj.HoiViens on a.MaHV equals b.MaHV
                      where  a.XacNhan == false
                      select a).ToList();
            List<XacNhanCapBacView> list = new List<XacNhanCapBacView>();
            foreach (var item in cb)
            {
                var capbac = new XacNhanCapBacView()
                {
                    Ma = item.MaLSCD,
                    TenHV = obj.HoiViens.Find(item.MaHV).TenHV,
                    CapBacCu = GetCapBacGanNhat(item.MaHV),
                    CapBacMoi = obj.CapBacs.Find(item.MaCB).TenCB,
                    NgayQD = item.NgayQD
                };
                list.Add(capbac);
            }
            ViewBag.list = list;
            return View();
        }
        public ActionResult XacNhanCapBac(int ma)
        {
            var cb = obj.LichSuCapBacs.Find(ma);
            cb.XacNhan = true;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult XoaLichSuCapBac(int ma)
        {
            var lscb = obj.LichSuCapBacs.Find(ma);
            obj.LichSuCapBacs.Remove(lscb);
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChucDanhHoiVien(string id)
        {
            var hv = obj.HoiViens.Find(id);
            var listcb = obj.ChucDanhs.ToList();
            var dv = obj.DonVis.ToList();
            string ChucDanhGanNhat = "";
            if (GetChucDanhGanNhat(id) != null)
            {
                ChucDanhGanNhat = GetChucDanhGanNhat(id);
            }
            else if (GetChucDanhGanNhat(id) == null)
            {
                ChucDanhGanNhat = "Chưa có thông tin";
            }
            var cb = (from a in obj.LichSuChucDanhs
                      join b in obj.ChucDanhs on a.MaCD equals b.MaCD
                      join c in obj.DonVis on a.MaDV equals c.MaDV
                      where a.MaHV == id
                      orderby a.NgayQD descending
                      select new LichSuChucDanhView()
                      {
                          Ma = a.MaLSCD,
                          TenChucDanh = b.TenCD,
                          TenDonVi = c.TenDV,
                          XacNhan = a.XacNhan,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.listcb = listcb;
            ViewBag.hv = hv;
            ViewBag.dv = dv;
            ViewBag.CapBacHienTai = ChucDanhGanNhat;
            ViewBag.list = cb;
            return View();
        }
        public ActionResult ThemLichSuChucDanh(string MaHV, int MaDV, int MaCD, DateTime NgayQD)
        {
            var chuaxacnhan = obj.LichSuChucDanhs.Where(s => s.MaHV == MaHV && s.XacNhan == false).ToList();
            if (chuaxacnhan.Count() > 0)
            {
                return Json(new
                {
                    status = false,
                    data = "0"
                }, JsonRequestBehavior.AllowGet);
            }
            var cd = obj.LichSuChucDanhs.Where(s => s.MaHV == MaHV && s.XacNhan == true).OrderByDescending(s => s.NgayQD).ToList();
            foreach (var item in cd)
            {
                if (item.MaCD == MaCD && item.MaDV == MaDV && item.NgayQD == NgayQD)
                {
                    return Json(new
                    {
                        status = false,
                        data = "1"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            LichSuChucDanh ls = new LichSuChucDanh();
            ls.MaHV = MaHV;
            ls.MaCD = MaCD;
            ls.MaDV = MaDV;
            ls.NgayQD = NgayQD;
            ls.XacNhan = false;
            obj.LichSuChucDanhs.Add(ls);
            obj.SaveChanges();
            return Json(new
            {
                status = true,

            }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult SuaLichSuChucDanh(int Ma)
        {
            var dv = obj.DonVis.ToList();
            var ls = obj.LichSuChucDanhs.Find(Ma);
            ViewBag.dv = dv;
            ViewBag.ls = ls;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SuaLichSuChucDanh(LichSuChucDanh ls)
        {
            var lscd = obj.LichSuChucDanhs.Find(ls.MaLSCD);
            lscd.MaDV = ls.MaDV;
            lscd.NgayQD = ls.NgayQD;
            obj.SaveChanges();
            return RedirectToAction("ChucDanhHoiVien", new { id = lscd.MaHV });
        }
        public ActionResult ChucDanhChuaXacNhan()
        {
            var cb = (from a in obj.LichSuChucDanhs
                      join b in obj.HoiViens on a.MaHV equals b.MaHV
                      where   a.XacNhan == false
                      select a).ToList();

            List<XacNhanChucDanhView> list = new List<XacNhanChucDanhView>();

            foreach (var item in cb)
            {
                var lastLichSu = obj.LichSuChucDanhs
                    .Where(s => s.MaHV == item.MaHV && s.XacNhan == true)
                    .OrderByDescending(x => x.NgayQD)
                    .FirstOrDefault();

                var lastDonVi = lastLichSu != null ? obj.DonVis.Find(lastLichSu.MaDV) : null;

                var chucdanh = new XacNhanChucDanhView()
                {
                    Ma = item.MaLSCD,
                    TenHV = obj.HoiViens.Find(item.MaHV)?.TenHV ?? "Không xác định",
                    ChucDanhCu = GetChucDanhGanNhat(item.MaHV),
                    DonViCu = lastDonVi?.TenDV ?? "Không xác định",
                    ChucDanhMoi = obj.ChucDanhs.Find(item.MaCD)?.TenCD ?? "Không xác định",
                    DonViMoi = obj.DonVis.Find(item.MaDV)?.TenDV ?? "Không xác định",
                    NgayQD = item.NgayQD
                };

                list.Add(chucdanh);
            }

            ViewBag.list = list;
            return View();
        }
        public ActionResult XacNhanChucDanh(int ma)
        {
            var cb = obj.LichSuChucDanhs.Find(ma);
            cb.XacNhan = true;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult XoaLichSuChucDanh(int ma)
        {
            var lscd = obj.LichSuChucDanhs.Find(ma);
            obj.LichSuChucDanhs.Remove(lscd);
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TrinhDoHoiVien(string id)
        {
            var hv = obj.HoiViens.Find(id);
            var listtd = obj.TrinhDoes.ToList();
            string TrinhDoGanNhat = "";
            if (GetTrinhDoGanNhat(id) != null)
            {
                TrinhDoGanNhat = GetTrinhDoGanNhat(id);
            }
            else
            {
                TrinhDoGanNhat = "Chưa có thông tin";
            }
            var cb = (from a in obj.LichSuTrinhDoes
                      join b in obj.TrinhDoes on a.MaTD equals b.MaTD
                      where a.MaHV == id
                      orderby a.NgayQD descending
                      select new LichSuTrinhDoView()
                      {
                          Ma = a.MaLSTD,
                          TenTrinhDo = b.TenTD,
                          NganhHoc = a.NganhHoc,
                          TrươngCap = a.TruongCap,
                          XacNhan = a.XacNhan,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.listtd = listtd;
            ViewBag.hv = hv;
            ViewBag.CapBacHienTai = TrinhDoGanNhat;
            ViewBag.list = cb;
            return View();
        }
        public ActionResult ThemLichSuTrinhDo(string MaHV, int MaTD, string ChuyenNganh, string NoiCap, DateTime NgayQD)
        {
            var chuaxacnhan = obj.LichSuTrinhDoes.Where(s => s.MaHV == MaHV && s.XacNhan == false).ToList();
            if (chuaxacnhan.Count() > 0)
            {
                return Json(new
                {
                    status = false,
                    data = "0"
                }, JsonRequestBehavior.AllowGet);
            }
            var cd = obj.LichSuTrinhDoes.Where(s => s.MaHV == MaHV && s.XacNhan == true).OrderByDescending(s => s.NgayQD).ToList();
            foreach (var item in cd)
            {
                if (item.MaTD == MaTD && item.NganhHoc == ChuyenNganh)
                {
                    return Json(new
                    {
                        status = false,
                        data = "1"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            LichSuTrinhDo ls = new LichSuTrinhDo();
            ls.MaHV = MaHV;
            ls.MaTD = MaTD;
            ls.NganhHoc = ChuyenNganh;
            ls.TruongCap = NoiCap;
            ls.NgayQD = NgayQD;
            ls.XacNhan = false;
            obj.LichSuTrinhDoes.Add(ls);
            obj.SaveChanges();
            return Json(new
            {
                status = true,

            }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult SuaLichSuTrinhDo(int Ma)
        {
            var ls = obj.LichSuTrinhDoes.Find(Ma);
            ViewBag.ls = ls;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SuaLichSuTrinhDo(LichSuTrinhDo ls)
        {
            var lscb = obj.LichSuTrinhDoes.Find(ls.MaLSTD);
            lscb.NgayQD = ls.NgayQD;
            obj.SaveChanges();
            return RedirectToAction("TrinhDoHoiVien", new { id = lscb.MaHV });
        }
        public ActionResult TrinhDoChuaXacNhan()
        {
            // Truy vấn danh sách Lịch Sử Trình Độ chưa được xác nhận
            var cb = (from a in obj.LichSuTrinhDoes
                      join b in obj.HoiViens on a.MaHV equals b.MaHV
                      where  a.XacNhan == false
                      select a).ToList();

            // Danh sách để lưu thông tin cần trả về
            List<XacNhanTrinhDoView> list = new List<XacNhanTrinhDoView>();

            foreach (var item in cb)
            {
                // Lấy Lịch Sử Trình Độ trước đó nếu tồn tại
                var lastLichSuTrinhDo = obj.LichSuTrinhDoes
                    .Where(s => s.MaHV == item.MaHV && s.XacNhan == true)
                    .OrderByDescending(x => x.NgayQD)
                    .FirstOrDefault();

                // Xử lý giá trị Trình Độ Cũ và Chuyên Ngành Cũ
                var trinhDoCu = lastLichSuTrinhDo != null ? GetTrinhDoGanNhat(item.MaHV) : "Không xác định";
                var chuyenNganhCu = lastLichSuTrinhDo?.NganhHoc ?? "Không xác định";

                // Xử lý giá trị Trình Độ Mới
                var trinhDoMoi = obj.TrinhDoes.Find(item.MaTD)?.TenTD ?? "Không xác định";

                // Tạo đối tượng Xác Nhận Trình Độ
                var trinhdo = new XacNhanTrinhDoView()
                {
                    Ma = item.MaLSTD,
                    TenHV = obj.HoiViens.Find(item.MaHV)?.TenHV ?? "Không xác định",
                    TrinhDoCu = trinhDoCu,
                    ChuyenNganhCu = chuyenNganhCu,
                    TrinhDoMoi = trinhDoMoi,
                    ChuyenNganhMoi = item.NganhHoc ?? "Không xác định",
                    NgayQD = item.NgayQD
                };

                // Thêm vào danh sách
                list.Add(trinhdo);
            }

            // Gán danh sách vào ViewBag
            ViewBag.list = list;

            // Trả về View
            return View();
        }

        public ActionResult XacNhantrinhDo(int ma)
        {
            var cb = obj.LichSuTrinhDoes.Find(ma);
            cb.XacNhan = true;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult XoaLichSuTrinhDo(int ma)
        {
            var lscd = obj.LichSuTrinhDoes.Find(ma);
            obj.LichSuTrinhDoes.Remove(lscd);
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LDSTHoiVien(string id)
        {
            var hv = obj.HoiViens.Find(id);
            var cb = (from a in obj.ThamGias
                      join b in obj.LaoDongSangTaos on a.MaLDST equals b.MaLDST
                      join c in obj.LoaiLDSTs on b.MaLoaiLDST equals c.MaLoaiLDST
                      where a.MaHV == id
                      select new LDSTView()
                      {
                          Ma = a.Ma,
                          MaLDST = b.MaLDST,
                          TenDeTai = b.TenDeTai,
                          LoaiDeTai = c.TenLoaiLDST,
                          KetQua = b.KetQua,
                          XacNhan = b.XacNhan,
                          VaiTro = a.VaiTro,
                          Nam = b.Nam
                      }).ToList();
            ViewBag.hv = hv;
            ViewBag.list = cb;
            return View();
        }

        //public ActionResult HoiVienChiBo(string ma)
        //{
        //    var hv = obj.HoiViens.Find(ma);
        //    if (hv.MaLDV == 3 || hv.MaLDV == 4)
        //    {
        //        ViewBag.hv = hv;
        //        ViewBag.tb = "Hiện tại bạn chưa là đảng viên nên chức năng này không khả dụng";
        //    }
        //    else
        //    {
        //        var cb = obj.ChiBoes.ToList();

        //        var hvcb = (from a in obj.HoiVien_ChiBo
        //                    join b in obj.HoiViens on a.MaHV equals b.MaHV
        //                    join c in obj.ChiBoes on a.MaChiBo equals c.MaChiBo
        //                    join d in obj.TaiKhoans on b.MaHV equals d.MaHV
        //                    orderby a.BatDau descending
        //                    where a.XacNhan == true && d.Khoa == false && b.MaHV == ma
        //                    select new HoiVienChiBoView()
        //                    {
        //                        Ma = a.MaVTCB,
        //                        TenChiBo = c.TenChiBo,
        //                        ViTri = a.TenVTCB,
        //                        BatDau = a.BatDau,
        //                        KetThuc = a.KetThuc
        //                    }).ToList();
        //        ViewBag.hv = hv;
        //        ViewBag.cb = cb;
        //        ViewBag.list = hvcb;
        //    }
        //    return View();
        //}
        //public ActionResult ThemHoiVienChiBo(string MaHV, int MaChiBo, string ChucVu, DateTime NgayQD)
        //{
        //    var chuaxacnhan = obj.HoiVien_ChiBo.Where(s => s.MaHV == MaHV && s.XacNhan == false).ToList();
        //    if (chuaxacnhan.Count() > 0)
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            data = "0"
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    var list = obj.HoiVien_ChiBo.Where(s => s.MaHV == MaHV && s.XacNhan == true).ToList();
        //    foreach (var item in list)
        //    {
        //        if (item.MaChiBo == MaChiBo && item.TenVTCB == ChucVu && item.BatDau == NgayQD)
        //        {
        //            return Json(new
        //            {
        //                status = false,
        //                data = "1"
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    var chucvuhientai = obj.HoiVien_ChiBo.Where(s => s.MaHV == MaHV && s.XacNhan == true && s.KetThuc == null).FirstOrDefault();
        //    if (chucvuhientai.BatDau > NgayQD)
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            data = "2"
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    HoiVien_ChiBo hv = new HoiVien_ChiBo();
        //    hv.TenVTCB = ChucVu;
        //    hv.MaHV = MaHV;
        //    hv.MaChiBo = MaChiBo;
        //    hv.BatDau = NgayQD;
        //    hv.KetThuc = null;
        //    hv.XacNhan = false;
        //    obj.HoiVien_ChiBo.Add(hv);
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);

        //}
        //public ActionResult HoiVienChiBoChuaXacNhan(int ma)
        //{
        //    var hvcb = (from a in obj.HoiVien_ChiBo
        //                join c in obj.HoiViens on a.MaHV equals c.MaHV
        //                where  a.XacNhan == false
        //                select a).ToList();
        //    List<XacNhanHoiVienChiBo> list = new List<XacNhanHoiVienChiBo>();
        //    foreach (var item in hvcb)
        //    {
        //        var hv = new XacNhanHoiVienChiBo()
        //        {
        //            Ma = item.MaVTCB,
        //            ChiBoCu = obj.ChiBoes.Find(obj.HoiVien_ChiBo.Where(s => s.MaHV == item.MaHV && s.KetThuc == null).FirstOrDefault().MaChiBo).TenChiBo,
        //            ChucVuCu = obj.HoiVien_ChiBo.Where(s => s.MaHV == item.MaHV && s.KetThuc == null).FirstOrDefault().TenVTCB,
        //            ChiBoMoi = obj.ChiBoes.Find(item.MaChiBo).TenChiBo,
        //            ChucVuMoi = item.TenVTCB,
        //            Hoten = obj.HoiViens.Find(item.MaHV).TenHV,
        //            NgayQD = item.BatDau
        //        };
        //        list.Add(hv);
        //    }
        //    ViewBag.list = list;
        //    return View();
        //}
        //public ActionResult XacNhanHoiVienChiBo(int ma)
        //{
        //    var hvcb = obj.HoiVien_ChiBo.Find(ma);
        //    var hvcbcu = obj.HoiVien_ChiBo.Where(s => s.MaHV == hvcb.MaHV && s.KetThuc == null).FirstOrDefault();
        //    hvcbcu.KetThuc = hvcb.BatDau;//Cập nhật lại ngày kết thúc của chức vụ trước đó là ngày bắt đàu của chức vụ mới
        //    hvcb.XacNhan = true;
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult XoaHoiVienChiBo(int ma)
        //{
        //    var hvcb = obj.HoiVien_ChiBo.Find(ma);
        //    obj.HoiVien_ChiBo.Remove(hvcb);
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult HoiVienDangUy(string ma)
        //{
        //    var hv = obj.HoiViens.Find(ma);
        //    if (hv.MaLDV == 3 || hv.MaLDV == 4)
        //    {
        //        ViewBag.hv = hv;
        //        ViewBag.tb = "Hiện tại bạn chưa là đảng viên nên chức năng này không khả dụng";
        //    }
        //    else
        //    {
        //        var cb = obj.DangUys.ToList();

        //        var hvcb = (from a in obj.HoiVien_DangUy
        //                    join b in obj.HoiViens on a.MaHV equals b.MaHV
        //                    join c in obj.DangUys on a.MaDangUy equals c.MaDangUy
        //                    join d in obj.TaiKhoans on b.MaHV equals d.MaHV
        //                    orderby a.BatDau descending
        //                    where a.XacNhan == true && d.Khoa == false && b.MaHV == ma
        //                    select new HoiVienDangUyView()
        //                    {
        //                        Ma = a.MaVTDU,
        //                        TenDangUy = c.TenDangUy,
        //                        ViTri = a.TenVTDU,
        //                        BatDau = a.BatDau,
        //                        KetThuc = a.KetThuc
        //                    }).ToList();
        //        ViewBag.hv = hv;
        //        ViewBag.cb = cb;
        //        ViewBag.list = hvcb;
        //    }
        //    return View();
        //}
        //public ActionResult ThemHoiVienDangUy(string MaHV, int MaDangUy, string ChucVu, DateTime NgayQD)
        //{
        //    var chuaxacnhan = obj.HoiVien_DangUy.Where(s => s.MaHV == MaHV && s.XacNhan == false).ToList();
        //    if (chuaxacnhan.Count() > 0)
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            data = "0"
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    var list = obj.HoiVien_DangUy.Where(s => s.MaHV == MaHV && s.XacNhan == true).ToList();
        //    foreach (var item in list)
        //    {
        //        if (item.MaDangUy == MaDangUy && item.TenVTDU == ChucVu && item.BatDau == NgayQD)
        //        {
        //            return Json(new
        //            {
        //                status = false,
        //                data = "1"
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    var chucvuhientai = obj.HoiVien_DangUy.Where(s => s.MaHV == MaHV && s.XacNhan == true && s.KetThuc == null).FirstOrDefault();
        //    if (chucvuhientai.BatDau > NgayQD)
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            data = "2"
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    HoiVien_DangUy hv = new HoiVien_DangUy();
        //    hv.TenVTDU = ChucVu;
        //    hv.MaHV = MaHV;
        //    hv.MaDangUy = MaDangUy;
        //    hv.BatDau = NgayQD;
        //    hv.KetThuc = null;
        //    hv.XacNhan = false;
        //    obj.HoiVien_DangUy.Add(hv);
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);

        //}
        //[UserAuthorize(quyen = 2)]
        //public ActionResult HoiVienDangUyChuaXacNhan(int ma)
        //{
        //    var hvdu = (from a in obj.HoiVien_DangUy
        //                join b in obj.DangUys on a.MaDangUy equals b.MaDangUy
        //                join c in obj.HoiViens on a.MaHV equals c.MaHV

        //                where  a.XacNhan == false
        //                select new HoiVienDangUyView()
        //                {
        //                    Ma = a.MaVTDU,
        //                    TenDangUy = b.TenDangUy,
        //                    Ten = c.TenHV,
        //                    ViTri = a.TenVTDU,
        //                    BatDau = a.BatDau
        //                }).ToList();
            //List<XacNhanHoiVienDangUy> list = new List<XacNhanHoiVienDangUy>();
            //foreach (var item in hvdu)
            //{
            //    var hv = new XacNhanHoiVienDangUy()
            //    {
            //        Ma = item.MaVTDU,
            //        ChiBoCu = obj.ChiBoes.Find(obj.HoiVien_DangUy.Where(s => s.MaHV == item.MaHV && s.KetThuc == null).FirstOrDefault().MaChiBo).TenChiBo,
            //        ChucVuCu = obj.HoiVien_ChiBo.Where(s => s.MaHV == item.MaHV && s.KetThuc == null).FirstOrDefault().TenVTCB,
            //        ChiBoMoi = obj.ChiBoes.Find(item.MaChiBo).TenChiBo,
            //        ChucVuMoi = item.TenVTCB,
            //        Hoten = obj.HoiViens.Find(item.MaHV).TenHV,
            //        NgayQD = item.BatDau
            //    };
            //    list.Add(hv);
            //}
        //    ViewBag.list = hvdu;
        //    return View();
        //}
        //public ActionResult XacNhanHoiVienDangUy(int ma)
        //{
        //    var hvcb = obj.HoiVien_DangUy.Find(ma);
        //    hvcb.XacNhan = true;
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult XoaHoiVienDangUy(int ma)
        //{
        //    var hvcb = obj.HoiVien_DangUy.Find(ma);
        //    obj.HoiVien_DangUy.Remove(hvcb);
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult LoadChiHoi(string Ma)
        //{

        //    var ChiHoiHienTai = obj.HoiPhuNus.Find(obj.HoiViens.Find(Ma).MaHPN);
        //    var ChiHoi = obj.HoiPhuNus.Where(s => s.MaHPN != ChiHoiHienTai.MaHPN).ToList();
        //    ViewBag.ch = ChiHoiHienTai;
        //    ViewBag.ls = ChiHoi;
        //    ViewBag.ma = Ma;
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult DieuChuyenHoiVien(string MaHV)
        //{
        //    var hv = obj.HoiViens.Find(MaHV);
        //    hv.MaHPN = MaHPN;
        //    obj.SaveChanges();
        //    return Json(new
        //    {
        //        status = true,
        //    }, JsonRequestBehavior.AllowGet);

        //}
        //[UserAuthorize(quyen = 2)] 
        public ActionResult AddHoiVien()
        {
            //var listhpn = obj.HoiPhuNus.ToList();
            //ViewBag.listhpn = listhpn;
            var listlhv = obj.LoaiHoiViens.ToList();
            ViewBag.listlhv = listlhv;
            var listdv = obj.DonVis.ToList();
            ViewBag.listdv = listdv;
            var listcb = obj.CapBacs.ToList();
            ViewBag.listcb = listcb;
            var listldv = obj.LoaiDangViens.ToList();
            ViewBag.listldv = listldv;
            var listdh = obj.DanhHieux.ToList();
            ViewBag.listdh = listdh;
            var listtd = obj.TrinhDoes.ToList();
            ViewBag.listtd = listtd;
            var listcd = obj.ChucDanhs.ToList();
            ViewBag.listcd = listcd;
            string trung = TempData["Trung"] as string;
            ViewBag.trung = trung;
            return View();
        }
        static string ChuanHoaTen(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark && !char.IsWhiteSpace(c))
                {
                    result.Append(c);
                }
            }

            return result.ToString().Normalize(NormalizationForm.FormC);
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;

            //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn thay chữ "X" in hoa trong "X2" thành "x"
        }
        [HttpPost]
        public ActionResult AddHoiVien(HocVien hv, HttpPostedFileBase image1, int MaCB, DateTime? NgayQDCB)
        {
            var list = obj.HoiViens.ToList();
            foreach(var item in list)
            {
                if(item.MaHV == hv.MaHV)
                {
                    TempData["Trung"] = "Số hiệu hội viên đã tồn tại";
                    return RedirectToAction("AddHoiVien");
                }
            }
            var hvmoi = new HocVien();
            hvmoi.MaHV = hv.MaHV;
            hvmoi.TenHV = hv.TenHV;
            hvmoi.NgaySinh = hv.NgaySinh;
            hvmoi.NamNhapNgu = hv.NamNhapNgu;
            hvmoi.SDT = hv.SDT;
            hvmoi.DanToc = hv.DanToc;
            hvmoi.BacTho = hv.BacTho;
            hvmoi.Quequan = hv.Quequan;
            hvmoi.Noiohiennay = hv.Noiohiennay;
            hvmoi.Email = hv.Email;
            hvmoi.DanToc = hv.DanToc;
            hvmoi.GioiTinh = false;
            hvmoi.MaDV = hv.MaDV;
            hvmoi.MaLDV = hv.MaLDV;
            hvmoi.MaLHV = hv.MaLHV;
            if (hvmoi.MaLHV != 3 || hvmoi.MaLHV != 4)
            {
                LichSuCapBac ls = new LichSuCapBac();
                ls.MaCB = MaCB;
                ls.MaHV = hvmoi.MaHV;
                //if (NgayQDCB != null)
                //{
                //    ls.NgayQD = NgayQDCB;
                //}
                //else
                //{
                 ls.NgayQD = null;
                //}
                ls.XacNhan = true;
                obj.LichSuCapBacs.Add(ls);
            }
            //hvmoi.MaHPN = Int32.Parse(Session["MaHoi"].ToString());
            if (image1 != null && image1.ContentLength > 0)
            {
                string Anh1 = Path.GetFileName(image1.FileName);
                hvmoi.avatar = Anh1;
                string path = Path.Combine(Server.MapPath("/Assets/Sbadmin/img/"), Anh1);
                if (System.IO.File.Exists(path))
                {
                }
                else
                {
                    image1.SaveAs(path);
                }
            }
            TaiKhoan tknew = new TaiKhoan();
            tknew.TenDN = hvmoi.MaHV;
            tknew.MatKhau = GetMD5("_" + ChuanHoaTen(hv.TenHV));
            tknew.Quyen = 3;
            tknew.MaHV = hvmoi.MaHV;
            tknew.EditTime = DateTime.Now;
            tknew.Khoa = false;
            obj.HoiViens.Add(hvmoi);
            obj.TaiKhoans.Add(tknew);
            obj.SaveChanges();
            TempData["OK"] = "Thêm mới hội viên thành công";
            return RedirectToAction("ThongtinHoiVien", "ChiHoi", new { id = Session["MaHoi"] });
        }
        [HttpPost]
        public JsonResult XoaHocVien(string ma)
        {
            try
            {
                var hocVien = obj.HoiViens.FirstOrDefault(tk => tk.MaHV == ma);
                if (hocVien == null)
                {
                    return Json(new { status = false, message = "Học viên không tồn tại." });
                }

                obj.HoiViens.Remove(hocVien);
                obj.SaveChanges();

                return Json(new { status = true, message = "Xóa học viên thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = $"Lỗi: {ex.Message}" });
            }
        }
        public ActionResult GetCapBac()
        {
            var cb = obj.CapBacs.ToList();
            ViewBag.listcb = cb;
            return View();
        }
        public ActionResult HoiVienDang()
        {

                var hv = (from a in obj.HoiViens
                          join b in obj.LoaiDangViens on a.MaLDV equals b.MaLDV
                          //join c in obj.HoiPhuNus on a.MaHPN equals c.MaHPN
                          join d in obj.DonVis on a.MaDV equals d.MaDV
                          select new DangVienView()
                          {
                              MaHV = a.MaHV,
                              TenHV = a.TenHV,
                              //TenChiHoi = c.TenHPN,
                              TenDonVi = d.TenDV,
                              LoaiDangVien = b.TenLDV
                          }).ToList();
                var listldv = obj.LoaiDangViens.ToList();
                ViewBag.ldv = listldv;
                ViewBag.list = hv;         
            return View();
        }
        public ActionResult LoaiDangVien(string ma)
        {
            var hv = obj.HoiViens.Find(ma);
            ViewBag.hv = hv;
            var listldv = obj.LoaiDangViens.ToList();
            ViewBag.ldv = listldv;
            return PartialView();
        }
        public ActionResult SuaLoaiDangVien(string MaHV, int MaLDV)
        {
            var hv = obj.HoiViens.Find(MaHV);
            hv.MaLDV = MaLDV;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LichSuCapBacHoiVien(string MaHV)
        {

            var cb = (from a in obj.LichSuCapBacs
                      join b in obj.CapBacs on a.MaCB equals b.MaCB
                      where a.MaHV == MaHV && a.XacNhan == true
                      orderby a.NgayQD descending
                      select new LichSuCapBacView()
                      {
                          TenCapBac = b.TenCB,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.list = cb;
            return PartialView();
        }
        public ActionResult LichSuTrinhDoHV(string MaHV)
        {

            var cb = (from a in obj.LichSuTrinhDoes
                      join b in obj.TrinhDoes on a.MaTD equals b.MaTD
                      where a.MaHV == MaHV && a.XacNhan == true
                      orderby a.NgayQD descending
                      select new LichSuTrinhDoView()
                      {
                          TenTrinhDo = b.TenTD,
                          NganhHoc = a.NganhHoc,
                          TrươngCap = a.TruongCap,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.list = cb;
            return PartialView();
        }
        public ActionResult LichSuChucDanhHV(string MaHV)
        {

            var cb = (from a in obj.LichSuChucDanhs
                      join b in obj.ChucDanhs on a.MaCD equals b.MaCD
                      where a.MaHV == MaHV && a.XacNhan == true
                      orderby a.NgayQD descending
                      select new LichSuChucDanhView()
                      {
                          TenChucDanh = b.TenCD,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.list = cb;
            return PartialView();
        }
        public ActionResult LichSuLDSTV(string MaHV)
        {
            var ldst = (from a in obj.ThamGias
                        join b in obj.LaoDongSangTaos on a.MaLDST equals b.MaLDST
                        join c in obj.LoaiLDSTs on b.MaLoaiLDST equals c.MaLoaiLDST
                        where a.MaHV == MaHV
                        orderby b.Nam descending
                        select new LDSTView()
                        {
                            TenDeTai = b.TenDeTai,
                            LoaiDeTai = c.TenLoaiLDST,
                            KetQua = b.KetQua,
                            VaiTro = a.VaiTro,
                            Nam = b.Nam
                        }).ToList();

            //var ldst = (from a in obj.LaoDongSangTaos
            //            join b in obj.HoiVien_LDST on a.MaLDST equals b.MaLDST
            //            join c in obj.LoaiLDSTs on a.MaLoaiLDST equals c.MaLoaiLDST
            //            where b.MaHV == MaHV
            //            orderby a.Nam descending
            //            select new LaoDongSangTaoView()
            //            {
            //                TenDeTai = a.TenDeTai,
            //                LoaiDeTai = c.TenLoaiLDST,
            //                KetQua = a.KetQua,
            //                VaiTro = b.ThamGia,
            //                Nam = a.Nam
            //            }).ToList();

            ViewBag.list = ldst;
            return PartialView();
        }
        public ActionResult LichSuDanhHieu(string MaHV)
        {

            var cb = (from a in obj.LichSuDanhHieux
                      join b in obj.DanhHieux on a.MaDH equals b.MaDH
                      where a.MaHV == MaHV && a.XacNhan == true
                      orderby a.NgayQD descending
                      select new LichSuDanhHieuView()
                      {
                          TenDanhHieu = b.TenDH,
                          NgayQD = a.NgayQD
                      }).ToList();
            ViewBag.list = cb;
            return PartialView();
        }
        public ActionResult HoanCanhHV()
        {
            //if (ma == 0)
            //{
                var list = (from a in obj.HoiViens
                            join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                            join c in obj.DonVis on a.MaDV equals c.MaDV
                            //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                            where b.Khoa == false && a.GioiTinh == false
                            select new HoiVienView
                            {
                                MaHV = a.MaHV,
                                TenHV = a.TenHV,
                                DonVi = c.TenDV,
                                //ChiHoiPN = d.TenHPN
                            }).ToList();
                var listhongheo = (from a in obj.HoiViens
                                   join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                   join c in obj.DonVis on a.MaDV equals c.MaDV
                                   //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                   join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                   where b.Khoa == false && a.GioiTinh == false && e.HoNgheo == true
                                   select new HoiVienView
                                   {
                                       MaHV = a.MaHV,
                                       TenHV = a.TenHV,
                                       DonVi = c.TenDV,
                                       //ChiHoiPN = d.TenHPN
                                   }).ToList();
                var listhocanngheo = (from a in obj.HoiViens
                                      join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                      join c in obj.DonVis on a.MaDV equals c.MaDV
                                      //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                      join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                      where b.Khoa == false && a.GioiTinh == false && e.HoCanNgheo == true
                                      select new HoiVienView
                                      {
                                          MaHV = a.MaHV,
                                          TenHV = a.TenHV,
                                          DonVi = c.TenDV,
                                          //ChiHoiPN = d.TenHPN
                                      }).ToList();
                var listdakethon = (from a in obj.HoiViens
                                    join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                    join c in obj.DonVis on a.MaDV equals c.MaDV
                                    //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                    join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                    where b.Khoa == false && a.GioiTinh == false && e.DaCoChong == true
                                    select new HoiVienView
                                    {
                                        MaHV = a.MaHV,
                                        TenHV = a.TenHV,
                                        DonVi = c.TenDV,
                                        //ChiHoiPN = d.TenHPN
                                    }).ToList();
                var listchuakethon = (from a in obj.HoiViens
                                      join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                      join c in obj.DonVis on a.MaDV equals c.MaDV
                                      //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                      join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                      where b.Khoa == false && a.GioiTinh == false && e.DaCoChong == false
                                      select new HoiVienView
                                      {
                                          MaHV = a.MaHV,
                                          TenHV = a.TenHV,
                                          DonVi = c.TenDV,
                                          //ChiHoiPN = d.TenHPN
                                      }).ToList();
                var listlyhon = (from a in obj.HoiViens
                                 join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                 join c in obj.DonVis on a.MaDV equals c.MaDV
                                 //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                 join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                 where b.Khoa == false && a.GioiTinh == false && e.LyHon == true
                                 select new HoiVienView
                                 {
                                     MaHV = a.MaHV,
                                     TenHV = a.TenHV,
                                     DonVi = c.TenDV,
                                     //ChiHoiPN = d.TenHPN
                                 }).ToList();
                var listchongcungdonvi = (from a in obj.HoiViens
                                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                          join c in obj.DonVis on a.MaDV equals c.MaDV
                                          //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                          join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                          where b.Khoa == false && a.GioiTinh == false && e.VoChong == true
                                          select new HoiVienView
                                          {
                                              MaHV = a.MaHV,
                                              TenHV = a.TenHV,
                                              DonVi = c.TenDV,
                                              //ChiHoiPN = d.TenHPN
                                          }).ToList();
                var listchongmat = (from a in obj.HoiViens
                                    join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                    join c in obj.DonVis on a.MaDV equals c.MaDV
                                    //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                    join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                    where b.Khoa == false && a.GioiTinh == false && e.MatChongVo == true
                                    select new HoiVienView
                                    {
                                        MaHV = a.MaHV,
                                        TenHV = a.TenHV,
                                        DonVi = c.TenDV,
                                        //ChiHoiPN = d.TenHPN
                                    }).ToList();
                var listconduoi16tuoi = (from a in obj.HoiViens
                                         join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                         join c in obj.DonVis on a.MaDV equals c.MaDV
                                         //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                         join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                         where b.Khoa == false && a.GioiTinh == false && e.ConDuoi16Tuoi == true
                                         select new HoiVienView
                                         {
                                             MaHV = a.MaHV,
                                             TenHV = a.TenHV,
                                             DonVi = c.TenDV,
                                             //ChiHoiPN = d.TenHPN
                                         }).ToList();
                var listbibenh = (from a in obj.HoiViens
                                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                                  join c in obj.DonVis on a.MaDV equals c.MaDV
                                  //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
                                  join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
                                  where b.Khoa == false && a.GioiTinh == false && e.MacBenhHiemNgheo == true
                                  select new HoiVienView
                                  {
                                      MaHV = a.MaHV,
                                      TenHV = a.TenHV,
                                      DonVi = c.TenDV,
                                      //ChiHoiPN = d.TenHPN
                                  }).ToList();
                ViewBag.listbibenh = listbibenh;
                ViewBag.listconduoi16tuoi = listconduoi16tuoi;
                ViewBag.listchongmat = listchongmat;
                ViewBag.listchongcungdonvi = listchongcungdonvi;
                ViewBag.listlyhon = listlyhon;
                ViewBag.listchuakethon = listchuakethon;
                ViewBag.listdakethon = listdakethon;
                ViewBag.listhocanngheo = listhocanngheo;
                ViewBag.listhongheo = listhongheo;
                ViewBag.list = list;
            //}
            //else
            //{
            //    var list = (from a in obj.HoiViens
            //                join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                join c in obj.DonVis on a.MaDV equals c.MaDV
            //                //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                where b.Khoa == false && a.GioiTinh == false
            //                select new HoiVienView
            //                {
            //                    MaHV = a.MaHV,
            //                    TenHV = a.TenHV,
            //                    DonVi = c.TenDV,
            //                    //ChiHoiPN = d.TenHPN
            //                }).ToList();
            //    var listhongheo = (from a in obj.HoiViens
            //                       join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                       join c in obj.DonVis on a.MaDV equals c.MaDV
            //                       //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                       join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                       where b.Khoa == false && a.GioiTinh == false && e.HoNgheo == true 
            //                       select new HoiVienView
            //                       {
            //                           MaHV = a.MaHV,
            //                           TenHV = a.TenHV,
            //                           DonVi = c.TenDV,
            //                           //ChiHoiPN = d.TenHPN
            //                       }).ToList();
            //    var listhocanngheo = (from a in obj.HoiViens
            //                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                          join c in obj.DonVis on a.MaDV equals c.MaDV
            //                          //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                          join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                          where b.Khoa == false && a.GioiTinh == false && e.HoCanNgheo == true
            //                          select new HoiVienView
            //                          {
            //                              MaHV = a.MaHV,
            //                              TenHV = a.TenHV,
            //                              DonVi = c.TenDV,
            //                              //ChiHoiPN = d.TenHPN
            //                          }).ToList();
            //    var listdakethon = (from a in obj.HoiViens
            //                        join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                        join c in obj.DonVis on a.MaDV equals c.MaDV
            //                        //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                        join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                        where b.Khoa == false && a.GioiTinh == false && e.DaCoChong == true 
            //                        select new HoiVienView
            //                        {
            //                            MaHV = a.MaHV,
            //                            TenHV = a.TenHV,
            //                            DonVi = c.TenDV,
            //                            //ChiHoiPN = d.TenHPN
            //                        }).ToList();
            //    var listchuakethon = (from a in obj.HoiViens
            //                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                          join c in obj.DonVis on a.MaDV equals c.MaDV
            //                          //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                          join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                          where b.Khoa == false && a.GioiTinh == false && e.DaCoChong == false
            //                          select new HoiVienView
            //                          {
            //                              MaHV = a.MaHV,
            //                              TenHV = a.TenHV,
            //                              DonVi = c.TenDV,
            //                              //ChiHoiPN = d.TenHPN
            //                          }).ToList();
            //    var listlyhon = (from a in obj.HoiViens
            //                     join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                     join c in obj.DonVis on a.MaDV equals c.MaDV
            //                     //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                     join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                     where b.Khoa == false && a.GioiTinh == false && e.LyHon == true 
            //                     select new HoiVienView
            //                     {
            //                         MaHV = a.MaHV,
            //                         TenHV = a.TenHV,
            //                         DonVi = c.TenDV,
            //                         //ChiHoiPN = d.TenHPN
            //                     }).ToList();
            //    var listchongcungdonvi = (from a in obj.HoiViens
            //                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                              join c in obj.DonVis on a.MaDV equals c.MaDV
            //                              //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                              join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                              where b.Khoa == false && a.GioiTinh == false && e.VoChong == true
            //                              select new HoiVienView
            //                              {
            //                                  MaHV = a.MaHV,
            //                                  TenHV = a.TenHV,
            //                                  DonVi = c.TenDV,
            //                                  //ChiHoiPN = d.TenHPN
            //                              }).ToList();
            //    var listchongmat = (from a in obj.HoiViens
            //                        join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                        join c in obj.DonVis on a.MaDV equals c.MaDV
            //                        //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                        join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                        where b.Khoa == false && a.GioiTinh == false && e.MatChongVo == true
            //                        select new HoiVienView
            //                        {
            //                            MaHV = a.MaHV,
            //                            TenHV = a.TenHV,
            //                            DonVi = c.TenDV,
            //                            //ChiHoiPN = d.TenHPN
            //                        }).ToList();
            //    var listconduoi16tuoi = (from a in obj.HoiViens
            //                             join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                             join c in obj.DonVis on a.MaDV equals c.MaDV
            //                             //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                             join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                             where b.Khoa == false && a.GioiTinh == false && e.ConDuoi16Tuoi == true 
            //                             select new HoiVienView
            //                             {
            //                                 MaHV = a.MaHV,
            //                                 TenHV = a.TenHV,
            //                                 DonVi = c.TenDV,
            //                                 //ChiHoiPN = d.TenHPN
            //                             }).ToList();
            //    var listbibenh = (from a in obj.HoiViens
            //                      join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                      join c in obj.DonVis on a.MaDV equals c.MaDV
            //                      //join d in obj.HoiPhuNus on a.MaHPN equals d.MaHPN
            //                      join e in obj.HoanCanhGDs on a.MaHV equals e.MaHV
            //                      where b.Khoa == false && a.GioiTinh == false && e.MacBenhHiemNgheo == true 
            //                      select new HoiVienView
            //                      {
            //                          MaHV = a.MaHV,
            //                          TenHV = a.TenHV,
            //                          DonVi = c.TenDV,
            //                          //ChiHoiPN = d.TenHPN
            //                      }).ToList();
            //    ViewBag.listbibenh = listbibenh;
            //    ViewBag.listconduoi16tuoi = listconduoi16tuoi;
            //    ViewBag.listchongmat = listchongmat;
            //    ViewBag.listchongcungdonvi = listchongcungdonvi;
            //    ViewBag.listlyhon = listlyhon;
            //    ViewBag.listchuakethon = listchuakethon;
            //    ViewBag.listdakethon = listdakethon;
            //    ViewBag.listhocanngheo = listhocanngheo;
            //    ViewBag.listhongheo = listhongheo;
            //    string OK = TempData["OK"] as string;
            //    ViewBag.OK = OK;
            //    ViewBag.list = list;
            //}
            return View();
        }

        public ActionResult ThongTinHoanCanh(string ma)
        {
            var hc = obj.HoanCanhGDs.Where(s => s.MaHV == ma).FirstOrDefault();
            ViewBag.hc = hc;
            return PartialView();
        }
        public ActionResult CapNhatHoanCanh(string ma)
        {
            var hc = obj.HoanCanhGDs.Where(s => s.MaHV == ma).FirstOrDefault();
            ViewBag.hc = hc;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SuaHoanCanh(string MaHV, string HoanCanh)
        {
            var hccu = obj.HoanCanhGDs.Where(s => s.MaHV == MaHV).FirstOrDefault();
            List<string> stringList = new List<string>();
            if (!string.IsNullOrEmpty(HoanCanh))
            {
                // Tách chuỗi thành mảng các chuỗi, sử dụng dấu phẩy làm dấu phân cách
                string[] stringArray = HoanCanh.Split(',');

                // Thêm từng chuỗi vào danh sách
                foreach (string item in stringArray)
                {
                    stringList.Add(item);
                }
            }
            foreach (var hc in stringList)
            {
                if (hc == "HoNgheo")
                {
                    hccu.HoNgheo = true;
                    break;
                }
                else hccu.HoNgheo = false;
            }
            foreach (var hc in stringList)
            {
                if (hc == "HoCanNgheo")
                {
                    hccu.HoCanNgheo = true;
                    break;
                }
                else hccu.HoCanNgheo = false;
            }
            foreach (var hc in stringList)
            {
                if (hc == "LyHon")
                {
                    hccu.LyHon = true;
                    break;
                }
                else hccu.LyHon = false;
            }
            foreach (var hc in stringList)
            {

                if (hc == "DaCoChong")
                {
                    hccu.DaCoChong = true;
                    break;
                }
                else hccu.DaCoChong = false;

            }
            foreach (var hc in stringList)
            {

                if (hc == "MatChongVo")
                {
                    hccu.MatChongVo = true;
                    break;
                }
                else hccu.MatChongVo = false;

            }
            foreach (var hc in stringList)
            {

                if (hc == "VoChong")
                {
                    hccu.VoChong = true; break;
                }
                else hccu.VoChong = false;

            }
            foreach (var hc in stringList)
            {

                if (hc == "ConDuoi16Tuoi")
                {
                    hccu.ConDuoi16Tuoi = true;
                    break;
                }
                else hccu.ConDuoi16Tuoi = false;

            }
            foreach (var hc in stringList)
            {

                if (hc == "MacBenhHiemNgheo")
                {
                    hccu.MacBenhHiemNgheo = true;
                    break;
                }
                hccu.MacBenhHiemNgheo = false;
            }
            obj.SaveChanges();
            TempData["OK"] = "Cập nhật thành công";
            return RedirectToAction("HoanCanhHV", new { ma = Session["MaHoi"] });
        }

    }
}