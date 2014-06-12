﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using CryptoGateway.RDB.Data.MembershipPlus;
using Archymeta.Web.Security.Resources;
using Archymeta.Web.MembershipPlus.AppLayer.Models;

namespace Archymeta.Web.MembershipPlus.AppLayer
{
    public class GroupChatContext
    {
        protected static string AppId
        {
            get
            {
                return ApplicationContext.App.ID;
            }
        }

        protected static CallContext Cntx
        {
            get
            {
                return ApplicationContext.ClientContext;
            }
        }

        protected static int MaxSubscriptions
        {
            get
            {
                if (!_maxSubscription.HasValue)
                {
                    string str = ConfigurationManager.AppSettings["MaxMemberChatGroupSubscriptions"] == null ? "" : ConfigurationManager.AppSettings["MaxMemberChatGroupSubscriptions"];
                    int ival;
                    if (int.TryParse(str, out ival))
                        _maxSubscription = ival;
                    else
                        _maxSubscription = 5;
                }
                return _maxSubscription.Value;
            }
        }
        protected static int? _maxSubscription;

        protected static int InitMsgTimeWindow
        {
            get
            {
                if (!_initMsgTimeWindow.HasValue)
                {
                    string str = ConfigurationManager.AppSettings["MaxInitialChatMessagesTimeWindowMinutes"] == null ? "" : ConfigurationManager.AppSettings["MaxInitialChatMessagesTimeWindowMinutes"];
                    int ival;
                    if (int.TryParse(str, out ival))
                        _initMsgTimeWindow = ival;
                    else
                        _initMsgTimeWindow = 30;
                }
                return _initMsgTimeWindow.Value;
            }
        }
        protected static int? _initMsgTimeWindow;

        public static async Task<ChatContextVM> ListChatRooms(string uId)
        {
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var m = new ChatContextVM();
            var cntx = Cntx;
            QueryExpresion qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "GroupName" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "GroupTypeID == " + ApplicationContext.ChatGroupTypeID + " && ParentID is null" }
            });
            m.TopRooms = new List<EntityAbs<UserGroup>>();
            var roots = await gsvc.QueryDatabaseAsync(cntx, new UserGroupSet(), qexpr);
            foreach (var r in roots)
            {
                var top = await gsvc.LoadEntityFullHierarchyRecursAsync(cntx, r);
                if (top != null)
                    m.TopRooms.Add(top);
            }
            UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
            qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UserID" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UserGroupRef.GroupTypeID == " + ApplicationContext.ChatGroupTypeID + " && UserID == \"" + uId + "\""  }
            });
            var recs = await uigsvc.QueryDatabaseAsync(cntx, new UserGroupMemberSet(), qexpr);
            if (recs.Count() > 0)
                m.MemberIds = (from d in recs select d.UserGroupID).ToArray();
            else
                m.MemberIds = new string[] { };
            return m;
        }

        public static async Task<dynamic> LoadRoomSummary(string hubId, string id)
        {
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var cntx = Cntx;
            string descr = await gsvc.LoadEntityGroupDescriptionAsync(cntx, id);
            MemberCallbackServiceProxy mcbsvc = new MemberCallbackServiceProxy();
            var qexpr = getConnectedGroupMemberFilter(hubId, id);
            long cnt = await mcbsvc.QueryEntityCountAsync(cntx, new MemberCallbackSet(), qexpr);
            UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
            qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UserID" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UserGroupID == \"" + id + "\" && SubscribedTo is not null && SubscribedTo == true"  }
            });
            long scnt = await uigsvc.QueryEntityCountAsync(cntx, new UserGroupMemberSet(), qexpr);
            qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "Username" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UsersInRole_UserID.RoleRef.UserGroupAdminRole.GroupID == \"" + id + "\"" }
            });
            UserServiceProxy usvc = new UserServiceProxy();
            var admins = await usvc.QueryDatabaseAsync(cntx, new UserSet(), qexpr);
            List<dynamic> ladms = new List<dynamic>();
            if (admins.Count() > 0)
            {
                RoleServiceProxy rsvc = new RoleServiceProxy();
                foreach (var u in admins)
                {
                    qexpr = new QueryExpresion();
                    qexpr.OrderTks = new List<QToken>(new QToken[] { 
                        new QToken { TkName = "ID" }
                    });
                    qexpr.FilterTks = new List<QToken>(new QToken[] { 
                        new QToken { TkName = "UserGroupAdminRole.GroupID == \"" + id + "\" && UsersInRole.UserID == \"" + u.ID + "\"" }
                    });
                    var role = (await rsvc.QueryDatabaseAsync(cntx, new RoleSet(), qexpr)).First();
                    ladms.Add(new { id = u.ID, name = u.Username, role = role.DisplayName });
                }
            }
            dynamic r = new { descr = descr, active = cnt, subscribers = scnt, admins = ladms };
            return r;
        }

        public static async Task<ChatRoomVM> LoadChatRoom(string hubId, string groupId, string userId, int maxMessages, bool dialog = false)
        {
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var cntx = Cntx;
            ChatRoomVM m = new ChatRoomVM() { RoomExists = false, DialogMode = dialog };
            if (!string.IsNullOrEmpty(groupId))
            {
                var g = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
                if (g != null)
                {
                    m.RoomExists = true;
                    m.ID = groupId;
                    m.RoomPath = g.DistinctString.Split('/');
                    m.RoomInfo = await gsvc.LoadEntityGroupDescriptionAsync(cntx, g.ID);
                    UserGroupMemberServiceProxy gmsvc = new UserGroupMemberServiceProxy();
                    UserGroupMember uig = await gmsvc.LoadEntityByKeyAsync(cntx, groupId, userId);
                    if (uig == null)
                    {
                        uig = new UserGroupMember
                        {
                            UserID = userId,
                            UserGroupID = groupId,
                            SubscribedTo = false,
                            ActivityNotification = false
                        };
                        await gmsvc.AddOrUpdateEntitiesAsync(cntx, new UserGroupMemberSet(), new UserGroupMember[] { uig });
                    }
                    cntx.DirectDataAccess = true;
                    MemberCallbackServiceProxy cbsv = new MemberCallbackServiceProxy();
                    var qexpr = getConnectedGroupMemberFilter(hubId, g.ID);
                    var peers = (await cbsv.QueryDatabaseAsync(cntx, new MemberCallbackSet(), qexpr)).ToArray();
                    List<string> jsonPeers = new List<string>();
                    if (peers.Length > 0)
                    {
                        for (int i = 0; i < peers.Length; i++)
                        {
                            // retrieve the related entity graph
                            peers[i] = await cbsv.LoadEntityGraphRecursAsync(cntx, groupId, hubId, AppId, peers[i].UserID, null, null);
                            jsonPeers.Add(GetJsonPeer(peers[i]));
                        }
                    }
                    m.ActivePeers = jsonPeers.ToArray();
                    cntx.DirectDataAccess = false;
                    ShortMessageServiceProxy msgsvc = new ShortMessageServiceProxy();
                    var cond = new ShortMessageSetConstraints
                    {
                        ApplicationIDWrap = new ForeignKeyData<string> { KeyValue = AppId },
                        TypeIDWrap = new ForeignKeyData<int> { KeyValue = ApplicationContext.ChatShortMsgTypeId },
                        GroupIDWrap = new ForeignKeyData<string> { KeyValue = groupId }
                    };
                    UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
                    qexpr = new QueryExpresion();
                    qexpr.OrderTks = new List<QToken>(new QToken[] { 
                        new QToken { TkName = "UserID" }
                    });
                    qexpr.FilterTks = new List<QToken>(new QToken[] { 
                        new QToken { TkName = "UserGroupID == \"" + g.ID + "\" && SubscribedTo is not null && SubscribedTo == true"  }
                    });
                    m.Subscribers = (int)(await uigsvc.QueryEntityCountAsync(cntx, new UserGroupMemberSet(), qexpr));
                    var svc = new MembershipPlusServiceProxy();
                    DateTime dt = DateTime.UtcNow.AddMinutes(-InitMsgTimeWindow);
                    qexpr = new QueryExpresion();
                    qexpr.OrderTks = new List<QToken>(new QToken[] { 
                        new QToken { TkName = "CreatedDate" },
                        new QToken { TkName = "desc" }
                    });
                    qexpr.FilterTks = new List<QToken>(new QToken[] {
                        new QToken { TkName = "ToID is null && CreatedDate > " + svc.FormatRepoDateTime(dt) }
                    });
                    if (dialog)
                    {
                        qexpr.FilterTks.Add(new QToken { TkName = " && ReplyToID is null" });
                    }
                    var msgs = (await msgsvc.ConstraintQueryLimitedAsync(cntx, new ShortMessageSet(), cond, qexpr, maxMessages)).ToArray();
                    List<string> jsonMsgs = new List<string>();
                    if (msgs.Length > 0)
                    {
                        for (int i = msgs.Length - 1; i >= 0; i--)
                        {
                            EntitySetType[] excludes;
                            if (dialog)
                            {
                                excludes = new EntitySetType[]
                                {
                                    EntitySetType.UserGroup,
                                    //EntitySetType.ShortMessageAudience,
                                    EntitySetType.ShortMessageAttachment
                                };
                            }
                            else
                            {
                                excludes = new EntitySetType[]
                                {
                                    EntitySetType.UserGroup,
                                    //EntitySetType.ShortMessageAudience,
                                    EntitySetType.ShortMessageAttachment,
                                    EntitySetType.ShortMessage
                                };
                            }
                            msgs[i] = await msgsvc.LoadEntityGraphRecursAsync(cntx, msgs[i].ID, excludes, null);
                            jsonMsgs.Add(GetJsonMessage(msgs[i], userId, g, dialog));
                        }
                    }
                    m.RecentMsgs = jsonMsgs.ToArray();
                }
            }
            return m;
        }

        public static async Task<string[]> LoadMessages(string groupId, string userId, int maxMessages, bool dialog)
        {
            var gsvc = new UserGroupServiceProxy();
            var svc = new MembershipPlusServiceProxy();
            var msgsvc = new ShortMessageServiceProxy();
            var cntx = Cntx;
            var g = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
            DateTime dt = DateTime.UtcNow.AddMinutes(-InitMsgTimeWindow);
            var cond = new ShortMessageSetConstraints
            {
                ApplicationIDWrap = new ForeignKeyData<string> { KeyValue = AppId },
                TypeIDWrap = new ForeignKeyData<int> { KeyValue = ApplicationContext.ChatShortMsgTypeId },
                GroupIDWrap = new ForeignKeyData<string> { KeyValue = groupId },
                ToIDWrap = new ForeignKeyData<string> { KeyValue = null }
            };
            var qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "CreatedDate" },
                new QToken { TkName = "desc" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] {
                new QToken { TkName = "CreatedDate > " + svc.FormatRepoDateTime(dt) }
            });
            if (dialog)
            {
                qexpr.FilterTks.Add(new QToken { TkName = " && ReplyToID is null" });
            }
            var msgs = (await msgsvc.ConstraintQueryLimitedAsync(cntx, new ShortMessageSet(), cond, qexpr, maxMessages)).ToArray();
            List<string> jsonMsgs = new List<string>();
            if (msgs.Length > 0)
            {
                for (int i = msgs.Length - 1; i >= 0; i--)
                {
                    EntitySetType[] excludes;
                    if (dialog)
                    {
                        excludes = new EntitySetType[]
                                {
                                    EntitySetType.UserGroup,
                                    //EntitySetType.ShortMessageAudience,
                                    EntitySetType.ShortMessageAttachment
                                };
                    }
                    else
                    {
                        excludes = new EntitySetType[]
                                {
                                    EntitySetType.UserGroup,
                                    //EntitySetType.ShortMessageAudience,
                                    EntitySetType.ShortMessageAttachment,
                                    EntitySetType.ShortMessage
                                };
                    }
                    msgs[i] = await msgsvc.LoadEntityGraphRecursAsync(cntx, msgs[i].ID, excludes, null);
                    jsonMsgs.Add(GetJsonMessage(msgs[i], userId, g, dialog));
                }
            }
            return jsonMsgs.ToArray();
        }

        public static async Task<MemberCallback> UserConnected(string hubId, string groupId, string userId, string connectId, string languages)
        {
            var mbsvc = new UserAppMemberServiceProxy();
            var cntx = Cntx;
            cntx.AcceptLanguages = languages;
            var memb = await mbsvc.LoadEntityGraphRecursAsync(cntx, AppId, userId, null, null);
            if (memb != null)
            {
                memb.StartAutoUpdating = true;
                memb.LastActivityDate = DateTime.UtcNow;
                memb.AcceptLanguages = languages;
                List<MemberCallback> callbacks;
                if (memb.ChangedMemberCallbacks == null)
                    callbacks = new List<MemberCallback>();
                else
                    callbacks = new List<MemberCallback>(memb.ChangedMemberCallbacks);
                var cbk = (from d in callbacks where d.HubID == hubId && d.ChannelID == groupId select d).SingleOrDefault();
                if (cbk == null)
                {
                    cbk = new MemberCallback
                    {
                        ApplicationID = AppId,
                        UserID = userId,
                        HubID = hubId,
                        ChannelID = groupId,
                        ConnectionID = connectId,
                        IsDisconnected = false,
                        LastActiveDate = DateTime.UtcNow
                    };
                }
                else
                {
                    // it is very important to turn this on, otherwise the property will not be marked as modified.
                    // and the service will not save the change!
                    cbk.StartAutoUpdating = true;
                    cbk.ConnectionID = connectId;
                    cbk.IsDisconnected = false;
                    cbk.LastActiveDate = DateTime.UtcNow;
                    // other more explicit way of doing so is given in the following:
                    //cbk.ConnectionID = connectId;
                    //cbk.IsConnectionIDModified = true;
                    //cbk.IsDisconnected = false;
                    //cbk.IsIsDisconnectedModified = true;
                    //cbk.LastActiveDate = DateTime.UtcNow;
                    //cbk.IsLastActiveDateModified = true;
                }
                memb.ChangedMemberCallbacks = new MemberCallback[] { cbk };
                await mbsvc.AddOrUpdateEntitiesAsync(cntx, new UserAppMemberSet(), new UserAppMember[] { memb });
                cbk.UserAppMemberRef = memb;
                if (memb.UserRef == null)
                    memb.UserRef = await mbsvc.MaterializeUserRefAsync(cntx, memb);
                return cbk;
            }
            return null;
        }

        public static async Task<string[]> ListConnectedMembers(string hubId, string groupId)
        {
            MemberCallbackServiceProxy cbsv = new MemberCallbackServiceProxy();
            var cntx = Cntx;
            var qexpr = getConnectedGroupMemberFilter(hubId, groupId);
            var peers = (await cbsv.QueryDatabaseAsync(cntx, new MemberCallbackSet(), qexpr)).ToArray();
            List<string> jsonPeers = new List<string>();
            if (peers.Length > 0)
            {
                for (int i = 0; i < peers.Length; i++)
                {
                    // retrieve the related entity graph
                    peers[i] = await cbsv.LoadEntityGraphRecursAsync(cntx, groupId, hubId, AppId, peers[i].UserID, null, null);
                    jsonPeers.Add(GetJsonPeer(peers[i]));
                }
            }
            return jsonPeers.ToArray();
        }

        public static async Task<MemberCallback[]> ListConnectIds(string hubId, string groupId, bool direct = false)
        {
            MemberCallbackServiceProxy cbsv = new MemberCallbackServiceProxy();
            var cntx = Cntx;
            cntx.DirectDataAccess = direct;
            var qexpr = getConnectedGroupMemberFilter(hubId, groupId);
            var peers = (await cbsv.QueryDatabaseAsync(cntx, new MemberCallbackSet(), qexpr)).ToList();
            await _listSupervisors(hubId, groupId, peers);
            return peers.ToArray();
        }

        private static async Task _listSupervisors(string hubId, string groupId, List<MemberCallback> list)
        {
            var cntx = Cntx;
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            MemberCallbackServiceProxy cbsv = new MemberCallbackServiceProxy();
            var g = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
            while (g.ParentID != null)
            {
                g = await gsvc.MaterializeUpperRefAsync(cntx, g);
                var qexpr = getConnectedGroupMemberFilter(hubId, g.ID, true);
                var peers = (await cbsv.QueryDatabaseAsync(cntx, new MemberCallbackSet(), qexpr)).ToArray();
                if (peers.Length > 0)
                    list.AddRange(peers);
            }
        }

        public static async Task<dynamic> Subscribe(string userId, string groupId)
        {
            var cntx = Cntx;
            UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
            var uig = await uigsvc.LoadEntityByKeyAsync(cntx, groupId, userId);
            if (uig != null && uig.SubscribedTo.HasValue && uig.SubscribedTo.Value)
                return new { ok = false, msg = ResourceUtils.GetString("73cca337310dc88725934ac22120ffaf", "You are already a subscriber!") };
            QueryExpresion qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>();
            qexpr.OrderTks.Add(new QToken { TkName = "UserGroupID" });
            qexpr.FilterTks = new List<QToken>();
            qexpr.FilterTks.Add(new QToken
            {
                TkName = "UserID == \"" + userId + "\" && SubscribedTo is not null && SubscribedTo == true"
            });
            var uigs = await uigsvc.QueryDatabaseAsync(cntx, new UserGroupMemberSet(), qexpr);
            if (uigs.Count() >= MaxSubscriptions)
            {
                return new
                {
                    ok = false,
                    msg = ResourceUtils.GetString("06200770094b57284bc7b16e0c7ea274", "You number of subscriptions exeeds the allowed limit, please unsubscribe from some of the existing ones first.")
                };
            }
            if (uig == null)
            {
                uig = new UserGroupMember
                {
                    UserID = userId,
                    UserGroupID = groupId,
                    SubscribedTo = true,
                    ActivityNotification = false
                };
            }
            else
                uig.SubscribedTo = true;
            await uigsvc.AddOrUpdateEntitiesAsync(cntx, new UserGroupMemberSet(), new UserGroupMember[] { uig });
            return new { ok = true, msg = "" };
        }

        public static async Task<dynamic> Unsubscribe(string userId, string groupId)
        {
            var cntx = Cntx;
            UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
            var uig = await uigsvc.LoadEntityByKeyAsync(cntx, groupId, userId);
            if (uig != null && (!uig.SubscribedTo.HasValue || !uig.SubscribedTo.Value))
                return new { ok = false, msg = ResourceUtils.GetString("735f81aa93cc3430592d2cc2a57b1cd7", "You are not a subscriber yet!") };
            if (uig == null)
            {
                uig = new UserGroupMember
                {
                    UserID = userId,
                    UserGroupID = groupId,
                    SubscribedTo = false,
                    ActivityNotification = false
                };
            }
            else
                uig.SubscribedTo = false;
            await uigsvc.AddOrUpdateEntitiesAsync(cntx, new UserGroupMemberSet(), new UserGroupMember[] { uig });
            return new { ok = true, msg = "" };
        }

        public static async Task<dynamic> ChangeNotification(string userId, string groupId)
        {
            var cntx = Cntx;
            UserGroupMemberServiceProxy uigsvc = new UserGroupMemberServiceProxy();
            var uig = await uigsvc.LoadEntityByKeyAsync(cntx, groupId, userId);
            if (!uig.ActivityNotification.HasValue)
                uig.ActivityNotification = true;
            else
                uig.ActivityNotification = !uig.ActivityNotification.Value;
            await uigsvc.AddOrUpdateEntitiesAsync(cntx, new UserGroupMemberSet(), new UserGroupMember[] { uig });
            return new { ok = true, msg = "" };
        }

        public static async Task<ShotMessageNotice> AddUserMessage(string noticeHubId, string chatHubId, string userId, string groupId, string replyId, string message)
        {
            var cntx = Cntx;
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var g = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
            ShortMessageServiceProxy msvc = new ShortMessageServiceProxy();
            var now = DateTime.UtcNow;
            ShortMessage msg = new ShortMessage
            {
                ID = Guid.NewGuid().ToString(),
                ApplicationID = AppId,
                TypeID = ApplicationContext.ChatShortMsgTypeId,
                GroupID = groupId,
                FromID = userId,
                ToID = null,
                ReplyToID = string.IsNullOrEmpty(replyId) ? null : replyId,
                CreatedDate = now,
                LastModified = now,
                MsgTitle = GetLeadText(message),
                MsgText = message
            };
            await msvc.AddOrUpdateEntitiesAsync(cntx, new ShortMessageSet(), new ShortMessage[] { msg });
            UserServiceProxy usvc = new UserServiceProxy();
            var u = await usvc.LoadEntityByKeyAsync(cntx, userId);
            msg.User_FromID = u;
            UserGroupMemberServiceProxy gmsvc = new UserGroupMemberServiceProxy();
            var cond = new UserGroupMemberSetConstraints
            {
                UserGroupIDWrap = new ForeignKeyData<string> { KeyValue = groupId }
            };
            var qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { new QToken { TkName = "UserID" } });
            qexpr.FilterTks = new List<QToken>(new QToken[] {
                new QToken 
                {
                    TkName = "SubscribedTo is not null && SubscribedTo == true"
                }
            });
            var gmbs = await gmsvc.ConstraintQueryAsync(cntx, new UserGroupMemberSet(), cond, qexpr);
            List<MemberNotification> notices = new List<MemberNotification>();
            List<MemberCallback> noteCbks = new List<MemberCallback>();
            MemberCallbackServiceProxy mcbsvc = new MemberCallbackServiceProxy();
            string noticeMsg = "Group message by " + u.Username + " in " + g.DistinctString;
            MemberNotificationTypeServiceProxy ntsvc = new MemberNotificationTypeServiceProxy();
            var ntype = await ntsvc.LoadEntityByKeyAsync(cntx, ApplicationContext.NewMessageNoticeTypeId);
            foreach (var m in gmbs)
            {
                if (m.ActivityNotification.HasValue && m.ActivityNotification.Value)
                {
                    var cb = await mcbsvc.LoadEntityByKeyAsync(cntx, groupId, noticeHubId, AppId, m.UserID);
                    if (cb.ConnectionID != null && !cb.IsDisconnected)
                    {
                        cb.UserAppMemberRef = await mcbsvc.MaterializeUserAppMemberRefAsync(cntx, cb);
                        noteCbks.Add(cb);
                    }
                }
                notices.Add(new MemberNotification
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = noticeMsg,
                    CreatedDate = now,
                    PriorityLevel = 0,
                    ReadCount = 0,
                    ApplicationID = AppId,
                    TypeID = ApplicationContext.NewMessageNoticeTypeId,
                    UserID = userId
                });
            }
            var peers = await ListConnectIds(chatHubId, groupId);
            List<ShortMessageAudience> laud = new List<ShortMessageAudience>();
            foreach (var peer in peers)
            {
                if (peer.UserID != userId)
                {
                    var a = new ShortMessageAudience
                    {
                        MsgID = msg.ID,
                        UserID = peer.UserID,
                        VoteCount = 0
                    };
                    laud.Add(a);
                }
            }
            if (laud.Count > 0)
            {
                ShortMessageAudienceServiceProxy audsvc = new ShortMessageAudienceServiceProxy();
                await audsvc.AddOrUpdateEntitiesAsync(cntx, new ShortMessageAudienceSet(), laud.ToArray());
            }
            if (notices.Count > 0)
            {
                MemberNotificationServiceProxy nsvc = new MemberNotificationServiceProxy();
                await nsvc.AddOrUpdateEntitiesAsync(cntx, new MemberNotificationSet(), notices.ToArray());
            }
            return new ShotMessageNotice { msg = GetJsonMessage(msg, userId, g, false), brief = noticeMsg, categ = ntype, peers = peers, callbacks = noteCbks };
        }

        public static async Task<ShotMessageNotice> UpdateUserMessage(string noticeHubId, string chatHubId, string userId, string groupId, string msgId, string message)
        {
            var cntx = Cntx;
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var g = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
            ShortMessageServiceProxy msvc = new ShortMessageServiceProxy();
            var msg = await msvc.LoadEntityByKeyAsync(cntx, msgId);
            if (msg == null || msg.FromID != userId)
                return null;
            if (msg.MsgText == message)
                return null;
            var now = DateTime.UtcNow;
            msg.MsgTitle = GetLeadText(message);
            msg.MsgText = message;
            msg.LastModified = now;
            await msvc.AddOrUpdateEntitiesAsync(cntx, new ShortMessageSet(), new ShortMessage[] { msg });
            UserServiceProxy usvc = new UserServiceProxy();
            var u = await usvc.LoadEntityByKeyAsync(cntx, userId);
            msg.User_FromID = u;
            UserGroupMemberServiceProxy gmsvc = new UserGroupMemberServiceProxy();
            var cond = new UserGroupMemberSetConstraints
            {
                UserGroupIDWrap = new ForeignKeyData<string> { KeyValue = groupId }
            };
            var qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { new QToken { TkName = "UserID" } });
            qexpr.FilterTks = new List<QToken>(new QToken[] {
                new QToken 
                {
                    TkName = "SubscribedTo is not null && SubscribedTo == true"
                }
            });
            var gmbs = await gmsvc.ConstraintQueryAsync(cntx, new UserGroupMemberSet(), cond, qexpr);
            List<MemberNotification> notices = new List<MemberNotification>();
            List<MemberCallback> noteCbks = new List<MemberCallback>();
            MemberCallbackServiceProxy mcbsvc = new MemberCallbackServiceProxy();
            string noticeMsg = "Group message by " + u.Username + " updated in " + g.DistinctString;
            MemberNotificationTypeServiceProxy ntsvc = new MemberNotificationTypeServiceProxy();
            var ntype = await ntsvc.LoadEntityByKeyAsync(cntx, ApplicationContext.NewMessageNoticeTypeId);
            foreach (var m in gmbs)
            {
                if (m.ActivityNotification.HasValue && m.ActivityNotification.Value)
                {
                    var cb = await mcbsvc.LoadEntityByKeyAsync(cntx, groupId, noticeHubId, AppId, m.UserID);
                    if (cb.ConnectionID != null && !cb.IsDisconnected)
                    {
                        cb.UserAppMemberRef = await mcbsvc.MaterializeUserAppMemberRefAsync(cntx, cb);
                        noteCbks.Add(cb);
                    }
                }
                notices.Add(new MemberNotification
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = noticeMsg,
                    CreatedDate = now,
                    PriorityLevel = 0,
                    ReadCount = 0,
                    ApplicationID = AppId,
                    TypeID = ApplicationContext.NewMessageNoticeTypeId,
                    UserID = userId
                });
            }
            var peers = await ListConnectIds(chatHubId, groupId);
            List<ShortMessageAudience> laud = new List<ShortMessageAudience>();
            foreach (var peer in peers)
            {
                if (peer.UserID != userId)
                {
                    var a = new ShortMessageAudience
                    {
                        MsgID = msg.ID,
                        UserID = peer.UserID,
                        VoteCount = 0
                    };
                    laud.Add(a);
                }
            }
            if (laud.Count > 0)
            {
                ShortMessageAudienceServiceProxy audsvc = new ShortMessageAudienceServiceProxy();
                await audsvc.AddOrUpdateEntitiesAsync(cntx, new ShortMessageAudienceSet(), laud.ToArray());
            }
            if (notices.Count > 0)
            {
                MemberNotificationServiceProxy nsvc = new MemberNotificationServiceProxy();
                await nsvc.AddOrUpdateEntitiesAsync(cntx, new MemberNotificationSet(), notices.ToArray());
            }
            return new ShotMessageNotice { msg = GetJsonMessage(msg, userId, g, false), brief = noticeMsg, categ = ntype, peers = peers, callbacks = noteCbks };
        }

        public static async Task<int> VoteOnMessage(string msgId, string userId, int del)
        {
            var cntx = Cntx;
            cntx.DirectDataAccess = true;
            ShortMessageAudienceServiceProxy audsvc = new ShortMessageAudienceServiceProxy();
            var aud = await audsvc.LoadEntityByKeyAsync(cntx, msgId, userId);
            if (aud == null)
            {
                aud = new ShortMessageAudience
                {
                    MsgID = msgId,
                    UserID = userId,
                    VoteCount = del
                };
            }
            else
                aud.VoteCount += del;
            await audsvc.AddOrUpdateEntitiesAsync(cntx, new ShortMessageAudienceSet(), new ShortMessageAudience[] { aud });
            return aud.VoteCount;
        }

        private static DateTime unix0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static string getUnixJsonTime(DateTime? dt)
        {
            if (dt.HasValue)
                return @"""/Date(" + (dt.Value.ToUniversalTime() - unix0).Ticks / 10000 + @")/""";
            else
                return "??";
        }

        public static string GetJsonPeer(MemberCallback peer)
        {
            bool hasIcon = false;
            if (peer.UserAppMemberRef != null)
                hasIcon = !string.IsNullOrEmpty(peer.UserAppMemberRef.IconMime);
            string json = "{ ";
            json += @"""id"": """ + peer.UserID + @""", ";
            json += @"""name"": """ + (peer.UserAppMemberRef != null ? peer.UserAppMemberRef.UserRef.Username : "") + @""", ";
            json += @"""email"": """ + (peer.UserAppMemberRef != null && peer.UserAppMemberRef.Email != null ? peer.UserAppMemberRef.Email : "") + @""", ";
            json += @"""active"": " + (peer.IsDisconnected ? "false" : "true") + @", ";
            json += @"""icon"": " + (hasIcon ? "true" : "false") + @", ";
            json += @"""lastActive"": " + getUnixJsonTime(peer.LastActiveDate) + @"";
            json += " }";
            return json;
        }

        public static string GetJsonMessage(ShortMessage msg, string userId, UserGroup g, bool dialog)
        {
            string json = "{ ";
            json += @"""id"": """ + msg.ID + @""", ";
            json += @"""from"": """ + msg.User_FromID.Username + @""", ";
            json += @"""fromId"": """ + msg.FromID + @""", ";
            json += @"""group"": """ + g.DistinctString + @""", ";
            json += @"""groupId"": """ + g.ID + @""", ";
            json += @"""replyToId"": """ + (msg.ReplyToID == null ? "" : msg.ReplyToID) + @""", ";
            json += @"""date"": " + getUnixJsonTime(msg.CreatedDate) + @", ";
            json += @"""self"": " + (msg.FromID == userId ? "true" : "false") + @", ";
            json += @"""lead"": """ + GetLeadText(msg.MsgText).Replace("\"", "\\\"") + @""", ";
            json += @"""text"": """ + msg.MsgText.Replace("\"", "\\\"") + @""", ";
            json += @"""score"": ";
            if (msg.ChangedShortMessageAudiences != null)
            {
                int cnt = 0;
                foreach (var v in msg.ChangedShortMessageAudiences)
                    cnt += v.VoteCount;
                json += cnt;
            }
            else
                json += "0";
            if (dialog)
            {
                json += @", ""replies"": [ ";
                if (msg.ChangedShortMessages != null)
                {
                    string subjson = "";
                    foreach (var r in from d in msg.ChangedShortMessages orderby d.CreatedDate ascending select d)
                        subjson += GetJsonMessage(r, userId, g, true) + ", ";
                    json += subjson.TrimEnd(" ,".ToCharArray());
                }
                json += " ]";
            }
            json += " }";
            return json;
        }

        public static string GetLeadText(string html)
        {
            html = HttpUtility.UrlDecode(html);
            int maxLenght = 15;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            StringBuilder sb = new StringBuilder();
            getText(doc.DocumentNode, sb, maxLenght);
            if (sb.Length > maxLenght)
                sb = sb.Remove(maxLenght, sb.Length - maxLenght);
            return sb.ToString().TrimEnd() + "...";
        }

        private static void getText(HtmlNode n, StringBuilder sb, int? maxLength)
        {
            if (n.NodeType == HtmlNodeType.Text)
            {
                string[] lines = n.InnerText.Split('\n');
                bool done = false;
                for (int i = 0; i < lines.Length && !done;i++ )
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        var line = Regex.Replace(lines[i].TrimEnd('\r'), @"\t+", " ");
                        line = Regex.Replace(line, @"\s{2,}", " ");
                        done = maxLength.HasValue && sb.Length + line.Length >= maxLength.Value;
                        if (done)
                            line = line.Substring(0, line.Length - sb.Length);
                        sb.Append(line + " ");
                    }
                }
            }
            else
            {
                foreach (var c in n.ChildNodes)
                {
                    getText(c, sb, maxLength);
                    if (maxLength.HasValue && sb.Length >= maxLength.Value)
                        break;
                }
            }
        }

        /// <summary>
        /// A message in a particular room will be broadcasted to all connected members in sub-tree of rooms
        /// </summary>
        /// <param name="groupId">The room id</param>
        /// <returns>A list of active connection ids covered.</returns>
        public static async Task<MemberCallback[]> SubTreeMembers(string hubId, string groupId)
        {
            UserGroupServiceProxy gsvc = new UserGroupServiceProxy();
            var cntx = Cntx;
            List<MemberCallback> callbacks = new List<MemberCallback>();
            var top = await gsvc.LoadEntityByKeyAsync(cntx, groupId);
            if (top != null)
            {
                var tree = await gsvc.LoadEntityHierarchyRecursAsync(cntx, top, 0, -1);
                await _groupMembersRecurs(hubId, cntx, tree, callbacks);
            }
            return callbacks.ToArray();
        }

        private static QueryExpresion getConnectedGroupMemberFilter(string hubId, string groupId, bool supervisor = false)
        {
            MembershipPlusServiceProxy svc = new MembershipPlusServiceProxy();
            DateTime dt = DateTime.UtcNow.AddMinutes(-ApplicationContext.OnlineUserInactiveTime);
            QueryExpresion qexpr = new QueryExpresion();
            qexpr.OrderTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "UserID" },
                new QToken { TkName = "asc" }
            });
            qexpr.FilterTks = new List<QToken>(new QToken[] { 
                new QToken { TkName = "HubID == \"" + hubId + "\" && ChannelID == \"" + groupId + "\" && ConnectionID is not null && IsDisconnected == false" + (!supervisor ? "" : " && SupervisorMode is not null && SupervisorMode == true") },
                new QToken { TkName = "&&" },
                new QToken { TkName = "ApplicationID == \"" + AppId + "\"" },
                new QToken { TkName = "&&" },
                new QToken { TkName = "UserAppMemberRef.UserRef.UserGroupMember.UserGroupID == \"" + groupId + "\"" },
                new QToken { TkName = "&&" },
                new QToken { TkName = "UserAppMemberRef.LastActivityDate > " + svc.FormatRepoDateTime(dt) }
            });
            return qexpr;
        }

        private static async Task _groupMembersRecurs(string hubId, CallContext cntx, EntityAbs<UserGroup> g, List<MemberCallback> callbacks, bool drillDown = false)
        {
            MemberCallbackServiceProxy cbsv = new MemberCallbackServiceProxy();
            var qexpr = getConnectedGroupMemberFilter(hubId, g.DataBehind.ID);
            var list = await cbsv.QueryDatabaseAsync(cntx, new MemberCallbackSet(), qexpr);
            if (list.Count() > 0)
            {
                callbacks.AddRange(list);
            }
            if (drillDown && g.ChildEntities != null)
            {
                foreach (var c in g.ChildEntities)
                    await _groupMembersRecurs(hubId, cntx, c, callbacks);
            }
        }
    }
}
