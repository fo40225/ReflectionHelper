using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ReflectionHelper
{
    public static class Extender
    {
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            return Interaction.CallByName(o, methodName, CallType.Method, args);
        }

        public static object Field(this object o, string fieldName)
        {
            Type t = o.GetType();
            FieldInfo fi = t.GetField(
                fieldName,
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public);
            return fi.GetValue(o);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static T Field<T>(this object o, string fieldName)
        {
            return (T)o.Field(fieldName);
        }

        public static void Field<T>(this T o, string fieldName, string fieldValue)
        {
            Type t = o.GetType();
            FieldInfo fi = t.GetField(
                fieldName,
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public);
            fi.SetValue(o, fieldValue);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static object Property(this object o, string propertyName)
        {
            return Interaction.CallByName(o, propertyName, CallType.Get);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static T Property<T>(this object o, string propertyName)
        {
            return (T)o.Property(propertyName);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Property<T>(this T o, string propertyName, object propertyValue)
        {
            Interaction.CallByName(o, propertyName, CallType.Set, propertyValue);
        }
    }

    public static class TypeFactory
    {
        public static Type GetTypeByName(string typeName)
        {
            var t = Type.GetType(typeName, false);

            if (t == null)
            {
                foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = item.GetType(typeName, false);
                    if (t != null)
                    {
                        break;
                    }
                }
            }
            return t;
        }
    }
}