using System;
using System.Collections.Generic;
using System.Text;

namespace TestPrsServer.Models {
    
    public class Config {
        public string Domain { get; set; }
        public int Port { get; set; } = 5000;
        public string fullUrl { get { return $"http://{Domain}:{Port}/api/users"; } }
    }
}
