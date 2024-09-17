namespace SFA.DAS.ApprenticePortal.Authentication;

public static class DomainExtensions
{
    public static string GetDomain(string environment)
    {
        return environment.ToLower() switch
        {
            "local" => "",
            "prd" => "my.apprenticeships.education.gov.uk",
            _ => $"{environment.ToLower()}-aas.apprenticeships.education.gov.uk"
        };
    }
}   