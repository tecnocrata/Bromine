using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;

namespace Bromine.Core
{
    public class ControlDictionary
    {
        private BasePage page;
        public ControlDictionary(BasePage page)
        {
            this.page = page;
            controls = new Dictionary<string, IWebControl>();
        }

        private Dictionary<string, IWebControl> controls;

        public IWebControl this[string key]
        {
            get
            {
                IWebControl c = null;
                if (controls.TryGetValue(key, out c))
                {
                    return c;
                }

                var property = GetAttributeInfo(key);
                var a = GetAttribute(property);
                c = GetElementBy(a.Criteria, a.TextCriteria, property.PropertyType, a.WaitConditionType);
                
                controls.Add(key, c);
                Debug.WriteLine("Key: {0}, Control: {1}", key, c.GetType());
                return c;
            }
        }

        private IWebControl GetElementBy(FindByType criteria, string textCriteria, Type type, WaitingConditionType waitCondition = WaitingConditionType.NoWait)
        {
            return page.GetElementBy(criteria, textCriteria, type, waitCondition);
        }

        private FindByAttribute GetAttribute(PropertyInfo property)
        {
            var attrs = property.GetCustomAttributes(true);
            var a = attrs.OfType<FindByAttribute>().FirstOrDefault();
            return a;
        }

        private PropertyInfo GetAttributeInfo(string propname)
        {
            var t = page.GetType();

            PropertyInfo[] propertyInfos = t.GetProperties() ;
            var property = (from p in propertyInfos
                where p.Name.Equals(propname)
                select p).FirstOrDefault();

            if (property == null) return property;
            return property;
        }
    }
}