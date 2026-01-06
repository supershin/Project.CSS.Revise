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
            const status = qbNormStatus(data?.CallStaffStatus);
            const counterNo = qbNormalizeCounterNo(data?.Counter);
            const projectId = (data?.ProjectID ?? "").toString();
            const registerLogId = parseInt(data?.RegisterLogID ?? "0", 10);

            if (!counterNo) { return; }

            if (status === "start") {
                qbCallStaffMap.set(counterNo, { projectId: projectId, registerLogId: registerLogId });

                // ถ้าจะให้กระพริบด้วย (ของพ่อใหญ่มีอยู่แล้ว)
                if (typeof qbBlinkCounters === "function") { qbBlinkCounters([counterNo], { durationMs: 15000 }); }
                if (typeof qbPlayDingCooldown === "function") { qbPlayDingCooldown(1500); }
            }

            if (status === "stop") {
                qbCallStaffMap.delete(counterNo);
                if (typeof qbBlinkStop === "function") { qbBlinkStop(counterNo); }
            }

            // ถ้ากำลังเปิด detail อยู่ counter เดียวกัน → update ปุ่มทันที
            if (String(currentCounterNo ?? "") === counterNo) {
                qbUpdateStopButtonUI(counterNo);
            }

        });

        ChatProxy.on("notifyCounter", function () {

            const btn = document.getElementById("btnSearch");
            const btnChecker = document.getElementById("btnRefreshChecker");
            const btnCounter = document.getElementById("btnRefreshCounter");

            if (btn) {
                btn.click();
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

            // ✅ CustomerView: reload ผ่าน global object ที่พ่อใหญ่ expose ไว้
            if (window.QueueBankCustomerView) {
                window.QueueBankCustomerView.reloadTable?.();
                window.QueueBankCustomerView.reloadSummary?.();
                return;
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