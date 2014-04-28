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
using System.Runtime.Serialization.Json;

namespace CryptoGateway.RDB.Data.MembershipPlus
{
    /// <summary>
    /// A entity in "ShortMessageTypes" data set.
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
    ///      <description>See <see cref="ShortMessageType.ID" />. Primary key; intrinsic id; fixed; not null.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Intrinsic Identifiers</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>ID</term>
    ///      <description>See <see cref="ShortMessageType.ID" />. Primary key; intrinsic id; fixed; not null.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>None editable properties</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>TypeName</term>
    ///      <description>See <see cref="ShortMessageType.TypeName" />. Fixed; not null; max-length = 80 characters.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Editable properties</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>TypeLabel</term>
    ///      <description>See <see cref="ShortMessageType.TypeLabel" />. Editable; nullable; max-length = 80 characters.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>The following entity sets depend on this entity</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>ShortMessages</term>
    ///      <description>See <see cref="ShortMessageType.ShortMessages" />, which is a sub-set of the data set "ShortMessages" for <see cref="ShortMessage" />.</description>
    ///    </item>
    ///  </list>
    /// </remarks>
    [DataContract]
    [Serializable]
    public class ShortMessageType : IDbEntity 
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
        /// Used to matching entities in input adding or updating entity list and the returned ones, see <see cref="IShortMessageTypeService.AddOrUpdateEntities" />.
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
                if (IsTypeLabelModified)
                    str += "Modified [TypeLabel] = " + TypeLabel + "\r\n";;
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

#region constructors and serialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShortMessageType()
        {
        }

        /// <summary>
        /// Constructor for serialization (<see cref="ISerializable" />).
        /// </summary>
        public ShortMessageType(SerializationInfo info, StreamingContext context)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ShortMessageType));
            var strm = new System.IO.MemoryStream();
            byte[] bf = (byte[])info.GetValue("data", typeof(byte[]));
            strm.Write(bf, 0, bf.Length);
            strm.Position = 0;
            var e = ser.ReadObject(strm) as ShortMessageType;
            IsPersisted = false;
            StartAutoUpdating = false;
            MergeChanges(e, this);
            StartAutoUpdating = true;
        }

        /// <summary>
        /// Implementation of the <see cref="ISerializable" /> interface
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ShortMessageType));
            var strm = new System.IO.MemoryStream();
            ser.WriteObject(strm, ShallowCopy());
            info.AddValue("data", strm.ToArray(), typeof(byte[]));
        }

#endregion

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
        /// Meta-info: fixed; not null; max-length = 80 characters.
        /// </summary>
        [Required]
        [Editable(false)]
        [StringLength(80)]
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
                }
            }
        }
        private string _TypeName = default(string);

        /// <summary>
        /// Meta-info: editable; nullable; max-length = 80 characters.
        /// </summary>
        [Editable(true)]
        [StringLength(80)]
        [DataMember(IsRequired = false)]
        public string TypeLabel
        { 
            get
            {
                return _TypeLabel;
            }
            set
            {
                if (_TypeLabel != value)
                {
                    _TypeLabel = value;
                    if (StartAutoUpdating)
                        IsTypeLabelModified = true;
                }
            }
        }
        private string _TypeLabel = default(string);

        /// <summary>
        /// Wether or not the value of <see cref="TypeLabel" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="TypeLabel" /> only if this is set to true no matter what
        /// the actual value of <see cref="TypeLabel" /> is.
        /// </summary>
        [DataMember]
        public bool IsTypeLabelModified
        { 
            get
            {
                return _isTypeLabelModified;
            }
            set
            {
                _isTypeLabelModified = value;
            }
        }
        private bool _isTypeLabelModified = false;

#endregion

#region Entities that the current one depends upon.

#endregion

#region Entities that depend on the current one.

        /// <summary>
        /// Entitity set <see cref="ShortMessageSet" /> for data set "ShortMessages" of <see cref="ShortMessage" /> that depend on the current entity.
        /// The corresponding foreign key in <see cref="ShortMessageSet" /> set is { <see cref="ShortMessage.TypeID" /> }.
        /// </summary>
        [DataMember]
		public ShortMessageSet ShortMessages
		{
			get
			{
                if (_ShortMessages == null)
                    _ShortMessages = new ShortMessageSet();
				return _ShortMessages;
			}
            set
            {
                _ShortMessages = value;
            }
		}
		private ShortMessageSet _ShortMessages = null;

        /// <summary>
        /// Entitites enumeration expression for data set "ShortMessages" of <see cref="ShortMessage" /> that depend on the current entity.
        /// The corresponding foreign key in <see cref="ShortMessageSet" /> set is { <see cref="ShortMessage.TypeID" /> }.
        /// </summary>
		public IEnumerable<ShortMessage> ShortMessageEnum
		{
			get;
            set;
		}

        /// <summary>
        /// A list of <see cref="ShortMessage" /> that is to be added or updated to the data source, together with the current entity.
        /// The corresponding foreign key in <see cref="ShortMessageSet" /> set is { <see cref="ShortMessage.TypeID" /> }.
        /// </summary>
        [DataMember]
		public ShortMessage[] ChangedShortMessages
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
        public bool IsEntityIdentical(ShortMessageType other)
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
        public bool IsEntityTheSame(ShortMessageType other)
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
        public static void MergeChanges(ShortMessageType from, ShortMessageType to)
        {
            if (to.IsPersisted)
            {
                if (from.IsTypeLabelModified && !to.IsTypeLabelModified)
                {
                    to.TypeLabel = from.TypeLabel;
                    to.IsTypeLabelModified = true;
                }
            }
            else
            {
                to.IsPersisted = from.IsPersisted;
                to.ID = from.ID;
                to.TypeName = from.TypeName;
                to.TypeLabel = from.TypeLabel;
                to.IsTypeLabelModified = from.IsTypeLabelModified;
            }
        }

        /// <summary>
        /// Update changes to the current entity compared to an input <paramref name="newdata" /> and set the entity to a proper state for updating.
        /// </summary>
        /// <param name="newdata">The "new" entity acting as the source of the changes, if any.</param>
        /// <returns>
        /// </returns>
        public void UpdateChanges(ShortMessageType newdata)
        {
            int cnt = 0;
            if (TypeLabel != newdata.TypeLabel)
            {
                TypeLabel = newdata.TypeLabel;
                IsTypeLabelModified = true;
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
                IsEntityChanged = IsTypeLabelModified;
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
        public ShortMessageType ShallowCopy(bool allData = false, bool preserveState = false)
        {
            ShortMessageType e = new ShortMessageType();
            e.StartAutoUpdating = false;
            e.ID = ID;
            e.TypeName = TypeName;
            e.TypeLabel = TypeLabel;
            if (preserveState)
                e.IsTypeLabelModified = IsTypeLabelModified;
            else
                e.IsTypeLabelModified = false;
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
----===== [[ShortMessageType]] =====----
  ID = " + ID + @"");
            sb.Append(@" (natural id)");
            sb.Append(@"
  TypeName = '" + TypeName + @"'
  TypeLabel = '" + (TypeLabel != null ? TypeLabel : "null") + @"'");
            if (IsTypeLabelModified)
                sb.Append(@" (modified)");
            else
                sb.Append(@" (unchanged)");
            sb.Append(@"
");
            return sb.ToString();
        }

    }

    ///<summary>
    ///The result of an add or update of type <see cref="ShortMessageType" />.
    ///</summary>
    [DataContract]
    [Serializable]
    public class ShortMessageTypeUpdateResult : IUpdateResult
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
        public ShortMessageType UpdatedItem
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
        public ShortMessageType ConflictItem
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