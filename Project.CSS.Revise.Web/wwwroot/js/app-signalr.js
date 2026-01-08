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

        ChatProxy.on("notifyCounter", async function (data) {

            // 🔍 LOG everything from SignalR
            console.log("📡 [SignalR] notifyCounter received:", data);
            console.log("  projectId:", data?.ProjectID);
            console.log("  counter:", data?.Counter);
            console.log("  registerLogId:", data?.RegisterLogID);
            console.log("  status:", data?.Status || data?.CallStaffStatus);

            // 1) refresh list/summary
            document.getElementById("btnSearch")?.click();
            document.getElementById("btnRefreshChecker")?.click();
            document.getElementById("btnRefreshCounter")?.click();

            // 2) 🔔 ding
            if (typeof qbPlayDingCooldown === "function") qbPlayDingCooldown(1500);

            // 3) ✅ refresh RIGHT PANEL
            try {
                const detailCol = document.getElementById("counterDetailColumn");
                const isRightOpen = detailCol && !detailCol.classList.contains("d-none");

                // if server tells which counter → use it
                const serverCounter = data?.Counter?.toString?.() || data?.counter?.toString?.();

                const counterToReload = serverCounter || currentCounterNo;

                if (isRightOpen && counterToReload && typeof loadCounterDetail === "function") {
                    console.log("🔄 Reload right panel for counter:", counterToReload);
                    await loadCounterDetail(counterToReload);
                    qbUpdateStopButtonUI(counterToReload);
                }

            } catch (e) {
                console.error("notifyCounter -> reload counter detail failed:", e);
            }

            // 4) Customer View
            if (window.QueueBankCustomerView) {
                window.QueueBankCustomerView.reloadTable?.();
                window.QueueBankCustomerView.reloadSummary?.();
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