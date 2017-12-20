using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
	public static class FinOrgExtensions
	{
		/// <summary>
		/// This will add an array of parameters to a SqlCommand. This is used for an IN statement.
		/// Use the returned value for the IN part of your SQL call. (i.e. SELECT * FROM table WHERE field IN ({paramNameRoot}))
		/// </summary>
		/// <param name="cmd">The SqlCommand object to add parameters to.</param>
		/// <param name="values">The array of strings that need to be added as parameters.</param>
		/// <param name="paramNameRoot">What the parameter should be named followed by a unique value for each value. This value surrounded by {} in the CommandText will be replaced.</param>
		/// <param name="start">The beginning number to append to the end of paramNameRoot for each value.</param>
		/// <param name="separator">The string that separates the parameter names in the sql command.</param>
		public static SqlParameter[] AddArrayParameters<T>(this SqlCommand cmd, IEnumerable<T> values, string paramNameRoot, int start = 1, string separator = ", ")
		{
			/* An array cannot be simply added as a parameter to a SqlCommand so we need to loop through things and add it manually. 
			 * Each item in the array will end up being it's own SqlParameter so the return value for this must be used as part of the
			 * IN statement in the CommandText.
			 */
			var parameters = new List<SqlParameter>();
			var parameterNames = new List<string>();
			var paramNbr = start;
			foreach (var value in values)
			{
				var paramName = string.Format("@{0}{1}", paramNameRoot, paramNbr++);
				parameterNames.Add(paramName);
				parameters.Add(cmd.Parameters.AddWithValue(paramName, value));
			}

			cmd.CommandText = cmd.CommandText.Replace("{" + paramNameRoot + "}", string.Join(separator, parameterNames));

			return parameters.ToArray();
		}

		/// <summary>
		/// Iterate through all child controls
		/// </summary>
		/// <param name="root"></param>
		/// <returns></returns>
		public static IEnumerable<Control> GetAllControlChildren(this Control root)
		{
			var stack = new Stack<Control>();
			stack.Push(root);

			while (stack.Any())
			{
				var next = stack.Pop();
				foreach (Control child in next.Controls)
					stack.Push(child);
				yield return next;
			}
		}

		/// <summary>
		///
		///  ToolStripItem provides Name & Text
		///
		///	System.Windows.Forms.Control
		///	System.Windows.Forms.ScrollableControl
		///		System.Windows.Forms.ToolStrip						Items !!
		///			System.Windows.Forms.BindingNavigator
		///			System.Windows.Forms.MenuStrip
		///			System.Windows.Forms.StatusStrip
		///			System.Windows.Forms.ToolStripDropDown			!!
		///
		///	System.ComponentModel.Component
		///		System.Windows.Forms.ToolStripItem
		///			System.Windows.Forms.ToolStripButton
		///			System.Windows.Forms.ToolStripControlHost
		///			System.Windows.Forms.ToolStripDropDownItem		DropDownItems !!
		///			System.Windows.Forms.ToolStripLabel
		///			System.Windows.Forms.ToolStripSeparator
		/// </summary>
		/// <param name="root"></param>
		/// <returns></returns>
		public static IEnumerable<ToolStripItem> GetAllToolStripItems(this ToolStrip root)
		{
			var stack = new Stack<ToolStripItem>();

			// https://msdn.microsoft.com/en-us/library/system.windows.forms.toolstrip(v=vs.110).aspx
			// for ToolStripItems (ToolStripMenuItem, ToolStripButton etc)
			foreach (ToolStripItem i in root.Items)
				stack.Push(i);

			while (stack.Any())
			{
				var next = stack.Pop();

				// https://msdn.microsoft.com/en-us/library/system.windows.forms.toolstripdropdownitem(v=vs.110).aspx
				// DropDownItems: MenuItem, DropDownButton..
				if (next.GetType().IsSubclassOf(typeof(ToolStripDropDownItem)))
					foreach (ToolStripItem child in ((ToolStripDropDownItem)next).DropDownItems)
						stack.Push(child);
				yield return next;
			}
		}
	}
}
