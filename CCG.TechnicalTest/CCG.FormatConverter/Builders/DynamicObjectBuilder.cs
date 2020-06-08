using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace CCG.FormatConverter.Builders
{
    public class DynamicObjectBuilder: IDynamicObjectBuilder
    {
        /// <summary>
        /// Builds a dynamic object for a line from a given array of array string <paramref name="fields"/> and array of string <paramref name="values"/>.
        /// Nested of values can be defined by the use of the '_' character.
        /// </summary>
        /// <param name="headers">Array of string field names, potentially delimited by the '_' character to indicate nesting.</param>
        /// <param name="values">Array of string values.</param>
        /// <returns>A dynamic object representing the <paramref name="fields"/> and <paramref name="values"/>.</returns>
        public dynamic Build(string[] fields, string[] values)
        {
            if (fields == null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (fields.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fields), "Must contain at least one item");
            }

            if (values.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Must contain at least one item");
            }

            if (values.Length != fields.Length)
            {
                throw new ArgumentException($"Arguments {nameof(fields)} and {nameof(values)} have different lengths. The {nameof(fields)} and {nameof(values)} must contain the same number of items.");
            }

            ExpandoObject item = new ExpandoObject();
            for (var i = 0; i < fields.Length; i++) 
            {
                AddValue(item, fields[i], values[i]);
            }
            return item;
        }

        /// <summary>
        /// Adds the value to the item, creating nested objects required to house the value.
        /// </summary>
        /// <param name="obj">The item the value is being added to.</param>
        /// <param name="header">The CSV header that applies to the value being added. Presence of '_' indicates that the value is being added to a nested object.</param>
        /// <param name="value">The value being added.</param>
        private void AddValue(ExpandoObject obj, string field, string value)
        {
            var fieldItems = GetFieldsQueue(field);
            while (fieldItems.Count > 1)
            {
                string fieldItem = fieldItems.Dequeue();
                ExpandoObject childObject = GetChildObject(obj, fieldItem);
                obj.TryAdd(fieldItem, childObject);
                obj = childObject;
            }
            var valueField = fieldItems.Dequeue();
            obj.TryAdd(valueField, value);
        }

        /// <summary>
        /// Retrieves a queue of fields from a given CSV header.
        /// </summary>
        /// <param name="header">The CSV header text.</param>
        /// <returns></returns>
        private Queue<string> GetFieldsQueue(string header)
        {
            return new Queue<string>(header.Split('_'));
        }

        /// <summary>
        /// Either retrieves a child object if the parent already contains an object with that key, or creates a new one if it doesn't.
        /// </summary>
        /// <param name="obj">The object containing the child object to be retrieved.</param>
        /// <param name="field">The field the child object is contained in.</param>
        /// <returns></returns>
        private dynamic GetChildObject(ExpandoObject obj, string field)
        {
            return obj
                    .Where(o => o.Key.Equals(field))
                    .Select(o => o.Value)
                    .DefaultIfEmpty(new ExpandoObject())
                    .FirstOrDefault();
        }
    }
}
