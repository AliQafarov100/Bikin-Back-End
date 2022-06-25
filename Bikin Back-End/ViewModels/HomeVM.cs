using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bikin_Back_End.Models;

namespace Bikin_Back_End.ViewModels
{
    public class HomeVM
    {
        public List<ServiceCard> Cards { get; set; }
        public MainPage Page { get; set; }
        public List<Sponsor> Sponsors { get; set; }
        public List<AboutCards> AboutCards { get; set; }
    }
}
