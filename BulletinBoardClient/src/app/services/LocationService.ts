import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  getCityName(lat: number, lon: number): Observable<string> {

    const url = `https://secure.geonames.org/findNearbyPlaceNameJSON?lat=${lat}&lng=${lon}&radius=20&username=roee1&lang=he`;

    return this.http.jsonp<any>(url, 'callback').pipe(
      map(response => {

        if (response && response.geonames && response.geonames.length > 0) {
          return response.geonames[0].name;
        }
        throw new Error('No city found at these coordinates');
      }),
      catchError(error => {
        console.error('Location API error:', error);
        return throwError(() => new Error('שגיאה בשליפת שם המקום'));
      })
    );
  }
}
