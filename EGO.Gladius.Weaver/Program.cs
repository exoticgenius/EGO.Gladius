using EGO.Gladius.DataTypes;
using EGO.Gladius.Metadata;

using Microsoft.Win32.SafeHandles;

using Mono.Cecil;
using Mono.Cecil.Cil;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: LibrarySkipper]
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
            return;
        Console.WriteLine("EGO Gladius Weaver: starting to weave dlls");
        int c = 0;
        foreach (var path in args)
        {
            SafeFileHandle sfh = File.OpenHandle(path,
                FileMode.Open,
                FileAccess.ReadWrite,
                FileShare.ReadWrite,
                FileOptions.RandomAccess);

            var cw = new FileStream(sfh, FileAccess.ReadWrite);
            try
            {

                var asm = AssemblyDefinition.ReadAssembly(cw, new ReaderParameters { ReadWrite = true, ReadSymbols = true });

                //if (!asm.CustomAttributes.Any(x => x.AttributeType.Resolve() == asm.MainModule.ImportReference(typeof(LibrarySkipper)).Resolve()))
                {
                    foreach (var method in asm.MainModule.Types.SelectMany(t => t.Methods).Where(m => m.HasBody))
                    {
                        if (method.ReturnType.Resolve() == method.Module.ImportReference(typeof(SPR<>)).Resolve())
                        {
                            HandleNormal(asm, method);
                            c++;
                        }
                        else if (method.ReturnType.Resolve() == method.Module.ImportReference(typeof(Task<>)).Resolve() ||
                            method.ReturnType.Resolve() == method.Module.ImportReference(typeof(ValueTask<>)).Resolve())
                        {
                            HandleTask(asm, method);
                            c++;
                        }
                    }
                    asm.Write(cw, new WriterParameters() { WriteSymbols = true });
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                sfh.Close();
            }


            //try
            //{
            //    var asms = Assembly.LoadFile(path);
            //    foreach (var type in asms.GetTypes())
            //    {
            //        foreach (var method in type.GetMethods(
            //            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            //        {
            //            try
            //            {
            //                if (method.MethodImplementationFlags.HasFlag(System.Reflection.MethodImplAttributes.IL))
            //                    RuntimeHelpers.PrepareMethod(method.MethodHandle);
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine($"{type.FullName}.{method.Name} failed JIT: {ex.GetType().Name} - {ex.Message}");
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //}
        }
        Console.WriteLine($"EGO Gladius Weaved {c} methods");
    }

    private static void HandleNormal(AssemblyDefinition asm, MethodDefinition method)
    {
        var il = method.Body.GetILProcessor();
        var oldInstr = method.Body.Instructions.ToList();
        var first = oldInstr.First();
        var last = oldInstr.Last();

        var retType = asm.MainModule.ImportReference(method.ReturnType);
        var retVar = new VariableDefinition(retType);
        method.Body.Variables.Add(retVar);

        var exType = asm.MainModule.ImportReference(typeof(Exception));
        var exVar = new VariableDefinition(exType);
        method.Body.Variables.Add(exVar);

        // labels
        var catchStart = il.Create(OpCodes.Nop);
        var catchEnd = il.Create(OpCodes.Nop);
        var ret = il.Create(OpCodes.Ret);
        var loadRet = il.Create(OpCodes.Ldloc_S, retVar);

        // hooks end of method to leave peacefully out of try block
        foreach (var item in oldInstr)
        {
            if (item.OpCode == OpCodes.Ret)
            {

                il.InsertBefore(item, il.Create(OpCodes.Stloc, retVar));
                il.Replace(item, il.Create(OpCodes.Leave, loadRet));
            }
        }



        // catch block
        il.Append(catchStart);
        il.Emit(OpCodes.Stloc, exVar);
        il.Emit(OpCodes.Ldloc, exVar);

        var spfType = asm.MainModule.ImportReference(typeof(SPF));

        var spfCtor = asm.MainModule.ImportReference(spfType.Resolve().Methods
            .First(m => m.IsConstructor &&
                        m.Parameters.Count == 1 &&
                        m.Parameters[0].ParameterType.FullName == exType.FullName));

        var retCtor = asm.MainModule.ImportReference(method.ReturnType.Resolve().Methods
            .First(m => m.IsConstructor &&
                        m.Parameters.Count == 1 &&
                        m.Parameters[0].ParameterType.FullName == spfType.FullName));

        retCtor.DeclaringType = method.ReturnType;

        il.Emit(OpCodes.Newobj, spfCtor);
        il.Emit(OpCodes.Newobj, retCtor);

        il.Emit(OpCodes.Stloc_S, retVar);
        il.Emit(OpCodes.Leave, loadRet);


        il.Append(catchEnd);


        il.Append(loadRet);
        il.Append(ret);


        var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
        {
            TryStart = first,
            TryEnd = catchStart,
            HandlerStart = catchStart,
            HandlerEnd = catchEnd,
            CatchType = asm.MainModule.ImportReference(typeof(System.Exception)),
        };

        method.Body.ExceptionHandlers.Add(handler);
        return;
    }
    private static void HandleTask(AssemblyDefinition asm, MethodDefinition methodBase)
    {
        var method = ((TypeDefinition)methodBase.CustomAttributes
            .First(a => a.AttributeType.FullName == typeof(System.Runtime.CompilerServices.AsyncStateMachineAttribute).FullName)
            .ConstructorArguments[0].Value)
            .Methods
            .First(x => x.Name == "MoveNext");

        var oldHandlers = method.Body.ExceptionHandlers;
        ExceptionHandler target = null;
        List<Instruction> targetInstructions = [];
        var il = method.Body.GetILProcessor();
        Instruction lastCatcher = null;
        foreach (var item in oldHandlers)
        {
            var start = item.HandlerStart.Offset;
            var end = item.HandlerEnd.Offset;

            var instrs = method.Body.Instructions.Where(x => x.Offset >= start && x.Offset < end).ToList();

            if (instrs.Any(x => x.Operand is MethodReference mr && mr.Name == "SetException"))
            {
                target = item;
                targetInstructions = instrs;
                lastCatcher = instrs[^1];
                //instrs[1..^1].ForEach(x => il.Replace(x, il.Create(OpCodes.Nop)));
                instrs[1..^1].ForEach(x => method.Body.Instructions.Remove(x));
                method.Body.ExceptionHandlers.Remove(target);
                break;
            }
        }

        var oldInstr = method.Body.Instructions.ToList();

        var retVar = method.Body.Variables.First(x => x.VariableType.Resolve() == ((Mono.Cecil.GenericInstanceType)methodBase.ReturnType).GenericArguments[0].Resolve());

        var exVar = method.Body.Variables.Last(x => x.VariableType.Resolve() == asm.MainModule.ImportReference(typeof(Exception)).Resolve());

        // labels
        //var ret = il.Create(OpCodes.Ret);
        //var loadRet = il.Create(OpCodes.Ldloc_S, retVar);

        // hooks and of method to leave peacefully out of try block
        //var storeRet = il.Create(OpCodes.Stloc, retVar);
        //il.Replace(last, storeRet);
        //il.Emit(OpCodes.Leave, loadRet);

        // catch block
        //var catchStart = il.Create(OpCodes.Stloc, exVar);
        //il.InsertBefore(lastCatcher, catchStart);
        il.InsertBefore(lastCatcher, il.Create(OpCodes.Ldloc, exVar));

        var spfType = asm.MainModule.ImportReference(typeof(SPF));

        var spfCtor = asm.MainModule.ImportReference(spfType.Resolve().Methods
            .First(m => m.IsConstructor &&
                        m.Parameters.Count == 1 &&
                        m.Parameters[0].ParameterType.FullName == asm.MainModule.ImportReference(typeof(Exception)).FullName));

        var retCtor = asm.MainModule.ImportReference(((Mono.Cecil.GenericInstanceType)methodBase.ReturnType).GenericArguments[0].Resolve().Methods
            .First(m => m.IsConstructor &&
                        m.Parameters.Count == 1 &&
                        m.Parameters[0].ParameterType.FullName == spfType.FullName));

        retCtor.DeclaringType = ((Mono.Cecil.GenericInstanceType)methodBase.ReturnType).GenericArguments[0];

        il.InsertBefore(lastCatcher, il.Create(OpCodes.Newobj, spfCtor));
        il.InsertBefore(lastCatcher, il.Create(OpCodes.Newobj, retCtor));





        il.InsertBefore(lastCatcher, il.Create(OpCodes.Stloc_S, retVar));
        il.Replace(lastCatcher, il.Create(OpCodes.Leave, lastCatcher.Next));



        //var catchEnd = il.Create(OpCodes.Leave, target.HandlerEnd);
        //il.InsertBefore(lastCatcher, il.Create(catchEnd);

        //il.Append(loadRet);
        //il.Append(ret);


        var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
        {
            TryStart = target.TryStart,
            TryEnd = target.TryEnd,
            HandlerStart = target.HandlerStart,
            HandlerEnd = target.HandlerEnd,
            CatchType = asm.MainModule.ImportReference(typeof(System.Exception)),
        };

        method.Body.ExceptionHandlers.Add(handler);
        return;
    }
}
