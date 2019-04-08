using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGENWINAPP.Models
{
    public class CustomMenuItem
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ReportId { get; set; }

        public string ReportName { get; set; }

        public string UserId { get; set; }
    }
}
