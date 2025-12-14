window.issMap = {
    map: null,
    marker: null,

    init: function (mapElement) {
        this.map = L.map(mapElement).setView([0, 0], 2);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(this.map);
        this.marker = L.marker([0, 0]).addTo(this.map).bindPopup("МКС");
    },

    updateMarker: function (lat, lon) {
        if (this.map && this.marker) {
            const latlng = [lat, lon];
            this.marker.setLatLng(latlng);
            this.map.setView(latlng, this.map.getZoom());
        }
    }
};

window.issCharts = {
    speedChart: null,
    altChart: null,

    init: function (speedCanvas, altCanvas) {
        const speedCtx = speedCanvas.getContext('2d');
        const altCtx = altCanvas.getContext('2d');

        this.speedChart = new Chart(speedCtx, {
            type: 'line',
            data: {
                labels: [],
                datasets: [{
                    label: 'Скорость (км/ч)',
                    data: [],
                    borderColor: '#3498db',
                    fill: false
                }]
            }
        });

        this.altChart = new Chart(altCtx, {
            type: 'line',
            data: {
                labels: [],
                datasets: [{
                    label: 'Высота (км)',
                    data: [],
                    borderColor: '#2ecc71',
                    fill: false
                }]
            }
        });
    },

    update: function (labels, speedData, altData) {
        if (this.speedChart) {
            this.speedChart.data.labels = labels;
            this.speedChart.data.datasets[0].data = speedData;
            this.speedChart.update();
        }

        if (this.altChart) {
            this.altChart.data.labels = labels;
            this.altChart.data.datasets[0].data = altData;
            this.altChart.update();
        }
    }
};