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

