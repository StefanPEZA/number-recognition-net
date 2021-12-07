// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


window.addEventListener("load", () => {

    const canvas = document.querySelector("#canvas");
    const context = canvas.getContext("2d");

    const predicted_canvas = document.querySelector("#predicted-canvas");
    const context_predicted = predicted_canvas.getContext("2d");

    const predict_text = document.getElementById("predict")
    predict_text.style = "font-size: 20px;"

    var imageLoader = document.getElementById('imageLoader');
    imageLoader.addEventListener('change', handleImage, true);

    var default_size = 420
    canvas.height = default_size;
    canvas.width = default_size;
    predicted_canvas.height = 280;
    predicted_canvas.width = 280;

    let painting = false;

    function startPosition(e) {

        painting = true;
        draw(e);
    }

    function finishedPosition() {

        painting = false;
        context.beginPath();
    }

    function draw(e) {
        if (!painting) return;

        context.lineWidth = 15;
        context.lineCap = 'round';

        context.lineTo(e.clientX - canvas.getBoundingClientRect().left,
            e.clientY - canvas.getBoundingClientRect().top);
        context.stroke();
        context.beginPath();
        context.moveTo(e.clientX - canvas.getBoundingClientRect().left,
            e.clientY - canvas.getBoundingClientRect().top);
    }
    canvas.addEventListener('mousedown', startPosition);
    canvas.addEventListener('mouseup', finishedPosition);
    canvas.addEventListener('mousemove', draw);

    $("#erase").click(function () {
        //context.fillStyle = start_background_color;
        context.clearRect(0, 0, canvas.width, canvas.height);
        //context.fillRect(0, 0, canvas.width, canvas.height);
    });

    $("#upload-link").click(function () {
        $('#imageLoader').click();
    });

    function calculateAspectRatioFit(srcWidth, srcHeight, default_size = 420) {
        var ratio = Math.min(default_size / srcWidth, default_size / srcHeight);
        return { width: srcWidth * ratio, height: srcHeight * ratio };
    }

    imageLoader.addEventListener('change', handleImage, false);
    function handleImage(e) {
        var reader = new FileReader();
        reader.onload = function (event) {
            var img = new Image();
            img.onload = function () {
                let size = calculateAspectRatioFit(img.width, img.height)
                context.drawImage(img, 0, 0, size.width, size.height);
            }
            img.src = event.target.result;
        }
        reader.readAsDataURL(e.target.files[0]);
    }

    function dataURItoBlob(dataURI) {
        const [metaData, data] = dataURI.split(',');
        const [prefix, mimeSection] = metaData.split(':');
        const [mimeString, separator] = mimeSection.split(';')

        const byteString = atob(data);
        const arrayBuffer = new ArrayBuffer(byteString.length);
        const ia = new Uint8Array(arrayBuffer);
        for (let i = 0; i < byteString.length; i++) {
            ia[i] = byteString.charCodeAt(i);
        }

        return new Blob([arrayBuffer], { type: mimeString });
    }

    // fac intai ajax la crop, transform sirul de bytes primit in imagine pe care dupa o trimit cropata la resize

    $('#predict-button').on('click', function () {

        var imgInfo = canvas.toDataURL("image/png");
        var blobImage = dataURItoBlob(imgInfo);
        var dataForm = new FormData();
        dataForm.append("image", blobImage)

        $.ajax({
            url: 'https://localhost:5001/api/v1/image/crop',
            data: dataForm,
            processData: false,
            contentType: false,
            type: "POST",

        }).done(HandleResizeAfterCrop).fail(function (error) {
            alert(JSON.stringify(error.responseJSON))
            console.log(error.responseJSON);
        });
    });


    function HandleResizeAfterCrop(response) {
        var url = "data:image/png;base64," + response.processed_image;
        var blobImageSecond = dataURItoBlob(url);

        var dataForm = new FormData();
        dataForm.append("image", blobImageSecond);

        $.ajax({
            url: 'https://localhost:5001/api/v1/image/resize?width=28&height=28',
            type: "POST",
            data: dataForm,
            processData: false,
            contentType: false,

        }).done(function (response) {
            var img = new Image();
            img.onload = function () {
                let size = calculateAspectRatioFit(img.width, img.height, 280)
                context_predicted.clearRect(0, 0, predicted_canvas.width, predicted_canvas.height);
                context_predicted.drawImage(img, 0, 0, size.width, size.height);
            }
            img.src = "data:image/png;base64," + response.processed_image;
            HandlePredict(response)
        }).fail(function (error) {
            alert("not ok");
        });
    }


    function HandlePredict(response) {
        var url = "data:image/png;base64," + response.processed_image;
        var blobImageSecond = dataURItoBlob(url);

        var dataForm = new FormData();
        dataForm.append("image", blobImageSecond);

        $.ajax({
            url: 'https://localhost:5001/api/v1/image/predict',
            type: "POST",
            data: dataForm,
            processData: false,
            contentType: false,

        }).done(function (response) {
            predict_text.value = response.predicted_label;
        }).fail(function (error) {
            alert("not ok");
        });
    }
});
