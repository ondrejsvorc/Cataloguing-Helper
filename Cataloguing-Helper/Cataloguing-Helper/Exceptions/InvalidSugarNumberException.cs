namespace CataloguingHelper.Exceptions
{
    public class InvalidSugarNumberException : BaseException
    {
        protected override string ExceptionMessage => "Musíte zadat číslo prvního cukru (např. 003447)";
    }
}
