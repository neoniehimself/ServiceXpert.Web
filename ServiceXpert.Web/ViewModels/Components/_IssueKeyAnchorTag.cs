namespace ServiceXpert.Web.ViewModels.Components
{
#pragma warning disable IDE1006 // Naming Styles
    public class _IssueKeyAnchorTag(string issueKey, string name, bool isKeyAsLink = true)
#pragma warning restore IDE1006 // Naming Styles
    {
        public string IssueKey { get; set; } = issueKey;

        public string Name { get; set; } = name;

        public bool IsKeyAsLinkName { get; set; } = isKeyAsLink;
    }
}
