import { Component } from '@angular/core';
import { CitySearchComponent } from "../city-search-component/city-search-component";
import { WeatherCardComponent } from "../weather-card-component/weather-card-component";
import { WeatherModel } from '../models/weatherModel';

@Component({
  selector: 'app-weather-dashboard-component',
  imports: [CitySearchComponent, WeatherCardComponent],
  templateUrl: './weather-dashboard-component.html',
  styleUrl: './weather-dashboard-component.css'
})

export class WeatherDashboardComponent {
  render:number = 0;
  weatherData: WeatherModel[] | null = null;
  setData(data: WeatherModel) {
    if (!this.weatherData) {
      this.weatherData = [];
    }

    if(this.weatherData.length >=5){
      const data = localStorage.getItem('weatherData');
      const jsonData = data ? JSON.parse(data) : null;
      jsonData.shift();
      this.weatherData = jsonData;
    }

    this.weatherData?.push(data);
    localStorage.setItem('weatherData', JSON.stringify(this.weatherData));
    sessionStorage.setItem('weatherData', JSON.stringify(this.weatherData));
    this.render++;
  }
}
