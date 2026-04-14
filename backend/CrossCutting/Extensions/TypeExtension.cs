using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Comfy.SystemObjects.Extensions
{
    public static class TypeExtension
    {
        private static Dictionary<Type, string> TypesToFriendlyNames = new Dictionary<Type, string>
        {
            {typeof(bool), "bool"},
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(char), "char"},
            {typeof(decimal), "decimal"},
            {typeof(double), "double"},
            {typeof(float), "float"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(object), "object"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(string), "string"}
        };
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }
        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            return type.GetConstants().Select(c => c.GetRawConstantValue() as T);
        }


        public static string GetFriendlyName(this Type type)
        {
            if (type.IsArray)
                return type.GetFriendlyNameOfArrayType();
            if (type.IsGenericType)
                return type.GetFriendlyNameOfGenericType();
            if (type.IsPointer)
                return type.GetFriendlyNameOfPointerType();
            var aliasName = default(string);
            return TypesToFriendlyNames.TryGetValue(type, out aliasName)
                ? aliasName
                : type.Name;
        }

        private static string GetFriendlyNameOfArrayType(this Type type)
        {
            var arrayMarker = string.Empty;
            while (type.IsArray)
            {
                var commas = new string(Enumerable.Repeat(',', type.GetArrayRank() - 1).ToArray());
                arrayMarker += $"[{commas}]";
                type = type.GetElementType();
            }
            return type.GetFriendlyName() + arrayMarker;
        }

        private static string GetFriendlyNameOfGenericType(this Type type)
        {
            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments().First().GetFriendlyName() + "?";
            var friendlyName = type.Name;
            var indexOfBacktick = friendlyName.IndexOf('`');
            if (indexOfBacktick > 0)
                friendlyName = friendlyName.Remove(indexOfBacktick);
            var typeParameterNames = type
                .GetGenericArguments()
                .Select(typeParameter => typeParameter.GetFriendlyName());
            var joinedTypeParameters = string.Join(", ", typeParameterNames);
            return string.Format("{0}<{1}>", friendlyName, joinedTypeParameters);
        }

        private static string GetFriendlyNameOfPointerType(this Type type) =>
            type.GetElementType().GetFriendlyName() + "*";


        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            return typeof(T);
        }
    }
}
