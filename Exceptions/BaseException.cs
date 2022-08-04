using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions
{
    public abstract class BaseException : Exception
    {
        #region Chain Of Responsibility

        protected BaseException successor;

        internal void setSuccessor(BaseException successor)
        {
            this.successor = successor;
        }

        public abstract void FindOwner(Exception exception, out Exception appropriateExceptionType);

        #endregion

        #region ctor

        public BaseException() { }
        public BaseException(string message) : base(message) { }
        public BaseException(Exception ex) : base(ex.Message) { }

        #endregion

    }
}
