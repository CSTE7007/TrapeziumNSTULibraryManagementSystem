using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class BookIssues
    {
        public int MemberCardNo { get; set; }
        public string BookID { get; set; }

        public string BookName { get; set; }
        public string MemberName { get; set; }

        public string DateIssues { get; set; }
        public string DateExpiry { get; set; }
    }
}
