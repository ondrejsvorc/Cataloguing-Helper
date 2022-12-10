using System;
using System.Windows;

namespace CataloguingHelper.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected abstract string ExceptionMessage { get; }

        public BaseException() => MessageBox.Show(ExceptionMessage);
    }
}
