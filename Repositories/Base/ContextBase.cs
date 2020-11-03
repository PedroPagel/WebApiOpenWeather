using Repositories.Context;
using System;

namespace Repositories.Base
{
    public class ContextBase
    {
        protected readonly WebAppContext _context;
        
        public ContextBase(WebAppContext context)
        {
            this._context = context;
        }
    }
}
