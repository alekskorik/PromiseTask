using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiseTask
{
    public enum OrderStatus
    {
        New,
        InWarehouse,
        InShipping,
        ReturnedToCustomer,
        Error,
        Closed
    }
}
