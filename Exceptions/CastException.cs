using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions
{
    public class CastException : BaseException
    {
        #region BaseException implementation

        public override void FindOwner(Exception exception, out Exception appropriateExceptionType)
        {
            appropriateExceptionType = null;
            // todo: write better check!
            if (exception.Message.ToUpper().Contains("CAST"))
            {
                appropriateExceptionType = new CastException(exception);
            }
            else if (successor != null)
            {
                successor.FindOwner(exception, out appropriateExceptionType);
            }
        }

        #endregion

        public CastException() { }
        public CastException(string message) : base(message) { }
        public CastException(Exception ex) : base(ex.Message) { }

        public object SourceValue { get; set; }

        public Type DestinationType { get; set; }
    }
}
