import { Component, Input } from '@angular/core';
import { CitySearchComponent } from "../city-search-component/city-search-component";
import { WeatherModel } from '../models/weatherModel';

@Component({
  selector: 'app-weather-card-component',
  imports: [],
  templateUrl: './weather-card-component.html',
  styleUrl: './weather-card-component.css'
})
export class WeatherCardComponent {


  @Input()
  set render(_: number | null) {
    this.getWeatherData();
  }

  weatherData: WeatherModel[]|null = [];
  currentWeatherData : WeatherModel|null = null;

  ngOnInit(){
    this.getWeatherData();
  }
  getWeatherData() {
    const data = localStorage.getItem('weatherData');
    this.weatherData = data ? JSON.parse(data) : null;
    this.weatherData?.reverse();
    this.currentWeatherData = this.weatherData && this.weatherData.length > 0 ? this.weatherData[0] : null;
  }
}
