import { Component, EventEmitter, Output } from '@angular/core';
import { WeatherService } from '../services/WeatherService';
import { WeatherModel } from '../models/weatherModel';

@Component({
  selector: 'app-city-search-component',
  imports: [],
  templateUrl: './city-search-component.html',
  styleUrl: './city-search-component.css',
})
export class CitySearchComponent {
  @Output() fetchedWeatherData: EventEmitter<WeatherModel> =
    new EventEmitter<WeatherModel>();
  cityName: string = '';

  constructor(private weatherService: WeatherService) {}

  onSearchClick(cityName: string) {
    this.cityName = cityName;
    console.log('Searching for weather in:', this.cityName);
    this.weatherService.getWeather(this.cityName).subscribe({
      next: (data) => {
        console.log('Weather data received:', data);
        this.weatherService.updateWeatherData(data);
        if (!data) {
          alert(
            'No weather data found for the specified city. Please try again.'
          );

          return;
        }

        this.fetchedWeatherData.emit(data);
        this.cityName = '';
      },
      error: (err) => {
        console.error('Error fetching weather data:', err);
        alert('Error fetching weather data. Please try again.');
      },
    });
  }
}
