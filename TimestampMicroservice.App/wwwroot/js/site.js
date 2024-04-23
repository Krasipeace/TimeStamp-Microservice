function copyUnix() {
    document.querySelector('.copy-btn').addEventListener('click', function() {
        var copyText = document.getElementById("unixInput");
        copyText.style.display = "block";
        copyText.select();
        document.execCommand("copy");
        copyText.style.display = "none";
        alert("Copied the text: " + copyText.value);
    });
}