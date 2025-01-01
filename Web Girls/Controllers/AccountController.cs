using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Web_Girls.App_Start;
using Web_Girls.Models.Context;
using Web_Girls.Models.ModelsView;

namespace Web_Girls.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext obj = new MyContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Login()
        {
            ViewBag.Khoa = TempData["Khoa"];
            ViewBag.Sai = TempData["Sai"];
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string tendn, string password)
        {
            string f_mk = GetMD5(password);
            var data = obj.TaiKhoans.Where(s => s.TenDN.Equals(tendn) && s.MatKhau.Equals(f_mk)).FirstOrDefault();

            if (data == null)
            {
                TempData["Sai"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return RedirectToAction("Login");
            }

            if (data.Khoa)
            {
                TempData["Khoa"] = "Tài khoản của bạn đã bị khóa.";
                return RedirectToAction("Login");
            }

            // Lưu thông tin vào Session
            Session["Ma"] = data.MaHV;
            Session["Ten"] = obj.HoiViens.Find(data.MaHV)?.TenHV;
            Session["Quyen"] = data.Quyen;

            // Điều hướng theo quyền
            switch (data.Quyen)
            {
                case 1:
                    return RedirectToAction("TrangQuanTriCapHV");
                case 2:
                    return RedirectToAction("TrangQuanTriCapHoi");
                case 3:
                    return RedirectToAction("TrangQuanTriCaNhan");
                default:
                    TempData["Sai"] = "Quyền không hợp lệ!";
                    return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult DangKy(string tendn, string password, string mahv, bool khoa, int quyen)
        {
            if (obj.TaiKhoans.Any(s => s.TenDN == tendn))
            {
                TempData["Sai"] = "Tên đăng nhập đã tồn tại.";
                return RedirectToAction("Index");
            }

            string hashedPassword = GetMD5(password);

            var newTaiKhoan = new TaiKhoan
            {
                TenDN = tendn,
                MatKhau = hashedPassword,
                MaHV = mahv,
                Khoa = khoa,
                Quyen = quyen,
                EditTime = DateTime.Now
            };

            obj.TaiKhoans.Add(newTaiKhoan);
            obj.SaveChanges();

            TempData["ThanhCong"] = "Đăng ký thành công.";
            return RedirectToAction("TatCaTaiKhoan");
        }
        public JsonResult CheckSession()
        {
            return Json(new { success = Session["Ten"] != null }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [UserAuthorize(quyen = new[] { 1 })]
        public ActionResult TrangQuanTriCapHV()
        {
            return View();
        }

        [UserAuthorize(quyen = new[] { 2 })]
        public ActionResult TrangQuanTriCapHoi()
        {
            return View();
        }

        [UserAuthorize(quyen = new[] { 1, 2, 3 })]
        public ActionResult TrangQuanTriCaNhan()
        {
            return View();
        }

        [UserAuthorize(quyen = new[] { 1 })]
        public ActionResult TatCaTaiKhoan()
        {
            var danhSachTaiKhoan = from a in obj.TaiKhoans
                                   join b in obj.HoiViens on a.MaHV equals b.MaHV
                                   where b.GioiTinh == false
                                   select new TaiKhoanView
                                   {
                                       TenDN = a.TenDN,
                                       MaHV = a.MaHV,
                                       MatKhau = a.MatKhau,
                                       EditTime = a.EditTime,
                                       TenHV = b.TenHV,
                                       Khoa = a.Khoa,
                                       Quyen = a.Quyen
                                   };
            ViewBag.HoiViens = obj.HoiViens
                        .Select(hv => new SelectListItem
                        {
                            Value = hv.MaHV.ToString(),
                            Text = hv.TenHV
                        })
                        .ToList();
            ViewBag.OK = TempData["OK"];
            return View(danhSachTaiKhoan.ToList());
        }

        [HttpPost]
        public JsonResult XoaTaiKhoan(string ma)
        {
            try
            {
                var taiKhoan = obj.TaiKhoans.FirstOrDefault(tk => tk.MaHV == ma);
                if (taiKhoan == null)
                {
                    return Json(new { status = false, message = "Tài khoản không tồn tại." });
                }

                obj.TaiKhoans.Remove(taiKhoan);
                obj.SaveChanges();

                return Json(new { status = true, message = "Xóa tài khoản thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        public ActionResult LockAccount(string ma)
        {
            var tk = obj.TaiKhoans.FirstOrDefault(s => s.MaHV == ma);
            if (tk != null)
            {
                tk.Khoa = true;
                obj.SaveChanges();
            }
            return RedirectToAction("TatCaTaiKhoan");
        }

        public ActionResult UnLockAccount(string ma)
        {
            var tk = obj.TaiKhoans.FirstOrDefault(s => s.MaHV == ma);
            if (tk != null)
            {
                tk.Khoa = false;
                obj.SaveChanges();
            }
            return RedirectToAction("TatCaTaiKhoan");
        }

        [UserAuthorize(quyen = new[] { 1, 2, 3 })]
        public ActionResult ThayDoiMK()
        {
            ViewBag.a = TempData["a"];
            ViewBag.Sai = TempData["Sai"];
            ViewBag.OK = TempData["OK"];
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string Ma, string MatKhauCu, string MatKhauMoi, string MatKhauMoiLai)
        {
            var tk = obj.TaiKhoans.Where(s => s.MaHV == Ma).FirstOrDefault();
            if (GetMD5(MatKhauCu) != tk.MatKhau)
            {
                TempData["a"] = "Mật khẩu hiện tại của bạn không đúng";
                return RedirectToAction("ThayDoiMK");
            }
            if (MatKhauMoi != MatKhauMoiLai)
            {
                TempData["Sai"] = "Mật khẩu nhập lại không đúng";
                return RedirectToAction("ThayDoiMK");
            }

            tk.MatKhau = GetMD5(MatKhauMoi);
            obj.SaveChanges();
            TempData["OK"] = "Thay đổi mật khẩu thành công";
            return RedirectToAction("ThayDoiMK");
        }
        [HttpPost]
        public ActionResult CapNhatQuyen(string TenDN, int Quyen)
        {
            var tk = obj.TaiKhoans.Find(TenDN);
            tk.Quyen = Quyen;
            obj.SaveChanges();
            TempData["OK"] = "Cập nhật thành công";
            return RedirectToAction("TatCaTaiKhoan");
        }
    }
}
