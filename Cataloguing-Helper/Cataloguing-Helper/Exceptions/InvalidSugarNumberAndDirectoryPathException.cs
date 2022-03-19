namespace CataloguingHelper.Exceptions
{
    internal class InvalidSugarNumberAndDirectoryPathException : BaseException
    {
        protected override string ExceptionMessage => "Musíte zadat číslo prvního cukru (např. 003447) a také vybrat složku s fotkami.";
    }
}
