using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build
{
    public class TaskResults
    {
        public Task Task;
        public Rule RuleBroke;
        public Task FixTask;
        public Task Addtional;
        public bool Pass;

        public TaskResults(Task _Task)
        {
            Task = _Task;
            RuleBroke = null;
            FixTask = null;
            Addtional = null;
            Pass = false;
        }

        public TaskResults(Task _Task,Rule _RuleBroke, Task _FixTask, Task _Addtional, bool _Pass)
        {
            Task = _Task;
            RuleBroke = _RuleBroke;
            FixTask = _FixTask;
            Addtional = _Addtional;
            Pass = _Pass;      
        }


    }
}
