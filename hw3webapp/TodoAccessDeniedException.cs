using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3webapp
{
    public class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(string message) : base(message)
        {

        }

        public TodoAccessDeniedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}