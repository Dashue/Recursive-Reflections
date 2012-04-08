using System;
using System.Collections.Generic;
using System.Linq;
using RecursiveReflection.Attributes;
using RecursiveReflection.Extensions;

namespace RecursiveReflection.Map
{
	internal static class ReflectionMapCreator
	{
		internal static RecursiveReflectionMap Create(Type modelType, string name = "")
		{
			var translationMap = new RecursiveReflectionMap
			{
				Maps = new List<RecursiveReflectionMap>(),
				Name = name,
				Properties = new List<string>(),
				Type = modelType
			};

			var properties = modelType.GetProperties();

			foreach (var property in properties)
			{
				if (property.PropertyType.IsClass)
				{
					if (property.PropertyType == typeof(string) && property.GetCustomAttributes(typeof(RecursiveReflectionAttribute), false).Any())
					{
						translationMap.Properties.Add(property.Name);
					}
					else if (property.PropertyType.IsCollection())
					{
						if (property.PropertyType.IsDictionary())
						{
							var arguments = property.PropertyType.GetGenericArguments();
							var keyType = arguments[0];
							var valueType = arguments[1];

							if (valueType.IsClass)
							{
								translationMap.Maps.Add(new RecursiveReflectionMap
								{
									IsDictionary = true,
									Maps = new List<RecursiveReflectionMap>
																{
																	Create(valueType, valueType.Name)
																},
									Name = property.Name,
									Properties = new List<string>(),
									Type = property.PropertyType
								});
							}
						}
						else if (property.PropertyType.IsList())
						{
							var arguments = property.PropertyType.GetGenericArguments();
							var valueType = arguments[0];

							if (valueType.IsClass)
							{
								translationMap.Maps.Add(new RecursiveReflectionMap
								{
									IsList = true,
									Maps = new List<RecursiveReflectionMap>
																       	{
																       		Create(valueType, valueType.Name)
																       	},
									Name = property.Name,
									Properties = new List<string>(),
									Type = property.PropertyType
								});
							}
						}
					}
					else
					{
						translationMap.Maps.Add(Create(property.PropertyType, property.Name));
					}
				}
			}

			return translationMap;
		}
	}
}