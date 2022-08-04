using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions
{
    public class ValidationException : BaseException
    {
        #region BaseException implementation

        public override void FindOwner(Exception exception, out Exception appropriateExceptionType)
        {
            appropriateExceptionType = null;
            // todo: write better check!
            if (exception.Message.Contains("Test"))
            {
                appropriateExceptionType = new ValidationException(exception);
            }
            else if (successor != null)
            {
                successor.FindOwner(exception, out appropriateExceptionType);
            }
        }

        #endregion


        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(Exception ex) : base(ex.Message) { }
    }
}
