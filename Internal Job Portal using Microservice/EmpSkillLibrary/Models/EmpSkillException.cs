using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpSkillLibrary.Models
{
    internal class EmpSkillException:Exception
    {
        public EmpSkillException(string errMsg):base(errMsg)
        {
            
        }
    }
}
