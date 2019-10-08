using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DTO
{
    public class BillInfo
    {
        private int iD;
        private int billID;
        private int foodID;
        private int count;

        public BillInfo()
        { }

        public BillInfo(int id, int bid, int fid, int c)
        {
            this.ID = id;
            this.BillID = bid;
            this.FoodID = fid;
            this.Count = c;
        }

        public BillInfo(DataRow row )
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["iDBill"];
            this.FoodID = (int)row["iDFood"];
            this.Count = (int)row["count"];
        }

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Count { get => count; set => count = value; }
    }
}
