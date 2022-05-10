using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
    public class DeskBookingsModel : PageModel
    {
        private readonly IDeskBookingRepository _deskBookingRepository;

        public DeskBookingsModel(IDeskBookingRepository deskBookingRepository)
        {
            _deskBookingRepository = deskBookingRepository;
        }

        public IEnumerable<DeskBooking> DeskBookings { get; set; }

        public void OnGet()
        {
            DeskBookings = _deskBookingRepository.GetAll();
        }
    }
}
