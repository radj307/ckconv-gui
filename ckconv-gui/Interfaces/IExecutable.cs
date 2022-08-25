using System;

namespace ckconv_gui.Interfaces
{
    internal interface IExecutable : IDisposable
    {
        bool Exists { get; }
        void Setup();
        void Teardown();
        string? Exec(params string[] arguments);
    }
}
