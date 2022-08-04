using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions
{
    public class ConflictException : BaseException
    {
        #region BaseException implementation

        public override void FindOwner(Exception exception, out Exception appropriateExceptionType)
        {
            appropriateExceptionType = null;
            if (exception.Message.ToLower().Contains("the conflict occurred in database") || exception.Message.ToLower().Contains("the relationship could not be changed because one or more of the foreign-key properties is non-nullable"))
            {
                appropriateExceptionType = new ConflictException(exception);
            }
            else if (successor != null)
            {
                successor.FindOwner(exception, out appropriateExceptionType);
            }
        }

        #endregion
        

        public ConflictException() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(Exception ex) : base(ex) { }
    }
}
