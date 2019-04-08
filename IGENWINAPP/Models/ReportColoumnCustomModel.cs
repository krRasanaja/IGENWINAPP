using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGENWINAPP.Models
{
    public class ReportColoumnCustomModel
    {
        public int ReportID { get; set; }

        public int CategoryId { get; set; }

        public string ReportName { get; set; }

        public int ViewId { get; set; }

        public string ColumnName { get; set; }
    }
}
