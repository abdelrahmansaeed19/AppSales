using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        Ready,
        Completed,
        Cancelled
    }
}
