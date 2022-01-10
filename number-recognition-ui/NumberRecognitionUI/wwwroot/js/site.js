// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var azureHost = "https://numberrecognitionapi.azurewebsites.net"
var localHost = "https://localhost:5001"
host = localHost

window.addEventListener("load", () => {

    const canvas = document.querySelector("#canvas");
    const context = canvas.getContext("2d");

    var processed_image;
    const predicted_canvas = document.querySelector("#predicted-canvas");
    const context_predicted = predicted_canvas.getContext("2d");

    var objLabels = [];
    


    const predict_text = document.getElementById("predict")
    predict_text.value = "";
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


    $('#predict-button').on('click', function () {
        var imgInfo = canvas.toDataURL("image/png");
        var blobImage = dataURItoBlob(imgInfo);
        var dataForm = new FormData();
        dataForm.append("image", blobImage)

        $.ajax({
            url: host + '/api/v1/image/split', // split
            data: dataForm,
            processData: false,
            contentType: false,
            type: "POST",

        }).done(CropImage).fail(function (error) {
            alert(JSON.stringify(error.responseJSON))
            console.log(error.responseJSON);
        });
    });

    async function CropImage(response) {


        const processedImageArray = response.processed_image;
        processedImageArray.forEach((image, index) =>  CropImagePost(image, index))
    }

    async function CropImagePost(image, index) {

        var url = "data:image/png;base64," + image;
        var blobImage = dataURItoBlob(url);

        var dataForm = new FormData();
        dataForm.append("image", blobImage)

        $.ajax({
            url: host + '/api/v1/image/crop',
            data: dataForm,
            processData: false,
            contentType: false,
            type: "POST",

        }).done(function (response) { HandleResizeAfterCrop(response, index) }).fail(function (error) {
            alert(JSON.stringify(error.responseJSON))
            console.log(error.responseJSON);
        });
    }
    async function HandleResizeAfterCrop(response, index) {

        var url = "data:image/png;base64," + response.processed_image;
        var blobImageSecond = dataURItoBlob(url);

        var dataForm = new FormData();
        dataForm.append("image", blobImageSecond);

        $.ajax({
            url: host + '/api/v1/image/resize?width=28&height=28',
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
            processed_image = response.processed_image;
            HandlePredict(response, index)
        }).fail(function (error) {
            alert("not ok");
        });
    }


    async function HandlePredict(response, index) {
        //$('#predict-button').css("display", "none");
        $('#after-predict').css("display", "flex");
        var url = "data:image/png;base64," + response.processed_image;
        var blobImageSecond = dataURItoBlob(url);

        
        var dataForm = new FormData();
        dataForm.append("image", blobImageSecond);

          $.ajax({
            url: host + '/api/v1/image/predict',
            type: "POST",
            data: dataForm,
            processData: false,
            contentType: false,

          }).done(function (response) {

              objLabels[index] = response.predicted_label;
              predict_text.value = objLabels.join("");
    
        }).fail(function (error) {
            alert("not ok");
        });
    }

    $('#predicted-false').on('click', function () {
        location.reload();
    });

    $('#close-modal').on('click', function () {
        location.reload();

    })

    async function AddToTrainDataset(dataForm, predicted_label) {
        $.ajax({

            url: host + '/api/v1/dataset/train/' + predicted_label,
            type: "POST",
            data: dataForm,
            processData: false,
            contentType: false,

        }).fail(function (reponse) {
            alert('Something wrong');
        });
    }

    async function AddToTestDataset(dataForm, predicted_label) {

        $.ajax({

            url: host + '/api/v1/dataset/test/' + predicted_label,
            type: "POST",
            data: dataForm,
            processData: false,
            contentType: false,

        }).done(function (response) {

            $('#save-image').css("display", "none");
            $('#modal-agree').css("display", "none");
            $('#after-save').css("display", "block");
        }).fail(function (reponse) {
            alert('Something wrong');
        });
    }

    $('#save-image').on('click', function () {

        var url = "data:image/png;base64," + processed_image;
        var blobImageSecond = dataURItoBlob(url);

        var dataForm = new FormData();
        dataForm.append("image", blobImageSecond);

        predicted_label = $('#predict').val();

        AddToTrainDataset(dataForm, predicted_label);
        AddToTestDataset(dataForm, predicted_label);

    });
});
