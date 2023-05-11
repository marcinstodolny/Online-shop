using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EmailContext
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string Address { get; init; }
        public string Smtp { get; init; }
        public int Port { get; init; }
    }
}
