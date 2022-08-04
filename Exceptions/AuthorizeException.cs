using System;

namespace Exceptions
{
    public class AuthorizeException : BaseException
    {
        #region BaseException implementation

        public override void FindOwner(Exception exception, out Exception appropriateExceptionType)
        {
            appropriateExceptionType = null;
            // todo: write better check!
            if (exception.Message.ToUpper().Contains("AUTHORIZE"))
            {
                appropriateExceptionType = new AuthorizeException(exception);
            }
            else if (successor != null)
            {
                successor.FindOwner(exception, out appropriateExceptionType);
            }
        }

        #endregion

        public AuthorizeException() { }
        public AuthorizeException(string message) : base(message) { }
        public AuthorizeException(Exception ex) : base(ex.Message) { }

        public object SourceValue { get; set; }

        public Type DestinationType { get; set; }
    }
}
