using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.Extensions
{
    public static class JsonPatchDocumentExtension
    {
        public static JsonPatchDocument<T> From<T>(T t) where T : class
        {
            var json = JObject.FromObject(t,
                new JsonSerializer()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            var objProps = json.Properties().Select(p => p as JProperty);

            var properties = new Stack<JProperty>(objProps);
            var operations = new List<Operation<T>>();

            JProperty prop = null;
            JObject obj = null;
            JArray arr = null;

            while (properties.Count > 0)
            {
                prop = properties.Pop();

                if (prop.Value is JObject)
                {
                    obj = prop.Value as JObject;
                    objProps = obj.Properties().Select(p => p as JProperty);

                    foreach (var o in objProps)
                    {
                        properties.Push(o);
                    }
                }
                else if (prop.Value is JArray)
                {
                    arr = prop.Value as JArray;
                    obj = arr.First as JObject;

                    do
                    {
                        objProps = obj.Properties().Select(p => p as JProperty);

                        foreach (var o in objProps)
                        {
                            properties.Push(o);
                        }

                        obj = obj.Next as JObject;

                    } while (obj != null);
                }
                else
                {
                    operations.Add(
                        new Operation<T>()
                        {
                            path = $"/{prop.Path.Replace(".", "/")}",
                            value = prop.Value,
                            op = "replace"
                        });
                }
            }
           
            return new JsonPatchDocument<T>(operations, null);
        }
    }
}
