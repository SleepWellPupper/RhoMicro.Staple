// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Content;

/// <summary>
/// Provides extension members for retrieving documentation contexts and parsed documentation content.
/// </summary>
public static class DocumentationAccessor
{
    private static readonly ConditionalWeakTable<Assembly, DocumentationContext> _contexts = new();
    private static readonly Object _contextSyncRoot = new();
    private static readonly ConditionalWeakTable<Object, Documentation?> _docs = new();
    private static readonly Object _docsSyncRoot = new();

    extension(Assembly assembly)
    {
        /// <summary>
        /// Gets the documentation context cached for the assembly.
        /// </summary>
        public DocumentationContext DocumentationContext
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (_contexts.TryGetValue(assembly, out var result))
                {
                    return result;
                }

                lock (_contextSyncRoot)
                {
                    if (_contexts.TryGetValue(assembly, out result))
                    {
                        return result;
                    }

                    result = DocumentationContext.Create(assembly);
                    _contexts.Add(assembly, result);

                    return result;
                }
            }
        }
    }

    private static String GetId(Type type)
    {
        var resultBuilder = new StringBuilder();
        resultBuilder.Append("T:");
        AppendTypeId(type, resultBuilder);
        var result = resultBuilder.ToString();

        return result;

        static void AppendTypeId(Type currentType, StringBuilder builder)
        {
            if (currentType.IsArray)
            {
                if (currentType.GetElementType() is { } elementType)
                {
                    AppendTypeId(elementType, builder);
                }

                builder.Append('[')
                    .Append(',', currentType.GetArrayRank() - 1)
                    .Append(']');

                return;
            }

            if (currentType.IsPointer)
            {
                if (currentType.GetElementType() is { } elementType)
                {
                    AppendTypeId(elementType, builder);
                }

                builder.Append('*');
                return;
            }

            if (currentType.IsByRef)
            {
                if (currentType.GetElementType() is { } elementType)
                {
                    AppendTypeId(elementType, builder);
                }

                builder.Append('@');
                return;
            }

            if (currentType.IsGenericParameter)
            {
                builder.Append(currentType.Name);
                return;
            }

            if (currentType is { IsGenericType: true, IsGenericTypeDefinition: false })
            {
                currentType = currentType.GetGenericTypeDefinition();
            }

            if (currentType.DeclaringType is null)
            {
                if (!String.IsNullOrEmpty(currentType.Namespace))
                {
                    builder.Append(currentType.Namespace);
                    builder.Append('.');
                }

                builder.Append(currentType.Name);
                return;
            }

            AppendTypeId(currentType.DeclaringType, builder);
            builder.Append('.');
            builder.Append(currentType.Name);
        }
    }

    private static String GetId(MemberInfo member)
    {
        var resultBuilder = new StringBuilder();

        resultBuilder.Append(member switch
        {
            FieldInfo => "F:",
            MethodInfo => "M:",
            PropertyInfo => "P:",
            EventInfo => "E:",
            ConstructorInfo => "M:",
            _ => String.Empty
        });
        AppendMemberId(member, resultBuilder);

        var result = resultBuilder.ToString();

        return result;

        static void AppendMemberId(MemberInfo currentMember, StringBuilder builder)
        {
            if (currentMember.DeclaringType is { } declaringType)
            {
                AppendTypeId(declaringType, builder);
                builder.Append('.');
            }

            switch (currentMember)
            {
                case FieldInfo field:
                    builder.Append(field.Name);
                    return;
                case PropertyInfo property:
                    builder.Append(property.Name);
                    return;
                case EventInfo @event:
                    builder.Append(@event.Name);
                    return;
                case MethodInfo method:
                    AppendMethodId(method, builder);
                    return;
                case ConstructorInfo constructor:
                    builder.Append(constructor.IsStatic ? "#cctor" : "#ctor");
                    return;
                default:
                    return;
            }
        }

        static void AppendMethodId(MethodInfo method, StringBuilder builder)
        {
            builder.Append(method.Name);

            if (method.IsGenericMethod)
            {
                builder.Append("``");
                builder.Append(method.GetGenericArguments().Length);
            }

            var parameters = method.GetParameters();
            if (parameters.Length == 0)
            {
                return;
            }

            builder.Append('(');

            for (var index = 0; index < parameters.Length; index++)
            {
                if (index > 0)
                {
                    builder.Append(',');
                }

                AppendParameterType(parameters[index].ParameterType, builder);
            }

            builder.Append(')');
        }

        static void AppendParameterType(Type parameterType, StringBuilder builder)
        {
            if (parameterType.IsGenericParameter)
            {
                builder.Append(parameterType.DeclaringMethod is null
                    ? "`"
                    : "``");
                builder.Append(parameterType.GenericParameterPosition);
                return;
            }

            AppendTypeId(parameterType, builder);
        }
    }

    private static void AppendTypeId(Type currentType, StringBuilder builder)
    {
        if (currentType.IsArray)
        {
            if (currentType.GetElementType() is { } elementType)
            {
                AppendTypeId(elementType, builder);
            }

            builder.Append('[')
                .Append(',', currentType.GetArrayRank() - 1)
                .Append(']');

            return;
        }

        if (currentType.IsPointer)
        {
            if (currentType.GetElementType() is { } elementType)
            {
                AppendTypeId(elementType, builder);
            }

            builder.Append('*');
            return;
        }

        if (currentType.IsByRef)
        {
            if (currentType.GetElementType() is { } elementType)
            {
                AppendTypeId(elementType, builder);
            }

            builder.Append('@');
            return;
        }

        if (currentType.IsGenericParameter)
        {
            builder.Append(currentType.Name);
            return;
        }

        if (currentType is { IsGenericType: true, IsGenericTypeDefinition: false })
        {
            currentType = currentType.GetGenericTypeDefinition();
        }

        if (currentType.DeclaringType is null)
        {
            if (!String.IsNullOrEmpty(currentType.Namespace))
            {
                builder.Append(currentType.Namespace);
                builder.Append('.');
            }

            builder.Append(currentType.Name);
            return;
        }

        AppendTypeId(currentType.DeclaringType, builder);
        builder.Append('.');
        builder.Append(currentType.Name);
    }

    extension(Type type)
    {
        /// <summary>
        /// Gets the documentation comment for the type, if one is available.
        /// </summary>
        public Documentation? Documentation
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (_docs.TryGetValue(type, out var result))
                {
                    return result;
                }

                lock (_docsSyncRoot)
                {
                    if (_docs.TryGetValue(type, out result))
                    {
                        return result;
                    }

                    var typeId = GetId(type);
                    result = type.Assembly.DocumentationContext.GetContent(typeId);
                    _docs.Add(type, result);

                    return result;
                }
            }
        }
    }

    extension(MemberInfo member)
    {
        /// <summary>
        /// Gets the documentation comment for the member, if one is available.
        /// </summary>
        public Documentation? Documentation
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (_docs.TryGetValue(member, out var result))
                {
                    return result;
                }

                lock (_docsSyncRoot)
                {
                    if (_docs.TryGetValue(member, out result))
                    {
                        return result;
                    }

                    var typeId = GetId(member);
                    result = member.DeclaringType?.Assembly.DocumentationContext.GetContent(typeId);
                    _docs.Add(member, result);

                    return result;
                }
            }
        }
    }
}
