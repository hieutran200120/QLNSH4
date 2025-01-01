using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using System.IO;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Web_Girls.Models.Context;
using Web_Girls.Models.ModelsView;
using System.Data.SqlClient;
using System.Text;

namespace Web_Girls.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        MyContext obj = new MyContext();
        string TenHPN = "";
        int SoLuongHV = 0, SiQuan = 0, QNCN = 0, CNVQP = 0, LDHD = 0, HV = 0, DTThieuSo = 0;
        int SoCap = 0, TrungCap = 0, CaoDang = 0, DaiHoc = 0, ThacSi = 0, TienSi = 0;
        int TroGiang = 0, GiangVien = 0, GVC = 0, PhoGS = 0, GS = 0;
        int Bac2 = 0, Bac3 = 0, Bac4 = 0, Bac5 = 0, Bac6 = 0, Bac7 = 0, Bac8 = 0;
        int CNKHoa = 0, PhoCNKhoa = 0, CNBoMon = 0, PhoCNBM = 0, TruongPhong = 0, PhoTP = 0,
        TruongBan = 0, PhoTruongBan = 0, TroLy = 0, NhanVienVanThu = 0, KTV = 0, BepTruong = 0, PhoCt = 0;
        int Tu18den30 = 0, tu31den49 = 0, tren50 = 0;
        int DangVienChinhThuc = 0, DangVienDuBi = 0, ChuaKetNap = 0;


        //List<HoiVienView> listhongheo = new List<HoiVienView>();
        //List<HoiVienView> listhocanngheo = new List<HoiVienView>();
        //List<HoiVienView> listdakethon = new List<HoiVienView>();
        //List<HoiVienView> listchuakethon = new List<HoiVienView>();
        //List<HoiVienView> listlyhon = new List<HoiVienView>();
        //List<HoiVienView> listchongmat = new List<HoiVienView>();
        //List<HoiVienView> listchongcungdonvi = new List<HoiVienView>();
        //List<HoiVienView> listconduoi16tuoi = new List<HoiVienView>();
        //List<HoiVienView> listmacbanh = new List<HoiVienView>();

        public int? GetChucDanhGanNhat(string maHV)
        {
            var td = obj.LichSuChucDanhs.Where(s => s.MaHV == maHV).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (td != null)
            {
                return td.MaCD;
            }
            else
            {
                return null;
            }
        }
        public int? GetTrinhDoGanNhat(string maHV)
        {
            var td = obj.LichSuTrinhDoes.Where(s => s.MaHV == maHV).OrderByDescending(x => x.NgayQD).FirstOrDefault();
            if (td != null)
            {
                return td.MaTD;
            }
            else
            {
                return null;
            }
        }
        public void BaoCao(int MaHoi)
        {
            if (MaHoi == 0) // 0 là báo cáo toàn học viện
            {
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false
                              select a).ToList();
                SoLuongHV = listHV.Count();
                TenHPN = "HPN HVKTQS";
                foreach (var item in listHV)
                {
                    if (item.MaLHV == 1) SiQuan += 1;
                    if (item.MaLHV == 2) QNCN += 1;
                    if (item.MaLHV == 3) CNVQP += 1;
                    if (item.MaLHV == 4) LDHD += 1;
                    if (item.MaLHV == 5) HV += 1;

                    if (item.BacTho == 2) Bac2 += 1;
                    else if (item.BacTho == 3) Bac3 += 1;
                    else if (item.BacTho == 4) Bac4 += 1;
                    else if (item.BacTho == 5) Bac5 += 1;
                    else if (item.BacTho == 6) Bac6 += 1;
                    else if (item.BacTho == 7) Bac7 += 1;
                    else if (item.BacTho == 8) Bac8 += 1;

                    if (item.DanToc != "Kinh") DTThieuSo += 1;

                    if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 18 && (DateTime.Now.Year - item.NgaySinh.Value.Year) <= 30) Tu18den30 += 1;
                    else if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 31 && (DateTime.Now.Year - item.NgaySinh.Value.Year) <= 49) tu31den49 += 1;
                    else if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 50) tren50 += 1;

                    if (GetChucDanhGanNhat(item.MaHV) == 1) CNKHoa += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 2) PhoCNKhoa += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 3) CNBoMon += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 4) PhoCNBM += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 5) TruongPhong += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 6) PhoTP += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 7) TruongBan += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 8) PhoTruongBan += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 9) TroLy += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 10 || GetChucDanhGanNhat(item.MaHV) == 12) NhanVienVanThu += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 13) BepTruong += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 11) KTV += 1;

                    if (item.MaLDV == 1) DangVienChinhThuc += 1;
                    else if (item.MaLDV == 2) DangVienDuBi += 1;
                    else if (item.MaLDV == 3 || item.MaLDV == 4) ChuaKetNap += 1;


                    if (GetTrinhDoGanNhat(item.MaHV) == 1) SoCap += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 2) TrungCap += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 3) CaoDang += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 4) DaiHoc += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 5) ThacSi += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 6) TienSi += 1;
                }
            }
            else
            {
                var listHV = (from a in obj.HoiViens
                              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                              where b.Khoa == false && a.GioiTinh == false 
                              select a).ToList();
                SoLuongHV = listHV.Count();
                //TenHPN = obj.HoiPhuNus.Find(MaHoi).TenHPN;
                foreach (var item in listHV)
                {
                    if (item.MaLHV == 1) SiQuan += 1;
                    if (item.MaLHV == 2) QNCN += 1;
                    if (item.MaLHV == 3) CNVQP += 1;
                    if (item.MaLHV == 4) LDHD += 1;
                    if (item.MaLHV == 5) HV += 1;

                    if (item.BacTho == 2) Bac2 += 1;
                    else if (item.BacTho == 3) Bac3 += 1;
                    else if (item.BacTho == 4) Bac4 += 1;
                    else if (item.BacTho == 5) Bac5 += 1;
                    else if (item.BacTho == 6) Bac6 += 1;
                    else if (item.BacTho == 7) Bac7 += 1;
                    else if (item.BacTho == 8) Bac8 += 1;

                    if (item.DanToc != "Kinh") DTThieuSo += 1;

                    if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 18 && (DateTime.Now.Year - item.NgaySinh.Value.Year) <= 30) Tu18den30 += 1;
                    else if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 31 && (DateTime.Now.Year - item.NgaySinh.Value.Year) <= 49) tu31den49 += 1;
                    else if ((DateTime.Now.Year - item.NgaySinh.Value.Year) >= 50) tren50 += 1;

                    if (GetChucDanhGanNhat(item.MaHV) == 1) CNKHoa += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 2) PhoCNKhoa += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 3) CNBoMon += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 4) PhoCNBM += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 5) TruongPhong += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 6) PhoTP += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 7) TruongBan += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 8) PhoTruongBan += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 9) TroLy += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 10 || GetChucDanhGanNhat(item.MaHV) == 12) NhanVienVanThu += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 13) BepTruong += 1;
                    else if (GetChucDanhGanNhat(item.MaHV) == 11) KTV += 1;

                    if (item.MaLDV == 1) DangVienChinhThuc += 1;
                    else if (item.MaLDV == 2) DangVienDuBi += 1;
                    else if (item.MaLDV == 3 || item.MaLDV == 4) ChuaKetNap += 1;


                    if (GetTrinhDoGanNhat(item.MaHV) == 1) SoCap += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 2) TrungCap += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 3) CaoDang += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 4) DaiHoc += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 5) ThacSi += 1;
                    else if (GetTrinhDoGanNhat(item.MaHV) == 6) TienSi += 1;
                }
            }
        }

        public List<HoiVienView> InDanhSach()
        {
            //if (MaHoi == 0)
            //{
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          where a.GioiTinh == false && b.Khoa == false
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
           // }
            //else
            //{
            //    List<HoiVienView> list = new List<HoiVienView>();
            //    var ds = (from a in obj.HoiViens
            //              join b in obj.TaiKhoans on a.MaHV equals b.MaHV
            //              where a.GioiTinh == false && b.Khoa == false
            //              select a).ToList();
            //    int i = 1;
            //    foreach (var it in ds)
            //    {
            //        var hv = new HoiVienView()
            //        {
            //            STT = i,
            //            TenHV = it.TenHV,
            //            NgaySinh = it.NgaySinh,
            //            MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
            //            DonVi = obj.DonVis.Find(it.MaDV).TenDV,
            //            //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN
            //        };
            //        if (hv.MaCB == 1) hv.CapBac = "H1";
            //        else if (hv.MaCB == 2) hv.CapBac = "H2";
            //        else if (hv.MaCB == 3) hv.CapBac = "H3";
            //        else if (hv.MaCB == 4) hv.CapBac = "1/";
            //        else if (hv.MaCB == 5) hv.CapBac = "2/";
            //        else if (hv.MaCB == 6) hv.CapBac = "3/";
            //        else if (hv.MaCB == 7) hv.CapBac = "4/";
            //        else if (hv.MaCB == 8) hv.CapBac = "1//";
            //        else if (hv.MaCB == 9) hv.CapBac = "2//";
            //        else if (hv.MaCB == 10) hv.CapBac = "3//";
            //        else if (hv.MaCB == 11) hv.CapBac = "4//";
            //        else if (hv.MaCB == 12) hv.CapBac = "Thiếu tướng";
            //        else if (hv.MaCB == 13) hv.CapBac = "Trung tướng";
            //        else if (hv.MaCB == 14) hv.CapBac = "Thượng tướng";
            //        else if (hv.MaCB == 15) hv.CapBac = "Đại tướng";

            //        if (it.MaLHV == 1) hv.CapBac += "SQ";
            //        else if (it.MaLHV == 2) hv.CapBac += "CN";
            //        else if (it.MaLHV == 3) hv.CapBac += "CNVQP";
            //        else if (it.MaLHV == 4) hv.CapBac += "LĐHD";
            //        list.Add(hv);
            //        i++;
            //    }
            //    return list;
            //}

        }
        //public string GetTen(int Ma)
        //{
        //    if (Ma == 0)
        //    {
        //        return "HPN HVKTQS";
        //    }
        //    else
        //    {
        //        return obj.HoiPhuNus.Find(Ma).TenHPN;
        //    }
        //}
        [Obsolete]
        public ActionResult InDanhSachHV()
        {
            // Load data and process as needed
            List<HoiVienView> list = InDanhSach();

            // Paths for template and output files
            string templatePath = Server.MapPath("~/Data/Templates/test2.docx");
            string outputPath = Server.MapPath("~/Data/Templates/DanhsachHV.docx");

            // Load the document from the template
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholder with actual value
                //document.ReplaceText("[TenHPN]", GetTen(Ma));

                // Find and populate the table in the document
                Table table = document.Tables[0]; // Assuming the table is the first table in the document
                foreach (var item in list)
                {
                    Row newRow = table.InsertRow(); // Insert a new row into the table

                    // Populate cells in the new row
                    newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                    newRow.Cells[1].Paragraphs.First().Append(item.TenHV).Alignment = Alignment.both;
                    newRow.Cells[2].Paragraphs.First().Append(item.NgaySinh?.ToString("dd/MM/yyyy")).Alignment = Alignment.center;
                    newRow.Cells[3].Paragraphs.First().Append(item.CapBac).Alignment = Alignment.center;
                    newRow.Cells[4].Paragraphs.First().Append(item.DonVi).Alignment = Alignment.center;
                    newRow.Cells[5].Paragraphs.First().Append(item.ChiHoiPN).Alignment = Alignment.center;
                }

                // Save the modified document to the output path
                document.SaveAs(outputPath);
            }

            // Convert the DOCX file to HTML for rendering
            string htmlPath = Server.MapPath("~/Data/Templates/render_DanhSach.html");
            ConvertDocxToHtml(outputPath, htmlPath);

            // Read the HTML content to display in the view
            string htmlContent = System.IO.File.ReadAllText(htmlPath);
            ViewBag.DocumentContent = htmlContent;

            return View();
        }

        // Helper method to convert DOCX to HTML
        private void ConvertDocxToHtml(string inputDocxPath, string outputHtmlPath)
        {
            using (DocX doc = DocX.Load(inputDocxPath))
            {
                StringBuilder htmlBuilder = new StringBuilder();

                // Start HTML document with <html>, <head>, and <body> tags
                htmlBuilder.Append("<!DOCTYPE html><html><head>");
                // Add CSS for table alignment, borders, and equal width for first row cells
                htmlBuilder.Append("<style>");
                htmlBuilder.Append("table { width: 100%; border-collapse: collapse; }");
                htmlBuilder.Append("th, td { padding: 8px; text-align: center; }");
                htmlBuilder.Append("tr:not(:first-child) th, tr:not(:first-child) td { border: 1px solid black; }");
                htmlBuilder.Append("</style>");
                htmlBuilder.Append("</head><body>");

                foreach (Table table in doc.Tables)
                {
                    // Calculate the total width of all cells in the first row
                    double totalWidth = 0;

                    // Iterate through the cells in the first row to calculate total width
                    foreach (Cell cell in table.Rows.First().Cells)
                    {
                        totalWidth += cell.Width;
                    }

                    htmlBuilder.Append("<table>");

                    int columnIndex = 0; // Variable to track the column index

                    foreach (Row row in table.Rows)
                    {
                        htmlBuilder.Append("<tr>");

                        foreach (Cell cell in row.Cells)
                        {
                            // Calculate the percentage width of each cell
                            double cellWidthPercentage;

                           
                                // Calculate width percentage based on original width
                                cellWidthPercentage = (cell.Width / totalWidth) * 100;
                          

                            htmlBuilder.Append("<td style=\"width:200px ");
                            htmlBuilder.Append(cellWidthPercentage.ToString("0.##")); // Format to two decimal places
                            htmlBuilder.Append("%;\">");

                            foreach (Xceed.Document.NET.Paragraph paragraph in cell.Paragraphs)
                            {
                                htmlBuilder.Append("<p>");
                                htmlBuilder.Append(paragraph.Text);
                                htmlBuilder.Append("</p>");
                            }

                            htmlBuilder.Append("</td>");

                            columnIndex++; // Increment the column index
                        }

                        htmlBuilder.Append("</tr>");
                        columnIndex = 0; // Reset column index at the end of each row
                    }

                    htmlBuilder.Append("</table>");
                }

                htmlBuilder.Append("</body></html>");

                // Write HTML string to output file
                using (StreamWriter writer = new StreamWriter(outputHtmlPath))
                {
                    writer.Write(htmlBuilder.ToString());
                }
            }
        }

        public string GetlistThamgia(int ma)
        {
            string Tg = "";
            var tg = obj.ThamGias.Where(s => s.MaLDST == ma && s.VaiTro == "Tham gia").ToList();
            foreach (var item in tg)
            {
                Tg += item.HoTen + " (" + item.DonVi + ") ,";
            }
            return Tg;
        }
        public List<XacNhanLDSTView> DanhsachLDST( int? nam)
        {
            //if (ma == 0)
            //{
                List<XacNhanLDSTView> list = new List<XacNhanLDSTView>();
                var ldst = (from b in obj.LaoDongSangTaos
                            join c in obj.LoaiLDSTs on b.MaLoaiLDST equals c.MaLoaiLDST
                            join d in obj.HoiViens on b.MaHV equals d.MaHV
                            where b.XacNhan == true && b.Nam == nam
                            select new LaoDongSangTaoView()
                            {
                                MaLDST = b.MaLDST,
                                TenDeTai = b.TenDeTai,
                                MaHV = b.MaHV,
                                LoaiDeTai = c.TenLoaiLDST,
                                KetQua = b.KetQua,
                                Nam = b.Nam
                            }).ToList();
                int i = 1;
                foreach (var item in ldst)
                {
                    var a = new XacNhanLDSTView()
                    {
                        STT = i,
                        Ma = item.MaLDST,
                        TenDetai = item.TenDeTai,
                        LoaiDetai = item.LoaiDeTai,
                        KetQua = item.KetQua,
                        Nam = item.Nam,
                        Chutri = obj.HoiViens.Find(item.MaHV).TenHV,
                        ThamGia = GetlistThamgia(item.MaLDST)
                    };
                    list.Add(a);
                    i++;
                }

                return list;

            //}
            //else
            //{
            //    List<XacNhanLDSTView> list = new List<XacNhanLDSTView>();
            //    var ldst = (from b in obj.LaoDongSangTaos
            //                join c in obj.LoaiLDSTs on b.MaLoaiLDST equals c.MaLoaiLDST
            //                join d in obj.HoiViens on b.MaHV equals d.MaHV
            //                where  b.XacNhan == true && b.Nam == nam
            //                select new LaoDongSangTaoView()
            //                {
            //                    MaLDST = b.MaLDST,
            //                    TenDeTai = b.TenDeTai,
            //                    MaHV = b.MaHV,
            //                    LoaiDeTai = c.TenLoaiLDST,
            //                    KetQua = b.KetQua,
            //                    Nam = b.Nam
            //                }).ToList();
            //    int i = 1;
            //    foreach (var item in ldst)
            //    {

            //        var a = new XacNhanLDSTView()
            //        {
            //            Ma = item.MaLDST,
            //            TenDetai = item.TenDeTai,
            //            LoaiDetai = item.LoaiDeTai,
            //            KetQua = item.KetQua,
            //            Nam = item.Nam,
            //            Chutri = obj.HoiViens.Find(item.MaHV).TenHV,
            //            ThamGia = GetlistThamgia(item.MaLDST)
            //        };
            //        list.Add(a);
            //        i++;
            //    }

            //    return list;
           // }
        }
        public List<KhenThuongView> Danhsachkhenthuong( int? nam)
        {
            //if (ma == 0)
            //{
                List<KhenThuongView> list = new List<KhenThuongView>();
                var khenthuong = (from a in obj.KhenThuongs
                                  join b in obj.HoiViens on a.MaHV equals b.MaHV
                                  join c in obj.DonVis on b.MaDV equals c.MaDV
                                  //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                                  where a.XacNhan == true && a.Nam == nam
                                  select new KhenThuongView()
                                  {
                                      TenHV = b.TenHV,
                                      Nam = a.Nam,
                                      TenKhenThuong = a.TenKhenThuong,
                                      LyDo = a.LyDo,
                                      DonVi = c.TenDV,
                                      //HPN = d.TenHPN,
                                      GhiChu = a.GhiChu
                                  }).ToList();
                list = khenthuong;
                int i = 1;
                foreach (var item in list)
                {
                    item.STT = i;
                    i++;
                }
                return list;
            //}
            //else
            //{
            //    List<KhenThuongView> list = new List<KhenThuongView>();
            //    var khenthuong = (from a in obj.KhenThuongs
            //                      join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                      join c in obj.DonVis on b.MaDV equals c.MaDV
            //                      //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
            //                      where a.XacNhan == true && a.Nam == nam
            //                      select new KhenThuongView()
            //                      {

            //                          TenHV = b.TenHV,
            //                          Nam = a.Nam,
            //                          TenKhenThuong = a.TenKhenThuong,
            //                          LyDo = a.LyDo,
            //                          DonVi = c.TenDV,
            //                          //HPN = d.TenHPN,
            //                          GhiChu = a.GhiChu
            //                      }).ToList();
            //    list = khenthuong;
            //    int i = 1;
            //    foreach (var item in list)
            //    {
            //        item.STT = i;
            //        i++;
            //    }
            //    return list;
            //}
        }
        [HttpPost]
        [Obsolete]
        public ActionResult InDanhSachKhenThuong( int nam)
        {
            List<KhenThuongView> list = Danhsachkhenthuong( nam);
            string templatePath = Server.MapPath(@"~/Data/Templates/test4.docx");
            string outputPath = Server.MapPath(@"~/Data/Templates/DanhSachKhenThuong.docx");
            // Load the document template
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholders in the document
                //document.ReplaceText("[TenDV]", GetTen(ma));
                document.ReplaceText("[nam]", nam.ToString());

                // Get the first table in the document
                Table table = document.Tables[0];

                // Populate data into the table
                foreach (var item in list)
                {
                    Row newRow = table.InsertRow(); // Insert a new row into the table

                    newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                    newRow.Cells[1].Paragraphs.First().Append(item.TenHV).Alignment = Alignment.both;
                    newRow.Cells[2].Paragraphs.First().Append(item.DonVi).Alignment = Alignment.center;
                    newRow.Cells[3].Paragraphs.First().Append(item.HPN).Alignment = Alignment.center;
                    newRow.Cells[4].Paragraphs.First().Append(item.TenKhenThuong).Alignment = Alignment.center;
                    newRow.Cells[5].Paragraphs.First().Append(item.LyDo).Alignment = Alignment.center;
                    newRow.Cells[6].Paragraphs.First().Append(item.GhiChu).Alignment = Alignment.center;
                    newRow.Cells[7].Paragraphs.First().Append(item.Nam.ToString()).Alignment = Alignment.center;
                }

               

                // Save the modified document to the output path
                document.SaveAs(outputPath);
            }

            // Convert the saved DOCX file to HTML
            string htmlPath = Server.MapPath(@"~/Data/Templates/render_DanhSachkhenthuong.doc.html");
            ConvertDocxToHtml(outputPath, htmlPath);

            // Read the HTML content to display in the view
            string htmlContent = System.IO.File.ReadAllText(htmlPath);
            ViewBag.DocumentContent = htmlContent;

            return View();
        }

        public List<KyLuatView> Danhsachkyluat( int? nam)
        {
            //if (ma == 0)
            //{
                List<KyLuatView> list = new List<KyLuatView>();
                var kyluat = (from a in obj.KyLuats
                              join b in obj.HoiViens on a.MaHV equals b.MaHV
                              join c in obj.DonVis on b.MaDV equals c.MaDV
                              //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
                              where a.Nam == nam
                              select new KyLuatView()
                              {
                                  TenHV = b.TenHV,
                                  Nam = a.Nam,
                                  HinhThucKL = a.HinhThucKyLuat,
                                  LyDo = a.LyDo,
                                  DonVi = c.TenDV,
                                  //HPN = d.TenHPN,
                                  GhiChu = a.GhiChu
                              }).ToList();
                list = kyluat;
                int i = 1;
                foreach (var item in list)
                {
                    item.STT = i;
                    i++;
                }
                return list;
            //}
            //else
            //{
            //    List<KyLuatView> list = new List<KyLuatView>();
            //    var khenthuong = (from a in obj.KyLuats
            //                      join b in obj.HoiViens on a.MaHV equals b.MaHV
            //                      join c in obj.DonVis on b.MaDV equals c.MaDV
            //                      //join d in obj.HoiPhuNus on b.MaHPN equals d.MaHPN
            //                      where a.Nam == nam
            //                      select new KyLuatView()
            //                      {

            //                          TenHV = b.TenHV,
            //                          Nam = a.Nam,
            //                          HinhThucKL = a.HinhThucKyLuat,
            //                          LyDo = a.LyDo,
            //                          DonVi = c.TenDV,
            //                          //HPN = d.TenHPN,
            //                          GhiChu = a.GhiChu
            //                      }).ToList();
            //    list = khenthuong;
            //    int i = 1;
            //    foreach (var item in list)
            //    {
            //        item.STT = i;
            //        i++;
            //    }
            //    return list;
           // }
        }
        [HttpPost]
        [Obsolete]
        public ActionResult InDanhSachKyLuat( int nam)
        {
            List<KyLuatView> list = Danhsachkyluat( nam);
            string templatePath = Server.MapPath(@"~/Data/Templates/test5.docx");
            string outputPath = Server.MapPath(@"~/Data/Templates/DanhSachKyLuat.docx");
            // Load the document template
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholders in the document
                //document.ReplaceText("[TenDV]", GetTen(ma));
                document.ReplaceText("[nam]", nam.ToString());

                // Get the first table in the document
                Table table = document.Tables[0];

                // Populate data into the table
                foreach (var item in list)
                {
                    Row newRow = table.InsertRow(); // Insert a new row into the table

                    newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                    newRow.Cells[1].Paragraphs.First().Append(item.TenHV).Alignment = Alignment.both;
                    newRow.Cells[2].Paragraphs.First().Append(item.DonVi).Alignment = Alignment.center;
                    newRow.Cells[3].Paragraphs.First().Append(item.HPN).Alignment = Alignment.center;
                    newRow.Cells[4].Paragraphs.First().Append(item.HinhThucKL).Alignment = Alignment.center;
                    newRow.Cells[5].Paragraphs.First().Append(item.LyDo).Alignment = Alignment.center;
                    newRow.Cells[6].Paragraphs.First().Append(item.GhiChu).Alignment = Alignment.center;
                    newRow.Cells[7].Paragraphs.First().Append(item.Nam.ToString()).Alignment = Alignment.center;
                }

                

                // Save the modified document to the output path
                document.SaveAs(outputPath);
            }

            // Convert the saved DOCX file to HTML
            string htmlPath = Server.MapPath(@"~/Data/Templates/render_DanhSachkyluat.doc.html");
            ConvertDocxToHtml(outputPath, htmlPath);

            // Read the HTML content to display in the view
            string htmlContent = System.IO.File.ReadAllText(htmlPath);
            ViewBag.DocumentContent = htmlContent;

            return View();
        }
        [HttpPost]
        [Obsolete]
        public ActionResult InDanhSachLDST( int nam)
        {
            List<XacNhanLDSTView> list = DanhsachLDST( nam);
            string templatePath = Server.MapPath(@"~/Data/Templates/test3.docx");
            string outputPath = Server.MapPath(@"~/Data/Templates/DanhSachLDST.docx");
            // Load the document template
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholders in the document
                //document.ReplaceText("[TenHPN]", GetTen(ma));
                document.ReplaceText("[nam]", nam.ToString());

                // Get the first table in the document
                Table table = document.Tables[0];

                // Populate data into the table
                foreach (var item in list)
                {
                    Row newRow = table.InsertRow(); // Insert a new row into the table

                    newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                    newRow.Cells[1].Paragraphs.First().Append(item.TenDetai).Alignment = Alignment.both;
                    newRow.Cells[2].Paragraphs.First().Append(item.LoaiDetai).Alignment = Alignment.center;
                    newRow.Cells[3].Paragraphs.First().Append(item.KetQua).Alignment = Alignment.center;
                    newRow.Cells[4].Paragraphs.First().Append(item.Chutri).Alignment = Alignment.center;
                    newRow.Cells[5].Paragraphs.First().Append(item.ThamGia).Alignment = Alignment.center;
                    newRow.Cells[6].Paragraphs.First().Append(item.Nam.ToString()).Alignment = Alignment.center;
                }

               

                // Save the modified document to the output path
                document.SaveAs(outputPath);
            }

            // Convert the saved DOCX file to HTML
            string htmlPath = Server.MapPath(@"~/Data/Templates/render_DanhSachLDST.doc.html");
            ConvertDocxToHtml(outputPath, htmlPath);

            // Read the HTML content to display in the view
            string htmlContent = System.IO.File.ReadAllText(htmlPath);
            ViewBag.DocumentContent = htmlContent;

            return View();
        }


        public List<HoiVienView> InDanhSachDangVien(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          where a.GioiTinh == false && b.Khoa == false && (a.MaLDV == 1 || a.MaLDV == 2)
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                        LoaiDangVien = obj.LoaiDangViens.Find(it.MaLDV).TenLDV
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && (a.MaLDV == 1 || a.MaLDV == 2)
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                        LoaiDangVien = obj.LoaiDangViens.Find(it.MaLDV).TenLDV
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachHoNgheo(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.HoNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.HoNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachHoCanNgheo(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.HoCanNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.HoCanNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachDaKetHon(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.DaCoChong == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.DaCoChong == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachChuaKetHon(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.DaCoChong == false
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.DaCoChong == false
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachLyHon(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.LyHon == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.LyHon == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachChongMat(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.MatChongVo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.MatChongVo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachConDuoi16Tuoi(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.ConDuoi16Tuoi == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.ConDuoi16Tuoi == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachBiBenh(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.MacBenhHiemNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.MacBenhHiemNgheo == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }
        public List<HoiVienView> InDanhSachChongCungDonVi(int MaHoi)
        {
            if (MaHoi == 0)
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false && c.VoChong == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;

            }
            else
            {
                List<HoiVienView> list = new List<HoiVienView>();
                var ds = (from a in obj.HoiViens
                          join b in obj.TaiKhoans on a.MaHV equals b.MaHV
                          join c in obj.HoanCanhGDs on a.MaHV equals c.MaHV
                          where a.GioiTinh == false && b.Khoa == false  && c.VoChong == true
                          select a).ToList();
                int i = 1;
                foreach (var it in ds)
                {
                    var hv = new HoiVienView()
                    {
                        STT = i,
                        TenHV = it.TenHV,
                        NgaySinh = it.NgaySinh,
                        MaCB = obj.Database.SqlQuery<int>("SELECT TOP 1 MaCB FROM dbo.LichSuCapBac WHERE MaHV = @MaHV AND XacNhan = 1 ORDER BY NgayQD DESC", new SqlParameter("MaHV", it.MaHV)).FirstOrDefault(),
                        DonVi = obj.DonVis.Find(it.MaDV).TenDV,
                        //ChiHoiPN = obj.HoiPhuNus.Find(it.MaHPN).TenHPN,
                    };
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
                    list.Add(hv);
                    i++;
                }
                return list;
            }

        }

        [Obsolete]
        public ActionResult InDanhSachHCGD(int Ma)
        {
            // Load data from various methods
            List<HoiVienView> listhongheo = InDanhSachHoNgheo(Ma);
            List<HoiVienView> listhocanngheo = InDanhSachHoCanNgheo(Ma);
            List<HoiVienView> listdakethon = InDanhSachDaKetHon(Ma);
            List<HoiVienView> listchuakethon = InDanhSachChuaKetHon(Ma);
            List<HoiVienView> listlyhon = InDanhSachLyHon(Ma);
            List<HoiVienView> listchongmat = InDanhSachChongMat(Ma);
            List<HoiVienView> listchongcungdonvi = InDanhSachChongCungDonVi(Ma);
            List<HoiVienView> listconduoi16tuoi = InDanhSachConDuoi16Tuoi(Ma);
            List<HoiVienView> listmacbenh = InDanhSachBiBenh(Ma);

            // Path to the template
            string templatePath = Server.MapPath(@"~/Data/Templates/test7.docx");

            // Load the template document
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholders with data
                //document.ReplaceText("[TenDV]", GetTen(Ma));
                document.ReplaceText("[thang]", DateTime.Now.Month.ToString());
                document.ReplaceText("[nam]", DateTime.Now.Year.ToString());
                document.ReplaceText("[HoNgheo]", listhongheo.Count().ToString());
                document.ReplaceText("[HoCanNgheo]", listhocanngheo.Count().ToString());
                document.ReplaceText("[DaKetHon]", listdakethon.Count().ToString());
                document.ReplaceText("[ChuaKetHon]", listchuakethon.Count().ToString());
                document.ReplaceText("[LyHon]", listlyhon.Count().ToString());
                document.ReplaceText("[ChongMat]", listchongmat.Count().ToString());
                document.ReplaceText("[ChongCungDonVi]", listchongcungdonvi.Count().ToString());
                document.ReplaceText("[ConDuoi16Tuoi]", listconduoi16tuoi.Count().ToString());
                document.ReplaceText("[MacBenhHiemNgheo]", listmacbenh.Count().ToString());

                // Insert data into tables for different categories
                InsertDataIntoTable(document.Tables[0], listhongheo);
                InsertDataIntoTable(document.Tables[1], listhocanngheo);
                InsertDataIntoTable(document.Tables[2], listdakethon);
                InsertDataIntoTable(document.Tables[3], listchuakethon);
                InsertDataIntoTable(document.Tables[4], listlyhon);
                InsertDataIntoTable(document.Tables[5], listchongmat);
                InsertDataIntoTable(document.Tables[6], listchongcungdonvi);
                InsertDataIntoTable(document.Tables[7], listconduoi16tuoi);
                InsertDataIntoTable(document.Tables[8], listmacbenh);

                // Save the modified document
                string outputPath = Server.MapPath(@"~/Data/Templates/DanhsachHCGD.docx");
                document.SaveAs(outputPath);

                // Convert DOCX to HTML using shared method
                string htmlPath = Server.MapPath(@"~/Data/Templates/render_DanhSachHCGD.html");
                ConvertDocxToHtml(outputPath, htmlPath);

                // Read HTML content to display in view
                string htmlContent = System.IO.File.ReadAllText(htmlPath);
                ViewBag.DocumentContent = htmlContent;
            }

            return View();
        }

        // Shared method to insert data into a table in DocX document
        private void InsertDataIntoTable(Table table, List<HoiVienView> dataList)
        {
            foreach (var item in dataList)
            {
                Row newRow = table.InsertRow();
                newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                newRow.Cells[1].Paragraphs.First().Append(item.TenHV).Alignment = Alignment.both;
                newRow.Cells[2].Paragraphs.First().Append(item.NgaySinh.Value.ToString("dd/MM/yyyy")).Alignment = Alignment.center;
                newRow.Cells[3].Paragraphs.First().Append(item.CapBac).Alignment = Alignment.center;
                newRow.Cells[4].Paragraphs.First().Append(item.DonVi).Alignment = Alignment.center;
                newRow.Cells[5].Paragraphs.First().Append(item.ChiHoiPN).Alignment = Alignment.center;
            }
        }
        [Obsolete]
        public ActionResult InDanhSachDV(int Ma)
        {
            List<HoiVienView> list = InDanhSachDangVien(Ma);
            string templatePath = Server.MapPath(@"~/Data/Templates/test6.docx");
            using (DocX document = DocX.Load(templatePath))
            {
                //document.ReplaceText("[TenDV]", GetTen(Ma));
                document.ReplaceText("[thang]", DateTime.Now.Month.ToString());
                document.ReplaceText("[nam]", DateTime.Now.Year.ToString());

                Table table = document.Tables[0]; // Assuming the table is the first table in the document

                foreach (var item in list)
                {
                    Row newRow = table.InsertRow();
                    newRow.Cells[0].Paragraphs.First().Append(item.STT.ToString()).Alignment = Alignment.center;
                    newRow.Cells[1].Paragraphs.First().Append(item.TenHV).Alignment = Alignment.both;
                    newRow.Cells[2].Paragraphs.First().Append(item.NgaySinh.Value.ToString("dd/MM/yyyy")).Alignment = Alignment.center;
                    newRow.Cells[3].Paragraphs.First().Append(item.CapBac).Alignment = Alignment.center;
                    newRow.Cells[4].Paragraphs.First().Append(item.DonVi).Alignment = Alignment.center;
                    newRow.Cells[5].Paragraphs.First().Append(item.ChiHoiPN).Alignment = Alignment.center;
                    newRow.Cells[6].Paragraphs.First().Append(item.LoaiDangVien).Alignment = Alignment.center;
                }

                string outputPath = Server.MapPath(@"~/Data/Templates/DanhsachDV.docx");
                document.SaveAs(outputPath);

                // Convert the DOCX file to HTML using shared method
                string htmlPath = Server.MapPath(@"~/Data/Templates/render_DanhSachDV.html");
                ConvertDocxToHtml(outputPath, htmlPath);

                // Read the HTML content to display in the view
                string htmlContent = System.IO.File.ReadAllText(htmlPath);
                ViewBag.DocumentContent = htmlContent;
            }

            return View();
        }
        [Obsolete]
        public ActionResult Report(int Ma)
        {
            // Generate report data
            BaoCao(Ma);

            // Path to the template
            string templatePath = Server.MapPath(@"~/Data/Templates/test.docx");

            // Load the template document
            using (DocX document = DocX.Load(templatePath))
            {
                // Replace placeholders with data in the loaded document
                document.ReplaceText("[TenHPN]", TenHPN);
                document.ReplaceText("[thang]", DateTime.Now.Month.ToString());
                document.ReplaceText("[nam]", DateTime.Now.Year.ToString());
                document.ReplaceText("[SoLuong]", SoLuongHV.ToString());
                document.ReplaceText("[SQ]", SiQuan.ToString());
                document.ReplaceText("[QNCN]", QNCN.ToString());
                document.ReplaceText("[CNVQP]", CNVQP.ToString());
                document.ReplaceText("[LDHD]", LDHD.ToString());
                document.ReplaceText("[HV]", HV.ToString());
                document.ReplaceText("[DTTS]", DTThieuSo.ToString());
                document.ReplaceText("[SoCap]", SoCap.ToString());
                document.ReplaceText("[TrungCap]", TrungCap.ToString());
                document.ReplaceText("[CaoDang]", CaoDang.ToString());
                document.ReplaceText("[DaiHoc]", DaiHoc.ToString());
                document.ReplaceText("[ThacSi]", ThacSi.ToString());
                document.ReplaceText("[TienSi]", TienSi.ToString());
                document.ReplaceText("[Bac2]", Bac2.ToString());
                document.ReplaceText("[Bac3]", Bac3.ToString());
                document.ReplaceText("[Bac4]", Bac4.ToString());
                document.ReplaceText("[Bac5]", Bac5.ToString());
                document.ReplaceText("[Bac6]", Bac6.ToString());
                document.ReplaceText("[Bac7]", Bac7.ToString());
                document.ReplaceText("[Bac8]", Bac8.ToString());
                document.ReplaceText("[CNKhoa]", CNKHoa.ToString());
                document.ReplaceText("[PhoCNKhoa]", PhoCNKhoa.ToString());
                document.ReplaceText("[CNBoMon]", CNBoMon.ToString());
                document.ReplaceText("[PhoCNBM]", PhoCNBM.ToString());
                document.ReplaceText("[TP]", TruongPhong.ToString());
                document.ReplaceText("[PhoTP]", PhoTP.ToString());
                document.ReplaceText("[TB]", TruongBan.ToString());
                document.ReplaceText("[PhoTB]", PhoTruongBan.ToString());
                document.ReplaceText("[TL]", TroLy.ToString());
                document.ReplaceText("[BepT]", BepTruong.ToString());
                document.ReplaceText("[KTV]", KTV.ToString());
                document.ReplaceText("[NV]", NhanVienVanThu.ToString());
                document.ReplaceText("[18-30]", Tu18den30.ToString());
                document.ReplaceText("[31-49]", tu31den49.ToString());
                document.ReplaceText("[50+]", tren50.ToString());
                document.ReplaceText("[DVCT]", DangVienChinhThuc.ToString());
                document.ReplaceText("[DVDB]", DangVienDuBi.ToString());
                document.ReplaceText("[ChuaKN]", ChuaKetNap.ToString());

                // Save the modified document
                string outputPath = Server.MapPath(@"~/Data/Templates/BaoCao.docx");
                document.SaveAs(outputPath);

                // Convert DOCX to HTML using shared method
                string htmlPath = Server.MapPath(@"~/Data/Templates/render_BaoCao2.html");
                ConvertDocxToHtml(outputPath, htmlPath);

                // Read HTML content to display in view
                string htmlContent = System.IO.File.ReadAllText(htmlPath);
                ViewBag.DocumentContent = htmlContent;
            }

            return View();
        }
        public ActionResult TaiDanhSach()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhsachHV.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách hội viên.docx");

        }
        public ActionResult TaiDanhSachDV()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhsachDV.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách đảng viên.docx");

        }
        public ActionResult TaiDanhSachLDST()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhsachLDST.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách LDST.docx");

        }
        public ActionResult TaiDanhSachHCGD()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhsachHCGD.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách HCGD.docx");

        }
        public ActionResult TaiDanhSachKhenThuong()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhSachKhenThuong.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách khen thưởng.docx");

        }
        public ActionResult TaiDanhSachKyLuat()
        {

            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/DanhSachKyLuat.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Danh sách kỷ luật.docx");

        }

        public ActionResult TaiBaoCao()
        {
            // Đường dẫn tới file Word
            string path = Path.Combine(Server.MapPath("/Data/Templates/BaoCao.docx"));

            // Kiểm tra xem file có tồn tại không

            // Tạo một FileResult để trả về file
            return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "BaoCao.docx");
        }


        //[HttpPost]
        //public void TaiDanhSach()
        //{           
        //        // Đường dẫn tới file Word
        //        string path = Path.Combine(Server.MapPath("/Assets/CongVan/"), file);

        //        // Kiểm tra xem file có tồn tại không

        //        // Tạo một FileResult để trả về file
        //        return File(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", file);


        //    //string inputPath = "DanhSachHV.doc";
        //    //string dataDir = System.Web.HttpContext.Current.Server.MapPath(@"~/Data/Templates/");

        //    //Aspose.Words.Document doc = new Aspose.Words.Document(dataDir + "DanhsachHV.docx");

        //    //doc.Save(dataDir + "html/Aspose_DocToHTML.html", SaveFormat.Html); //Save the document in HTML format.

        //    //var list_fields = doc.MailMerge.GetFieldNames().ToArray();
        //    ////String[] field_array = field_list.ToArray();
        //    ////field_array[field_array.Length-1] = field_array[field_array.Length-1].Replace("\r\n", string.Empty);
        //    //doc.MailMerge.UseNonMergeFields = true;


        //    //// Fill the fields in the document with user data.  

        //    //dataDir = dataDir + ".docx";
        //    //// Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
        //    //doc.Save(@dataDir);

        //    //string time = DateTime.Now.ToString();


        //    //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //    //response.ClearContent();
        //    //response.Clear();
        //    //response.ContentType = "Application/msword";
        //    //response.AddHeader("Content-Disposition", "attachment; filename=" + time + "_" + @inputPath + ";");
        //    //response.TransmitFile(dataDir);
        //    //response.Flush();
        //    //response.End();
        //}
        //[HttpPost]
        //public void Gen(FormCollection ls)
        //{
        //    string inputPath = "BaoCao.doc";
        //    string dataDir = System.Web.HttpContext.Current.Server.MapPath(@"~/Data/Templates/");

        //    Aspose.Words.Document doc = new Aspose.Words.Document(dataDir + "BaoCao.doc");

        //    doc.Save(dataDir + "html/Aspose_DocToHTML.html", SaveFormat.Html); //Save the document in HTML format.

        //    var list_fields = doc.MailMerge.GetFieldNames().ToArray();
        //    //String[] field_array = field_list.ToArray();
        //    //field_array[field_array.Length-1] = field_array[field_array.Length-1].Replace("\r\n", string.Empty);
        //    doc.MailMerge.UseNonMergeFields = true;


        //    // Fill the fields in the document with user data.  

        //    dataDir = dataDir + ".docx";
        //    // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
        //    doc.Save(@dataDir);

        //    string time = DateTime.Now.ToString();


        //    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //    response.ClearContent();
        //    response.Clear();
        //    response.ContentType = "Application/msword";
        //    response.AddHeader("Content-Disposition", "attachment; filename=" + time + "_" + @inputPath + ";");
        //    response.TransmitFile(dataDir);
        //    response.Flush();
        //    response.End();
        //}

    }
}