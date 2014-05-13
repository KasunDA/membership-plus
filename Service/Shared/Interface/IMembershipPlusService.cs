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
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CryptoGateway.RDB.Data.MembershipPlus
{
    /// <summary>
    /// It is bound to webHttp end points accessed by javascript using json serialization method.
    /// </summary>
    [ServiceContract(Namespace = "http://relationaldb.archymeta.com/MembershipPlus/", SessionMode = SessionMode.Allowed)]
    public interface IMembershipPlusService
    {
        /// <summary>
        ///   Sign in the service for relational database "MembershipPlus" and authenticate the identity of the caller. 
        /// Depending on the end points, the authentication may have been delegated to the host. E.g., the end point serving javascript
        /// requests are delegated to Asp.Net website authentication system. For other end points, the caller must provide correct credentials
        /// in order to have permission to continue the call processing.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not check for credentials. It also does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Caller supplied and initialized caller context. If it is null, the service will create an initial one.</param>
        /// <param name="credentials">Caller credential information.</param>
        /// <returns>
        ///   An initialized caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/SignInService")]
        CallContext SignInService(CallContext cntx, CallerCredentials credentials);

        /// <summary>
        ///   Initialize or refresh and check the validity of the caller context information of the caller. 
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   An initialized and refreshed caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/InitializeCallContext")]
        CallContext InitializeCallContext(CallContext cntx);

        /// <summary>
        ///   Retrieve information about the database. 
        /// </summary>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Brief information about current database.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/GetDatabaseInfo")]
        DBInformation GetDatabaseInfo(CallContext cntx);

        /// <summary>
        ///   If the targeting database does not exist or is an empty one, create the database and/or the tables and other constructs. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For real relational database stores, it is safer to create an empty database named "MembershipPlus" inside the targeting database server and
        /// then call this method to create the tables and other constructs.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/CreateDatabase")]
        bool CreateDatabase(CallContext cntx);

        /// <summary>
        ///   Load persisted database information from local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/LoadDatabase")]
        bool LoadDatabase(CallContext cntx);

        /// <summary>
        ///   Save database information to local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/SaveDatabase")]
        bool SaveDatabase(CallContext cntx);

    }

    /// <summary>
    /// Interface for data change notification callbacks.
    /// </summary>
    public interface IServiceNotificationCallback
    {
        /// <summary>
        /// Change notification callback.
        /// </summary>
        /// <param name="SetType">The type of the changed entity.</param>
        /// <param name="Status">The type of changes of the entity.</param>
        /// <param name="Entity">The changed entity.</param>
        [OperationContract(IsOneWay = true)]
        void EntityChanged(EntitySetType SetType, int Status, string Entity);
    }

    /// <summary>
    /// It is bound to basicHttp end points accessed by clients other than a web browser.
    /// </summary>
    [ServiceContract(Namespace = "http://relationaldb.archymeta.com/MembershipPlus/", SessionMode = SessionMode.Allowed)]
    public interface IMembershipPlusService2
    {
        /// <summary>
        ///   Sign in the service for relational database "MembershipPlus" and authenticate the identity of the caller. 
        /// Depending on the end points, the authentication may have been delegated to the host. E.g., the end point serving javascript
        /// requests are delegated to Asp.Net website authentication system. For other end points, the caller must provide correct credentials
        /// in order to have permission to continue the call processing.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not check for credentials. It also does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Caller supplied and initialized caller context. If it is null, the service will create an initial one.</param>
        /// <param name="credentials">Caller credential information.</param>
        /// <returns>
        ///   An initialized caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        CallContext SignInService(CallContext cntx, CallerCredentials credentials);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Sign in the service for relational database "MembershipPlus" and authenticate the identity of the caller. Awaitable asynchronous version.
        /// Depending on the end points, the authentication may have been delegated to the host. E.g., the end point serving javascript
        /// requests are delegated to Asp.Net website authentication system. For other end points, the caller must provide correct credentials
        /// in order to have permission to continue the call processing.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not check for credentials. It also does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Caller supplied and initialized caller context. If it is null, the service will create an initial one.</param>
        /// <param name="credentials">Caller credential information.</param>
        /// <returns>
        ///   An initialized caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<CallContext> SignInServiceAsync(CallContext cntx, CallerCredentials credentials);
#endif

        /// <summary>
        /// Register a subscription to notification of data source changes.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        /// <param name="sets">A list of data sets that the client will receive notifications. If it is set to null, then change notifications 
        /// about all data sets will be sent to the client.</param>
        [OperationContract]
        void SubscribeToUpdates(string clientID, EntitySetType[] sets);
#if SUPPORT_ASYNC
        /// <summary>
        /// Register a subscription to notification of data source changes.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        /// <param name="sets">A list of data sets that the client will receive notifications. If it is set to null, then change notifications 
        /// about all data sets will be sent to the client.</param>
        [OperationContract]
        System.Threading.Tasks.Task SubscribeToUpdatesAsync(string clientID, EntitySetType[] sets);
#endif

        /// <summary>
        /// un-register a subscription to data source change notifications.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        [OperationContract]
        void UnsubscribeToUpdates(string clientID);
#if SUPPORT_ASYNC
        /// <summary>
        /// un-register a subscription to data source change notifications.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        [OperationContract]
        System.Threading.Tasks.Task UnsubscribeToUpdatesAsync(string clientID);
#endif

        /// <summary>
        ///   Initialize or refresh and check the validity of the caller context information of the caller. 
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   An initialized and refreshed caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        CallContext InitializeCallContext(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Initialize or refresh and check the validity of the caller context information of the caller. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   An initialized and refreshed caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<CallContext> InitializeCallContextAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Retrieve information about the database. 
        /// </summary>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Brief information about current database.
        /// </returns>
        [OperationContract]
        DBInformation GetDatabaseInfo(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Retrieve information about the database. Awaitable asynchronous version.
        /// </summary>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Brief information about current database.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<DBInformation> GetDatabaseInfoAsync(CallContext cntx);
#endif

        /// <summary>
        ///   If the targeting database does not exist or is an empty one, create the database and/or the tables and other constructs. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For real relational database stores, it is safer to create an empty database named "MembershipPlus" inside the targeting database server and
        /// then call this method to create the tables and other constructs.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool CreateDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   If the targeting database does not exist or is an empty one, create the database and/or the tables and other constructs. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For real relational database stores, it is safer to create an empty database named "MembershipPlus" inside the targeting database server and
        /// then call this method to create the tables and other constructs.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> CreateDatabaseAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Load persisted database information from local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool LoadDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Load persisted database information from local storage. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> LoadDatabaseAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Save database information to local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool SaveDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Save database information to local storage. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> SaveDatabaseAsync(CallContext cntx);
#endif
    }
    /// <summary>
    /// Duplex channel that is bound to wsDualHttp end points accessed by clients other than a web browser.
    /// </summary>
    [ServiceContract(Namespace = "http://relationaldb.archymeta.com/MembershipPlus/", SessionMode = SessionMode.Allowed, CallbackContract = typeof(IServiceNotificationCallback))]
    public interface IMembershipPlusService3
    {
        /// <summary>
        ///   Sign in the service for relational database "MembershipPlus" and authenticate the identity of the caller. 
        /// Depending on the end points, the authentication may have been delegated to the host. E.g., the end point serving javascript
        /// requests are delegated to Asp.Net website authentication system. For other end points, the caller must provide correct credentials
        /// in order to have permission to continue the call processing.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not check for credentials. It also does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Caller supplied and initialized caller context. If it is null, the service will create an initial one.</param>
        /// <param name="credentials">Caller credential information.</param>
        /// <returns>
        ///   An initialized caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        CallContext SignInService(CallContext cntx, CallerCredentials credentials);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Sign in the service for relational database "MembershipPlus" and authenticate the identity of the caller. Awaitable asynchronous version.
        /// Depending on the end points, the authentication may have been delegated to the host. E.g., the end point serving javascript
        /// requests are delegated to Asp.Net website authentication system. For other end points, the caller must provide correct credentials
        /// in order to have permission to continue the call processing.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not check for credentials. It also does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Caller supplied and initialized caller context. If it is null, the service will create an initial one.</param>
        /// <param name="credentials">Caller credential information.</param>
        /// <returns>
        ///   An initialized caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<CallContext> SignInServiceAsync(CallContext cntx, CallerCredentials credentials);
#endif

        /// <summary>
        /// Register a subscription to notification of data source changes.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        /// <param name="sets">A list of data sets that the client will receive notifications. If it is set to null, then change notifications 
        /// about all data sets will be sent to the client.</param>
        [OperationContract]
        void SubscribeToUpdates(string clientID, EntitySetType[] sets);
#if SUPPORT_ASYNC
        /// <summary>
        /// Register a subscription to notification of data source changes.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        /// <param name="sets">A list of data sets that the client will receive notifications. If it is set to null, then change notifications 
        /// about all data sets will be sent to the client.</param>
        [OperationContract]
        System.Threading.Tasks.Task SubscribeToUpdatesAsync(string clientID, EntitySetType[] sets);
#endif

        /// <summary>
        /// un-register a subscription to data source change notifications.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        [OperationContract]
        void UnsubscribeToUpdates(string clientID);
#if SUPPORT_ASYNC
        /// <summary>
        /// un-register a subscription to data source change notifications.
        /// </summary>
        /// <param name="clientID">An identifier that the client is assigned during signin/initialization stage.</param>
        [OperationContract]
        System.Threading.Tasks.Task UnsubscribeToUpdatesAsync(string clientID);
#endif

        /// <summary>
        ///   Initialize or refresh and check the validity of the caller context information of the caller. 
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   An initialized and refreshed caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        CallContext InitializeCallContext(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Initialize or refresh and check the validity of the caller context information of the caller. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Note: The current version of the system does not validate the returned caller context object. 
        /// Therefore care must be taken to limit the access to the service to trusted nodes or users within a secured network environment.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   An initialized and refreshed caller context object used for subsequent API calls. Supplying an invalid caller context will
        /// result in a deny of the service.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<CallContext> InitializeCallContextAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Retrieve information about the database. 
        /// </summary>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Brief information about current database.
        /// </returns>
        [OperationContract]
        DBInformation GetDatabaseInfo(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Retrieve information about the database. Awaitable asynchronous version.
        /// </summary>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Brief information about current database.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<DBInformation> GetDatabaseInfoAsync(CallContext cntx);
#endif

        /// <summary>
        ///   If the targeting database does not exist or is an empty one, create the database and/or the tables and other constructs. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For real relational database stores, it is safer to create an empty database named "MembershipPlus" inside the targeting database server and
        /// then call this method to create the tables and other constructs.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool CreateDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   If the targeting database does not exist or is an empty one, create the database and/or the tables and other constructs. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For real relational database stores, it is safer to create an empty database named "MembershipPlus" inside the targeting database server and
        /// then call this method to create the tables and other constructs.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> CreateDatabaseAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Load persisted database information from local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool LoadDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Load persisted database information from local storage. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> LoadDatabaseAsync(CallContext cntx);
#endif

        /// <summary>
        ///   Save database information to local storage. 
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        bool SaveDatabase(CallContext cntx);
#if SUPPORT_ASYNC
        /// <summary>
        ///   Save database information to local storage. Awaitable asynchronous version.
        /// </summary>
        /// <remarks>
        ///   Depending on the type of the relational data source attached, this method may not be relevent. 
        /// For self loading relational data stores, calling this method will not have any effect.
        /// </remarks>
        /// <param name="cntx">Authenticated caller context object. If cannot be null.</param>
        /// <returns>
        ///   Whether or not the call is successful.
        /// </returns>
        [OperationContract]
        System.Threading.Tasks.Task<bool> SaveDatabaseAsync(CallContext cntx);
#endif
    }
}