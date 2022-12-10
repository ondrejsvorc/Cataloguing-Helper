namespace CataloguingHelper.Exceptions
{
    public class InvalidDirectoryPathException : BaseException
    {
        protected override string ExceptionMessage => "Musíte vybrat složku s fotkami. Klikněte na tlačítko vedle textového pole.";
    }
}
