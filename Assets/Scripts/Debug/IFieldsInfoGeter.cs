using System.Collections.Generic;

namespace Debug
{
    public interface IFieldsInfoGetter
    {
        public List<(string name, string value)> GetDebugFields();
    }
}