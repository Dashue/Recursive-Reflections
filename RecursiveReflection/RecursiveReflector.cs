using System;
using System.Collections;
using System.Reflection;
using RecursiveReflection.Map;

namespace RecursiveReflection
{
	public class RecursiveReflector
	{
		public T Reflect<T>(T model) where T : class
		{
			var reflectionMap = ReflectionMapCreator.Create(model.GetType());
			var reflectedModel = Reflect(model, reflectionMap);

			return reflectedModel;
		}

		private T Reflect<T>(T model, RecursiveReflectionMap recursiveReflectionMap) where T : class
		{
			if (model == null)
			{
				return null;
			}

			foreach (var property in recursiveReflectionMap.Properties)
			{
				PropertyInfo info = recursiveReflectionMap.Type.GetProperty(property);
			    var originalValue = (string) info.GetValue(model, null);
                
                Console.WriteLine(originalValue);
			}

			foreach (var map in recursiveReflectionMap.Maps)
			{
				PropertyInfo propertyInfo = model.GetType().GetProperty(map.Name);
				var value = propertyInfo.GetValue(model, null);

				if (value != null)
				{
					if (map.IsDictionary)
					{
						var forLoopingAsToNotChangeOriginal = (IDictionary)Activator.CreateInstance(propertyInfo.PropertyType);
						var dictionary = (IDictionary)value;

						foreach (DictionaryEntry entry in dictionary)
						{
							forLoopingAsToNotChangeOriginal.Add(entry.Key, entry.Value);
						}

						foreach (DictionaryEntry entry in forLoopingAsToNotChangeOriginal)
						{
							dictionary[entry.Key] = Reflect(entry.Value, map.Maps[0]);
						}
					}
					else if (map.IsList)
					{
						var list = (IList)value;

						for (int i = 0; i < list.Count; i++)
						{
							list[i] = Reflect(list[i], map.Maps[0]);
						}
					}
					else
					{
						Reflect(value, map);
					}
				}
			}

			return model;
		}
	}
}