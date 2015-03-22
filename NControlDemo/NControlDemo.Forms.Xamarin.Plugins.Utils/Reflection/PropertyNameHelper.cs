/****************************** Module Header ******************************\
Module NamNControlDemo.Forms.Xamarin.Plugins.FormsAppcs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Linq.Expressions;

namespace NControlDemo.Helpers
{
	/// <summary>
	/// Maps an expression like (m => m.SomeProperty) to the property name "SomeProperty"
	/// </summary>
	public static class PropertyNameHelper
	{
		/// <summary>
		/// Calls the notify property changed event if it is attached. By using some
		/// Expression/Func magic we get compile time type checking on our property
		/// names by using this method instead of calling the event with a string in code.
		/// </summary>
		/// <param name="property">Property.</param>
		public static string GetPropertyName<TModel>(Expression<Func<object>> property)
		{
			var propertyName = string.Empty;

			if (property.Body.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = property.Body as MemberExpression;
				if (memberExpression != null)
					propertyName = memberExpression.Member.Name;
			}
			else
			{
				var unary = property.Body as UnaryExpression;
				if (unary != null)
				{
					var member = unary.Operand as MemberExpression;
					if (member != null) propertyName = member.Member.Name;
				}
			}

			return propertyName;
		}

		/// <summary>
		/// Calls the notify property changed event if it is attached. By using some
		/// Expression/Func magic we get compile time type checking on our property
		/// names by using this method instead of calling the event with a string in code.
		/// </summary>
		/// <param name="property">Property.</param>
		public static string GetPropertyName<TModel>(Expression<Func<TModel, object>> property)
		{
			var propertyName = string.Empty;

			if (property.Body.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = property.Body as MemberExpression;
				if (memberExpression != null)
					propertyName = memberExpression.Member.Name;
			}
			else
			{
				var unary = property.Body as UnaryExpression;
				if (unary != null)
				{
					var member = unary.Operand as MemberExpression;
					if (member != null) propertyName = member.Member.Name;
				}
			}

			return propertyName;
		}
	}
}

