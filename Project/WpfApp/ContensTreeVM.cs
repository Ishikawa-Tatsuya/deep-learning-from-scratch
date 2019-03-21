using System;
using System.Collections.Generic;

namespace WpfApp
{
    public class ContensTreeVM
    {
        public string Name { get; set; }
        public List<ContensTreeVM> Nodes { get; set; } = new List<ContensTreeVM>();
        public Action Execute { get; set; }
    }
}
