﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Models
{
    public class PageType
    {
        public String Name { get; set; }
        public String Icon { get; set; }
        public Type Type { get; set; }
        public Type NavigateType { get; set; }
    }
}
