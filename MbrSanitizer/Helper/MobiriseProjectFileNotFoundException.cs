namespace MbrSanitizer.Helper;

internal class MobiriseProjectFileNotFoundException : FileNotFoundException
{
    public MobiriseProjectFileNotFoundException() : base("Im Projekt Verzeichnis existiert keine Mobirise Projekt-Datei.")
    {
    }
    public MobiriseProjectFileNotFoundException(string message) : base(message)
    {
    }
    public MobiriseProjectFileNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}