using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Diagnostics;
using System.Windows.Input;
using WPFExtension.Basic;

namespace IOSOverlay.Data.Models {
	public class MultiSelectModelCollectionView:INotifyPropertyChanged {
		private ObservableCollection<SelectableModelWrapper> _SelectableModels = new ObservableCollection<SelectableModelWrapper>();
		private CollectionViewSource _CollectionView = new CollectionViewSource();
		private string _CollectionName;
		private string[] _GroupingProperties;

		public event PropertyChangedEventHandler PropertyChanged;
		public ListCollectionView SelectableModels {
			get {
				if(_CollectionView.Source == null) _CollectionView.Source = _SelectableModels;
				return (ListCollectionView)_CollectionView.View;
			}
		}
		public string CollectionName {
			get {
				return _CollectionName;
			}
			set {
				_CollectionName = value;
				RaisePropertyChanged(nameof(CollectionName));
			}
		}
		public ICommand SelectAll => new RelayCommand(() => {
			foreach(var m in _SelectableModels) {
				m.IsSelected = true;
			}
		});
		public ICommand SelectNone => new RelayCommand(() => {
			foreach(var m in _SelectableModels) {
				m.IsSelected = false;
			}
		});

		public MultiSelectModelCollectionView() {

		}
		public MultiSelectModelCollectionView(ModelCollection wrapableCollection, Func<Model, SelectableModelWrapper> wrapModelFunction, params string[] groupingProperties) {
			Wrap(wrapableCollection, wrapModelFunction);
			_GroupingProperties = groupingProperties;
			ApplyDefaultViewSettings(SelectableModels);
		}
		public MultiSelectModelCollectionView(ModelCollection wrapableCollection, Func<Model, SelectableModelWrapper> wrapModelFunction, string collectionName, params string[] groupingProperties) : this(wrapableCollection, wrapModelFunction, groupingProperties) {
			CollectionName = collectionName;
		}
		public MultiSelectModelCollectionView(ModelCollection wrapableCollection, Func<Model, SelectableModelWrapper> wrapModelFunction, string collectionName, bool filter, params string[] groupingProperties) : this(wrapableCollection, wrapModelFunction, collectionName, groupingProperties) {
			SetFilter(filter);
		}

		public void SetSelected(Guid uid, bool selected) {
			if(_SelectableModels.Any(a => a.Model.UID == uid)) {
				var m = (from a in _SelectableModels where a.Model.UID == uid select a).First();
				if(m != null) {
					m.IsSelected = selected;
				}
			}
		}

		public void Wrap(ModelCollection collection, Func<Model, SelectableModelWrapper> wrapModelFunction) {
			_SelectableModels.Clear();
			foreach(var m in collection) {
				_SelectableModels.Add(wrapModelFunction(m));
			}
		}

		public void SetFilter(bool filter) {
			if(filter) {
				SelectableModels.Filter = (m) => (!(m as SelectableModelWrapper).Model.Hidden && !(m as SelectableModelWrapper).Model.Archived);
			} else {
				SelectableModels.Filter = null;
			}
		}
		public IEnumerable<Model> SelectedModels() {
			foreach(var m in _SelectableModels) {
				if(m.IsSelected) yield return m.Model;
			}
		}
		private void RaisePropertyChanged(string property) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
		public void RefreshUpdate() {
			SelectableModels.Refresh();
			SelectableModels.DeferRefresh();
			RaisePropertyChanged(nameof(SelectableModels));
		}
		public static MultiSelectModelCollectionView Wrap(CollectionView collection) {
			throw new NotImplementedException();
		}

		private void ApplyDefaultViewSettings(ListCollectionView view) {
			view.GroupDescriptions.Clear();
			foreach(var p in _GroupingProperties) {
				view.GroupDescriptions.Add(new PropertyGroupDescription(p));
			}
			view.SortDescriptions.Clear();
			foreach(var p in _GroupingProperties) {
				view.SortDescriptions.Add(new SortDescription(p, ListSortDirection.Ascending));
			}
			view.SortDescriptions.Add(new SortDescription("Visual", ListSortDirection.Ascending));
		}
	}
}
