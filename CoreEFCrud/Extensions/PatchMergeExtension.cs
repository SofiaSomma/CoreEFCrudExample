using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;


namespace CoreEFCrud.Extensions
{
    public static class PatchMergeExtension
    {
        public static T From<T, U>(T modelObject, U plainObject) where T : class where U : class
        {
            var plainObjProperties = typeof(U).GetProperties();
            var modelObjProperties = typeof(T).GetProperties();

            foreach (var plainObjProperty in plainObjProperties)
            {
                foreach (var modelObjProperty in modelObjProperties)
                {
                    if (plainObjProperty.Name == modelObjProperty.Name && plainObjProperty.PropertyType == modelObjProperty.PropertyType)
                    {
                        var value = plainObjProperty.GetValue(plainObject);
                        if (value != default)
                            modelObjProperty.SetValue(modelObject, value);
                        break;
                    }
                }
            }

            return modelObject;
        }
    }
}
