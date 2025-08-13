using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace EGO.Gladius.Core;

public static class SP
{
    private static ConcurrentDictionary<Assembly, ModuleBuilder> ModuleBuilders;
    private static ConcurrentDictionary<Type, Type> TypeMapper;
    static SP()
    {
        ModuleBuilders = new();
        TypeMapper = new();
    }

    public static Type Extend<T>() =>
        Extend(typeof(T));

    public static Type? Extend(Type target)
    {
        if (!target
            .GetRuntimeMethods()
            .Any(x =>
                typeof(ISP).IsAssignableFrom(x.ReturnType) ||
                    typeof(Task).IsAssignableFrom(x.ReturnType) &&
                    x.ReturnType.IsGenericType &&
                    typeof(ISP).IsAssignableFrom(x.ReturnType.GenericTypeArguments[0])))
            return null;

        ModuleBuilder module = ModuleBuilders
            .GetOrAdd(
                target.Assembly,
                (a) => AssemblyBuilder
                    .DefineDynamicAssembly(
                        new AssemblyName($"RTGA_{a.FullName}"),
                        AssemblyBuilderAccess.Run)
                    .DefineDynamicModule("RTGM_RTGT_container"));

        string typeName = $"{target.Name}_{Guid.NewGuid().ToString().Replace('-', '_')}_RTGT";

        if (TypeMapper.TryGetValue(target, out Type? type))
            return type;

        TypeBuilder typeBuilder = module.DefineType(
            typeName,
            TypeAttributes.Public,
            target,
            target.GetInterfaces());

        foreach (ConstructorInfo item in target.GetConstructors())
            GenerateConstructor(typeBuilder, item);

        foreach (MethodInfo? item in target
            .GetRuntimeMethods()
            .Where(x =>
                typeof(ISP).IsAssignableFrom(x.ReturnType) ||
                    typeof(Task).IsAssignableFrom(x.ReturnType) &&
                    x.ReturnType.IsGenericType &&
                    typeof(ISP).IsAssignableFrom(x.ReturnType.GenericTypeArguments[0])))
            GenerateMethod(item, typeBuilder);

        Type ct = TypeMapper[target] = typeBuilder.CreateType()!;

        return ct;
    }

    private static void GenerateConstructor(TypeBuilder type, ConstructorInfo item)
    {
        List<Type> prms = new();
        prms.AddRange(item.GetParameters().Select(x => x.ParameterType).ToArray());
        ConstructorBuilder ctor = type.DefineConstructor(item.Attributes, item.CallingConvention, prms.ToArray());
        ILGenerator il = ctor.GetILGenerator();

        for (int i = 0; i < prms.Count + 1; i++)
            il.Emit(OpCodes.Ldarg, i);

        il.Emit(OpCodes.Call, item);
        il.Emit(OpCodes.Ret);
    }

    private static void GenerateMethod(MethodInfo methodinfo, TypeBuilder type)
    {
        List<Type> prms = new();
        prms.AddRange(methodinfo.GetParameters().Select(x => x.ParameterType).ToArray());
        Type returnType = methodinfo.ReturnType;
        MethodBuilder method = type.DefineMethod(
            methodinfo.Name,
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual | MethodAttributes.NewSlot,
            CallingConventions.Standard | CallingConventions.HasThis,
            methodinfo.ReturnType,
            methodinfo.GetParameters().Select(x => x.ParameterType).ToArray());
        ILGenerator il = method.GetILGenerator();

        Label exBlock = il.BeginExceptionBlock();
        Label end = il.DefineLabel();
        il.DeclareLocal(methodinfo.ReturnType); //      0
        il.DeclareLocal(typeof(Type)); //               1
        il.DeclareLocal(typeof(Exception)); //          2

        for (int i = 0; i < prms.Count + 1; i++)
            il.Emit(OpCodes.Ldarg, i);
        il.Emit(OpCodes.Call, methodinfo);

        if (typeof(Task).IsAssignableFrom(returnType))
        {
            Type[] genArg = [returnType.GenericTypeArguments[0]];

            if (genArg[0].GenericTypeArguments.Length > 0)
                genArg = genArg[0].GenericTypeArguments;

            string suppressor = "SuppressTask";
            if (typeof(VSP).IsAssignableFrom(genArg[0]))
                suppressor = "SuppressTaskVoid";

            il.Emit(OpCodes.Call, typeof(SP).GetMethods().First(x => x.Name == suppressor).MakeGenericMethod(genArg));
        }
        else if (typeof(ValueTask).IsAssignableFrom(returnType))
        {
            Type[] genArg = [returnType.GenericTypeArguments[0]];

            if (genArg[0].GenericTypeArguments.Length > 0)
                genArg = genArg[0].GenericTypeArguments;

            string suppressor = "SuppressValueTask";
            if (typeof(VSP).IsAssignableFrom(genArg[0]))
                suppressor = "SuppressValueTaskVoid";

            il.Emit(OpCodes.Call, typeof(SP).GetMethods().First(x => x.Name == "SuppressValueTask").MakeGenericMethod(genArg));
        }

        il.Emit(OpCodes.Stloc_0);
        il.Emit(OpCodes.Leave_S, end);
        il.BeginCatchBlock(typeof(Exception));

        il.Emit(OpCodes.Stloc_2);
        il.Emit(OpCodes.Ldtoken, methodinfo);
        il.Emit(OpCodes.Ldloc_1); // 1 => 0

        il.Emit(OpCodes.Ldc_I4, prms.Count);
        il.Emit(OpCodes.Newarr, typeof(object)); // => 1

        for (int i = 0; i < prms.Count; i++)
        {
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldarg, i + 1);
            if (prms[i].IsValueType)
                il.Emit(OpCodes.Box, prms[i]);
            il.Emit(OpCodes.Stelem_Ref);
        }

        il.Emit(OpCodes.Ldloc_2); //2 => 2

        il.Emit(OpCodes.Newobj, typeof(SPF).GetConstructor(new Type[] { typeof(MethodInfo), typeof(object[]), typeof(Exception) })!);

        if (typeof(Task).IsAssignableFrom(returnType))
        {
            il.Emit(OpCodes.Newobj, returnType.GenericTypeArguments[0].GetConstructor(new Type[] { typeof(SPF) })!);
            il.Emit(OpCodes.Call, typeof(Task).GetRuntimeMethods().First(x => x.Name == "FromResult").MakeGenericMethod(returnType.GenericTypeArguments[0]));

        }
        else if (typeof(ValueTask).IsAssignableFrom(returnType))
        {
            il.Emit(OpCodes.Newobj, returnType.GenericTypeArguments[0].GetConstructor(new Type[] { typeof(SPF) })!);
            il.Emit(OpCodes.Call, typeof(ValueTask<>).MakeGenericType(returnType.GenericTypeArguments[0]).GetConstructor(new Type[] { returnType.GenericTypeArguments[0] })!);
        }
        else
        {
            il.Emit(OpCodes.Newobj, methodinfo.ReturnType.GetConstructor(new Type[] { typeof(SPF) })!);
        }
        il.Emit(OpCodes.Stloc_0);
        il.Emit(OpCodes.Leave_S, end);

        il.EndExceptionBlock();

        il.MarkLabel(end);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ret);
    }

    [DebuggerStepThrough]
    public static async Task<VSP> SuppressTaskVoid<T>(Task<VSP> input)
    {
        try
        {
            return await input;
        }
        catch (Exception ex)
        {
            return new VSP(new SPF(ex));
        }
    }

    [DebuggerStepThrough]
    public static async Task<SPR<T>> SuppressTask<T>(Task<SPR<T>> input)
    {
        try
        {
            return await input;
        }
        catch (Exception ex)
        {
            return new SPR<T>(new SPF(ex));
        }
    }

    [DebuggerStepThrough]
    public static async ValueTask<VSP> SuppressValueTaskVoid<T>(ValueTask<VSP> input)
    {
        try
        {
            return await input;
        }
        catch (Exception ex)
        {
            return new VSP(new SPF(ex));
        }
    }

    [DebuggerStepThrough]
    public static async ValueTask<SPR<T>> SuppressValueTask<T>(ValueTask<SPR<T>> input)
    {
        try
        {
            return await input;
        }
        catch (Exception ex)
        {
            return new SPR<T>(new SPF(ex));
        }
    }

    public static T Gen<T>(params object?[]? @params) =>
        SP<T>.Gen(@params);

    public static SPR<T> Sup<T>(Func<T> function)
    {
        try
        {
            return function();
        }
        catch (Exception e)
        {
            return new SPR<T>(new SPF(e));
        }
    }
}

public static class SP<T>
{
    private static Type GeneratedType = SP.Extend<T>();

    public static T Gen(params object?[]? @params)
    {
        return (T)Activator.CreateInstance(GeneratedType, @params)!;
    }
}
