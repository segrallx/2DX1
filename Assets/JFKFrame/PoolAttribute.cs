using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// 确定一个雷是否需要对象池. 特性
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PoolAttribute : Attribute
{

}
