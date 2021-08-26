using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Common {
	public class PageTemplate {
		public readonly int PageTagIndex;
		public readonly Type ViewType;
		public readonly Type ViewModelType;
		public readonly BottomBarConfigurations[] BottomBarConfiguration;

		public PageTemplate(int tagIndex, Type viewType, Type viewModelType) {
			this.PageTagIndex = tagIndex;
			this.ViewType = viewType;
			this.ViewModelType = viewModelType;
		}
		public PageTemplate(int tagIndex, Type viewType, Type viewModelType, BottomBarConfigurations[] bottomBarConfiguration) : this(tagIndex, viewType, viewModelType) {
			this.BottomBarConfiguration = bottomBarConfiguration;
		}
		public PageTemplate(KnownPagesIndex tagIndex, Type viewType, Type viewModelType) : this((int)tagIndex, viewType, viewModelType) {

		}
		public PageTemplate(KnownPagesIndex tagIndex, Type viewType, Type viewModelType, BottomBarConfigurations[] bottomBarConfiguration) : this((int)tagIndex, viewType, viewModelType, bottomBarConfiguration) {

		}
		public static PageTemplate CreateTemplate<TView, TViewModel>(int tagIndex)
			where TView : Views.View
			where TViewModel : ViewModels.ViewModel {
			return new PageTemplate(tagIndex, typeof(TView), typeof(TViewModel));
		}
		public static PageTemplate CreateTemplate<TView, TViewModel>(int tagIndex, BottomBarConfigurations[] bottomBarConfiguration)
		where TView : Views.View
		where TViewModel : ViewModels.ViewModel {
			return new PageTemplate(tagIndex, typeof(TView), typeof(TViewModel), bottomBarConfiguration);
		}
		public static PageTemplate CreateTemplate<TView, TViewModel>(KnownPagesIndex tagIndex)
	where TView : Views.View
	where TViewModel : ViewModels.ViewModel {
			return new PageTemplate(tagIndex, typeof(TView), typeof(TViewModel));
		}
		public static PageTemplate CreateTemplate<TView, TViewModel>(KnownPagesIndex tagIndex, BottomBarConfigurations[] bottomBarConfiguration)
		where TView : Views.View
		where TViewModel : ViewModels.ViewModel {
			return new PageTemplate(tagIndex, typeof(TView), typeof(TViewModel), bottomBarConfiguration);
		}

		public override int GetHashCode() {
			return PageTagIndex;
		}
	}
}
