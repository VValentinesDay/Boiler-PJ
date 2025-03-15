using Model;

namespace kotlTest.Common
{
    public abstract class TestCommandBase: IDisposable
    {
        protected readonly Context _context;

        public TestCommandBase() 
        {
            _context = ContextFactory.Create();
        }

        public void Dispose() 
        {
            ContextFactory.Destroy(_context);
        }
    }
}
