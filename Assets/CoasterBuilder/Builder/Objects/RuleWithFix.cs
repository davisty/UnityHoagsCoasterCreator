using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build
{
    public class RuleWithFix
    {
        public Rule Rule;
        public Task Fix;

        public RuleWithFix(Rule _Rule, Task _Fix)
        {
            Rule = _Rule;
            Fix = _Fix;
        }
    }
}
