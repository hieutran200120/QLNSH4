using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Girls.Models.ModelsView;
using Web_Girls.Models.Context;


namespace Web_Girls.Controllers
{
    public class LDSTController : Controller
    {
        MyContext obj = new MyContext();
        // GET: LDST
        public ActionResult Index()
        {
            //if(ma == 0)
            //{
                var detai = (from a in obj.LaoDongSangTaos
                             join b in obj.LoaiLDSTs on a.MaLoaiLDST equals b.MaLoaiLDST
                             where a.XacNhan == true 
                             select new LaoDongSangTaoView()
                             {
                                 MaHV = a.MaHV,
                                 MaLDST = a.MaLDST,
                                 TenDeTai = a.TenDeTai,
                                 LoaiDeTai = b.TenLoaiLDST,
                                 KetQua = a.KetQua,
                                 Nam = a.Nam
                             }).ToList();
                foreach( var item in detai)
                {
                    string ChuTri = obj.HoiViens.Find(item.MaHV).TenHV;
                    item.ChuTri = ChuTri;
                }
                ViewBag.list = detai;
            //}
            //else
            //{
            //    var detai = (from a in obj.LaoDongSangTaos
            //                 join b in obj.LoaiLDSTs on a.MaLoaiLDST equals b.MaLoaiLDST
            //                 join c in obj.HoiViens on a.MaHV equals c.MaHV
            //                 where a.XacNhan == true
            //                 select new LaoDongSangTaoView()
            //                 {
            //                     MaHV = a.MaHV,
            //                     MaLDST = a.MaLDST,
            //                     TenDeTai = a.TenDeTai,
            //                     LoaiDeTai = b.TenLoaiLDST,
            //                     KetQua = a.KetQua,
            //                     Nam = a.Nam
            //                 }).ToList();
            //    foreach (var item in detai)
            //    {
            //        string ChuTri = obj.HoiViens.Find(item.MaHV).TenHV;             
            //        item.ChuTri = ChuTri;
            //    }
            //    ViewBag.list = detai;
            //}
            
            return View();
        }
        public ActionResult ThamGia(int ma)
        {
            var thamgia = obj.ThamGias.Where(s => s.MaLDST == ma).ToList();
            ViewBag.thamgia = thamgia;
            return PartialView();
        }
        public ActionResult GetDV(string ma)
        {

            string dv = obj.DonVis.Find(obj.HoiViens.Find(ma).MaDV).TenDV;
            return Json(new
            {
                status = true,
                data = dv
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Xoa(int ma)
        {
            try
            {
                var detai = obj.LaoDongSangTaos.Find(ma);
                obj.LaoDongSangTaos.Remove(detai);
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

        public ActionResult ThemLSDT()
        {
            var hoivien = (from a in obj.HoiViens
                           join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                           where a.GioiTinh == false && b.Khoa == false
                           select a).ToList();
            var LoaiLDST = obj.LoaiLDSTs.ToList();
            var donvi = obj.DonVis.ToList();
            ViewBag.dv = donvi;
            ViewBag.hoivien = hoivien;
            ViewBag.loaildst = LoaiLDST;
            return View();
        }
        [HttpPost]
        public ActionResult Them(string TenDeTai, int LoaiLDST, string MaHV, string KetQua, int Nam, List<string> ThanhVien, List<string> DonVi)
        {
            //try
            //{
            var ldst = new LaoDongSangTao();
            ldst.TenDeTai = TenDeTai;
            ldst.Nam = Nam;
            ldst.MaLoaiLDST = LoaiLDST;
            ldst.MaHV = MaHV;
            ldst.KetQua = KetQua;
            ldst.XacNhan = false;
            ThamGia thamgia = new ThamGia();
            var hv = obj.HoiViens.Find(MaHV);
            thamgia.HoTen = hv.TenHV;
            thamgia.VaiTro = "Chủ trì";
            thamgia.MaLDST = ldst.MaLDST;
            thamgia.DonVi = obj.DonVis.Find(hv.MaDV).TenDV;
            thamgia.MaHV = MaHV;
            obj.ThamGias.Add(thamgia);
            if(ThanhVien != null)
            {
                for (int i = 0; i < ThanhVien.Count(); i++)
                {
                    ThamGia tg = new ThamGia();
                    tg.HoTen = ThanhVien[i];
                    tg.DonVi = DonVi[i];
                    tg.MaLDST = ldst.MaLDST;
                    tg.VaiTro = "Tham gia";
                    if (obj.HoiViens.Where(s => s.TenHV == tg.HoTen).FirstOrDefault() != null)
                    {
                        tg.MaHV = obj.HoiViens.Where(s => s.TenHV == tg.HoTen).FirstOrDefault().MaHV;
                    }
                    else
                    {
                        tg.MaHV = "";
                    }
                    obj.ThamGias.Add(tg);
                }
            }           
            obj.LaoDongSangTaos.Add(ldst);
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    return Json(new
            //    {
            //        status = false,
            //    }, JsonRequestBehavior.AllowGet);
            //}
        }
        public string GetlistThamgia(int ma)
        {
            string Tg = "";
            var tg = obj.ThamGias.Where(s => s.MaLDST == ma && s.VaiTro == "Tham gia").ToList();
            foreach( var item in tg)
            {
                Tg += item.HoTen + " (" + item.DonVi + ") ,";
            }
            return Tg; 
        }
        public ActionResult LDSTChuaXacNhan()
        {
            var ldst = (from b in obj.LaoDongSangTaos
                        join c in obj.LoaiLDSTs on b.MaLoaiLDST equals c.MaLoaiLDST
                        join d in obj.HoiViens on b.MaHV equals d.MaHV
                        where b.XacNhan == false
                        select new LaoDongSangTaoView()
                        {
                            MaLDST = b.MaLDST,
                            TenDeTai = b.TenDeTai,
                            MaHV = b.MaHV,
                            LoaiDeTai = c.TenLoaiLDST,
                            KetQua = b.KetQua,
                            Nam = b.Nam
                        }).ToList();
            List<XacNhanLDSTView> list = new List<XacNhanLDSTView>();
            foreach (var item in ldst)
            {
                var a = new XacNhanLDSTView()
                {
                    Ma = item.MaLDST,
                    TenDetai = item.TenDeTai,
                    LoaiDetai = item.LoaiDeTai,
                    KetQua = item.KetQua,
                    Nam = item.Nam,
                    Chutri = obj.HoiViens.Find(item.MaHV).TenHV,
                    ThamGia = GetlistThamgia(item.MaLDST)
                };
                list.Add(a);             
            }
            ViewBag.list = list;
            return View();
        }
        public ActionResult XacNhanLDST(int ma)
        {
            var cb = obj.LaoDongSangTaos.Find(ma);
            cb.XacNhan = true;
            obj.SaveChanges();
            return Json(new
            {
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
