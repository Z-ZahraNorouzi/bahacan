using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions
{
    public class ConstraintException : BaseException
    {
        #region BaseException implementation

        public override void FindOwner(Exception exception, out Exception appropriateExceptionType)
        {
            appropriateExceptionType = null;
            // todo: write better check!
            if (exception.Message.ToLower().Contains("cannot insert duplicate key in object"))
            {
                appropriateExceptionType = new ConstraintException(exception);
            }
            else if (successor != null)
            {
                successor.FindOwner(exception, out appropriateExceptionType);
            }
        }

        #endregion
        

        public ConstraintException() { }
        public ConstraintException(string message) : base(message) { }
        public ConstraintException(Exception ex) : base(ex.Message) { }
    }
}
