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
            console.log("sendCallStaff:", data);

            const currentProjectId = document.getElementById("hidProjectId")?.value || "";

            // ignore event จาก project อื่น
            if (data?.ProjectID && currentProjectId && String(data.ProjectID) !== String(currentProjectId)) {
                return;
            }

            const counterNo = data?.Counter;
            if (counterNo === undefined || counterNo === null) return;

            const status = qbNormStatus(data?.CallStaffStatus);

            if (status === "start") {
                // ✅ start: blink (จนกว่าจะ stop หรือ timeout)
                qbBlinkCounters([counterNo], { durationMs: 0, replace: false }); // durationMs:0 = ไม่หมดเวลาเอง
                qbPlayDingSafe(); // 🔔 เล่นเสียง (ถ้า unlock แล้ว)

            } else if (status === "stop") {
                // ✅ stop: หยุด blink
                qbBlinkStop(counterNo);
            } else {
                // ถ้า status แปลกๆ -> treat as start (หรือจะ ignore ก็ได้)
                qbBlinkCounters([counterNo], { durationMs: 15000, replace: false });
                qbPlayDingSafe();
            }
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