using System.Text.RegularExpressions;

namespace ErpApi.Logic
{
    public class InternalLogic
    {
        public string uniqueCode()
        {
            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var barcode = "OCT-" + Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 12);
            return barcode;
        }
    }
}
