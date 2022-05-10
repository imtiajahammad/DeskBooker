using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
    public class DesksModel : PageModel
    {
        private readonly IDeskRepository _deskRepository;

        public DesksModel(IDeskRepository deskRepository)
        {
            _deskRepository = deskRepository;
        }

        public IEnumerable<Desk> Desks { get; set; }

        public void OnGet()
        {
            Desks = _deskRepository.GetAll();
        }
    }
}
