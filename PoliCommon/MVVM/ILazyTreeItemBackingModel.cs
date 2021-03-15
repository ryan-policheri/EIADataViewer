using System.Collections.Generic;

namespace PoliCommon.MVVM
{
    public interface ILazyTreeItemBackingModel
    {
        string GetId();
        string GetItemName();
        bool IsKnownLeaf();
    }
}
