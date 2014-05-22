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

namespace CryptoGateway.RDB.Data.MembershipPlus
{
    /// <summary>
    /// A structure representing a block of pages.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ShortMessageDataBlockPageBlock
    {
        /// <summary>
        /// The total number of entities.
        /// </summary>
        [DataMember]
        public Int64 TotalEntities
        {
            get { return _totalEntities; } 
            set { _totalEntities = value; }
        }
        private Int64 _totalEntities = 0;

        /// <summary>
        /// The total number of pages.
        /// </summary>
        [DataMember]
        public Int64 TotalPages
        {
            get { return _totalPages; } 
            set { _totalPages = value; }
        }
        private Int64 _totalPages = 0;

        /// <summary>
        /// The number of pages inside the block.
        /// </summary>
        [DataMember]
        public int BlockCount
        {
            get { return _blockCount; } 
            set { _blockCount = value; }
        }
        private int _blockCount = 0;

        /// <summary>
        /// Whether or not this is the last blcok of pages inside the data set.
        /// </summary>
        [DataMember]
        public bool IsLastBlock
        {
            get;
            set;
        }

        /// <summary>
        /// The collection of pages inside the block.
        /// </summary>
        [DataMember]
        public ShortMessageDataBlockPage[] Pages
        {
            get;
            set;
        }
    }

    /// <summary>
    /// A structure representing a page of entity <see cref="ShortMessageDataBlock" />.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ShortMessageDataBlockPage
    {
        /// <summary>
        /// The zero based index of the page.
        /// </summary>
        [DataMember]
        public int Index_
        {
            get { return _index; } 
            set { _index = value; }
        }
        private int _index = 0;

        /// <summary>
        /// The page identifier.
        /// </summary>
        [DataMember]
        public string PageNumber
        {
            get { return (Index_ + 1).ToString(); }
            set { }
        }

        /// <summary>
        /// The first entity of the page.
        /// </summary>
        [DataMember]
        public ShortMessageDataBlock FirstItem
        {
            get;
            set;
        }

        /// <summary>
        /// The last entity of the page.
        /// </summary>
        [DataMember]
        public ShortMessageDataBlock LastItem
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the current page is the last page of the data set.
        /// </summary>
        [DataMember]
        public bool IsLastPage
        {
            get;
            set;
        }

        /// <summary>
        /// The collection of entities inside the page.
        /// </summary>
        public List<ShortMessageDataBlock> Items
        {
            get;
            set;
        }

        public ShortMessageDataBlockPage(int idx)
        {
            _index = idx;
        }

    }

}
