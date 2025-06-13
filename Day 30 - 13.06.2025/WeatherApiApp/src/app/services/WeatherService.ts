import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map, Observable } from "rxjs";
import { WeatherModel } from "../models/weatherModel";
  import { catchError, of } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private apiKey = 'f2a0b21b8228a5223a2afa0db264751a';

  constructor(private http: HttpClient) {}

  weatherData = new BehaviorSubject<WeatherModel | null>(null);
  weatherData$: Observable<WeatherModel | null> = this.weatherData.asObservable();
  updateWeatherData(data: WeatherModel | null) {
    this.weatherData.next(data);
  }



getWeather(location: string): Observable<WeatherModel | null> {
  const url = `https://api.openweathermap.org/data/2.5/weather?q=${location}&appid=${this.apiKey}&units=metric`;
  return this.http.get<any>(url).pipe(
    map(response => ({
      cityName: response.name,
      temperature: response.main.temp,
      humidity: response.main.humidity,
      description: response.weather[0].description,
      windSpeed: response.wind.speed,
      icon: `http://openweathermap.org/img/wn/${response.weather[0].icon}.png`
    })),
    catchError(err => {
      console.error('Weather API Error:', err);
      return of(null);
    })
  );
}

  }
