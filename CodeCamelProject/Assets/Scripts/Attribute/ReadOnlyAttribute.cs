using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class ReadOnlyAttribute : PropertyAttribute{

}
