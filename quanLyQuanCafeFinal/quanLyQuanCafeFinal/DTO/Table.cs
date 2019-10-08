using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DTO
{
    public class Table
    {
        private int iD;
        private String name;
        private String status;

        public Table()
        {

        }

        public Table(int id, String name, String stt)
        {
            this.Name = name;
            this.ID = id;
            this.Status = stt;
        }

        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["Status"].ToString();
        }

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
