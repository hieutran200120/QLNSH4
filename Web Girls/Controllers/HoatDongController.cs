using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.ModelsView;
using Web_Girls.Models.Context;

namespace Web_Girls.Controllers
{
    public class HoatDongController : Controller
    {
        MyContext obj = new MyContext();
        // GET: HoatDong

        public ActionResult Index()
        {

                var hoatdong = (from a in obj.HoatDongs
                                join b in obj.LoaiHoatDongs on a.MaLoaiHoatDong equals b.MaLoaiHoatDong
                                //join c in obj.HoiPhuNus on a.MaHPN equals c.MaHPN
                                orderby a.ThoiGian descending
                                select new HoatDongView()
                                {
                                    Ma = a.MaHoatDong,
                                    TenHoatDong = a.TenHoatDong,
                                    ThoiGian = a.ThoiGian,
                                    SoTien = a.TongTienThuDuoc,
                                    //MaHoi = a.MaHPN,
                                    MaLoaiHoatDong = b.MaLoaiHoatDong,
                                    LoaiHoatDong = b.TenLoaiHoatDong
                                }).ToList();
                foreach (var item in hoatdong)
                {
                    if (item.MaHoi == 0)
                    {
                        item.DVToChuc = "HPN Học viện";
                    }
                    //else
                    //{
                    //    item.DVToChuc = obj.HoiPhuNus.Find(item.MaHoi).TenHPN;
                    //}
                }
                var loaihoatdong = obj.LoaiHoatDongs.ToList();
                ViewBag.loaihoatdong = loaihoatdong;
                ViewBag.hoatdong = hoatdong;
                return View();
            //}
            //else
            //{
            //    var hoatdong = (from a in obj.HoatDongs
            //                    join b in obj.LoaiHoatDongs on a.MaLoaiHoatDong equals b.MaLoaiHoatDong
            //                    //join c in obj.HoiPhuNus on a.MaHPN equals c.MaHPN
            //                    //where a.MaHPN == ma || a.MaHPN == 0
            //                    orderby a.ThoiGian descending
            //                    select new HoatDongView()
            //                    {
            //                        Ma = a.MaHoatDong,
            //                        TenHoatDong = a.TenHoatDong,
            //                        ThoiGian = a.ThoiGian,
            //                        SoTien = a.TongTienThuDuoc,
            //                        //MaHoi = a.MaHPN,
            //                        MaLoaiHoatDong = b.MaLoaiHoatDong,
            //                        LoaiHoatDong = b.TenLoaiHoatDong
            //                    }).ToList();
            //    foreach (var item in hoatdong)
            //    {
            //        if (item.MaHoi == 0)
            //        {
            //            item.DVToChuc = "HPN Học viện";
            //        }
            //        //else
            //        //{
            //        //    item.DVToChuc = obj.HoiPhuNus.Find(item.MaHoi).TenHPN;
            //        //}
            //    }
            //    var loaihoatdong = obj.LoaiHoatDongs.ToList();
            //    ViewBag.loaihoatdong = loaihoatdong;
            //    ViewBag.hoatdong = hoatdong;
            //    return View();
            //}

        }
        public int? TinhTongTienUngHoCapHoi(int mahoi, int ma)
        {
            var thamgia = (from a in obj.HoatDong_HoiVien
                           join b in obj.HoiViens on a.MaHV equals b.MaHV
                           join c in obj.HoatDongs on a.MaHoatDong equals c.MaHoatDong
                           //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                           where a.MaHoatDong == ma 
                           select a).ToList();
            int? sum = 0;
            foreach (var item in thamgia)
            {
                sum += item.SoTien;
            }
            return sum;

        }
        public ActionResult SuaHoatDong(int ma)
        {
            var hd = obj.HoatDongs.Find(ma);
            ViewBag.hd = hd;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SuaHoatDong( HoatDong hd)
        {
            var hdsua = obj.HoatDongs.Find(hd.MaHoatDong);
            hdsua.TenHoatDong = hd.TenHoatDong;
            if(hdsua.MaLoaiHoatDong == 3)
            {
                hdsua.TongTienThuDuoc = hd.TongTienThuDuoc;
            }
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SuaHoatDongUngHo(int ma)
        {
            //if (mahoi == 0)
            //{
                var hd = obj.HoatDongs.Find(ma);
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();
                var thamgia = (from a in obj.HoatDong_HoiVien
                               join b in obj.HoiViens on a.MaHV equals b.MaHV
                               join c in obj.HoatDongs on a.MaHoatDong equals c.MaHoatDong
                               //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                               where a.MaHoatDong == ma
                               select new ThamGiaUngHoView()
                               {
                                   Ma = a.MaHD_HV,
                                   TenHV = b.TenHV,
                                   //ChiHoi = d.TenHPN,
                                   SoTien = a.SoTien
                               }).ToList();
                ViewBag.hv = listHV;
                ViewBag.hd = hd;
                ViewBag.list = thamgia;
                int? Sotienthuduoc = hd.TongTienThuDuoc;
                ViewBag.tong = Sotienthuduoc;
                string tienbangchu = NumberToText(hd.TongTienThuDuoc);
                ViewBag.tienbangchu = tienbangchu;
                string successMessage = TempData["Loi"] as string;
                string xoa = TempData["Xoa"] as string;
                ViewBag.message = successMessage;
                ViewBag.xoa = xoa;
                return View();
            //}
            //else
            //{
            //    var hd = obj.HoatDongs.Find(ma);
            //    var listHV = (from a in obj.HoiViens
            //                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                  where b.Khoa == false && a.GioiTinh == false
            //                  select a).ToList();
            //    var thamgia = (from a in obj.HoatDong_HoiVien
            //                   join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                   join c in obj.HoatDongs on a.MaHoatDong equals c.MaHoatDong
            //                   //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
            //                   where a.MaHoatDong == ma
            //                   select new ThamGiaUngHoView()
            //                   {
            //                       Ma = a.MaHD_HV,
            //                       TenHV = b.TenHV,
            //                       //ChiHoi = d.TenHPN,
            //                       SoTien = a.SoTien
            //                   }).ToList();
            //    ViewBag.hv = listHV;
            //    ViewBag.hd = hd;
            //    ViewBag.list = thamgia;
            //    int? SoTienCapHoiUngHo = TinhTongTienUngHoCapHoi(mahoi, ma);
            //    int? Sotienthuduoc = SoTienCapHoiUngHo;
            //    ViewBag.tong = Sotienthuduoc;
            //    string tienbangchu = NumberToText(SoTienCapHoiUngHo);
            //    ViewBag.tienbangchu = tienbangchu;
            //    string successMessage = TempData["Loi"] as string;
            //    string xoa = TempData["Xoa"] as string;
            //    ViewBag.message = successMessage;
            //    ViewBag.xoa = xoa;
            //    return View();
            //}

        }
        public static string NumberToText(int? inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.Value.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (result.Contains("mươi một"))
            {
                result = result.Replace("mươi một", " mươi mốt");
            }
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng" : "");
        }

        public ActionResult ChiTietUngHo(int ma)
        {
            var hd = obj.HoatDong_HoiVien.Find(ma);
            ViewBag.hd = hd;
            return PartialView();
        }
        public ActionResult SuaTenHoatDongUngHo(int ma, string TenHoatDong)
        {
            var hd = obj.HoatDongs.Find(ma);
            hd.TenHoatDong = TenHoatDong;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SuaSoTien(int ma, int SoTien)
        {
            var hdhv = obj.HoatDong_HoiVien.Find(ma);
            var hd = obj.HoatDongs.Find(hdhv.MaHoatDong);
            hd.TongTienThuDuoc = hd.TongTienThuDuoc - hdhv.SoTien + SoTien;
            hdhv.SoTien = SoTien;
            obj.SaveChanges();
            return RedirectToAction("SuaHoatDongUngHo", new { ma = hd.MaHoatDong });
        }
        public ActionResult XoaUngHo(int ma)
        {
            var hdhv = obj.HoatDong_HoiVien.Find(ma);
            var hd = obj.HoatDongs.Find(hdhv.MaHoatDong);
            obj.HoatDong_HoiVien.Remove(hdhv);
            hd.TongTienThuDuoc = hd.TongTienThuDuoc - hdhv.SoTien;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ThemUngHo(HoatDong_HoiVien hdhv)
        {
            var list = obj.HoatDong_HoiVien.Where(s => s.MaHoatDong == hdhv.MaHoatDong).ToList();
            var hd = obj.HoatDongs.Find(hdhv.MaHoatDong);
            foreach (var item in list)
            {
                if (item.MaHV == hdhv.MaHV)
                {
                    TempData["Loi"] = "Hội viên bạn vừa chọn đã tham gia ủng hộ.";
                    return RedirectToAction("SuaHoatDongUngHo", new { ma = hd.MaHoatDong });
                }
            }
            HoatDong_HoiVien hdhvnew = new HoatDong_HoiVien();
            hdhvnew.MaHoatDong = hdhv.MaHoatDong;
            hdhvnew.MaHV = hdhv.MaHV;
            hdhvnew.SoTien = hdhv.SoTien;
            hdhvnew.XacNhan = true;
            obj.HoatDong_HoiVien.Add(hdhvnew);
            hd.TongTienThuDuoc += hdhv.SoTien;
            obj.SaveChanges();
            return RedirectToAction("SuaHoatDongUngHo", new { ma = hd.MaHoatDong });
        }

        public ActionResult XoaHoatDong(int ma)
        {
            try
            {
                var hoatdong = obj.HoatDongs.Find(ma);
                obj.HoatDongs.Remove(hoatdong);
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

        public ActionResult ThamGia(int ma)
        {
            var thamgia = (from a in obj.HoatDong_HoiVien
                           join b in obj.HoiViens on a.MaHV equals b.MaHV
                           join c in obj.HoatDongs on a.MaHoatDong equals c.MaHoatDong
                           //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                           where a.MaHoatDong == ma
                           select new ThamGiaUngHoView()
                           {
                               TenHV = b.TenHV,
                               //ChiHoi = d.TenHPN,
                               SoTien = a.SoTien
                           }).ToList();
            ViewBag.thamgia = thamgia;
            return PartialView();
        }

        [HttpPost]
        public ActionResult ThemHoatDong(int MaLoaiHD, string TenHD, int? SoTien, int MaHPN)
        {
            try
            {
                if (MaLoaiHD == 3)
                {
                    HoatDong hd = new HoatDong();
                    hd.MaLoaiHoatDong = MaLoaiHD;
                    hd.TenHoatDong = TenHD;
                    hd.TongTienThuDuoc = SoTien;
                    hd.ThoiGian = DateTime.Now;
                    //hd.MaHPN = MaHPN;
                    obj.HoatDongs.Add(hd);
                    obj.SaveChanges();

                }
                if (MaLoaiHD == 2)
                {
                    HoatDong hd = new HoatDong();
                    hd.MaLoaiHoatDong = MaLoaiHD;
                    hd.TenHoatDong = TenHD;
                    hd.TongTienThuDuoc = null;
                    hd.ThoiGian = DateTime.Now;
                    //hd.MaHPN = MaHPN;
                    obj.HoatDongs.Add(hd);
                    obj.SaveChanges();

                }
                if (MaLoaiHD == 1)
                {
                    HoatDong hd = new HoatDong();
                    hd.MaLoaiHoatDong = MaLoaiHD;
                    hd.TenHoatDong = TenHD;
                    hd.TongTienThuDuoc = 0;
                    hd.ThoiGian = DateTime.Now;
                    //hd.MaHPN = MaHPN;
                    obj.HoatDongs.Add(hd);
                    obj.SaveChanges();

                }
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DanhSachPTTD()
        {
            string Thieu = TempData["Thieu"] as string;
            ViewBag.Thieu = Thieu;
            string TC = TempData["TC"] as string;
            ViewBag.TC = TC;
            var pttd = obj.PhongTraoThiDuas.OrderBy(s => s.Ma).ToList();
            ViewBag.pttd = pttd;
            return View();
        }
        public ActionResult ThemPTTD(PhongTraoThiDua pttd)
        {
            if (pttd.TenPhongTraoThiDua == null || pttd.NgayBatDau == null || pttd.NgayKetThuc == null || pttd.SoLuongDeCu == null)
            {
                TempData["Thieu"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("DanhSachPTTD");
            }
            else
            {
                PhongTraoThiDua pttdnew = new PhongTraoThiDua();
                pttdnew.TenPhongTraoThiDua = pttd.TenPhongTraoThiDua;
                pttdnew.NgayBatDau = pttd.NgayBatDau;
                pttdnew.NgayKetThuc = pttd.NgayKetThuc;
                pttdnew.SoLuongDeCu = pttd.SoLuongDeCu;
                pttdnew.Khoa = false;
                obj.PhongTraoThiDuas.Add(pttdnew);
                obj.SaveChanges();
                TempData["TC"] = "Thêm thành công";
                return RedirectToAction("DanhSachPTTD");
            }
        }
        public ActionResult KhenThuongChuaXacNhan(int ma)
        {
            var kt = obj.DanhSachBieuDuongPTTDs.Where(s => s.MaPTTD == ma && s.XacNhan == false).ToList();
            List<BieuDuongView> list = new List<BieuDuongView>();
            foreach (var item in kt)
            {
                BieuDuongView hv = new BieuDuongView()
                {
                    Ma = item.Ma,
                    TenHV = obj.HoiViens.Find(item.MaHV).TenHV,
                    DonVi = obj.DonVis.Find(obj.HoiViens.Find(item.MaHV).MaDV).TenDV,
                    //ChiHoiPN = obj.HoiPhuNus.Find(obj.HoiViens.Find(item.MaHV).MaHPN).TenHPN
                };
                list.Add(hv);
            }
            var pt = obj.PhongTraoThiDuas.Find(ma);
            ViewBag.pt = pt;
            ViewBag.list = list;
            return View();
        }
        public ActionResult ThemDeNghi(string MaHV, int MaPTTD)
        {
            //int MaHoi = Int32.Parse(Session["MaHoi"].ToString());
            var list = (from a in obj.HoiViens
                        join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                        where b.Khoa == false
                        select a).ToList();
            int soluonghv = list.Count();
            var pttd = obj.PhongTraoThiDuas.Find(MaPTTD);
            int? soluongkhenthuong = soluonghv * pttd.SoLuongDeCu / 100;

            var hv = (from a in obj.DanhSachBieuDuongPTTDs
                      join b in obj.PhongTraoThiDuas on a.MaPTTD equals b.Ma
                      join c in obj.HoiViens on a.MaHV equals c.MaHV
                      where b.Ma == MaPTTD 
                      select a).ToList();

            int soluongkhenthuongchihoi = hv.Count();
            if (pttd.Khoa == false)
            {
                if (soluongkhenthuongchihoi >= soluongkhenthuong)
                {
                    TempData["Thua"] = "Bạn đã đề cử đủ tỉ lệ khen thưởng cho chi hội mình";
                    return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pttd.Ma });
                }
                else
                {
                    foreach (var item in hv)
                    {
                        if (item.MaHV == MaHV)
                        {
                            TempData["Trung"] = "Bạn đã đề nghị hội viên này rồi";
                            return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pttd.Ma });
                        }
                    }
                    DanhSachBieuDuongPTTD ds = new DanhSachBieuDuongPTTD();
                    ds.MaHV = MaHV;
                    ds.MaPTTD = MaPTTD;
                    ds.XacNhan = false;
                    obj.DanhSachBieuDuongPTTDs.Add(ds);
                    obj.SaveChanges();
                    TempData["OK"] = "Đề nghị của bạn đã được gửi đi và đang chờ xác nhận";
                    return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pttd.Ma });
                }

            }
            else if (pttd.Khoa == true)
            {
                TempData["Khoa"] = "Hiện tại người quản trị đã khóa phong trào này lại,vì thế bạn không thể thêm đề nghị";
                return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pttd.Ma});
            }
            return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pttd.Ma });
        }
        public ActionResult XoaPTTD(int ma)
        {
            try
            {
                var hoatdong = obj.PhongTraoThiDuas.Find(ma);
                obj.PhongTraoThiDuas.Remove(hoatdong);
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
        public ActionResult XoaDeNghi(int ma)
        {
            var kt = obj.DanhSachBieuDuongPTTDs.Find(ma);
            var pt = obj.PhongTraoThiDuas.Find(kt.MaPTTD);
            if (pt.Khoa == true)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
                //TempData["DaKhoa"] = "Hiện tại người quản trị đã khóa phong trào này lại, bạn không thể xóa đề nghị của mình";
                //return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pt.Ma, mahoi = Int32.Parse(Session["MaHoi"].ToString()) });
            }
            else
            {
                obj.DanhSachBieuDuongPTTDs.Remove(kt);
                obj.SaveChanges();
                //TempData["DaXoa"] = "Xóa thành công";
                return Json(new
                {
                    status = true,
                }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pt.Ma, mahoi = Int32.Parse(Session["MaHoi"].ToString()) });
            }

        }
        public ActionResult XoaDeNghiKT(int ma)
        {
            var kt = obj.DanhSachBieuDuongPTTDs.Find(ma);
            
            obj.DanhSachBieuDuongPTTDs.Remove(kt);
            obj.SaveChanges();
            //TempData["DaXoa"] = "Xóa thành công";
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pt.Ma, mahoi = Int32.Parse(Session["MaHoi"].ToString()) });
        }
        public ActionResult XacNhanDeNghi(int ma)
        {
            var kt = obj.DanhSachBieuDuongPTTDs.Find(ma);
            kt.XacNhan = true;
            obj.SaveChanges();
            //TempData["DaXoa"] = "Xóa thành công";
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("DanhSachDeNghiBieuDuongChiHoi", new { ma = pt.Ma, mahoi = Int32.Parse(Session["MaHoi"].ToString()) });
        }

        public ActionResult CapNhatPTTD(int ma)
        {
            var pt = obj.PhongTraoThiDuas.Find(ma);
            ViewBag.pt = pt;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CapNhatPTTD(PhongTraoThiDua pttd)
        {
            var pt = obj.PhongTraoThiDuas.Find(pttd.Ma);
            pt.TenPhongTraoThiDua = pttd.TenPhongTraoThiDua;
            pt.NgayBatDau = pttd.NgayBatDau;
            pt.NgayKetThuc = pttd.NgayKetThuc;
            pt.SoLuongDeCu = pt.SoLuongDeCu;
            obj.SaveChanges();
            return RedirectToAction("DanhSachPTTD");
        }


        public ActionResult KhoaPTTD(int ma)
        {
            var pt = obj.PhongTraoThiDuas.Find(ma);
            pt.Khoa = true;
            obj.SaveChanges();
            return RedirectToAction("DanhSachPTTD");
        }
        public ActionResult MoKhoaPTTD(int ma)
        {
            var pt = obj.PhongTraoThiDuas.Find(ma);
            pt.Khoa = false;
            obj.SaveChanges();
            return RedirectToAction("DanhSachPTTD");
        }
        public ActionResult DanhSachBieuDuong(int ma)
        {
            var kt = obj.DanhSachBieuDuongPTTDs.Where(s => s.MaPTTD == ma && s.XacNhan == true).ToList();
            List<BieuDuongView> list = new List<BieuDuongView>();
            foreach (var item in kt)
            {
                BieuDuongView hv = new BieuDuongView()
                {
                    TenHV = obj.HoiViens.Find(item.MaHV).TenHV,
                    DonVi = obj.DonVis.Find(obj.HoiViens.Find(item.MaHV).MaDV).TenDV,
                    //ChiHoiPN = obj.HoiPhuNus.Find(obj.HoiViens.Find(item.MaHV).MaHPN).TenHPN
                };
                list.Add(hv);
            }
            ViewBag.list = list;
            return PartialView();
        }
        //[HttpPost]
        public ActionResult DanhSachDeNghiBieuDuongChiHoi(int ma)
        {

            var list = (from a in obj.HoiViens
                        join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                        where b.Khoa == false 
                        select a).ToList();
            var pt = obj.PhongTraoThiDuas.Find(ma);
            var hv = (from a in obj.DanhSachBieuDuongPTTDs
                      join b in obj.PhongTraoThiDuas on a.MaPTTD equals b.Ma
                      join c in obj.HoiViens on a.MaHV equals c.MaHV
                      where b.Ma == ma 
                      select new BieuDuongView()
                      {
                          Ma = a.Ma,
                          TenHV = c.TenHV,
                          XacNhan = a.XacNhan
                      }).ToList();
            ViewBag.hv = list;
            ViewBag.list = hv;
            ViewBag.pt = pt;
            string thua = TempData["Thua"] as string;
            ViewBag.thua = thua;
            string OK = TempData["OK"] as string;
            ViewBag.OK = OK;
            string Khoa = TempData["Khoa"] as string;
            ViewBag.Khoa = Khoa;
            string DaKhoa = TempData["DaKhoa"] as string;
            ViewBag.DaKhoa = DaKhoa;
            string DaXoa = TempData["DaXoa"] as string;
            ViewBag.DaXoa = DaXoa;
            string Trung = TempData["Trung"] as string;
            ViewBag.Trung = Trung;
            return View();
        }
    }
}