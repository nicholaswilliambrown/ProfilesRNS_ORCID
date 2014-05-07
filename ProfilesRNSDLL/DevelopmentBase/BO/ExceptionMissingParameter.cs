using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilesRNSDLL.DevelopmentBase.BO
{
    public class ExceptionMissingParameter : ExceptionSafeToDisplay
    {
        public ExceptionMissingParameter() : base()
        {
        }

        public ExceptionMissingParameter(string friendlyMsg)
            : base(friendlyMsg)
        {
        }
    }
}
