var SignalrConnection;
var ChatProxy;

var appSignalR = {
    init: function () {

        //This will hold the connection to the signalr hub   
        SignalrConnection = $.hubConnection(ChatServerUrl, {
            useDefaultPath: false
        });
        ChatProxy = SignalrConnection.createHubProxy('NotifyHub');

        //trigger call staff
        ChatProxy.on("sendCallStaff", function (data) {
            alert(data.Counter);
            //alert(data.CallStaffStatus);
        });

        ChatProxy.on("notifyCounter", function () {

            const btn = document.getElementById("btnSearch");
            const btnChecker = document.getElementById("btnRefreshChecker");
            const btnCounter = document.getElementById("btnRefreshCounter");

            if (btn) {
                btn.click();
                /*alert("notifyCounter");*/
            } else {
                console.warn("btnSearch not found");
            }

            if (btnChecker) {
                btnChecker.click();
            } else {
                console.warn("btnRefreshChecker not found");
            }

            if (btnCounter) {
                btnCounter.click();
            } else {
                console.warn("btnRefreshCounter not found");
            }

        });


        //connecting the client to the signalr hub   
        SignalrConnection.start().done(function () {
            console.log("Connected to Signalr Server");
        })
            .fail(function () {
                appSignalR.reconnect();
            })
    },
    reconnect: function () {
        SignalrConnection.start().done(function () {
            console.log("Connected to Signalr Server");
        }).fail(function () {
            alert("failed in connecting to the signalr server");           
            setTimeout(() => {
                appSignalR.reconnect();
            }, 5000);
        })
    },
    sendUnitStatusHubServer: function (obj) {
        try {
            ChatProxy.invoke('sendUnitStatus', obj);
        } catch (e) {
            appSignalR.reconnect();
        }
    }
}