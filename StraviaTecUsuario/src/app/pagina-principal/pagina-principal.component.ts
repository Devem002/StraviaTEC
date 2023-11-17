import { Component } from '@angular/core';
import * as L from 'leaflet';
import * as tj from '@tmcw/togeojson';

@Component({
  selector: 'app-pagina-principal',
  templateUrl: './pagina-principal.component.html',
})
export class PaginaPrincipalComponent {
  map!: L.Map;

  ngOnInit() {
    this.map = L.map('map').setView([0, 0], 2);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(this.map);
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = (e) => {
        const xml = new DOMParser().parseFromString(reader.result as string, 'text/xml');
        const geojson = tj.gpx(xml);
        L.geoJSON(geojson).addTo(this.map);
      };
      reader.readAsText(file);
    }
  }
}