using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilesRNSDLL.DevelopmentBase
{
    public class DALException : Exception
    {
        public DALException(string message)
            : base(message)
        { }
    }
}
