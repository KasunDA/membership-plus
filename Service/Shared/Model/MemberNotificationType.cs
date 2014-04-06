//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool from CryptoGateway Software Inc.
//     Tool name: CGW X-Script RDB visual Layer Generator
//
//     Archymeta Information Technologies Co., Ltd.
//
//     Changes to this file, could be overwritten if the code is re-generated.
//     Add (if not yet) a code-manager node to the generator to specify 
//     how existing files are processed.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CryptoGateway.RDB.Data.MembershipPlus
{
    /// <summary>
    /// A entity in "MemberNotificationTypes" data set.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///    Properties of the entity are categorized in the following:
    ///  </para>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Primary keys</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>ID</term>
    ///      <description>See <see cref="MemberNotificationType.ID" />. Primary key; intrinsic id; fixed; not null.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Intrinsic Identifiers</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>ID</term>
    ///      <description>See <see cref="MemberNotificationType.ID" />. Primary key; intrinsic id; fixed; not null.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Editable properties</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>TypeName</term>
    ///      <description>See <see cref="MemberNotificationType.TypeName" />. Editable; not null; max-length = 70 characters.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>The following entity sets depend on this entity</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>MemberNotifications</term>
    ///      <description>See <see cref="MemberNotificationType.MemberNotifications" />, which is a sub-set of the data set "MemberNotifications" for <see cref="MemberNotification" />.</description>
    ///    </item>
    ///  </list>
    /// </remarks>
    [DataContract]
    public class MemberNotificationType : IDbEntity 
    {
        /// <summary>
        /// For internal use only.
        /// </summary>
        public bool IsOperationHandled = false;

        /// <summary>
        /// Used on the server side to return an unique key for caching purposes.
        /// </summary>
        public string CacheKey
        {
            get
            {
                return this.ID.ToString();
            }
        }

        /// <summary>
        /// Whether or not the entity was already persisted into to the data source. 
        /// </summary>
        [DataMember]
        public bool IsPersisted
        {
            get { return _isPersisted; }
            set { _isPersisted = value; }
        }
        private bool _isPersisted = false;

        /// <summary>
        /// Used internally.
        /// </summary>
        public bool StartAutoUpdating
        {
            get { return _startAutoUpdating; }
            set { _startAutoUpdating = value; }
        }
        private bool _startAutoUpdating = false;

        /// <summary>
        /// Used to matching entities in input adding or updating entity list and the returned ones, see <see cref="IMemberNotificationTypeService.AddOrUpdateEntities" />.
        /// </summary>
        [DataMember]
        public int UpdateIndex
        {
            get { return _updateIndex; }
            set { _updateIndex = value; }
        }
        private int _updateIndex = -1;

        /// <summary>
        /// Its value provides a list of value for intrinsic keys and modified properties.
        /// </summary>
        public string SignatureString 
        { 
            get
            {
                string str = "";
                str += "ID = " + ID + "\r\n";
                if (IsTypeNameModified)
                    str += "Modified [TypeName] = " + TypeName + "\r\n";;
                return str.Trim();
            }
        }

        /// <summary>
        /// Configured at system generation step, its value provides a short, but characteristic summary of the entity.
        /// </summary>
        [DataMember]
        public string DistinctString
        {
            get 
            {
                if (_distinctString == null)
                    _distinctString = GetDistinctString(true);
                return _distinctString;
            }
            set
            {
                _distinctString = value;
            }
        }
        private string _distinctString = null;

        private string GetDistinctString(bool ShowPathInfo)
        {
            return String.Format(@"{0}", TypeName.Trim());
        }

        /// <summary>
        /// Whether or not the entity was edited.
        /// </summary>
        [DataMember]
        public bool IsEntityChanged
        {
            get { return _isEntityChanged; }
            set { _isEntityChanged = value; }
        }
        private bool _isEntityChanged = true;

        /// <summary>
        /// Whether or not the entity was to be deleted.
        /// </summary>
        [DataMember]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        private bool _isDeleted = false;

#region Properties of the current entity

        /// <summary>
        /// Meta-info: primary key; intrinsic id; fixed; not null.
        /// </summary>
        [Key]
        [Required]
        [Editable(false)]
        [DataMember(IsRequired = true)]
        public int ID
        { 
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                }
            }
        }
        private int _ID = default(int);

        /// <summary>
        /// Meta-info: editable; not null; max-length = 70 characters.
        /// </summary>
        [Required]
        [Editable(true)]
        [StringLength(70)]
        [DataMember(IsRequired = true)]
        public string TypeName
        { 
            get
            {
                return _TypeName;
            }
            set
            {
                if (_TypeName != value)
                {
                    _TypeName = value;
                    if (StartAutoUpdating)
                        IsTypeNameModified = true;
                }
            }
        }
        private string _TypeName = default(string);

        /// <summary>
        /// Wether or not the value of <see cref="TypeName" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="TypeName" /> only if this is set to true no matter what
        /// the actual value of <see cref="TypeName" /> is.
        /// </summary>
        [DataMember]
        public bool IsTypeNameModified
        { 
            get
            {
                return _isTypeNameModified;
            }
            set
            {
                _isTypeNameModified = value;
            }
        }
        private bool _isTypeNameModified = false;

#endregion

#region Entities that the current one depends upon.

#endregion

#region Entities that depend on the current one.

        /// <summary>
        /// Entitity set <see cref="MemberNotificationSet" /> for data set "MemberNotifications" of <see cref="MemberNotification" /> that depend on the current entity.
        /// The corresponding foreign key in <see cref="MemberNotificationSet" /> set is { <see cref="MemberNotification.TypeID" /> }.
        /// </summary>
        [DataMember]
		public MemberNotificationSet MemberNotifications
		{
			get
			{
                if (_MemberNotifications == null)
                    _MemberNotifications = new MemberNotificationSet();
				return _MemberNotifications;
			}
            set
            {
                _MemberNotifications = value;
            }
		}
		private MemberNotificationSet _MemberNotifications = null;

        /// <summary>
        /// Entitites enumeration expression for data set "MemberNotifications" of <see cref="MemberNotification" /> that depend on the current entity.
        /// The corresponding foreign key in <see cref="MemberNotificationSet" /> set is { <see cref="MemberNotification.TypeID" /> }.
        /// </summary>
		public IEnumerable<MemberNotification> MemberNotificationEnum
		{
			get;
            set;
		}

        /// <summary>
        /// A list of <see cref="MemberNotification" /> that is to be added or updated to the data source, together with the current entity.
        /// The corresponding foreign key in <see cref="MemberNotificationSet" /> set is { <see cref="MemberNotification.TypeID" /> }.
        /// </summary>
        [DataMember]
		public MemberNotification[] ChangedMemberNotifications
		{
			get;
            set;
		}

#endregion

        /// <summary>
        /// Whether or not the present entity is identitical to <paramref name="other" />, in the sense that they have the same (set of) primary key(s).
        /// </summary>
        /// <param name="other">The entity to be compared to.</param>
        /// <returns>
        ///   The result of comparison.
        /// </returns>
        public bool IsEntityIdentical(MemberNotificationType other)
        {
            if (other == null)
                return false;
            if (ID != other.ID)
                return false;
            return true;
        }              

        /// <summary>
        /// Whether or not the present entity is identitical to <paramref name="other" />, in the sense that they have the same (set of) intrinsic identifiers.
        /// </summary>
        /// <param name="other">The entity to be compared to.</param>
        /// <returns>
        ///   The result of comparison.
        /// </returns>
        public bool IsEntityTheSame(MemberNotificationType other)
        {
            if (other == null)
                return false;
            else
                return ID == other.ID;
        }              

        /// <summary>
        /// Merge changes inside entity <paramref name="from" /> to the entity <paramref name="to" />. Any changes in <paramref name="from" /> that is not changed in <paramref name="to" /> is updated inside <paramref name="to" />.
        /// </summary>
        /// <param name="from">The "old" entity acting as merging source.</param>
        /// <param name="to">The "new" entity which inherits changes made in <paramref name="from" />.</param>
        /// <returns>
        /// </returns>
        public static void MergeChanges(MemberNotificationType from, MemberNotificationType to)
        {
            if (to.IsPersisted)
            {
                if (from.IsTypeNameModified && !to.IsTypeNameModified)
                {
                    to.TypeName = from.TypeName;
                    to.IsTypeNameModified = true;
                }
            }
            else
            {
                to.IsPersisted = from.IsPersisted;
                to.ID = from.ID;
                to.TypeName = from.TypeName;
                to.IsTypeNameModified = from.IsTypeNameModified;
            }
        }

        /// <summary>
        /// Update changes to the current entity compared to an input <paramref name="newdata" /> and set the entity to a proper state for updating.
        /// </summary>
        /// <param name="newdata">The "new" entity acting as the source of the changes, if any.</param>
        /// <returns>
        /// </returns>
        public void UpdateChanges(MemberNotificationType newdata)
        {
            int cnt = 0;
            if (TypeName != newdata.TypeName)
            {
                TypeName = newdata.TypeName;
                IsTypeNameModified = true;
                cnt++;
            }
            IsEntityChanged = cnt > 0;
        }

        /// <summary>
        /// Internal use
        /// </summary>
        public void NormalizeValues()
        {
            StartAutoUpdating = false;
            if (TypeName == null)
                TypeName = "";
            if (!IsEntityChanged)
                IsEntityChanged = IsTypeNameModified;
            StartAutoUpdating = true;
        }

        /// <summary>
        /// Make a shallow copy of the entity.
        /// </summary>
        IDbEntity IDbEntity.ShallowCopy(bool preserveState)
        {
            return ShallowCopy(false, preserveState);
        }

        /// <summary>
        /// Internal use
        /// </summary>
        public MemberNotificationType ShallowCopy(bool allData = false, bool preserveState = false)
        {
            MemberNotificationType e = new MemberNotificationType();
            e.StartAutoUpdating = false;
            e.ID = ID;
            e.TypeName = TypeName;
            if (preserveState)
                e.IsTypeNameModified = IsTypeNameModified;
            else
                e.IsTypeNameModified = false;
            e.DistinctString = GetDistinctString(true);
            e.IsPersisted = IsPersisted;
            if (preserveState)
                e.IsEntityChanged = IsEntityChanged;
            else
                e.IsEntityChanged = false;
            e.StartAutoUpdating = true;
            return e;
        }

        /// <summary>
        /// A textual representation of the entity.
        /// </summary>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"
----===== [[MemberNotificationType]] =====----
  ID = " + ID + @"");
            sb.Append(@" (natural id)");
            sb.Append(@"
  TypeName = '" + TypeName + @"'");
            if (IsTypeNameModified)
                sb.Append(@" (modified)");
            else
                sb.Append(@" (unchanged)");
            sb.Append(@"
");
            return sb.ToString();
        }

    }

    ///<summary>
    ///The result of an add or update of type <see cref="MemberNotificationType" />.
    ///</summary>
    [DataContract]
    public class MemberNotificationTypeUpdateResult : IUpdateResult
    {
        /// <summary>
        /// Status of add, update or delete operation
        /// </summary>
        [DataMember]
        public int OpStatus
        {
            get { return _opStatus; }
            set { _opStatus = value; }
        }
        private int _opStatus = (int)EntityOpStatus.Unknown;

        /// <summary>
        /// Parents or child operation status
        /// </summary>
        [DataMember]
        public int RelatedOpStatus
        {
            get { return _relatedOpStatus; }
            set { _relatedOpStatus = value; }
        }
        private int _relatedOpStatus = (int)EntityOpStatus.Unknown;

        /// <summary>
        /// The updated entity.
        /// </summary>
        [DataMember]
        public MemberNotificationType UpdatedItem
        {
            get;
            set;
        }

        /// <summary>
        /// If the relational data source has a way of detecting concurrent update conflicts, this is the item inside the
        /// data source that had been updated by other agents in between the load and update time interval of the present
        /// agent. The client software should resolve the conflict.
        /// </summary>
        [DataMember]
        public MemberNotificationType ConflictItem
        {
            get;
            set;
        }

        /// <summary>
        /// String representation of the entity.
        /// </summary>
        public string EntityInfo 
        { 
            get { return UpdatedItem == null ? "NULL" : UpdatedItem.ToString(); }
        }
    }

}