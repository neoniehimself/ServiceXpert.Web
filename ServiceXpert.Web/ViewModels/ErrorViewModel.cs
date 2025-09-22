namespace ServiceXpert.Web.ViewModels;
public class ErrorViewModel
{
    public string ErrorString { get; set; } = "An error occurred while processing your request.";

    public IEnumerable<string> ErrorList { get; set; }

    public ErrorViewModel()
    {
        this.ErrorList = [];
    }
}
