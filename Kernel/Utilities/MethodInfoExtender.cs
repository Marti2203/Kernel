using System;
namespace Kernel.Utilities
{
    public static class MethodInfoExtender
    {
        public static bool IsOrIsSubclassOf(this Type type, Type hierarchyRoot)
        => type == hierarchyRoot || type.IsSubclassOf(hierarchyRoot);
    }
}
