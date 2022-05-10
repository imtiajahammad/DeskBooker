using DeskBooker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBooker.Core.Processor
{
    public interface IDeskBookingRequestProcessor
    {
        DeskBookingResult BookDesk(DeskBookingRequest request);
    }
}
