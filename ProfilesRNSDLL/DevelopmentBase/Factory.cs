using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilesRNSDLL.DevelopmentBase
{
    public abstract class Factory
    {
        protected abstract int PersonID { get; }
        protected abstract int LoggedInPersonID { get; } 
    }
}
