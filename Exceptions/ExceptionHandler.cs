using System;
using BusinessLogic;
using BusinessLogic.Action;
using BusinessModel;

namespace Exceptions
{
    public static class ExceptionHandler
    {
        public static Exception Throw(Exception exception)
        {
            Exception appropriateException = GetAppropriateException(exception);

            Log(appropriateException);

            throw appropriateException;
        }

        public static Exception GetAppropriateException(Exception exception)
        {
            // using ChainOfResponsibility pattern to find appropriate exception type
            var validationHandler = new ValidationException();
            var castingHandler = new CastException();
            var constraintHandler = new ConstraintException();
            var conflictHandler = new ConflictException();
            var authorizeHandler = new AuthorizeException();

            validationHandler.setSuccessor(castingHandler);
            castingHandler.setSuccessor(constraintHandler);
            constraintHandler.setSuccessor(conflictHandler);
            conflictHandler.setSuccessor(authorizeHandler);

            var startOfChain = validationHandler;

            Exception appropriateException = null;
            startOfChain.FindOwner(exception, out appropriateException);
            if (appropriateException == null)
                appropriateException = exception;
            return appropriateException;
        }

        public static void Log(Exception ex)
        {
            try
            {
                //ErrorHandlerBusinessModel errorHandlerBusinessModel = new ErrorHandlerBusinessModel();
                //errorHandlerBusinessModel.Message = ex.Message;
                //errorHandlerBusinessModel.ErrorData = ex.Data.ToString();
                //errorHandlerBusinessModel.HelpLink = ex.HelpLink;
                //errorHandlerBusinessModel.InnerException = ex.InnerException != null? ex.InnerException.ToString(): null;
                //errorHandlerBusinessModel.Source = ex.Source;
                //errorHandlerBusinessModel.CreationDate = DateTime.Now;
                //var action = new ErrorHandlerAction();
                //action.Add(errorHandlerBusinessModel);
                WriteInTxtFile(ex.ToString());
            }
            catch (Exception)
            {
                WriteInTxtFile(ex.ToString());
                throw (ex);
            }
        }

        public static Exception GetAppropriateException(string rawException)
        {
            Exception exception;
            try
            {
                exception = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(rawException);
            }
            catch (Exception)
            {
                exception = new Exception(rawException);
            }
            return GetAppropriateException(exception);
        }

        public static void WriteInTxtFile(string Text)
        {
            try
            {
                //System.IO.File.WriteAllText(@"c:\Log\" + DateTime.Now.ToString().Replace(":", "-").Replace("/", "-") + ".txt", Text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
