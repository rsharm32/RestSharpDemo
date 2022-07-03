using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo
{
    public  class CreateUserRequestDTO //POST method takes only 2 arguments name and Job
    {
        public string Name { get; set; }
        public string Job { get; set; }
    }
}

