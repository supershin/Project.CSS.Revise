(function initQueueBankCustomerViewSignalR() {
    if (typeof signalR === "undefined") {
        console.warn("[CustomerView] signalR not loaded");
        return;
    }

    function getProjectId() {
        const hid = document.getElementById("hidProjectId");
        return hid ? (hid.value || "").toString().trim() : "";
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(baseUrl + "hubs/queuebank")
        .withAutomaticReconnect()
        .build();

    async function join() {
        const pid = getProjectId();
        if (!pid) {
            console.warn("[CustomerView] hidProjectId empty -> cannot join group");
            return;
        }
        try {
            await connection.invoke("JoinProject", pid);
            console.log("[CustomerView] Joined:", pid);
        } catch (e) {
            console.error("[CustomerView] JoinProject error:", e);
        }
    }

    connection.on("QueueBankChanged", (payload) => {
        const pidNow = getProjectId();

        // กันหลุดโปรเจกต์
        if (payload?.projectId && payload.projectId !== pidNow) return;

        console.log("[CustomerView] QueueBankChanged:", payload);

        // ✅ refresh โดยเรียก function ตรงๆ (เร็ว+ชัวร์กว่า click ปุ่ม)
        if (window.QueueBankCustomerView?.reloadTable) {
            window.QueueBankCustomerView.reloadTable();
        } else {
            console.warn("[CustomerView] QueueBankCustomerView.reloadTable not found");
        }

        if (window.QueueBankCustomerView?.reloadSummary) {
            window.QueueBankCustomerView.reloadSummary();
        } else {
            console.warn("[CustomerView] QueueBankCustomerView.reloadSummary not found");
        }
    });

    connection.start()
        .then(async () => {
            console.log("[CustomerView] SignalR started");
            await join();
        })
        .catch(err => console.error("[CustomerView] start error:", err));

    connection.onreconnected(async () => {
        console.log("[CustomerView] reconnected");
        await join();
    });
})();
