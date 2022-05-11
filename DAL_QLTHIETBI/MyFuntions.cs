using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DAL_QLTHIETBI
{
    public class MyFuntions
    {
        public object ImageHelper { get; private set; }

        public string SDienMaTuDong(string Ma)
        {
            int iMax = 0;
            string sMa = "";
            string sql = "";
            int lenMa = 0;
            switch (Ma)
            {
                case "DVT":
                    lenMa = 3;
                    sql += "select top(1) MADVT from DONVITINH  order by MADVT DESC";
                    break;
                case "TT":
                    lenMa = 2;
                    sql += "select top(1) MATT from TRANGTHAI  order by MATT DESC";
                    break;
                case "NCC":
                    lenMa = 3;
                    sql += "select top(1) MANCC from NHACUNGCAP  order by MANCC DESC";
                    break;
                case "NTS":
                    lenMa = 3;
                    sql += "select top(1) MANHOMTS from NHOMTAISAN  order by MANHOMTS DESC";
                    break;
                case "LTS":
                    lenMa = 3;
                    sql += "select top(1) MALOAITS from LOAITAISAN  order by MALOAITS DESC";
                    break;
                case "CV":
                    lenMa = 2;
                    sql += "select top(1) MACV from CHUCVU  order by MACV DESC";
                    break;
                case "DV":
                    lenMa = 2;
                    sql += "select top(1) MADV from DONVI  order by MADV DESC";
                    break;
                case "PB":
                    lenMa = 2;
                    sql += "select top(1) MAPB from PHONGBAN  order by MAPB DESC";
                    break;
                case "NV":
                    lenMa = 2;
                    sql += "select top(1) MANV from NHANVIEN  order by MANV DESC";
                    break;
                case "PTB":
                    lenMa = 3;
                    sql += "select top(1) MAPTB from PHONGTHIETBI  order by MAPTB DESC";
                    break;
                case "TB":
                    lenMa = 2;
                    sql += "select top(1) MATB from THIETBI  order by MATB DESC";
                    break;
                case "TTS":
                    lenMa = 3;
                    sql += "select top(1) MATHETS from THETAISAN  order by MATHETS DESC";
                    break;
                case "PN":
                    lenMa = 2;
                    sql += "select top(1) MAPN from PHIEUNHAPTB  order by MAPN DESC";
                    break;
                case "PKH":
                    lenMa = 3;
                    sql += "select top(1) MAPKH from PHIEUKHAUHAOTB  order by MAPKH DESC";
                    break;
                case "PDGL":
                    lenMa = 4;
                    sql += "select top(1) MAPDGL from PHIEUDANHGIALAI  order by MAPDGL DESC";
                    break;
                case "PSC":
                    lenMa = 3;
                    sql += "select top(1) MAPSC from PHIEUSUACHUATB  order by MAPSC DESC";
                    break;
                case "PKK":
                    lenMa = 3;
                    sql += "select top(1) MAPKK from PHIEUKIEMKETB  order by MAPKK DESC";
                    break;
                case "PTL":
                    lenMa = 3;
                    sql += "select top(1) MAPTL from PHIEUTHANHLYTB  order by MAPTL DESC";
                    break;
                case "PLC":
                    lenMa = 3;
                    sql += "select top(1) MAPLC from PHIEULUANCHUYENTB  order by MAPLC DESC";
                    break;
                case "PBT":
                    lenMa = 3;
                    sql += "select top(1) MAPBT from PHIEUBAOTRITB  order by MAPBT DESC";
                    break;
                case "KHMS":
                    lenMa = 4;
                    sql += "Select top(1) MAKHMS from KEHOACHMUASAM  order by MAKHMS DESC";
                    break;
                case "DXMS":
                    lenMa = 4;
                    sql += "Select top(1) MADXMS from DEXUATMUASAM  order by MADXMS DESC";
                    break;

            }

            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionSTR))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader sqlreader = cmd.ExecuteReader();

                while (sqlreader.Read())
                {
                    int iSoSanh = int.Parse(sqlreader.GetValue(0).ToString().Substring(lenMa, 4));
                    if (iSoSanh > iMax)
                    {
                        iMax = iSoSanh;
                    }
                }
                sqlreader.Close();
                connection.Close();
            }

            if (iMax + 1 < 10)
            {
                sMa = string.Concat(Ma, "000", iMax + 1);
            }
            if (iMax + 1 >= 10)
            {
                sMa = string.Concat(Ma, "00", iMax + 1);
            }
            if (iMax + 1 >= 100)
            {
                sMa = string.Concat(Ma, "0", iMax + 1);
            }
            if (iMax + 1 >= 1000)
            {
                sMa = string.Concat(Ma, iMax + 1);
            }
            return sMa;
        }

        public string NumberToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
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
            if (isNegative) result = "Âm " + result;
            return (result.First().ToString().ToUpper() + result.Substring(1)) + (suffix ? " đồng" : "");
        }
        public string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("123"));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes("123");

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public string ReverseString(string passWord)
        {
            string s = "";
            char[] arr = passWord.ToCharArray(); // chuỗi thành mảng ký tự
            Array.Reverse(arr); // đảo ngược mảng
            for (int i = 0; i < arr.Length; i++)
                s += arr[i];
            return s; // trả về chuỗi mới sau khi đảo mảng
        }

        public string RemoveChars(string str)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "'" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            return str;
        }
        public string ReplaceChars(string str)
        {
            var charsToReplace = new string[] { "," };
            foreach (var c in charsToReplace)
            {
                str = str.Replace(c, ".");
            }
            return str;
        }


        public byte[] imgToByteArray(Image img)
        {
            //ImageConverter imgCon = new ImageConverter();
            //return (byte[])imgCon.ConvertTo(img, typeof(byte[]));

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms,ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }           
        }
        public Image GetImageFromByteArray(byte[] byteArray)
        {
            ImageConverter imgCon = new ImageConverter();
            return (Image)imgCon.ConvertFrom(byteArray);
        }

        public Bitmap ImageFromByteArray(byte[] byteArray)
        {
            Bitmap bm = (Bitmap)new ImageConverter().ConvertFrom(byteArray);

            if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                               bm.VerticalResolution != (int)bm.VerticalResolution))
            {
                // Correct a strange glitch that has been observed in the test program when converting 
                //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
                //  slightly away from the nominal integer value
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm;
        }
    }
}
