using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudRegSys
{
    public class Result
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string ModuleCode { get; set; }

        public string Grade { get; set; }

    }
}
