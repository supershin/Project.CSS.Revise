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

            if (btn) {
                btn.click();
            } else {
                console.warn("btnSearch not found");
            }

        });


        //connecting the client to the signalr hub   
        SignalrConnection.start().done(function () {
            
            alert("Connected to Signalr Server");
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