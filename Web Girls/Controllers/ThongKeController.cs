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
    public class ThongKeController : Controller
    {
        MyContext obj = new MyContext();

        // GET: ThongKe
        [UserAuthorize(quyen = new[] { 1,2 })]
        public ActionResult Index()
        {
            return View();
        }
        [UserAuthorize(quyen = new[] { 1, 2 })]
        public ActionResult ThongKeLDST()
        {
            return View();
        }
        [UserAuthorize(quyen = new[] { 1, 2 })]
        public ActionResult ThongKeKTKL()
        {
            return View();
        }
        [UserAuthorize(quyen = new[] { 1, 2 })]
        public ActionResult ThongKeHCGD()
        {
            return View();
        }

        public JsonResult TKKT()
        {
            //if(id == 0)
            //{
                var listkt = obj.KhenThuongs.Where(s => s.XacNhan == true).ToList();
                var list = new List<ThongKeKTKL>();

                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
                {
                    ThongKeKTKL tk = new ThongKeKTKL();
                    tk.Nam = i;
                    int soluong = 0;
                    foreach (var item in listkt)
                    {
                        if (item.Nam == i) soluong += 1;
                    }
                    tk.SoLuong = soluong;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listkt = (from a in obj.KhenThuongs
            //                 join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                 where a.XacNhan == true 
            //                 select a).ToList();
            //    var list = new List<ThongKeKTKL>();

            //    for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
            //    {
            //        ThongKeKTKL tk = new ThongKeKTKL();
            //        tk.Nam = i;
            //        int soluong = 0;
            //        foreach (var item in listkt)
            //        {
            //            if (item.Nam == i) soluong += 1;
            //        }
            //        tk.SoLuong = soluong;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}
           

        }
        public JsonResult TKKL()
        {
            //if(id == 0)
            //{
                var listkt = obj.KyLuats.ToList();
                var list = new List<ThongKeKTKL>();

                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
                {
                    ThongKeKTKL tk = new ThongKeKTKL();
                    tk.Nam = i;
                    int soluong = 0;
                    foreach (var item in listkt)
                    {
                        if (item.Nam == i) soluong += 1;
                    }
                    tk.SoLuong = soluong;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listkt = (from a in obj.KyLuats
            //                 join b in obj.HoiViens on a.MaHV equals b.MaHV
                         
            //                 select a);
            //    var list = new List<ThongKeKTKL>();

            //    for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
            //    {
            //        ThongKeKTKL tk = new ThongKeKTKL();
            //        tk.Nam = i;
            //        int soluong = 0;
            //        foreach (var item in listkt)
            //        {
            //            if (item.Nam == i) soluong += 1;
            //        }
            //        tk.SoLuong = soluong;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}
           

        }
        public JsonResult TKLDST()
        {
            //if(id == 0)
            //{
                var listldst = obj.LaoDongSangTaos.Where(s => s.XacNhan == true).ToList();
                var list = new List<ThongKeLDST>();

                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
                {
                    ThongKeLDST tk = new ThongKeLDST();
                    tk.Nam = i;
                    int soluong = 0;
                    foreach (var item in listldst)
                    {
                        if (item.Nam == i) soluong += 1;
                    }
                    tk.SoLuong = soluong;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listldst = (from a in obj.LaoDongSangTaos
            //                    join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                    where a.XacNhan == true
            //                    select a).ToList();
            //    var list = new List<ThongKeLDST>();

            //    for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
            //    {
            //        ThongKeLDST tk = new ThongKeLDST();
            //        tk.Nam = i;
            //        int soluong = 0;
            //        foreach (var item in listldst)
            //        {
            //            if (item.Nam == i) soluong += 1;
            //        }
            //        tk.SoLuong = soluong;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}
           

        }
        public JsonResult TKSoLuongHV()
        {
            var listHV = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          where b.Khoa == false && a.GioiTinh == false
                          select a).ToList();
            var thongKe = new ThongKeSoLuongHV
            {
                TenHoiPN = "Tổng số lượng học viên", // Không còn phụ thuộc vào HoiPhuNu
                SoLuongHV = listHV.Count // Đếm số lượng hội viên trong danh sách
            };
            var list = new List<ThongKeSoLuongHV> { thongKe };
            return Json(new
            {
                status = true,
                data = list
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TKLoaiHV()
        {
            //if (id == 0)
            //{
                var listLHV = obj.LoaiHoiViens.ToList();
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
                var list = new List<ThongKeLoaiHV>();
                foreach (var it in listLHV)
                {
                    ThongKeLoaiHV tk = new ThongKeLoaiHV();
                    tk.TenLoaiHV = it.TenLHV;
                    int sum = 0;
                    foreach (var item in listHV)
                    {
                        if (item.MaLHV == it.MaLHV)
                        {
                            sum += 1;
                        }
                    }
                    tk.SoLuongHV = sum;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listLHV = obj.LoaiHoiViens.ToList();
            //    var listHV = (from a in obj.HoiViens
            //                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                  where b.Khoa == false && a.GioiTinh == false 
            //                  select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
            //    var list = new List<ThongKeLoaiHV>();
            //    foreach (var it in listLHV)
            //    {
            //        ThongKeLoaiHV tk = new ThongKeLoaiHV();
            //        tk.TenLoaiHV = it.TenLHV;
            //        int sum = 0;
            //        foreach (var item in listHV)
            //        {
            //            if (item.MaLHV == it.MaLHV)
            //            {
            //                sum += 1;
            //            }
            //        }
            //        tk.SoLuongHV = sum;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);

            //}


        }
        public int? GetTrinhDoGanNhat(string maHV)
        {
            var td = obj.LichSuTrinhDoes.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (td != null)
            {
                return td.MaTD;
            }
            else
            {
                return null;
            }
        }
        public JsonResult TKTrinhDoHV()
        {
            //if (id == 0)
            //{
                var listLHV = obj.TrinhDoes.ToList();
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
                var list = new List<ThongKeTrinhDo>();
                foreach (var it in listLHV)
                {
                    ThongKeTrinhDo tk = new ThongKeTrinhDo();
                    tk.TenTrinhDo = it.TenTD;
                    int sum = 0;
                    foreach (var item in listHV)
                    {
                        if (GetTrinhDoGanNhat(item.MaHV) == it.MaTD)
                        {
                            sum += 1;
                        }
                    }
                    tk.SoLuongHV = sum;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listLHV = obj.TrinhDoes.ToList();
            //    var listHV = (from a in obj.HoiViens
            //                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                  where b.Khoa == false && a.GioiTinh == false 
            //                  select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
            //    var list = new List<ThongKeTrinhDo>();
            //    foreach (var it in listLHV)
            //    {
            //        ThongKeTrinhDo tk = new ThongKeTrinhDo();
            //        tk.TenTrinhDo = it.TenTD;
            //        int sum = 0;
            //        foreach (var item in listHV)
            //        {
            //            if (GetTrinhDoGanNhat(item.MaHV) == it.MaTD)
            //            {
            //                sum += 1;
            //            }
            //        }
            //        tk.SoLuongHV = sum;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}

        }
        public int? GetChucDanhGanNhat(string maHV)
        {
            var td = obj.LichSuChucDanhs.Where(s => s.MaHV == maHV && s.XacNhan == true).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (td != null)
            {
                return td.MaCD;
            }
            else
            {
                return null;
            }
        }
        public JsonResult TKChucDanhHV()
        {
            //if (id == 0)
            //{
                var listLHV = obj.ChucDanhs.ToList();
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
                var list = new List<ThongKeChucDanh>();
                foreach (var it in listLHV)
                {
                    ThongKeChucDanh tk = new ThongKeChucDanh();
                    tk.TenChucDanh = it.TenCD;
                    int sum = 0;
                    foreach (var item in listHV)
                    {
                        if (GetChucDanhGanNhat(item.MaHV) == it.MaCD)
                        {
                            sum += 1;
                        }
                    }
                    tk.SoLuongHV = sum;
                    list.Add(tk);
                }
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listLHV = obj.ChucDanhs.ToList();
            //    var listHV = (from a in obj.HoiViens
            //                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                  where b.Khoa == false && a.GioiTinh == false 
            //                  select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
            //    var list = new List<ThongKeChucDanh>();
            //    foreach (var it in listLHV)
            //    {
            //        ThongKeChucDanh tk = new ThongKeChucDanh();
            //        tk.TenChucDanh = it.TenCD;
            //        int sum = 0;
            //        foreach (var item in listHV)
            //        {
            //            if (GetChucDanhGanNhat(item.MaHV) == it.MaCD)
            //            {
            //                sum += 1;
            //            }
            //        }
            //        tk.SoLuongHV = sum;
            //        list.Add(tk);
            //    }
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}

        }
        public JsonResult TKDangVien()
        {
            //if (id == 0)
            //{
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
                var list = new List<ThongKeTuoi>();
                ThongKeTuoi tk1 = new ThongKeTuoi();
                tk1.DoTuoi = "Đảng viên chính thức";
                int sum = 0;
                foreach (var it in listHV)
                {
                    if (it.MaLDV ==1)
                    {
                        sum += 1;
                    }
                }
                tk1.SoLuongHV = sum;
                list.Add(tk1);

                ThongKeTuoi tk2 = new ThongKeTuoi();
                tk2.DoTuoi = "Đảng viên dự bị";
                int sum1 = 0;
                foreach (var it in listHV)
                {
                    if (it.MaLDV == 2)
                    {
                        sum1 += 1;
                    }
                }
                tk2.SoLuongHV = sum1;
                list.Add(tk2);

                ThongKeTuoi tk3 = new ThongKeTuoi();
                tk3.DoTuoi = "Chưa kết nạp Đảng";
                int sum2 = 0;
                foreach (var it in listHV)
                {
                    if (it.MaLDV == 3 || it.MaLDV ==4)
                    {
                        sum2 += 1;
                    }
                }
                tk3.SoLuongHV = sum2;
                list.Add(tk3);
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var listHV = (from a in obj.HoiViens
            //                  join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //                  where b.Khoa == false && a.GioiTinh == false
            //                  select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
            //    var list = new List<ThongKeTuoi>();
            //    ThongKeTuoi tk1 = new ThongKeTuoi();
            //    tk1.DoTuoi = "Đảng viên chính thức";
            //    int sum = 0;
            //    foreach (var it in listHV)
            //    {
            //        if (it.MaLDV == 1)
            //        {
            //            sum += 1;
            //        }
            //    }
            //    tk1.SoLuongHV = sum;
            //    list.Add(tk1);

            //    ThongKeTuoi tk2 = new ThongKeTuoi();
            //    tk2.DoTuoi = "Đảng viên dự bị";
            //    int sum1 = 0;
            //    foreach (var it in listHV)
            //    {
            //        if (it.MaLDV == 2)
            //        {
            //            sum1 += 1;
            //        }
            //    }
            //    tk2.SoLuongHV = sum1;
            //    list.Add(tk2);

            //    ThongKeTuoi tk3 = new ThongKeTuoi();
            //    tk3.DoTuoi = "Chưa kết nạp Đảng";
            //    int sum2 = 0;
            //    foreach (var it in listHV)
            //    {
            //        if (it.MaLDV == 3 || it.MaLDV ==4)
            //        {
            //            sum2 += 1;
            //        }
            //    }
            //    tk3.SoLuongHV = sum2;
            //    list.Add(tk3);
            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}

        }
        public JsonResult TKDoTuoiHV()
        {
            //if (id == 0)
            //{
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();// lấy danh sách những hội viên mà tài khoản không bị khóa, tức là vẫn công tác ở học viện
                var list = new List<ThongKeTuoi>();
                ThongKeTuoi tk1 = new ThongKeTuoi();
                tk1.DoTuoi = "Từ 18 đến 30 tuôi";
                int sum = 0;
                foreach (var it in listHV)
                {
                    if ((DateTime.Now.Year - it.NgaySinh.Value.Year) >= 18 && (DateTime.Now.Year - it.NgaySinh.Value.Year) <= 30)
                    {
                        sum += 1;
                    }
                }
                tk1.SoLuongHV = sum;
                list.Add(tk1);

                ThongKeTuoi tk2 = new ThongKeTuoi();
                tk2.DoTuoi = "Từ 31 đến 49 tuôi";
                int sum1 = 0;
                foreach (var it in listHV)
                {
                    if ((DateTime.Now.Year - it.NgaySinh.Value.Year) >= 31 && (DateTime.Now.Year - it.NgaySinh.Value.Year) <= 49)
                    {
                        sum1 += 1;
                    }
                }
                tk2.SoLuongHV = sum1;
                list.Add(tk2);

                ThongKeTuoi tk3 = new ThongKeTuoi();
                tk3.DoTuoi = "Trên 50 tuôi";
                int sum2 = 0;
                foreach (var it in listHV)
                {
                    if ((DateTime.Now.Year - it.NgaySinh.Value.Year) >= 50)
                    {
                        sum2 += 1;
                    }
                }
                tk3.SoLuongHV = sum2;
                list.Add(tk3);
                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            

        }
        public JsonResult TKHoanCanhGD()
        {
            
                var listhcgd = (from a in obj.HoanCanhGDs
                                join b in obj.HoiViens on a.MaHV equals b.MaHV
                                join c in obj.TaiKhoans on b.MaHV equals c.MaHV
                                where b.GioiTinh == false && c.Khoa == false
                                select a).ToList();
                var list = new List<ThongKeHCGD>();
                int DaKetHon = 0;
                int LyHon = 0;
                int ChongMat = 0;
                int HoNgheo = 0;
                int HoCanNgheo = 0;
                int ChongCungDonVi = 0;
                int ConBiBenh = 0;
                int MacBenh = 0;
                int ConDuoi16Tuoi = 0;
                int ChuaKetHon = 0;
                foreach (var it in listhcgd)
                {
                    if (it.DaCoChong == true) DaKetHon += 1;
                    else if (it.DaCoChong == false) ChuaKetHon += 1;
                    if (it.LyHon == true) LyHon += 1;
                    if (it.MatChongVo == true) ChongMat += 1;
                    if (it.HoNgheo == true) HoNgheo += 1;
                    if (it.HoCanNgheo == true) HoCanNgheo += 1;
                    if (it.VoChong == true) ChongCungDonVi += 1;
                    if (it.ConBiBenh == true) ConBiBenh += 1;
                    if (it.ConDuoi16Tuoi == true) ConDuoi16Tuoi += 1;
                    if (it.MacBenhHiemNgheo == true) MacBenh += 1;
                }
                ThongKeHCGD tk1 = new ThongKeHCGD();
                tk1.TenHCGD = "Đã kết hôn";
                tk1.SoLuongHV = DaKetHon;
                list.Add(tk1);

                ThongKeHCGD tk2 = new ThongKeHCGD();
                tk2.TenHCGD = "Chưa kết hôn";
                tk2.SoLuongHV = ChuaKetHon;
                list.Add(tk2);

                ThongKeHCGD tk3 = new ThongKeHCGD();
                tk3.TenHCGD = "Ly hôn";
                tk3.SoLuongHV = LyHon;
                list.Add(tk3);

                ThongKeHCGD tk4 = new ThongKeHCGD();
                tk4.TenHCGD = "Chông mất";
                tk4.SoLuongHV = ChongMat;
                list.Add(tk4);

                ThongKeHCGD tk5 = new ThongKeHCGD();
                tk5.TenHCGD = "Hộ nghèo";
                tk5.SoLuongHV = HoNgheo;
                list.Add(tk5);

                ThongKeHCGD tk6 = new ThongKeHCGD();
                tk6.TenHCGD = "Hộ cận nghèo";
                tk6.SoLuongHV = HoCanNgheo;
                list.Add(tk6);

                ThongKeHCGD tk7 = new ThongKeHCGD();
                tk7.TenHCGD = "Chồng cùng đơn vị";
                tk7.SoLuongHV = ChongCungDonVi;
                list.Add(tk7);

                ThongKeHCGD tk8 = new ThongKeHCGD();
                tk8.TenHCGD = "Con bị bệnh";
                tk8.SoLuongHV = ConBiBenh;
                list.Add(tk8);

                ThongKeHCGD tk9 = new ThongKeHCGD();
                tk9.TenHCGD = "Con dưới 16 tuổi";
                tk9.SoLuongHV = ConDuoi16Tuoi;
                list.Add(tk9);

                ThongKeHCGD tk10 = new ThongKeHCGD();
                tk10.TenHCGD = "Măc bệnh hiểm nghèo";
                tk10.SoLuongHV = MacBenh;
                list.Add(tk10);

                return Json(new
                {
                    status = true,
                    data = list
                }, JsonRequestBehavior.AllowGet);
            
            //else
            //{
            //    var listhcgd = (from a in obj.HoanCanhGDs
            //                    join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                    join c in obj.TaiKhoans on b.MaHV equals c.MaHV
            //                    where b.GioiTinh == false && c.Khoa == false 
            //                    select a).ToList();
            //    var list = new List<ThongKeHCGD>();
            //    int DaKetHon = 0;
            //    int LyHon = 0;
            //    int ChongMat = 0;
            //    int HoNgheo = 0;
            //    int HoCanNgheo = 0;
            //    int ChongCungDonVi = 0;
            //    int ConBiBenh = 0;
            //    int MacBenh = 0;
            //    int ConDuoi16Tuoi = 0;
            //    int ChuaKetHon = 0;
            //    foreach (var it in listhcgd)
            //    {
            //        if (it.DaCoChong == true) DaKetHon += 1;
            //        else if (it.DaCoChong == false) ChuaKetHon += 1;
            //        if (it.LyHon == true) LyHon += 1;
            //        if (it.MatChongVo == true) ChongMat += 1;
            //        if (it.HoNgheo == true) HoNgheo += 1;
            //        if (it.HoCanNgheo == true) HoCanNgheo += 1;
            //        if (it.VoChong == true) ChongCungDonVi += 1;
            //        if (it.ConBiBenh == true) ConBiBenh += 1;
            //        if (it.ConDuoi16Tuoi == true) ConDuoi16Tuoi += 1;
            //        if (it.MacBenhHiemNgheo == true) MacBenh += 1;
            //    }
            //    ThongKeHCGD tk1 = new ThongKeHCGD();
            //    tk1.TenHCGD = "Đã kết hôn";
            //    tk1.SoLuongHV = DaKetHon;
            //    list.Add(tk1);

            //    ThongKeHCGD tk2 = new ThongKeHCGD();
            //    tk2.TenHCGD = "Chưa kết hôn";
            //    tk2.SoLuongHV = ChuaKetHon;
            //    list.Add(tk2);

            //    ThongKeHCGD tk3 = new ThongKeHCGD();
            //    tk3.TenHCGD = "Ly hôn";
            //    tk3.SoLuongHV = LyHon;
            //    list.Add(tk3);

            //    ThongKeHCGD tk4 = new ThongKeHCGD();
            //    tk4.TenHCGD = "Chông mất";
            //    tk4.SoLuongHV = ChongMat;
            //    list.Add(tk4);

            //    ThongKeHCGD tk5 = new ThongKeHCGD();
            //    tk5.TenHCGD = "Hộ nghèo";
            //    tk5.SoLuongHV = HoNgheo;
            //    list.Add(tk5);

            //    ThongKeHCGD tk6 = new ThongKeHCGD();
            //    tk6.TenHCGD = "Hộ cận nghèo";
            //    tk6.SoLuongHV = HoCanNgheo;
            //    list.Add(tk6);

            //    ThongKeHCGD tk7 = new ThongKeHCGD();
            //    tk7.TenHCGD = "Chồng cùng đơn vị";
            //    tk7.SoLuongHV = ChongCungDonVi;
            //    list.Add(tk7);

            //    ThongKeHCGD tk8 = new ThongKeHCGD();
            //    tk8.TenHCGD = "Con bị bệnh";
            //    tk8.SoLuongHV = ConBiBenh;
            //    list.Add(tk8);

            //    ThongKeHCGD tk9 = new ThongKeHCGD();
            //    tk9.TenHCGD = "Con dưới 16 tuổi";
            //    tk9.SoLuongHV = ConDuoi16Tuoi;
            //    list.Add(tk9);

            //    ThongKeHCGD tk10 = new ThongKeHCGD();
            //    tk10.TenHCGD = "Măc bệnh hiểm nghèo";
            //    tk10.SoLuongHV = MacBenh;
            //    list.Add(tk10);

            //    return Json(new
            //    {
            //        status = true,
            //        data = list
            //    }, JsonRequestBehavior.AllowGet);
            //}

        }


        // GET: Report



    }
}