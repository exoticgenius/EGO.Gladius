using Mono.Cecil;
using Mono.Cecil.Cil;

using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var path = args[0];
        var asm = AssemblyDefinition.ReadAssembly(path, new ReaderParameters { ReadWrite = true });
        foreach (var method in asm.MainModule.Types.SelectMany(t => t.Methods).Where(m => m.HasBody))
        {
            var il = method.Body.GetILProcessor();
            var handlerStart = il.Create(OpCodes.Nop);
            var handlerEnd = il.Create(OpCodes.Nop);
            var catchStart = il.Create(OpCodes.Nop);
            var oldInstr = method.Body.Instructions.ToList();
            method.Body.Instructions.Clear();
            il.Append(handlerStart);
            foreach (var i in oldInstr) il.Append(i);
            il.Append(handlerEnd);
            il.Append(catchStart);
            il.Append(il.Create(OpCodes.Ldstr, "str to write"));
            var writeLine = asm.MainModule.ImportReference(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            il.Append(il.Create(OpCodes.Call, writeLine));
            il.Append(il.Create(OpCodes.Ret));
            var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                CatchType = asm.MainModule.ImportReference(typeof(Exception)),
                TryStart = handlerStart,
                TryEnd = handlerEnd,
                HandlerStart = catchStart,
                HandlerEnd = il.Create(OpCodes.Nop)
            };
            il.Append(handler.HandlerEnd);
            method.Body.ExceptionHandlers.Add(handler);
        }
        asm.Write(path);
    }
}
