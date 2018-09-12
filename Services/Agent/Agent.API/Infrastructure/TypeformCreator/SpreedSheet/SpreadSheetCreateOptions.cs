using System.Collections.Generic;

namespace TypeFormIntegration
{
    public class SpreadSheetCreateOptions
    {
        public string SpreadSheetName;
        public string WorkSheetName;
        public IList<object> headerValues;
        public IList<object> InitialValues;
    }
}
