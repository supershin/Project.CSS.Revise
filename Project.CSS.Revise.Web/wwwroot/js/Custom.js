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

