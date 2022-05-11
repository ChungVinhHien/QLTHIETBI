using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using Microsoft.Reporting.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPrint : Form
    {
        DataSetPhieuBaoTri dsPBT = new DataSetPhieuBaoTri();
        DataSetPhieuKiemKe dsPKK = new DataSetPhieuKiemKe();
        DataSetPhieuLuanChuyen dsPLC = new DataSetPhieuLuanChuyen();
        DataSetPhieuSuaChua dsPSC = new DataSetPhieuSuaChua();
        DataSetPhieuThanhLy dsPTL = new DataSetPhieuThanhLy();
        DataSetNhaCungCap dsNCC = new DataSetNhaCungCap();
        DataSetPhieuDanNhan dsPDN = new DataSetPhieuDanNhan();
        DataSetPhieuKHNam dsPKHNam = new DataSetPhieuKHNam();
        DataSetPhieuKHThang dsPKHThang = new DataSetPhieuKHThang();
        DataSetTheTaiSan dsTTS = new DataSetTheTaiSan();
        DataSetTaiSanMat dsTSMat = new DataSetTaiSanMat();
        DataSetTaiSanHuHong dsTSHuHong = new DataSetTaiSanHuHong();
        DataSetTaiSanThanhLy dsTSThanhLy = new DataSetTaiSanThanhLy();
        DataSetTaiSanHoatDong dsTSHoatDong = new DataSetTaiSanHoatDong();
        public frmPrint()
        {
            InitializeComponent();
            //this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer.ZoomMode = ZoomMode.Percent;
            //this.reportViewer.ZoomPercent = 75;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            this.reportViewer.RefreshReport();
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = ZoomMode.Percent;
            this.reportViewer.ZoomPercent = 75;
            string querry = "";
            switch (HoatDongObj.Noidung)
            {
                case "TTS":
                    querry += "Exec PrintChiTietTheTaiSan '" + TheTaiSanObj.Matts + "'";
                    Print(querry, "PrintChiTietTheTaiSan", "rptTheTaiSan.rdlc");
                    break;
                case "KHThang":
                    panel.Visible = true;
                    LoadComboBox();
                    break;
                case "KHNam":
                    panel.Visible = true;
                    cbxThang.Enabled = false;
                    LoadComboBox();
                    break;
                case "TBPTB":
                    querry = "Exec PrintTaiSanPTB '" + PhongThietBiObj.Maptb + "'";
                    Print(querry, "PrintTaiSanPTB", "rptThietBiPTB.rdlc");
                    break;
                case "PHIEUDANNHAN":
                    querry = "Exec PrintPhieuDanNhan '" + DonViObj.Madv + "'";
                    Print(querry, "PrintPhieuDanNhan", "rptPhieuDanNhan.rdlc");
                    break;
                case "NCC":
                    querry = "Select * from NHACUNGCAP";
                    Print(querry, "NHACUNGCAP", "rptNhaCungCap.rdlc");
                    break;
                case "ThietBiHD":
                    querry = "Exec PrintTaiSanDangHoatDong";
                    Print(querry, "PrintTaiSanDangHoatDong", "rptThietBiHD.rdlc");
                    break;
                case "ThietBiHH":
                    querry = "Exec PrintTaiSanHuHong";
                    Print(querry, "PrintTaiSanHuHong", "rptThietBiHH.rdlc");
                    break;
                case "ThietBiBM":
                    querry = "Exec PrintTaiSanBiMat";
                    Print(querry, "PrintTaiSanBiMat", "rptThietBiBM.rdlc");
                    break;
                case "ThietBiTL":
                    querry = "Exec PrintTaiSanThanhLy";
                    Print(querry, "PrintTaiSanThanhLy", "rptThietBiTL.rdlc");
                    break;
                case "PSC":
                    querry = "Exec PrintChiTietPSC '" + PhieuSuaChuaObj.Maphieusc + "'";
                    Print(querry, "PrintChiTietPSC", "rptCTPSC.rdlc");
                    break;
                case "PKK":
                    querry = "Exec PrintChiTietPKK '" + PhieuKiemKeObj.Mapkk + "'";
                    Print(querry, "PrintChiTietPKK", "rptCTPKK.rdlc");
                    break;
                case "PLC":
                    querry = "Exec PrintChiTietPLC '" + PhieuLuanChuyenObj.Maplc + "'";
                    Print(querry, "PrintChiTietPLC", "rptCTPLC.rdlc");
                    break;
                case "PTL":
                    querry = "Exec PrintChiTietPTL '" + PhieuThanhLyObj.Maptl + "'";
                    Print(querry, "PrintChiTietPTL", "rptCTPTL.rdlc");
                    break;
                case "PBT":
                    querry += "Exec PrintChiTietPBT '" + PhieuBaoTriObj.Maphieubt + "'";
                    Print(querry, "PrintChiTietPBT", "rptCTPBT.rdlc");
                    break;
            }
        }

        void Print(string sql, string srcTable, string file)
        {          
            int Condition = 0;

            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionSTR))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, connection);
                
                switch (HoatDongObj.Noidung)
                {
                    case "TTS":
                        da.Fill(dsTTS, srcTable);
                        Condition = dsTTS.Tables[0].Rows.Count;
                        break;
                    case "KHThang":
                        da.Fill(dsPKHThang, srcTable);
                        Condition = dsPKHThang.Tables[0].Rows.Count;
                        break;
                    case "KHNam":
                        da.Fill(dsPKHNam, srcTable);
                        Condition = dsPKHNam.Tables[0].Rows.Count;
                        break;
                    case "PHIEUDANNHAN":
                        da.Fill(dsPDN, srcTable);
                        Condition = dsPDN.Tables[0].Rows.Count;
                        break;
                    case "NCC":
                        da.Fill(dsNCC, srcTable);
                        Condition = dsNCC.Tables[0].Rows.Count;
                        break;
                    case "ThietBiHD":
                        da.Fill(dsTSHoatDong, srcTable);
                        Condition = dsTSHoatDong.Tables[0].Rows.Count;
                        break;
                    case "ThietBiHH":
                        da.Fill(dsTSHuHong, srcTable);
                        Condition = dsTSHuHong.Tables[0].Rows.Count;
                        break;
                    case "ThietBiBM":
                        da.Fill(dsTSMat, srcTable);
                        Condition = dsTSMat.Tables[0].Rows.Count;
                        break;
                    case "ThietBiTL":
                        da.Fill(dsTSThanhLy, srcTable);
                        Condition = dsTSThanhLy.Tables[0].Rows.Count;
                        break;
                    case "PSC":
                        da.Fill(dsPSC, srcTable);
                        Condition = dsPSC.Tables[0].Rows.Count;
                        break;
                    case "PKK":
                        da.Fill(dsPKK, srcTable);
                        Condition = dsPKK.Tables[0].Rows.Count;
                        break;
                    case "PLC":
                        da.Fill(dsPLC, srcTable);
                        Condition = dsPLC.Tables[0].Rows.Count;
                        break;
                    case "PTL":
                        da.Fill(dsPTL, srcTable);
                        Condition = dsPTL.Tables[0].Rows.Count;
                        break;
                    case "PBT":
                        da.Fill(dsPBT, srcTable);
                        Condition = dsPBT.Tables[0].Rows.Count;
                        break;
                }

                reportViewer.LocalReport.ReportPath = System.IO.Directory.GetCurrentDirectory() + "\\Reports\\" + file;

                if (Condition > 0)
                {
                    ReportDataSource rps = new ReportDataSource();
                    rps.Name = "DataSet1";
                    switch (HoatDongObj.Noidung)
                    {
                        case "TTS":
                            rps.Value = dsTTS.Tables[0];
                            break;
                        case "KHThang":
                            rps.Value = dsPKHThang.Tables[0];
                            break;
                        case "KHNam":
                            rps.Value = dsPKHNam.Tables[0];
                            break;
                        case "PHIEUDANNHAN":
                            rps.Value = dsPDN.Tables[0];
                            break;
                        case "NCC":
                            rps.Value = dsNCC.Tables[0];
                            break;
                        case "ThietBiHD":
                            rps.Value = dsTSHoatDong.Tables[0];
                            break;
                        case "ThietBiHH":
                            rps.Value = dsTSHuHong.Tables[0];
                            break;
                        case "ThietBiBM":
                            rps.Value = dsTSMat.Tables[0];
                            break;
                        case "ThietBiTL":
                            rps.Value = dsTSThanhLy.Tables[0];
                            break;
                        case "PSC":
                            rps.Value = dsPSC.Tables[0];
                            break;
                        case "PKK":
                            rps.Value = dsPKK.Tables[0];
                            break;
                        case "PLC":
                            rps.Value = dsPLC.Tables[0];
                            break;
                        case "PTL":
                            rps.Value = dsPTL.Tables[0];
                            break;
                        case "PBT":
                            rps.Value = dsPBT.Tables[0];
                            break;
                    }

                    reportViewer.LocalReport.EnableExternalImages = true;
                    reportViewer.LocalReport.DataSources.Clear();
                    this.reportViewer.LocalReport.ReportEmbeddedResource = file;
                    reportViewer.LocalReport.DataSources.Add(rps);
                    reportViewer.LocalReport.Refresh();
                }
                connection.Close();
                this.reportViewer.RefreshReport();
                
            }

            //reportViewer.ProcessingMode = ProcessingMode.Local;

            //reportViewer.LocalReport.ReportPath = System.IO.Path.GetFullPath("..\\Report\\") + file;
           
        }
        void LoadComboBox()
        {
            for (int i = 1; i <= 12; i++)
                cbxThang.Items.Add(i.ToString());
            for (int i = 2000; i <= DateTime.Now.Year; i++)
                cbxNam.Items.Add(i.ToString());

        }

        private void cbxNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            string querry = "";
            switch (HoatDongObj.Noidung)
            {
                case "KHThang":
                    querry = "Exec PrintChiTietPhieuKhauHaoTheoThang " + cbxThang.Text + "," + cbxNam.Text + "";
                    Print(querry, "PrintChiTietPhieuKhauHaoTheoThang", "rptPhieuKhauHaoThang.rdlc");
                    break;
                case "KHNam":
                    querry = "Exec PrintChiTietPhieuKhauHaoTheoNam '" + cbxNam.Text + "'";
                    Print(querry, "PrintChiTietPhieuKhauHaoTheoNam", "rptPhieuKhauHaoNam.rdlc");
                    break;
            }
        }
    }
}
