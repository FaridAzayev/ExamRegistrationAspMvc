using ExamRegistrationAspMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamRegistrationAspMvc.Extensions
{
    public static class ObjectMapper
    {
        public static Subject Copy(this Subject destination, Subject source)
        {
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(destination, sourceProperty.GetValue(
                            source, new object[] { }), new object[] { });

                        break;
                    }
                }
            }
            return destination;
        }
    }
}