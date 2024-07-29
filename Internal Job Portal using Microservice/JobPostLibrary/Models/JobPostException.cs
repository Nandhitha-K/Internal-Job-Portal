using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary.Models
{
    internal class JobPostException:Exception
    {
        public JobPostException(string errMsg):base(errMsg) 
        {
            
        }
    }
}
