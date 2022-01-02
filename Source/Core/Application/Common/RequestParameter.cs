using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
   public class PagingParameter
    {

        public int  PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagingParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }


       public  PagingParameter(int pageNumber,int pageSize)
        {
            this.PageNumber = PageNumber < 1 ? 1 : PageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
        
    }
}
