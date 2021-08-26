using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FunApp.Web.Extentions
{
    public static class TempDataDictionaryExtention
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData["SuccessMessage"] = message;
        }
    }
}
