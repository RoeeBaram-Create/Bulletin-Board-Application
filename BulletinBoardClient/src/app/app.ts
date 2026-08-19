import { Component, signal, inject, OnInit, computed, OnDestroy, DestroyRef, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { takeUntilDestroyed, toObservable, toSignal } from '@angular/core/rxjs-interop';
import { debounceTime, distinctUntilChanged, switchMap, tap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AdsService } from './services/AdsService ';
import { AdDialogComponent } from './dialogs/ad-dialog';
import { Ad } from './models/Ad';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, FormsModule, MatToolbarModule, MatButtonModule,
    MatIconModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatCardModule, MatDialogModule, MatSlideToggleModule, MatDividerModule,
    MatProgressSpinnerModule, MatCheckboxModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {

  private adsService = inject(AdsService);
  private dialog = inject(MatDialog);

  searchTitle = signal('');
  category = signal<string>("All");
  maxPrice = signal<number | null>(null);
  locationSearch = signal('');
  isLoading = signal(false);

  private combinedFilters = computed(() => {

    const locFilter = this.userLocationFilter();

    return {
      title: this.searchTitle(),
      category: this.category(),
      maxPrice: this.maxPrice(),
      location: this.locationSearch(),
      userLat: locFilter.userLat,
      userLng: locFilter.userLng,
      maxDistanceKm: locFilter.maxDistanceKm
    };
  });

  private userLocationFilter = signal({
    userLat: null as number | null,
    userLng: null as number | null,
    maxDistanceKm: null as number | null
  });

  ads = signal<Ad[]>([]);
  private destroyRef = inject(DestroyRef);
  private injector = inject(Injector);

  ngOnInit(): void {

    toObservable(this.combinedFilters, { injector: this.injector }).pipe(
      debounceTime(400),
      distinctUntilChanged((p, c) => JSON.stringify(p) === JSON.stringify(c)),
      tap(() => this.isLoading.set(true)),
      switchMap(filters =>
        this.adsService.getFilteredAds(filters).pipe(
          catchError(err => {
            console.error('Server Error:', err);
            return of([]);
          })
        )
      ),
      tap(() => this.isLoading.set(false)),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe(fetchedAds => {
      this.ads.set(fetchedAds);
    });
  }

  toggleLocationFilter(isChecked: boolean) {
    if (isChecked) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.userLocationFilter.set({
            userLat: position.coords.latitude,
            userLng: position.coords.longitude,
            maxDistanceKm: 15
          });
        },
        (error) => {
          console.error('Error getting location', error);
          alert('לא הצלחנו לגשת למיקום שלך. אנא ודא שאישרת הרשאות מיקום בדפדפן.');
        }
      );
    } else {
      this.userLocationFilter.set({
        userLat: null,
        userLng: null,
        maxDistanceKm: null
      });
    }
  }

  openAdDialog(ad?: Ad) {
    const dialogRef = this.dialog.open(AdDialogComponent, {
      width: '550px',
      direction: 'rtl',
      data: ad ? { ...ad } : { title: '', price: 0, category: 'כללי', description: '', isMyAd: true },
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const isUpdate = !!result.id;

        const action$ = isUpdate
          ? this.adsService.updateAd(result.id, result.dto)
          : this.adsService.createAd(result.dto);

        action$.subscribe({
          next: (serverAd) => {
            if (isUpdate) {
              this.ads.update(currentAds => {
                const index = currentAds.findIndex(a => a.id === serverAd.id);
                if (index !== -1) {
                  currentAds[index] = serverAd;
                }
                return [...currentAds];
              });
            } else {
              this.ads.update(currentAds => [serverAd, ...currentAds]);
            }
          },
          error: (err) => console.error('Operation failed', err)
        });
      }
    });

  }

  onDeleteAd(id: number) {
    if (confirm('האם אתה בטוח שברצונך למחוק מודעה זו?')) {
      this.adsService.deleteAd(id).subscribe({
        next: () => {
          this.ads.update(currentAds =>
            currentAds.filter(ad => ad.id != id)
          );
        },
        error: (err) => console.error('שגיאה במחיקה:', err)

      });
    }
  }

  resetFilters() {
    this.searchTitle.set('');
    this.category.set("0");
    this.maxPrice.set(null);
    this.locationSearch.set('');
  }
}


