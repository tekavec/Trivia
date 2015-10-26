using System;
using System.ComponentModel;
using System.Reflection;

namespace Trivia
{
    public static class EnumExtensions
    {

        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
            return ((DescriptionAttribute) attributes[0]).Description;
        }
    }
}