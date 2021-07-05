namespace SFA.DAS.Apprentice.SharedUi.Menu
{
    public class NavigationSection
    {
        public static readonly NavigationSection Home = new NavigationSection("Home");
        public static readonly NavigationSection HelpAndSupport = new NavigationSection("HelpAndSupport");
        public static readonly NavigationSection ConfirmMyApprenticeship = new NavigationSection("ConfirmMyApprenticeship");

        public string Name { get; }

        public NavigationSection(string name) => Name = name;

        public override string ToString() => Name;
    }
}