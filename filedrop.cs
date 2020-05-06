using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace minty
{
    public class fileHandling
    {
        public ObservableCollection<fileHandling> myfiles = new ObservableCollection<fileHandling>();
        public string filePath { get; set; }
    }
}
