﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

namespace UserMaintenance.Entities
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}",
              FirstName,
              LastName);
            }
        }
    }
}
