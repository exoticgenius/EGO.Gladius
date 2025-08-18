using Microsoft.Win32.SafeHandles;

using Mono.Cecil;
using Mono.Cecil.Cil;

using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        //var path = args[0];
        var path = "C:\\Users\\f.sadeghi\\source\\repos\\EGO.Gladius\\EGO.Gladius.Experiments\\bin\\Debug\\net9.0\\EGO.Gladius.Experiments.dll";
        SafeFileHandle sfh = File.OpenHandle(path,
            FileMode.Open,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            FileOptions.RandomAccess);

        var cw = new FileStream(sfh, FileAccess.ReadWrite);

        var asm = AssemblyDefinition.ReadAssembly(cw, new ReaderParameters { ReadWrite = true });
        foreach (var method in asm.MainModule.Types.SelectMany(t => t.Methods).Where(m => m.HasBody))
        {
            if (method.Name != "Transform")
                continue;

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

            //var writeLine = asm.MainModule.ImportReference(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            //il.InsertBefore(first, il.Create(OpCodes.Ldstr, "Hello from Cecil"));
            //il.InsertBefore(first, il.Create(OpCodes.Call, writeLine));
            var ret = il.Create(OpCodes.Ret);
            var loadRet = il.Create(OpCodes.Ldloc_S, retVar);
            var storeEx = il.Create(OpCodes.Stloc, exVar);

            var storeRet = il.Create(OpCodes.Stloc, retVar);
            il.Replace(last, storeRet);
            
            il.Append(il.Create(OpCodes.Leave, loadRet));


            var point = il.Create(OpCodes.Nop);
            il.Append(point);

            il.Append(storeEx);
            il.Append(il.Create(OpCodes.Ldc_I4, 10));
            il.Append(il.Create(OpCodes.Stloc_S, retVar));

            il.Append(il.Create(OpCodes.Leave, loadRet));

            var cartchEnd = il.Create(OpCodes.Nop);
            il.Append(cartchEnd);

            il.Append(loadRet);
            il.Append(ret);


            var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = first,
                TryEnd = point,
                HandlerStart = point,
                HandlerEnd = cartchEnd,
                CatchType = asm.MainModule.ImportReference(typeof(System.Exception)),
            };

            method.Body.ExceptionHandlers.Add(handler);
        }
        asm.Write(cw);
    }
}
