using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Common {
	using Views;
	using ViewModels;

	public static class PageFactory {
		public static Dictionary<int, PageTemplate> PageTemplates = new Dictionary<int, PageTemplate>();

		public static void ResetTemplates() {
			PageTemplates.Clear();
		}

		public static void AddTemplate(PageTemplate template) {
			PageTemplates.Add(template.PageTagIndex, template);
		}

		public static Page CreatePage(int pageTagIndex) {
			var entry = PageTemplates[pageTagIndex];
			return new Page(entry.PageTagIndex, Activator.CreateInstance(entry.ViewType) as View, entry.BottomBarConfiguration);
		}
		public static Page CreatePage(int pageTagIndex, int layer) {
			var entry = PageTemplates[pageTagIndex];
			var x = new Page(entry.PageTagIndex, layer, Activator.CreateInstance(entry.ViewType) as View, entry.BottomBarConfiguration);
			return x;
		}
		public static Page CreatePage(PageTemplate template) {
			return new Page(template.PageTagIndex, Activator.CreateInstance(template.ViewType) as View, template.BottomBarConfiguration);
		}
		public static Page CreatePage(PageTemplate template, int layer) {
			return new Page(template.PageTagIndex, layer,
				Activator.CreateInstance(template.ViewType) as View, template.BottomBarConfiguration);
		}
	}
}
