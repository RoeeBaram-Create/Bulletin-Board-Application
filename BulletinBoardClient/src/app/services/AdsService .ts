import { Injectable, signal, inject, computed } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Ad } from '../models/Ad';

@Injectable({
  providedIn: 'root'
})
export class AdsService {

  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7293/api/ads';

  private adsSignal = signal<Ad[]>([]);

  readonly ads = computed(() => this.adsSignal());


  getFilteredAds(filters: {
    title: string, category: string, maxPrice: number | null, location: string,

    userLat: number | null,
    userLng: number | null,
    maxDistanceKm: number | null,

  }): Observable<Ad[]> {
    let params = new HttpParams();

    debugger;

    if (filters.title) params = params.set('title', filters.title);
    if (filters.category && filters.category != "All") params = params.set('category', filters.category);
    if (filters.maxPrice) params = params.set('maxPrice', filters.maxPrice.toString());
    if (filters.location) params = params.set('location', filters.location);
    if (filters.userLat) params = params.set('latitude', filters.userLat);
    if (filters.userLng) params = params.set('longitude', filters.userLng);
    if (filters.maxDistanceKm) params = params.set('radiusInKm', filters.maxDistanceKm);

    return this.http.get<Ad[]>(this.apiUrl, { params });
  }

  createAd(ad: Ad): Observable<Ad> {
    return this.http.post<Ad>(this.apiUrl, ad);
  }

  updateAd(id: any, ad: Ad): Observable<Ad> {
    return this.http.put<Ad>(`${this.apiUrl}/${id}`, ad);
  }

  deleteAd(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getAllAds(): Observable<Ad[]> {
    return this.getFilteredAds({
      title: '', category: "All", maxPrice: null, location: "",
      userLat: null,
      userLng: null,
      maxDistanceKm: null
    });
  }
}
