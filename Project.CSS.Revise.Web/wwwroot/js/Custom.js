let viewer;

function openViewer(imageSrc) {

    const viewerContainer = document.getElementById('imageViewerContainer');
    const viewerImage = document.getElementById('viewerImage');

    viewerImage.src = imageSrc;

    if (viewer) {
        viewer.destroy();
    }

    viewer = new Viewer(viewerContainer, {
        inline: false,
        navbar: false,
        toolbar: {
            zoomIn: 1,
            zoomOut: 1,
            oneToOne: 1,
            reset: 1,
            rotateLeft: 1,
            rotateRight: 1,
            flipHorizontal: 1,
            flipVertical: 1,
        },
        hidden() {
            viewerImage.src = ""; // clear image after close
        }
    });

    viewer.show();
}

function showLoading() {
    document.getElementById('global-loading-overlay').style.display = 'block';
}

function hideLoading() {
    document.getElementById('global-loading-overlay').style.display = 'none';
}


/**
 * Show a warning alert top-right that auto hides after duration
 * @param {string} message - the text to show
 * @param {number} durationMs - how long before hide (default 60000 = 1 min)
 */
function showWarning(message, durationMs = 60000) {
    const container = document.getElementById('alertContainer');
    if (!container) return;

    const wrapper = document.createElement('div');
    wrapper.className = "alert alert-warning alert-dismissible fade show shadow-sm";
    wrapper.role = "alert";
    wrapper.innerHTML = `
    <div><i class="fa-solid fa-triangle-exclamation me-2"></i>${message}</div>
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
  `;

    container.appendChild(wrapper);

    // auto remove after duration
    setTimeout(() => {
        const alert = bootstrap.Alert.getOrCreateInstance(wrapper);
        alert.close();
    }, durationMs);
}


// ===== Simple SweetAlert2 wrappers (global functions) =====

/**
 * Modal: Success (กด OK ปิด)
 * @param {string} message
 * @param {string} [title='Success']
 */
function successMessage(message, title = 'Success') {
    if (typeof Swal === 'undefined') return alert(`[SUCCESS] ${title}\n${message}`);

    Swal.fire({
        icon: 'success',
        title: title,
        text: message,
        buttonsStyling: false,
        confirmButtonText: 'OK',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        allowOutsideClick: false
    });
}

/**
 * Modal: Error (กด OK ปิด)
 * @param {string} message
 * @param {string} [title='Update Failed']
 */
function errorMessage(message, title = 'Update Failed') {
    if (typeof Swal === 'undefined') return alert(`[ERROR] ${title}\n${message}`);

    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        buttonsStyling: false,
        confirmButtonText: 'OK',
        customClass: {
            confirmButton: 'btn btn-danger'
        },
        allowOutsideClick: false
    });
}

/**
 * Toast: Success มุมขวาบน (auto-hide)
 * @param {string} message
 * @param {number} [durationMs=3000]
 */
function successToast(message, durationMs = 3000) {
    if (typeof Swal === 'undefined') return console.log('SUCCESS:', message);

    Swal.fire({
        toast: true,
        icon: 'success',
        title: message,
        position: 'top-end',
        showConfirmButton: false,
        timer: durationMs,
        timerProgressBar: true
    });
}

/**
 * Toast: Error มุมขวาบน (auto-hide)
 * @param {string} message
 * @param {number} [durationMs=4000]
 */
function errorToast(message, durationMs = 4000) {
    if (typeof Swal === 'undefined') return console.error('ERROR:', message);

    Swal.fire({
        toast: true,
        icon: 'error',
        title: message,
        position: 'top-end',
        showConfirmButton: false,
        timer: durationMs,
        timerProgressBar: true
    });
}

/**
 * Helper: โยนผลลัพธ์ API เข้ามาแล้วแจ้งให้เอง
 * ใช้กับ response ที่มี { success: boolean, message: string }
 * @param {{success?: boolean, message?: string}} resp
 * @param {Object} [opts]
 * @param {'modal'|'toast'} [opts.mode='modal']  // modal = successMessage/errorMessage, toast = successToast/errorToast
 */
function showApiResult(resp, opts = {}) {
    const mode = opts.mode || 'modal';
    const ok = !!(resp && resp.success);
    const msg = (resp && resp.message) || (ok ? 'Success' : 'Something went wrong');

    if (mode === 'toast') {
        return ok ? successToast(msg) : errorToast(msg);
    }
    return ok ? successMessage(msg) : errorMessage(msg);
}


