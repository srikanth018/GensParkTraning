export class WeatherModel {
  constructor(
    public cityName: string,
    public temperature: number,
    public humidity: number,
    public description: string,
    public icon: string,
    public windSpeed: number,
  ) {}
}