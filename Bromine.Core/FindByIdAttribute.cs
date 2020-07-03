using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Core
{
    public enum FindByType { ById = 1, ByName = 2, ByCssSelector = 3, ByClassName = 4, ByXPath = 5 }

    public enum WaitingConditionType
    {
        NoWait = 0,
        UntilElementIsExists = 1,
        UntilElementIsVisible = 2,
        UntilElementContains = 3
    }

    public class FindByAttribute:Attribute
    {
        public string TextCriteria { get; set; }

        public FindByType Criteria { get; set; }

        public WaitingConditionType WaitConditionType { get; set; }

        public int Timeout { get; set; }

        public FindByAttribute(FindByType criteria, string textCriteria, WaitingConditionType waitConditiontype = WaitingConditionType.NoWait)
        {
            Criteria = criteria;
            TextCriteria = textCriteria;
            WaitConditionType = waitConditiontype;
        }

        public FindByAttribute(FindByType criteria, WaitCondition condition)
        {
            Criteria = criteria;
            TextCriteria = condition.TextContained;
            WaitConditionType = condition.WaitingConditionType;
            Timeout = condition.Timeout;
        }
    }
}