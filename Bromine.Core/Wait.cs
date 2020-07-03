using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Core
{
    public static class Wait
    {
        public static WaitCondition Until(WaitingConditionType waitingConditionType, string textContained, int timeout = 90)
        {
            if (waitingConditionType == WaitingConditionType.UntilElementContains && string.IsNullOrEmpty(textContained))
                throw new WaitElementContainsException(
                    "You have chosen wait until the element contains certain text, but that text is empty, that is not allowed");
            var condition = new WaitCondition
            {
                TextContained = textContained,
                WaitingConditionType = waitingConditionType,
                Timeout = timeout
            };
            return condition;
        }
    }

    public class WaitCondition
    {
        public WaitingConditionType WaitingConditionType { get; set; }

        public string TextContained { get; set; }

        public int  Timeout { get; set; }
    }
}
