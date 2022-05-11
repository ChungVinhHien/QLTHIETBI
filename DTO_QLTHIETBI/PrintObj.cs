using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PrintObj
    {
        private string value1;
        private string value2;
        private string value3;
        private string value4;
        private string value5;
        private string value6;
        private string value7;
        private string value8;
        private string value9;
        private string value10;
        private string value11;
        private string value12;
        private string value13;
        private string value14;
        private string value15;

        public PrintObj(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, 
            string value9, string value10, string value11, string value12, string value13, string value14, string value15)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
            Value6 = value6;
            Value7 = value7;
            Value8 = value8;
            Value9 = value9;
            Value10 = value10;
            Value11 = value11;
            Value12 = value12;
            Value13 = value13;
            Value14 = value14;
            Value15 = value15;
        }
        public PrintObj(DataRow row)
        {
            this.Value1 = row[0].ToString();
            this.Value2 = row[1].ToString();
            this.Value3 = row[2].ToString();
            this.Value4 = row[3].ToString();
            this.Value5 = row[4].ToString();
            this.Value6 = row[5].ToString();
            this.Value7 = row[6].ToString();
            this.Value8 = row[7].ToString();
            this.Value9 = row[8].ToString();
            this.Value10 = row[9].ToString();
            this.Value11 = row[10].ToString();
            this.Value12 = row[11].ToString();
            this.Value13= row[12].ToString();
            this.Value14 = row[13].ToString();
            this.Value15 = row[14].ToString();
        }


        public string Value1 { get => value1; set => value1 = value; }
        public string Value2 { get => value2; set => value2 = value; }
        public string Value3 { get => value3; set => value3 = value; }
        public string Value4 { get => value4; set => value4 = value; }
        public string Value5 { get => value5; set => value5 = value; }
        public string Value6 { get => value6; set => value6 = value; }
        public string Value7 { get => value7; set => value7 = value; }
        public string Value8 { get => value8; set => value8 = value; }
        public string Value9 { get => value9; set => value9 = value; }
        public string Value10 { get => value10; set => value10 = value; }
        public string Value11 { get => value11; set => value11 = value; }
        public string Value12 { get => value12; set => value12 = value; }
        public string Value13 { get => value13; set => value13 = value; }
        public string Value14 { get => value14; set => value14 = value; }
        public string Value15 { get => value15; set => value15 = value; }
    }
}
