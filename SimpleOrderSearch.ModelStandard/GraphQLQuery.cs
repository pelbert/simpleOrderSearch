﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SimpleOrderSearch.Model
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }

        public string NamedQuery { get; set; }

        public string Query { get; set; }

        public JObject Variables { get; set; }
    }
}
