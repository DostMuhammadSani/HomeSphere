﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        
        public bool RememberMe { get; set; }

       public LoginModel() { 
        RememberMe = false;
        }
    }

}
