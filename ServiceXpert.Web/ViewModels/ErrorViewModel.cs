namespace ServiceXpert.Web.ViewModels;
public class ErrorViewModel
{
    public string Error { get; set; } = "An error occurred while processing your request.";

    public IEnumerable<string> Errors { get; set; }

    public ErrorViewModel()
    {
        this.Errors = [];
    }
}
