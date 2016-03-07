/*!
 * ASP.NET SignalR JavaScript Library v2.1.2
 * http://signalr.net/
 *
 * Copyright Microsoft Open Technologies, Inc. All rights reserved.
 * Licensed under the Apache 2.0
 * https://github.com/SignalR/SignalR/blob/master/LICENSE.md
 *
 */

/// <reference path="..\..\SignalR.Client.JS\Scripts\jquery-1.6.4.js" />
/// <reference path="jquery.signalR.js" />
(function ($, window, undefined) {
    /// <param name="$" type="jQuery" />
    "use strict";

    if (typeof ($.signalR) !== "function") {
        throw new Error("SignalR: SignalR is not loaded. Please ensure jquery.signalR-x.js is referenced before ~/signalr/js.");
    }

    var signalR = $.signalR;

    function makeProxyCallback(hub, callback) {
        return function () {
            // Call the client hub method
            callback.apply(hub, $.makeArray(arguments));
        };
    }

    function registerHubProxies(instance, shouldSubscribe) {
        var key, hub, memberKey, memberValue, subscriptionMethod;

        for (key in instance) {
            if (instance.hasOwnProperty(key)) {
                hub = instance[key];

                if (!(hub.hubName)) {
                    // Not a client hub
                    continue;
                }

                if (shouldSubscribe) {
                    // We want to subscribe to the hub events
                    subscriptionMethod = hub.on;
                } else {
                    // We want to unsubscribe from the hub events
                    subscriptionMethod = hub.off;
                }

                // Loop through all members on the hub and find client hub functions to subscribe/unsubscribe
                for (memberKey in hub.client) {
                    if (hub.client.hasOwnProperty(memberKey)) {
                        memberValue = hub.client[memberKey];

                        if (!$.isFunction(memberValue)) {
                            // Not a client hub function
                            continue;
                        }

                        subscriptionMethod.call(hub, memberKey, makeProxyCallback(hub, memberValue));
                    }
                }
            }
        }
    }

    $.hubConnection.prototype.createHubProxies = function () {
        var proxies = {};
        this.starting(function () {
            // Register the hub proxies as subscribed
            // (instance, shouldSubscribe)
            registerHubProxies(proxies, true);

            this._registerSubscribedHubs();
        }).disconnected(function () {
            // Unsubscribe all hub proxies when we "disconnect".  This is to ensure that we do not re-add functional call backs.
            // (instance, shouldSubscribe)
            registerHubProxies(proxies, false);
        });

        proxies['hubSync'] = this.createHubProxy('hubSync'); 
        proxies['hubSync'].client = { };
        proxies['hubSync'].server = {
            getCurrentUserInfo: function (connectionID) {
            /// <summary>Calls the GetCurrentUserInfo method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"connectionID\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["GetCurrentUserInfo"], $.makeArray(arguments)));
             },

            heartbeat: function () {
            /// <summary>Calls the Heartbeat method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["Heartbeat"], $.makeArray(arguments)));
             },

            refreshAllClientList: function () {
            /// <summary>Calls the RefreshAllClientList method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["RefreshAllClientList"], $.makeArray(arguments)));
             },

            sendAllClient: function (name, message) {
            /// <summary>Calls the SendAllClient method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendAllClient"], $.makeArray(arguments)));
             },

            sendAllClientExcept: function (name, message, connectionIds) {
            /// <summary>Calls the SendAllClientExcept method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"connectionIds\" type=\"Object\">Server side type is System.String[]</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendAllClientExcept"], $.makeArray(arguments)));
             },

            sendAllClientExceptSelf: function (name, message) {
            /// <summary>Calls the SendAllClientExceptSelf method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendAllClientExceptSelf"], $.makeArray(arguments)));
             },

            sendToGroup: function (groupName, name, message) {
            /// <summary>Calls the SendToGroup method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToGroup"], $.makeArray(arguments)));
             },

            sendToGroupExcept: function (groupName, name, message, connectionIds) {
            /// <summary>Calls the SendToGroupExcept method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"connectionIds\" type=\"Object\">Server side type is System.String[]</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToGroupExcept"], $.makeArray(arguments)));
             },

            sendToGroups: function (groupNameList, name, message) {
            /// <summary>Calls the SendToGroups method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupNameList\" type=\"Object\">Server side type is System.Collections.Generic.IList`1[System.String]</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToGroups"], $.makeArray(arguments)));
             },

            sendToMany: function (connectionIds, name, message) {
            /// <summary>Calls the SendToMany method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"connectionIds\" type=\"Object\">Server side type is System.Collections.Generic.IList`1[System.String]</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToMany"], $.makeArray(arguments)));
             },

            sendToOtherGroups: function (groupName, name, message) {
            /// <summary>Calls the SendToOtherGroups method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToOtherGroups"], $.makeArray(arguments)));
             },

            sendToOtherManyGroups: function (groupNameList, name, message) {
            /// <summary>Calls the SendToOtherManyGroups method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupNameList\" type=\"Object\">Server side type is System.Collections.Generic.IList`1[System.String]</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToOtherManyGroups"], $.makeArray(arguments)));
             },

            sendToSelf: function (name, message) {
            /// <summary>Calls the SendToSelf method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToSelf"], $.makeArray(arguments)));
             },

            sendToSingle: function (toConnectionID, name, message) {
            /// <summary>Calls the SendToSingle method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"toConnectionID\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SendToSingle"], $.makeArray(arguments)));
             },

            subscribe: function (name) {
            /// <summary>Calls the Subscribe method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["Subscribe"], $.makeArray(arguments)));
             },

            subscribeGroup: function (groupName) {
            /// <summary>Calls the SubscribeGroup method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["SubscribeGroup"], $.makeArray(arguments)));
             },

            unsubscribe: function (name) {
            /// <summary>Calls the Unsubscribe method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"name\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["Unsubscribe"], $.makeArray(arguments)));
             },

            unsubscribeGroup: function (groupName) {
            /// <summary>Calls the UnsubscribeGroup method on the server-side HubSync hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
                return proxies['hubSync'].invoke.apply(proxies['hubSync'], $.merge(["UnsubscribeGroup"], $.makeArray(arguments)));
             }
        };

        return proxies;
    };

    signalR.hub = $.hubConnection("/signalr", { useDefaultPath: false });
    $.extend(signalR, signalR.hub.createHubProxies());

}(window.jQuery, window));