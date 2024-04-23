function copyUnix() {
    document.querySelector('.copy-btn').addEventListener('click', function() {
        var copyText = document.getElementById("unixInput");
        copyText.style.display = "block";
        copyText.select();
        navigator.clipboard.writeText(copyText.value)
            .then(function() {
                copyText.style.display = "none";
                var popup = document.createElement("div");
                popup.innerHTML = "Copied the timestamp: " + copyText.value;
                popup.classList.add("popup");
                popup.style.display = "flex";
                popup.style.justifyContent = "center";
                popup.style.alignItems = "center";
                document.body.appendChild(popup);
                setTimeout(function() {
                    document.body.removeChild(popup);
                }, 1000);
            })
            .catch(function(error) {
                console.error("Failed to copy timestamp: " + error);
            });
    });
}