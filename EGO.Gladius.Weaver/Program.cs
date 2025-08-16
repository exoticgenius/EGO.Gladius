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
            method.Body.Instructions.Clear();

            foreach (var i in oldInstr) il.Append(i);
            il.Append(il.Create(OpCodes.Ldstr, "str to write"));
            var writeLine = asm.MainModule.ImportReference(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            il.Append(il.Create(OpCodes.Call, writeLine));

            il.Append(il.Create(OpCodes.Ret));




            var write = il.Create(
                OpCodes.Call,
                module.Import(typeof(Console).GetMethod("WriteLine", new[] { typeof(object) })));
            var ret = il.Create(OpCodes.Ret);
            var leave = il.Create(OpCodes.Leave, ret);

            il.InsertAfter(
                method.Body.Instructions.Last(),
                write);

            il.InsertAfter(write, leave);
            il.InsertAfter(leave, ret);

            var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = method.Body.Instructions.First(),
                TryEnd = write,
                HandlerStart = write,
                HandlerEnd = ret,
                CatchType = asm.MainModule.ImportReference(typeof(Exception)),
            };

            method.Body.ExceptionHandlers.Add(handler);
        }
        asm.Write(cw);
    }
}
