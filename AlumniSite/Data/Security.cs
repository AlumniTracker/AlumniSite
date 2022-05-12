using System.Text.RegularExpressions;
using System.Web;

namespace AlumniSite.Data
{
    public static class Security
    {
        private static string BlackList = 
            @"<>/\'{};:`&|";


        public static bool GeneralInput(string input)
        {
            input = input.ToLower();
            //input = (string)input.Distinct();

            if (input.Contains(BlackList))
            { return false; }
            return true;
        }
        public static bool EmailInput(string input)
        {
            if (GeneralInput(input))
            {
                //HttpUtility.HtmlEncode(input); //
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"); //TEST
                Match match = regex.Match(input);
                if (!match.Success)
                { return false; }
                return true;
            }
            return false;
        }
        //don't actually know of a reason for this line, since there will be alumni pass and admin pass checked in browser. I guess we could double check since why not
        //public static bool PasswordInput(string input)
        //{
        //    if (GeneralInput(input))
        //    {
        //        if (!)
        //        {

        //        }
        //        return true;
        //    }
        //    return false;
        //}

        // not implemented
        //public static bool SearchInput(string input)
        //{
        //    if (GeneralInput(input))
        //    {
        //        if(!)
        //        {

        //        }
        //        return true;
        //    }
        //    return false;
        //}
        
    }
}
