using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data.Models {
	/// <summary>
	/// Package Tag's identify objects and the packages they belong to.
	/// </summary>
	/// <seealso cref="System.IComparable" />
	/// <seealso cref="System.IComparable{IOSOverlay.Data.Models.PackageTag}" />
	/// <seealso cref="System.IEquatable{IOSOverlay.Data.Models.PackageTag}" />
	[Serializable]
	public struct PackageTag:IComparable, IComparable<PackageTag>, IEquatable<PackageTag> {
		public static readonly PackageTag None = new PackageTag(false);

		private string _PackageName;
		private Guid _PackageGuid;
		private int _PackageVersion;
		private bool _IsValid;

		/// <summary>
		/// Gets the name of the package.
		/// </summary>
		/// <value>
		/// The name of the package.
		/// </value>
		public string PackageName => _PackageName;
		/// <summary>
		/// Gets the package unique identifier.
		/// </summary>
		/// <value>
		/// The package unique identifier.
		/// </value>
		public Guid PackageGuid => _PackageGuid;
		/// <summary>
		/// Gets the package version.
		/// </summary>
		/// <value>
		/// The package version.
		/// </value>
		public int PackageVersion => _PackageVersion;
		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
		public bool IsValid => _IsValid;

		/// <summary>
		/// Initializes a new instance of the <see cref="PackageTag"/> struct.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="guid">The unique identifier.</param>
		/// <param name="version">The version.</param>
		public PackageTag(string name, Guid guid, int version = 0) {
			_PackageName = name;
			_PackageGuid = guid;
			_PackageVersion = version;
			_IsValid = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="PackageTag"/> struct.
		/// </summary>
		/// <param name="valid">if set to <c>true</c> [valid].</param>
		private PackageTag(bool valid) {
			_IsValid = valid;
			_PackageName = string.Empty;
			_PackageGuid = Guid.Empty;
			_PackageVersion = -1;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			if(obj is PackageTag) return ((PackageTag)obj).Equals(this);
			return base.Equals(obj);
		}
		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() {
			return _PackageName.GetHashCode() + _PackageVersion.GetHashCode() + _PackageGuid.GetHashCode();
		}
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString() {
			_PackageVersion.CompareTo(0);
			return $"{_PackageName} @{_PackageVersion}";
		}
		/// <summary>
		/// Checks if two package tags are for the same package Guid, ignores version number.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		public bool Equals(PackageTag obj) {
			return obj._PackageGuid.Equals(_PackageGuid);
		}
		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
		/// </returns>
		public int CompareTo(object obj) {
			return _PackageVersion.CompareTo(obj);
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
		/// </returns>
		public int CompareTo(PackageTag other) {
			return _PackageVersion.CompareTo(other.PackageVersion);
		}
	}
}
