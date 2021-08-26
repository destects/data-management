namespace IOSOverlay.Data.Models {
	/// <summary>
	/// Model manipulation and creation states.
	/// </summary>
	public enum ModelStates {
		/// <summary>
		/// Describes a model that is currently readonly.
		/// </summary>
		Immutable,
		/// <summary>
		/// Describes a model that is in an editable state.
		/// </summary>
		Mutable,
		/// <summary>
		/// Describes a model that has been created and is in an editable state but has
		/// not yet been saved.
		/// </summary>
		New,
		/// <summary>
		/// Describes a model that is marked for removal an is immutable.
		/// </summary>
		Removed,
		/// <summary>
		/// Describes a model that is immutable, not a member of a ModelCollection, and not savable.
		/// </summary>
		ReadOnly,
		/// <summary>
		/// Describes a model that is in an archival state. Archived models are kept for backup purposes and immutable.
		/// </summary>
		Archived,
		/// <summary>
		/// Describes a model that is hidden from all users except the master
		/// </summary>
		Hidden
	}
}
