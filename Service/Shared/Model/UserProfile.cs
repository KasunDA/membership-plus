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
    /// A entity in "UserProfiles" data set.
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
    ///      <description>See <see cref="UserProfile.ID" />. Auto-gen primary key; fixed; not null.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Intrinsic Identifiers</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>PropName</term>
    ///      <description>See <see cref="UserProfile.PropName" />. Intrinsic id; fixed; not null; max-length = 80 characters.</description>
    ///    </item>
    ///    <item>
    ///      <term>ApplicationID</term>
    ///      <description>See <see cref="UserProfile.ApplicationID" />. Intrinsic id; fixed; not null; foreign key.</description>
    ///    </item>
    ///    <item>
    ///      <term>UserID</term>
    ///      <description>See <see cref="UserProfile.UserID" />. Intrinsic id; fixed; nullable; foreign key.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Editable properties</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>BinaryValue</term>
    ///      <description>See <see cref="UserProfile.BinaryValue" />. Editable; nullable; load delayed.</description>
    ///    </item>
    ///    <item>
    ///      <term>LastAccessTime</term>
    ///      <description>See <see cref="UserProfile.LastAccessTime" />. Editable; nullable.</description>
    ///    </item>
    ///    <item>
    ///      <term>LastUpdateTime</term>
    ///      <description>See <see cref="UserProfile.LastUpdateTime" />. Editable; nullable.</description>
    ///    </item>
    ///    <item>
    ///      <term>StringValue</term>
    ///      <description>See <see cref="UserProfile.StringValue" />. Editable; nullable; load delayed.</description>
    ///    </item>
    ///    <item>
    ///      <term>ValueMimeType</term>
    ///      <description>See <see cref="UserProfile.ValueMimeType" />. Editable; nullable; max-length = 50 characters.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>Foreign keys</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>ApplicationID</term>
    ///      <description>See <see cref="UserProfile.ApplicationID" />. Intrinsic id; fixed; not null; foreign key.</description>
    ///    </item>
    ///    <item>
    ///      <term>TypeID</term>
    ///      <description>See <see cref="UserProfile.TypeID" />. Fixed; not null; foreign key.</description>
    ///    </item>
    ///    <item>
    ///      <term>UserID</term>
    ///      <description>See <see cref="UserProfile.UserID" />. Intrinsic id; fixed; nullable; foreign key.</description>
    ///    </item>
    ///  </list>
    ///  <list type="table">
    ///    <listheader>
    ///       <term>This entity depends on</term><description>Description</description>
    ///    </listheader>
    ///    <item>
    ///      <term>Application_Ref</term>
    ///      <description>See <see cref="UserProfile.Application_Ref" />, which is a member of the data set "Applications" for <see cref="Application_" />.</description>
    ///    </item>
    ///    <item>
    ///      <term>UserProfileTypeRef</term>
    ///      <description>See <see cref="UserProfile.UserProfileTypeRef" />, which is a member of the data set "UserProfileTypes" for <see cref="UserProfileType" />.</description>
    ///    </item>
    ///    <item>
    ///      <term>UserRef</term>
    ///      <description>See <see cref="UserProfile.UserRef" />, which is a member of the data set "Users" for <see cref="User" />. Nullable.</description>
    ///    </item>
    ///  </list>
    /// </remarks>
    [DataContract]
    [Serializable]
    public class UserProfile : IDbEntity 
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
        /// Used to matching entities in input adding or updating entity list and the returned ones, see <see cref="IUserProfileService.AddOrUpdateEntities" />.
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
                str += "PropName = " + PropName + "\r\n";
                str += "ApplicationID = " + ApplicationID + "\r\n";
                str += "UserID = " + UserID + "\r\n";
                if (IsBinaryValueModified)
                    str += "Modified [BinaryValue] = " + BinaryValue + "\r\n";
                if (IsLastAccessTimeModified)
                    str += "Modified [LastAccessTime] = " + LastAccessTime + "\r\n";
                if (IsLastUpdateTimeModified)
                    str += "Modified [LastUpdateTime] = " + LastUpdateTime + "\r\n";
                if (IsStringValueModified)
                    str += "Modified [StringValue] = " + StringValue + "\r\n";
                if (IsValueMimeTypeModified)
                    str += "Modified [ValueMimeType] = " + ValueMimeType + "\r\n";;
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
            return String.Format(@"{0}", PropName.Trim());
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
        public UserProfile()
        {
        }

        /// <summary>
        /// Constructor for serialization (<see cref="ISerializable" />).
        /// </summary>
        public UserProfile(SerializationInfo info, StreamingContext context)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UserProfile));
            var strm = new System.IO.MemoryStream();
            byte[] bf = (byte[])info.GetValue("data", typeof(byte[]));
            strm.Write(bf, 0, bf.Length);
            strm.Position = 0;
            var e = ser.ReadObject(strm) as UserProfile;
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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UserProfile));
            var strm = new System.IO.MemoryStream();
            ser.WriteObject(strm, ShallowCopy());
            info.AddValue("data", strm.ToArray(), typeof(byte[]));
        }

#endregion

#region Properties of the current entity

        /// <summary>
        /// Meta-info: auto-gen primary key; fixed; not null.
        /// </summary>
        [Key]
        [Editable(false)]
        [DataMember(IsRequired = false)]
        public Int64 ID
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
        private Int64 _ID = default(Int64);

        /// <summary>
        /// Meta-info: intrinsic id; fixed; not null; max-length = 80 characters.
        /// </summary>
        [Key]
        [Required]
        [Editable(false)]
        [StringLength(80)]
        [DataMember(IsRequired = true)]
        public string PropName
        { 
            get
            {
                return _PropName;
            }
            set
            {
                if (_PropName != value)
                {
                    _PropName = value;
                }
            }
        }
        private string _PropName = default(string);

        /// <summary>
        /// Meta-info: editable; nullable; load delayed.
        /// </summary>
        [Editable(true)]
        [DataMember(IsRequired = false)]
        public byte[] BinaryValue
        { 
            get
            {
                return _BinaryValue;
            }
            set
            {
                if (_BinaryValue != value)
                {
                    _BinaryValue = value;
                    if (StartAutoUpdating)
                        IsBinaryValueModified = true;
                    if (StartAutoUpdating)
                        IsBinaryValueLoaded = value != null;
                }
            }
        }
        private byte[] _BinaryValue = default(byte[]);

        /// <summary>
        /// Wether or not the value of <see cref="BinaryValue" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="BinaryValue" /> only if this is set to true no matter what
        /// the actual value of <see cref="BinaryValue" /> is.
        /// </summary>
        [DataMember]
        public bool IsBinaryValueModified
        { 
            get
            {
                return _isBinaryValueModified;
            }
            set
            {
                _isBinaryValueModified = value;
            }
        }
        private bool _isBinaryValueModified = false;

        /// <summary>
        /// Wether or not the value of the delay loaded "BinaryValue" is Loaded. Clients are responsible for keeping 
        /// track of loading status of delay loading properties.
        /// </summary>
        [DataMember]
        public bool IsBinaryValueLoaded
        { 
            get
            {
                return _isBinaryValueLoaded;
            }
            set
            {
                _isBinaryValueLoaded = value;
            }
        }
        private bool _isBinaryValueLoaded = false;

        /// <summary>
        /// Meta-info: editable; nullable.
        /// </summary>
        [Editable(true)]
        [DataMember(IsRequired = false)]
        public System.Nullable<DateTime> LastAccessTime
        { 
            get
            {
                return _LastAccessTime;
            }
            set
            {
                if (_LastAccessTime != value)
                {
                    _LastAccessTime = value;
                    if (StartAutoUpdating)
                        IsLastAccessTimeModified = true;
                }
            }
        }
        private System.Nullable<DateTime> _LastAccessTime = default(System.Nullable<DateTime>);

        /// <summary>
        /// Wether or not the value of <see cref="LastAccessTime" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="LastAccessTime" /> only if this is set to true no matter what
        /// the actual value of <see cref="LastAccessTime" /> is.
        /// </summary>
        [DataMember]
        public bool IsLastAccessTimeModified
        { 
            get
            {
                return _isLastAccessTimeModified;
            }
            set
            {
                _isLastAccessTimeModified = value;
            }
        }
        private bool _isLastAccessTimeModified = false;

        /// <summary>
        /// Meta-info: editable; nullable.
        /// </summary>
        [Editable(true)]
        [DataMember(IsRequired = false)]
        public System.Nullable<DateTime> LastUpdateTime
        { 
            get
            {
                return _LastUpdateTime;
            }
            set
            {
                if (_LastUpdateTime != value)
                {
                    _LastUpdateTime = value;
                    if (StartAutoUpdating)
                        IsLastUpdateTimeModified = true;
                }
            }
        }
        private System.Nullable<DateTime> _LastUpdateTime = default(System.Nullable<DateTime>);

        /// <summary>
        /// Wether or not the value of <see cref="LastUpdateTime" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="LastUpdateTime" /> only if this is set to true no matter what
        /// the actual value of <see cref="LastUpdateTime" /> is.
        /// </summary>
        [DataMember]
        public bool IsLastUpdateTimeModified
        { 
            get
            {
                return _isLastUpdateTimeModified;
            }
            set
            {
                _isLastUpdateTimeModified = value;
            }
        }
        private bool _isLastUpdateTimeModified = false;

        /// <summary>
        /// Meta-info: editable; nullable; load delayed.
        /// </summary>
        [Editable(true)]
        [DataMember(IsRequired = false)]
        public string StringValue
        { 
            get
            {
                return _StringValue;
            }
            set
            {
                if (_StringValue != value)
                {
                    _StringValue = value;
                    if (StartAutoUpdating)
                        IsStringValueModified = true;
                    if (StartAutoUpdating)
                        IsStringValueLoaded = value != null;
                }
            }
        }
        private string _StringValue = default(string);

        /// <summary>
        /// Wether or not the value of <see cref="StringValue" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="StringValue" /> only if this is set to true no matter what
        /// the actual value of <see cref="StringValue" /> is.
        /// </summary>
        [DataMember]
        public bool IsStringValueModified
        { 
            get
            {
                return _isStringValueModified;
            }
            set
            {
                _isStringValueModified = value;
            }
        }
        private bool _isStringValueModified = false;

        /// <summary>
        /// Wether or not the value of the delay loaded "StringValue" is Loaded. Clients are responsible for keeping 
        /// track of loading status of delay loading properties.
        /// </summary>
        [DataMember]
        public bool IsStringValueLoaded
        { 
            get
            {
                return _isStringValueLoaded;
            }
            set
            {
                _isStringValueLoaded = value;
            }
        }
        private bool _isStringValueLoaded = false;

        /// <summary>
        /// Meta-info: editable; nullable; max-length = 50 characters.
        /// </summary>
        [Editable(true)]
        [StringLength(50)]
        [DataMember(IsRequired = false)]
        public string ValueMimeType
        { 
            get
            {
                return _ValueMimeType;
            }
            set
            {
                if (_ValueMimeType != value)
                {
                    _ValueMimeType = value;
                    if (StartAutoUpdating)
                        IsValueMimeTypeModified = true;
                }
            }
        }
        private string _ValueMimeType = default(string);

        /// <summary>
        /// Wether or not the value of <see cref="ValueMimeType" /> was changed compared to what it was loaded last time. 
        /// Note: the backend data source updates the changed <see cref="ValueMimeType" /> only if this is set to true no matter what
        /// the actual value of <see cref="ValueMimeType" /> is.
        /// </summary>
        [DataMember]
        public bool IsValueMimeTypeModified
        { 
            get
            {
                return _isValueMimeTypeModified;
            }
            set
            {
                _isValueMimeTypeModified = value;
            }
        }
        private bool _isValueMimeTypeModified = false;

        /// <summary>
        /// Meta-info: intrinsic id; fixed; not null; foreign key.
        /// </summary>
        [Key]
        [Required]
        [Editable(false)]
        [DataMember(IsRequired = true)]
        public string ApplicationID
        { 
            get
            {
                return _ApplicationID;
            }
            set
            {
                if (_ApplicationID != value)
                {
                    _ApplicationID = value;
                }
            }
        }
        private string _ApplicationID = default(string);

        /// <summary>
        /// Meta-info: fixed; not null; foreign key.
        /// </summary>
        [Required]
        [Editable(false)]
        [DataMember(IsRequired = true)]
        public int TypeID
        { 
            get
            {
                return _TypeID;
            }
            set
            {
                if (_TypeID != value)
                {
                    _TypeID = value;
                }
            }
        }
        private int _TypeID = default(int);

        /// <summary>
        /// Meta-info: intrinsic id; fixed; nullable; foreign key.
        /// </summary>
        [Key]
        [Editable(false)]
        [DataMember(IsRequired = false)]
        public string UserID
        { 
            get
            {
                return _UserID;
            }
            set
            {
                if (_UserID != value)
                {
                    _UserID = value;
                }
            }
        }
        private string _UserID = default(string);

#endregion

#region Entities that the current one depends upon.

        /// <summary>
        /// Entity in data set "Applications" for <see cref="Application_" /> that this entity depend upon through .
        /// The corresponding foreign key set is { <see cref="UserProfile.ApplicationID" /> }.
        /// </summary>
        [DataMember]
        public Application_ Application_Ref
        {
            get 
            {
                if (_Application_Ref == null && AutoLoadApplication_Ref != null)
                    _Application_Ref = AutoLoadApplication_Ref();
                return _Application_Ref; 
            }
            set 
            { 
                _Application_Ref = value; 
            }
        }
        private Application_ _Application_Ref = null;

        /// <summary>
        /// <see cref="UserProfile.Application_Ref" /> is not initialized when the entity is created. Clients could call this method to load it provided a proper delegate <see cref="UserProfile.DelLoadApplication_Ref" /> was setup
        /// before calling it.
        /// </summary>
        public void LoadApplication_Ref()
        {
            if (_Application_Ref != null)
                return;
            if (DelLoadApplication_Ref != null)
                _Application_Ref = DelLoadApplication_Ref();
        }

        /// <summary>
        /// A delegate to load <see cref="UserProfile.Application_Ref" />.
        /// </summary>
        public Func<Application_> DelLoadApplication_Ref = null;

        /// <summary>
        /// A delegate to load <see cref="UserProfile.Application_Ref" /> automatically when it is referred to at the first time.
        /// </summary>
        public Func<Application_> AutoLoadApplication_Ref = null;

        /// <summary>
        /// Entity in data set "UserProfileTypes" for <see cref="UserProfileType" /> that this entity depend upon through .
        /// The corresponding foreign key set is { <see cref="UserProfile.TypeID" /> }.
        /// </summary>
        [DataMember]
        public UserProfileType UserProfileTypeRef
        {
            get 
            {
                if (_UserProfileTypeRef == null && AutoLoadUserProfileTypeRef != null)
                    _UserProfileTypeRef = AutoLoadUserProfileTypeRef();
                return _UserProfileTypeRef; 
            }
            set 
            { 
                _UserProfileTypeRef = value; 
            }
        }
        private UserProfileType _UserProfileTypeRef = null;

        /// <summary>
        /// <see cref="UserProfile.UserProfileTypeRef" /> is not initialized when the entity is created. Clients could call this method to load it provided a proper delegate <see cref="UserProfile.DelLoadUserProfileTypeRef" /> was setup
        /// before calling it.
        /// </summary>
        public void LoadUserProfileTypeRef()
        {
            if (_UserProfileTypeRef != null)
                return;
            if (DelLoadUserProfileTypeRef != null)
                _UserProfileTypeRef = DelLoadUserProfileTypeRef();
        }

        /// <summary>
        /// A delegate to load <see cref="UserProfile.UserProfileTypeRef" />.
        /// </summary>
        public Func<UserProfileType> DelLoadUserProfileTypeRef = null;

        /// <summary>
        /// A delegate to load <see cref="UserProfile.UserProfileTypeRef" /> automatically when it is referred to at the first time.
        /// </summary>
        public Func<UserProfileType> AutoLoadUserProfileTypeRef = null;

        /// <summary>
        /// Entity in data set "Users" for <see cref="User" /> that this entity depend upon through .
        /// The corresponding foreign key set is { <see cref="UserProfile.UserID" /> }.
        /// </summary>
        [DataMember]
        public User UserRef
        {
            get 
            {
                if (UserID == null)
                    return null;
                else if (_UserRef == null && AutoLoadUserRef != null)
                    _UserRef = AutoLoadUserRef();
                return _UserRef; 
            }
            set 
            { 
                _UserRef = value; 
            }
        }
        private User _UserRef = null;

        /// <summary>
        /// <see cref="UserProfile.UserRef" /> is not initialized when the entity is created. Clients could call this method to load it provided a proper delegate <see cref="UserProfile.DelLoadUserRef" /> was setup
        /// before calling it.
        /// </summary>
        public void LoadUserRef()
        {
            if (UserID == null || _UserRef != null)
                return;
            if (DelLoadUserRef != null)
                _UserRef = DelLoadUserRef();
        }

        /// <summary>
        /// A delegate to load <see cref="UserProfile.UserRef" />.
        /// </summary>
        public Func<User> DelLoadUserRef = null;

        /// <summary>
        /// A delegate to load <see cref="UserProfile.UserRef" /> automatically when it is referred to at the first time.
        /// </summary>
        public Func<User> AutoLoadUserRef = null;

#endregion

#region Entities that depend on the current one.

#endregion

        /// <summary>
        /// Whether or not the present entity is identitical to <paramref name="other" />, in the sense that they have the same (set of) primary key(s).
        /// </summary>
        /// <param name="other">The entity to be compared to.</param>
        /// <returns>
        ///   The result of comparison.
        /// </returns>
        public bool IsEntityIdentical(UserProfile other)
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
        public bool IsEntityTheSame(UserProfile other)
        {
            if (other == null)
                return false;
            else
                return PropName == other.PropName &&  ApplicationID == other.ApplicationID &&  UserID == other.UserID;
        }              

        /// <summary>
        /// Merge changes inside entity <paramref name="from" /> to the entity <paramref name="to" />. Any changes in <paramref name="from" /> that is not changed in <paramref name="to" /> is updated inside <paramref name="to" />.
        /// </summary>
        /// <param name="from">The "old" entity acting as merging source.</param>
        /// <param name="to">The "new" entity which inherits changes made in <paramref name="from" />.</param>
        /// <returns>
        /// </returns>
        public static void MergeChanges(UserProfile from, UserProfile to)
        {
            if (to.IsPersisted)
            {
                if (from.IsBinaryValueModified && !to.IsBinaryValueModified)
                {
                    to.BinaryValue = from.BinaryValue;
                    to.IsBinaryValueModified = true;
                }
                if (from.IsLastAccessTimeModified && !to.IsLastAccessTimeModified)
                {
                    to.LastAccessTime = from.LastAccessTime;
                    to.IsLastAccessTimeModified = true;
                }
                if (from.IsLastUpdateTimeModified && !to.IsLastUpdateTimeModified)
                {
                    to.LastUpdateTime = from.LastUpdateTime;
                    to.IsLastUpdateTimeModified = true;
                }
                if (from.IsStringValueModified && !to.IsStringValueModified)
                {
                    to.StringValue = from.StringValue;
                    to.IsStringValueModified = true;
                }
                if (from.IsValueMimeTypeModified && !to.IsValueMimeTypeModified)
                {
                    to.ValueMimeType = from.ValueMimeType;
                    to.IsValueMimeTypeModified = true;
                }
            }
            else
            {
                to.IsPersisted = from.IsPersisted;
                to.ID = from.ID;
                to.PropName = from.PropName;
                to.BinaryValue = from.BinaryValue;
                to.IsBinaryValueModified = from.IsBinaryValueModified;
                to.LastAccessTime = from.LastAccessTime;
                to.IsLastAccessTimeModified = from.IsLastAccessTimeModified;
                to.LastUpdateTime = from.LastUpdateTime;
                to.IsLastUpdateTimeModified = from.IsLastUpdateTimeModified;
                to.StringValue = from.StringValue;
                to.IsStringValueModified = from.IsStringValueModified;
                to.ValueMimeType = from.ValueMimeType;
                to.IsValueMimeTypeModified = from.IsValueMimeTypeModified;
                to.ApplicationID = from.ApplicationID;
                to.TypeID = from.TypeID;
                to.UserID = from.UserID;
            }
        }

        /// <summary>
        /// Update changes to the current entity compared to an input <paramref name="newdata" /> and set the entity to a proper state for updating.
        /// </summary>
        /// <param name="newdata">The "new" entity acting as the source of the changes, if any.</param>
        /// <returns>
        /// </returns>
        public void UpdateChanges(UserProfile newdata)
        {
            int cnt = 0;
            bool bBinaryValue = BinaryValue == null && newdata.BinaryValue != null ||
                                                         BinaryValue != null && newdata.BinaryValue == null ||
                                                         BinaryValue != null && newdata.BinaryValue != null && BinaryValue.Length != newdata.BinaryValue.Length;
            if (!bBinaryValue && BinaryValue != null)
            {
                for (int i = 0; i < BinaryValue.Length; i++)
                {
                    bBinaryValue = BinaryValue[i] != newdata.BinaryValue[i];
                    if (bBinaryValue)
                        break;
                }
            }
            if (bBinaryValue)
            {
                BinaryValue = newdata.BinaryValue;
                IsBinaryValueModified = true;
                cnt++;
            }
            if (LastAccessTime != newdata.LastAccessTime)
            {
                LastAccessTime = newdata.LastAccessTime;
                IsLastAccessTimeModified = true;
                cnt++;
            }
            if (LastUpdateTime != newdata.LastUpdateTime)
            {
                LastUpdateTime = newdata.LastUpdateTime;
                IsLastUpdateTimeModified = true;
                cnt++;
            }
            if (StringValue != newdata.StringValue)
            {
                StringValue = newdata.StringValue;
                IsStringValueModified = true;
                cnt++;
            }
            if (ValueMimeType != newdata.ValueMimeType)
            {
                ValueMimeType = newdata.ValueMimeType;
                IsValueMimeTypeModified = true;
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
            if (PropName == null)
                PropName = "";
            if (ApplicationID == null)
                ApplicationID = "";
            if (!IsEntityChanged)
                IsEntityChanged = IsBinaryValueModified || IsLastAccessTimeModified || IsLastUpdateTimeModified || IsStringValueModified || IsValueMimeTypeModified;
            if (IsBinaryValueModified && !IsBinaryValueLoaded)
                IsBinaryValueLoaded = true;
            if (IsStringValueModified && !IsStringValueLoaded)
                IsStringValueLoaded = true;
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
        public UserProfile ShallowCopy(bool allData = false, bool preserveState = false, bool checkLoadState = false)
        {
            UserProfile e = new UserProfile();
            e.StartAutoUpdating = false;
            e.ID = ID;
            e.PropName = PropName;
            e.LastAccessTime = LastAccessTime;
            if (preserveState)
                e.IsLastAccessTimeModified = IsLastAccessTimeModified;
            else
                e.IsLastAccessTimeModified = false;
            e.LastUpdateTime = LastUpdateTime;
            if (preserveState)
                e.IsLastUpdateTimeModified = IsLastUpdateTimeModified;
            else
                e.IsLastUpdateTimeModified = false;
            e.ValueMimeType = ValueMimeType;
            if (preserveState)
                e.IsValueMimeTypeModified = IsValueMimeTypeModified;
            else
                e.IsValueMimeTypeModified = false;
            e.ApplicationID = ApplicationID;
            e.TypeID = TypeID;
            e.UserID = UserID;
            if (allData)
            {
                if (!checkLoadState || IsBinaryValueLoaded)
                    e.BinaryValue = BinaryValue;
                if (preserveState)
                    e.IsBinaryValueModified = IsBinaryValueModified;
                else
                    e.IsBinaryValueModified = false;
                e.IsBinaryValueLoaded = IsBinaryValueLoaded;
                if (!checkLoadState || IsStringValueLoaded)
                    e.StringValue = StringValue;
                if (preserveState)
                    e.IsStringValueModified = IsStringValueModified;
                else
                    e.IsStringValueModified = false;
                e.IsStringValueLoaded = IsStringValueLoaded;
            }
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
----===== [[UserProfile]] =====----
  ID = " + ID + @"
  PropName = '" + PropName + @"'");
            sb.Append(@" (natural id)");
            sb.Append(@"
  LastAccessTime = " + (LastAccessTime.HasValue ? LastAccessTime.Value.ToString() : "null") + @"");
            if (IsLastAccessTimeModified)
                sb.Append(@" (modified)");
            else
                sb.Append(@" (unchanged)");
            sb.Append(@"
  LastUpdateTime = " + (LastUpdateTime.HasValue ? LastUpdateTime.Value.ToString() : "null") + @"");
            if (IsLastUpdateTimeModified)
                sb.Append(@" (modified)");
            else
                sb.Append(@" (unchanged)");
            sb.Append(@"
  ValueMimeType = '" + (ValueMimeType != null ? ValueMimeType : "null") + @"'");
            if (IsValueMimeTypeModified)
                sb.Append(@" (modified)");
            else
                sb.Append(@" (unchanged)");
            sb.Append(@"
  ApplicationID = '" + ApplicationID + @"'");
            sb.Append(@" (natural id)");
            sb.Append(@"
  TypeID = " + TypeID + @"
  UserID = '" + (UserID != null ? UserID : "null") + @"'");
            sb.Append(@" (natural id)");
            sb.Append(@"
");
            return sb.ToString();
        }

    }

    ///<summary>
    ///The result of an add or update of type <see cref="UserProfile" />.
    ///</summary>
    [DataContract]
    [Serializable]
    public class UserProfileUpdateResult : IUpdateResult
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
        public UserProfile UpdatedItem
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
        public UserProfile ConflictItem
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
