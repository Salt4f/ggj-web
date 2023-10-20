window.addEventListener('DOMContentLoaded', () => {
    const mainCountdown = document.getElementById('mainCountdown');

    function getTimeFormat(element, distance) {
        var days = Math.floor(distance / (1000 * 60 * 60 * 24)).toString();
        var hours = Math.floor(distance % (1000 * 60 * 60 * 24) / (1000 * 60 * 60)).toString();
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60)).toString();
        var seconds = Math.floor((distance % (1000 * 60)) / 1000).toString();

        if (hours.length == 1) hours = '0' + hours;
        if (minutes.length == 1) minutes = '0' + minutes;
        if (seconds.length == 1) seconds = '0' + seconds;

        if (days == 0 && hours == 0 && !element.classList.contains("alerting")) {
            element.classList.add("alerting");
        }

        if (days > 0) return days + "d " + hours + ":" + minutes + ":" + seconds;
        else return hours + ":" + minutes + ":" + seconds;

    }

    function updateCountdown() {

        let now = new Date().getTime();

        let mainDist = mainDate - now;

        if (mainDate - now > 0) {
            mainCountdown.innerText = getTimeFormat(mainCountdown, mainDist);
            setTimeout(updateCountdown, 1000); // Loop
        } else {
            mainCountdown.innerText = "Started!";
        }
    }

    updateCountdown();
});