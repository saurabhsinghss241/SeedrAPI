using System.Text.RegularExpressions;

namespace SeedrService.Helpers
{
    public static class HelperMethods
    {
        public static bool IsMagnetLinkValid(string magnetLink)
        {
            // Regular expression to match a valid magnet link
            string pattern = @"^magnet:\?xt=urn:[a-zA-Z0-9]+:[a-zA-Z0-9]+&dn=.+&tr=.+";

            // Create a regular expression object
            Regex regex = new Regex(pattern);

            // Check if the input string matches the pattern
            return regex.IsMatch(magnetLink);
        }
    }
}
