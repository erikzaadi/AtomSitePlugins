using System;

namespace ThemeExtensions.Internal
{
    public abstract class HideDefaultMethods
    {
        private new string ToString()
        {
            return base.ToString();
        }

        private new bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        private new int GetHashCode()
        {
            return base.GetHashCode();
        }

        private new Type GetType()
        {
            return base.GetType();
        }

        private new Object MemberwiseClone()
        {
            return base.MemberwiseClone();
        }
    }
}
