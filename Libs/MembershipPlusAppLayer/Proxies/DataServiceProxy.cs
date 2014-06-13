﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Archymeta.Web.Security.Resources;
using Archymeta.Web.Search.Proxies;
using Archymeta.Web.MembershipPlus.AppLayer.Models;
using CryptoGateway.RDB.Data.MembershipPlus;

namespace Archymeta.Web.MembershipPlus.AppLayer.Proxies
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataServiceProxy : IDataServiceProxy
    {
        public async Task<string> GetSetInfo(string sourceId, string set)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            dynamic sobj = jser.DeserializeObject(set) as dynamic;
            EntitySetType type;
            if (Enum.TryParse<EntitySetType>(sobj["set"], out type))
            {
                string filter = null;
                if (sobj.ContainsKey("setFilter"))
                    filter = sobj["setFilter"];
                switch (type)
                {
                    case EntitySetType.User:
                        {
                            UserServiceProxy svc = new UserServiceProxy();
                            var si = await svc.GetSetInfoAsync(ApplicationContext.ClientContext, filter);
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            string json = ser.Serialize(new { EntityCount = si.EntityCount, Sorters = si.Sorters });
                            return json;
                        }
                    case EntitySetType.Role:
                        {
                            RoleServiceProxy svc = new RoleServiceProxy();
                            var si = await svc.GetSetInfoAsync(ApplicationContext.ClientContext, filter);
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            string json = ser.Serialize(new { EntityCount = si.EntityCount, Sorters = si.Sorters });
                            return json;
                        }
                    case EntitySetType.MemberNotification:
                        {
                            MemberNotificationServiceProxy svc = new MemberNotificationServiceProxy();
                            var si = await svc.GetSetInfoAsync(ApplicationContext.ClientContext, filter);
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            string json = ser.Serialize(new { EntityCount = si.EntityCount, Sorters = si.Sorters });
                            return json;
                        }
                    case EntitySetType.ShortMessage:
                        {
                            if (filter == null)
                                throw new Exception("The page is not properly parameterized!");
                            else
                            {
                                Func<string, string, int> count = (s, p) =>
                                {
                                    int _cnt = 0;
                                    int i = 0;
                                    while ((i = s.IndexOf(p, i)) != -1)
                                    {
                                        _cnt++;
                                        i += p.Length;
                                    }
                                    return _cnt;
                                };
                                if (filter.Contains("ToID is null") && filter.Contains("___usergroups___") && count(filter, "||") == 0)
                                {
                                    string[] mbgrpIds = await GroupChatViewContext.UserGroupChatMembers(System.Web.HttpContext.Current.User.Identity.GetUserId());
                                    if (mbgrpIds == null || mbgrpIds.Length == 0)
                                        throw new Exception(ResourceUtils.GetString("234038e6185f013e25d0213c06f5a0e9", "You are not a member of any chat group."));
                                    string groupexpr = "";
                                    foreach (var gid in mbgrpIds)
                                        groupexpr += (groupexpr != "" ? " || " : "") + "GroupID == \"" + gid + "\"";
                                    filter = filter.Replace("___usergroups___", groupexpr);
                                }
                                else if (filter.EndsWith("&& ToID is not null && GroupID is null && ( ToID == \"{0}\" || FromID == \"{0}\" )") && count(filter, "||") == 1)
                                {
                                    filter = string.Format(filter, System.Web.HttpContext.Current.User.Identity.GetUserId());
                                }
                                else
                                    throw new Exception("The page is not properly parameterized!");
                            }
                            ShortMessageServiceProxy svc = new ShortMessageServiceProxy();
                            var si = await svc.GetSetInfoAsync(ApplicationContext.ClientContext, filter);
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            string json = ser.Serialize(new { EntityCount = si.EntityCount, Sorters = si.Sorters });
                            return json;
                        }
                }
            }
            return null;
        }

        public async Task<string> GetNextSorterOps(string sourceId, string set, string sorters)
        {
            EntitySetType type;
            if (Enum.TryParse<EntitySetType>(set, out type))
            {
                switch (type)
                {
                    case EntitySetType.User:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(List<QToken>));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(sorters);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _sorters = ser1.ReadObject(strm) as List<QToken>;
                            UserServiceProxy svc = new UserServiceProxy();
                            var result = await svc.GetNextSorterOpsAsync(ApplicationContext.ClientContext, _sorters);
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.Role:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(List<QToken>));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(sorters);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _sorters = ser1.ReadObject(strm) as List<QToken>;
                            RoleServiceProxy svc = new RoleServiceProxy();
                            var result = await svc.GetNextSorterOpsAsync(ApplicationContext.ClientContext, _sorters);
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.MemberNotification:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(List<QToken>));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(sorters);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _sorters = ser1.ReadObject(strm) as List<QToken>;
                            MemberNotificationServiceProxy svc = new MemberNotificationServiceProxy();
                            var result = await svc.GetNextSorterOpsAsync(ApplicationContext.ClientContext, _sorters);
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.ShortMessage:
                        {
                            var ser1 = new DataContractJsonSerializer(typeof(List<QToken>));
                            var ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(sorters);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _sorters = ser1.ReadObject(strm) as List<QToken>;
                            var svc = new ShortMessageServiceProxy();
                            var result = await svc.GetNextSorterOpsAsync(ApplicationContext.ClientContext, _sorters);
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                }
            }
            return null;
        }

        public async Task<string> GetNextFilterOps(string sourceId, string set, string qexpr)
        {
            EntitySetType type;
            if (Enum.TryParse<EntitySetType>(set, out type))
            {
                switch (type)
                {
                    case EntitySetType.User:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            UserServiceProxy svc = new UserServiceProxy();
                            var result = await svc.GetNextFilterOpsAsync(ApplicationContext.ClientContext, _qexpr, "");
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.Role:
                        {
                            var ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            var ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            RoleServiceProxy svc = new RoleServiceProxy();
                            var result = await svc.GetNextFilterOpsAsync(ApplicationContext.ClientContext, _qexpr, "");
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.MemberNotification:
                        {
                            var ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            var ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            MemberNotificationServiceProxy svc = new MemberNotificationServiceProxy();
                            var result = await svc.GetNextFilterOpsAsync(ApplicationContext.ClientContext, _qexpr, "");
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.ShortMessage:
                        {
                            var ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            var ser2 = new DataContractJsonSerializer(typeof(TokenOptions));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            var svc = new ShortMessageServiceProxy();
                            var result = await svc.GetNextFilterOpsAsync(ApplicationContext.ClientContext, _qexpr, "");
                            strm = new System.IO.MemoryStream();
                            ser2.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                }
            }
            return null;
        }

        public async Task<string> NextPageBlock(string sourceId, string set, string qexpr, string prevlast)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            dynamic sobj = jser.DeserializeObject(set) as dynamic;
            EntitySetType type;
            if (Enum.TryParse<EntitySetType>(sobj["set"], out type))
            {
                switch (type)
                {
                    case EntitySetType.User:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(User));
                            DataContractJsonSerializer ser3 = new DataContractJsonSerializer(typeof(UserPageBlock));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            UserServiceProxy svc = new UserServiceProxy();
                            UserSet _set = new UserSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            User _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as User;
                            }
                            var result = await svc.NextPageBlockAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            strm = new System.IO.MemoryStream();
                            ser3.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.Role:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(Role));
                            DataContractJsonSerializer ser3 = new DataContractJsonSerializer(typeof(RolePageBlock));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            RoleServiceProxy svc = new RoleServiceProxy();
                            RoleSet _set = new RoleSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            Role _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as Role;
                            }
                            var result = await svc.NextPageBlockAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            strm = new System.IO.MemoryStream();
                            ser3.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.MemberNotification:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(MemberNotification));
                            DataContractJsonSerializer ser3 = new DataContractJsonSerializer(typeof(MemberNotificationPageBlock));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            MemberNotificationServiceProxy svc = new MemberNotificationServiceProxy();
                            MemberNotificationSet _set = new MemberNotificationSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            MemberNotification _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as MemberNotification;
                            }
                            var result = await svc.NextPageBlockAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            strm = new System.IO.MemoryStream();
                            ser3.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                    case EntitySetType.ShortMessage:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(ShortMessage));
                            DataContractJsonSerializer ser3 = new DataContractJsonSerializer(typeof(ShortMessagePageBlock));
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            var svc = new ShortMessageServiceProxy();
                            var _set = new ShortMessageSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (!sobj.ContainsKey("setFilter"))
                                throw new Exception("The page is not properly parameterized!");
                            else
                            {
                                Func<string, string, int> count = (s, p) =>
                                {
                                    int _cnt = 0;
                                    int i = 0;
                                    while ((i = s.IndexOf(p, i)) != -1)
                                    {
                                        _cnt++;
                                        i += p.Length;
                                    }
                                    return _cnt;
                                };
                                string filter = sobj["setFilter"];
                                if (filter.Contains("ToID is null") && filter.Contains("___usergroups___") && count(filter, "||") == 0)
                                {
                                    string[] mbgrpIds = await GroupChatViewContext.UserGroupChatMembers(System.Web.HttpContext.Current.User.Identity.GetUserId());
                                    if (mbgrpIds == null || mbgrpIds.Length == 0)
                                        throw new Exception(ResourceUtils.GetString("234038e6185f013e25d0213c06f5a0e9", "You are not a member of any chat group."));
                                    string groupexpr = "";
                                    foreach (var gid in mbgrpIds)
                                        groupexpr += (groupexpr != "" ? " || " : "") + "GroupID == \"" + gid + "\"";
                                    _set.SetFilter = filter.Replace("___usergroups___", groupexpr);
                                }
                                else if (filter.EndsWith("&& ToID is not null && GroupID is null && ( ToID == \"{0}\" || FromID == \"{0}\" )") && count(filter, "||") == 1)
                                {
                                    filter = string.Format(filter, System.Web.HttpContext.Current.User.Identity.GetUserId());
                                    _set.SetFilter = filter;
                                }
                                else
                                    throw new Exception("The page is not properly parameterized!");
                            }
                            ShortMessage _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as ShortMessage;
                            }
                            var result = await svc.NextPageBlockAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            strm = new System.IO.MemoryStream();
                            ser3.WriteObject(strm, result);
                            string json = System.Text.Encoding.UTF8.GetString(strm.ToArray());
                            return json;
                        }
                }
            }
            return null;
        }

        public async Task<string> GetPageItems(string sourceId, string set, string qexpr, string prevlast)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            dynamic sobj = jser.DeserializeObject(set) as dynamic;
            EntitySetType type;
            if (Enum.TryParse<EntitySetType>(sobj["set"], out type))
            {
                switch (type)
                {
                    case EntitySetType.User:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(User));
                            var ser3 = new JavaScriptSerializer();
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            UserServiceProxy svc = new UserServiceProxy();
                            UserSet _set = new UserSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            User _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as User;
                            }
                            var result = await svc.GetPageItemsAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            var ar = new List<dynamic>();
                            foreach (var e in result)
                            {
                                ar.Add(new { Id = e.ID.ToString(), DistinctString = e.DistinctString });
                            }
                            string json = ser3.Serialize(ar);
                            return json;
                        }
                    case EntitySetType.Role:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(Role));
                            var ser3 = new JavaScriptSerializer();
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            RoleServiceProxy svc = new RoleServiceProxy();
                            RoleSet _set = new RoleSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            Role _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as Role;
                            }
                            var result = await svc.GetPageItemsAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            var ar = new List<dynamic>();
                            foreach (var e in result)
                            {
                                ar.Add(new { Id = e.ID.ToString(), DistinctString = e.DistinctString });
                            }
                            string json = ser3.Serialize(ar);
                            return json;
                        }
                    case EntitySetType.MemberNotification:
                        {
                            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(QueryExpresion));
                            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(MemberNotification));
                            var ser3 = new JavaScriptSerializer();
                            System.IO.MemoryStream strm = new System.IO.MemoryStream();
                            byte[] sbf = System.Text.Encoding.UTF8.GetBytes(qexpr);
                            strm.Write(sbf, 0, sbf.Length);
                            strm.Position = 0;
                            var _qexpr = ser1.ReadObject(strm) as QueryExpresion;
                            MemberNotificationServiceProxy svc = new MemberNotificationServiceProxy();
                            MemberNotificationSet _set = new MemberNotificationSet();
                            _set.PageBlockSize = int.Parse(sobj["pageBlockSize"]);
                            _set.PageSize_ = int.Parse(sobj["pageSize"]);
                            if (sobj.ContainsKey("setFilter"))
                                _set.SetFilter = sobj["setFilter"];
                            MemberNotification _prevlast = null;
                            if (!string.IsNullOrEmpty(prevlast))
                            {
                                strm = new System.IO.MemoryStream();
                                sbf = System.Text.Encoding.UTF8.GetBytes(prevlast);
                                strm.Write(sbf, 0, sbf.Length);
                                strm.Position = 0;
                                _prevlast = ser2.ReadObject(strm) as MemberNotification;
                            }
                            var result = await svc.GetPageItemsAsync(ApplicationContext.ClientContext, _set, _qexpr, _prevlast);
                            var ar = new List<dynamic>();
                            foreach (var e in result)
                            {
                                ar.Add(new { Id = e.ID.ToString(), DistinctString = e.DistinctString });
                            }
                            string json = ser3.Serialize(ar);
                            return json;
                        }
                }
            }
            return null;
        }
    }
}
