using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Head.Dto
{
    public class HeaderDto
    {
        public string path { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public string dataTargetName { get; set; }

        /// <summary>
        /// 子選單
        /// </summary>
        public ChildrenDto childrenDto { get; set; }
    }


    public class ChildrenDto
    {
        public string path { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public string dataTargetName { get; set; }

        public bool isactive { get; set; }
    }

    public class ParentDto
    {
        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public int Order { get; set; }

        public string URL { get; set; }

        public int Status { get; set; }

        public string Code { get; set; }
    }
}
