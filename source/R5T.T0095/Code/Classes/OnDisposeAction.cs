using System;
using System.Threading.Tasks;


namespace R5T.T0095
{
    public sealed class OnDisposeAction : IAsyncDisposable
    {
        private Func<Task> Action { get; }


        public OnDisposeAction(Func<Task> action)
        {
            this.Action = action;
        }

        private bool zDisposed = false;

        public async ValueTask DisposeAsync()
        {
            if (this.zDisposed)
            {
                return;
            }

            await this.Action();

            this.zDisposed = true;

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }
    }
}
