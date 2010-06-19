using System;
using System.ComponentModel;

namespace ThemeExtensions.Internal
{
    //http://stackoverflow.com/questions/1464737/hiding-gethashcode-equals-tostring-from-fluent-interface-classes-intellisense-in
    public abstract class HideDefaultMethods
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Boolean Equals(Object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override String ToString()
        {
            return base.ToString();
        }
    }
}
