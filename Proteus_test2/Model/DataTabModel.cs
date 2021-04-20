using Proteus_test2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proteus_test2.Model
{
    public class DataTabModel : Tab
    {
        public DataTabModel()
        {
            Name = DateTime.Now.ToString();
        }
    }
}
