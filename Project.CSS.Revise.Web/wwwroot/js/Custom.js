let viewer;
const FALLBACK_THUMB_PATH = window.floorplanFallbackThumb || baseUrl + 'assets/images/file-image-missing.jpg';
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

function isImageMime(mime) {
    return typeof mime === 'string' && mime.toLowerCase().startsWith('image/');
}

function thumbIconHtml(fileName) {
    // Bootstrap icon look-alike thumbnail
    const name = escapeHtml(fileName || 'file');
    return `
      <div class="d-inline-flex align-items-center justify-content-center bg-light border rounded"
           style="width:50px;height:50px;">
        <i class="bi bi-file-earmark-image text-secondary" title="${name}" aria-label="${name}"></i>
      </div>`;
}

// after you inject rows, call this to wire up fallback on broken images
function wireThumbFallback(container) {
    const fallback = FALLBACK_THUMB_PATH;
    container.querySelectorAll('img.thumb-img').forEach(img => {
        img.addEventListener('error', () => {
            // prevent loops, swap to fallback, mark styling
            img.onerror = null;
            img.src = fallback;
            img.classList.add('thumb-fallback');

            // disable viewer click when file is missing
            const link = img.closest('a');
            if (link) {
                link.replaceWith(img); // remove <a>, keep the img
            }
        }, { once: true });
    });
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
        allowOutsideClick: false,
        didOpen: (popup) => {
            popup.parentNode.style.zIndex = 200000; // container
        }
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
        timerProgressBar: true,
        customClass: {
            container: 'swal-toast-container', // 👈 เลื่อนลงใช้ตัวนี้
            title: 'swal-toast-center'
        }
    });
}


function successToastV2(message, durationMs = 3000, offsetY = 40) {

    if (typeof Swal === 'undefined') {
        console.log('SUCCESS:', message);
        return;
    }

    const Toast = Swal.mixin({
        toast: true,
        showConfirmButton: false,
        timer: durationMs,
        timerProgressBar: true,
        icon: 'success',
        title: message,
        position: 'top-end',

        // 🎯 คุมตำแหน่งแบบ 100%
        didOpen: (toast) => {
            toast.style.marginTop = offsetY + 'px';   // 👈 เลื่อนลงตรงนี้
            toast.style.borderRadius = "10px";
            toast.style.padding = "10px 15px";
            toast.style.fontSize = "14px";
            toast.style.display = "flex";
            toast.style.justifyContent = "center";
            toast.style.alignItems = "center";
            toast.style.textAlign = "center";
            toast.style.width = "260px";  // ปรับได้ตามใจ
        }
    });

    Toast.fire();
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

function confirmMessage(message, opts = {}) {
    const {
        title = 'Are you sure?',
        confirmText = 'Yes',
        cancelText = 'No',
        icon = 'warning'
    } = opts;

    console.log(">> confirmMessage() called");
    console.log(">> Swal typeof =", typeof Swal);

    // fallback ถ้า Swal ไม่มี
    if (typeof Swal === 'undefined') {
        console.warn(">> Swal is undefined, using window.confirm fallback");
        const ok = window.confirm(`${title}\n\n${message}`);
        return Promise.resolve(ok);
    }

    return Swal.fire({
        icon: icon,
        title: title,
        text: message,
        showCancelButton: true,
        confirmButtonText: confirmText,
        cancelButtonText: cancelText,
        allowOutsideClick: false
    }).then(result => {
        console.log(">> Swal.fire result =", result);
        return result.isConfirmed === true;
    });
}


