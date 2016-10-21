using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleToDo.Api.formatters
{
    public class DefaultJsonSerializerSettings : JsonSerializerSettings
    {
        public DefaultJsonSerializerSettings()
            : base()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // "Dates are written in the ISO 8601 format, e.g. "2012-03-21T05:40Z"."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateFormatHandling.htm
            this.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            // "Date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed to
            // DateTimeOffset."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateParseHandling.htm
            this.DateParseHandling = DateParseHandling.DateTimeOffset;

            // "Time zone information should be preserved when converting."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateTimeZoneHandling.htm
            this.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

            // "Ignore members where the member value is the same as the member's default value when serializing
            // objects and sets members to their default value when deserializing."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DefaultValueHandling.htm
            this.DefaultValueHandling = DefaultValueHandling.Include;

            // "Floating point numbers are parsed to Decimal."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_FloatParseHandling.htm
            this.FloatParseHandling = FloatParseHandling.Decimal;

#if DEBUG
            // "Causes child objects to be indented according to the Indentation and IndentChar settings."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Formatting.htm
            this.Formatting = Formatting.Indented;
#endif
            // "Ignore a missing member and do not attempt to deserialize it."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_MissingMemberHandling.htm
            this.MissingMemberHandling = MissingMemberHandling.Ignore;

            // "Ignore null values when serializing and deserializing objects."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_NullValueHandling.htm
            this.NullValueHandling = NullValueHandling.Ignore;

            // "Ignore loop references and do not serialize."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_ReferenceLoopHandling.htm
            this.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // "Do not include the .NET type name when serializing types."
            // http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_TypeNameHandling.htm
            this.TypeNameHandling = TypeNameHandling.None;
        }
    }
}