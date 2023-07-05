var marker = "";
let map = "";
async function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 11,
        center: { lat: 24.390426217012137, lng: 54.5343818870348 },
        streetViewControl: false,
    });

    marker = new google.maps.Marker({
        position: { lat: 24.34041248980433, lng: 54.534240861973046 },
        icon: "../img/TechNexus/icon_marker2.svg",
        map,
    });

    map.addListener("zoom_changed", function (event) {
        marker.setMap(null);
        switch (map.getZoom()) {
            case 17:
                {
                    marker = new google.maps.Marker({
                        position: { lat: 24.340473379605356, lng: 54.534223203124064 },
                        icon: "../img/TechNexus/icon_marker2.svg",
                        map,
                    });
                }
                break;
            case 19:
                {
                    marker = new google.maps.Marker({
                        position: { lat: 24.340389252402634, lng: 54.53423197477317 },
                        icon: "../img/TechNexus/icon_marker2.svg",
                        map,
                    });
                }
                break;

            case 20:
                {
                    marker = new google.maps.Marker({
                        position: { lat: 24.340402033612865, lng: 54.534236236818664 },
                        icon: "../img/TechNexus/icon_marker2.svg",
                        map,
                    });
                }
                break;

            case 21:
                {
                    marker = new google.maps.Marker({
                        position: { lat: 24.340396535073722, lng: 54.53423724265299 },
                        icon: "../img/TechNexus/icon_marker2.svg",
                        map,
                    });
                }
                break;
            case 22:
                {
                    marker = new google.maps.Marker({
                        position: { lat: 24.340397146022514, lng: 54.53423724265299 },
                        icon: "../img/TechNexus/icon_marker2.svg",
                        map,
                    });
                }
                break;
            default: {
                marker = new google.maps.Marker({
                    position: { lat: 24.34041248980433, lng: 54.534240861973046 },
                    icon: "../img/TechNexus/icon_marker2.svg",
                    map,
                });
            }
        }
    });

    marker.addListener("click", () => {
        window.open("https://goo.gl/maps/eUuQw3tAtvzoCjjr9");
    })
}

window.initMap = initMap;
