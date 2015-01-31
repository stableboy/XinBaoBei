using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Runtime.CompilerServices;


public class TableTypeParameter
{
    public string ParameterName { get; set; }

    public object ParameterValue { get; set; }

    public TableTypeParameter()
    {
    }

    public TableTypeParameter(string name, object value)
    {
        ParameterName = name;
        ParameterValue = value;
    }
}
