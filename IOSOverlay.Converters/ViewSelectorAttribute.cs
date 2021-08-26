using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ViewSelectorAttribute:Attribute {
		private readonly Type viewType;

		public ViewSelectorAttribute(Type viewType) {
			this.viewType = viewType;
		}

		public Type ViewType {
			get { return viewType; }
		}

	}

	internal static class ViewSelectorExtension {
		public static Type GetView(this object obj) {
			var attr = obj.GetType().GetCustomAttribute<ViewSelectorAttribute>(false);
			if(attr != null) {
				return attr.ViewType;
			}
			return null;
		}
	}
}
