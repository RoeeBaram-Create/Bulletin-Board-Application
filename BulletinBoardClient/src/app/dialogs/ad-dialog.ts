import { Component, inject, Inject, OnInit, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; // 👈 ייבוא הכלים לניהול טפסים
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { LocationService } from '../services/LocationService';

declare const google: any;

@Component({
    selector: 'app-ad-dialog',
    templateUrl: './ad-dialog.html',
    styleUrls: ['./ad-dialog.scss'],
    standalone: true,
    imports: [

        MatDialogModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        ReactiveFormsModule,
    ]
})
export class AdDialogComponent implements OnInit {

    adForm!: FormGroup;

    private locationService = inject(LocationService);

    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<AdDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) { }

    ngOnInit() {

        this.adForm = this.fb.group({
            title: [this.data?.title || '', [Validators.required, Validators.minLength(3)]],
            price: [this.data?.price || '', [Validators.required, Validators.min(1)]],
            category: [this.data?.category || 'הכל', [Validators.required]],
            description: [this.data?.description || '', [Validators.required]],
            city: [this.data?.city || '', [Validators.required]],
            latitude: [this.data?.latitude || null],
            longitude: [this.data?.longitude || null]
        });
    }


    detectLocation(): void {
        if (!navigator.geolocation) {
            alert('הדפדפן שלך לא תומך בזיהוי מיקום.');
            return;
        }

        this.adForm.patchValue({ city: 'מזהה מיקום...' });

        navigator.geolocation.getCurrentPosition(
            (position) => {
                debugger;
                const lat = position.coords.latitude;
                const lon = position.coords.longitude;

                this.adForm.patchValue({ latitude: lat, longitude: lon });

                this.locationService.getCityName(lat, lon).subscribe({
                    next: (cityName) => {
                        this.adForm.patchValue({ city: `${cityName} (המיקום הנוכחי שלי)` });
                    },
                    error: (err) => {
                        this.adForm.patchValue({ city: 'המיקום הנוכחי שלי (זוהה גיאוגרפית)' });
                    }
                });
            },
            (error) => {
                console.error('Geolocation tracking error:', error);
                this.adForm.patchValue({ city: '' });
                alert('לא הצלחנו לגשת ל-GPS. אנא ודא שאישרת הרשאות מיקום בדפדפן.');
            },
            { timeout: 8000 }
        );
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onSave(): void {

        if (this.adForm.valid == false) {
            this.adForm.markAllAsTouched();
            return;
        }

        const adDto = this.adForm.value;

        this.dialogRef.close({
            id: this.data?.id,
            dto: adDto
        });
    }
}
